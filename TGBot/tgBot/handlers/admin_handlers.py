from telegram import Update, InlineKeyboardButton, InlineKeyboardMarkup
from telegram.ext import ContextTypes, CallbackQueryHandler, ConversationHandler
from config import settings
from utils.decorators import restricted
from services.api_client import (
    get_seasons, get_users, get_task_groups,
    add_user_to_season, create_task_group, add_task_to_group
)
from models.states import WAITING_FOR_USERNAME, WAITING_FOR_GROUP_NAME, ADDING_TASKS

@restricted
async def show_admin_menu(update: Update, context: ContextTypes.DEFAULT_TYPE):
    """Главное меню администратора: список сезонов."""
    if update.callback_query:
        query = update.callback_query
        await query.answer()
        send_target = query.message
    else:
        send_target = update.message

    seasons = get_seasons()
    buttons = [[InlineKeyboardButton(s["name"], callback_data=f"season_{s['id']}")] for s in seasons]
    buttons.append([InlineKeyboardButton("➕ Создать новый сезон", callback_data="create_season")])
    reply_markup = InlineKeyboardMarkup(buttons)
    text = "🗂 *Админ-панель*\nВыберите сезон или создайте новый:"
    await send_target.reply_text(text, reply_markup=reply_markup, parse_mode='Markdown')

@restricted
async def season_selected(update: Update, context: ContextTypes.DEFAULT_TYPE):
    """Обработка выбора сезона админом."""
    query = update.callback_query
    await query.answer()
    season_id = int(query.data.split("_")[1])
    context.user_data["selected_season"] = season_id

    seasons = get_seasons()
    season = next((s for s in seasons if s["id"] == season_id), {})
    name = season.get("name", "Сезон")

    buttons = [
        [InlineKeyboardButton("👥 Список пользователей", callback_data="view_users")],
        [InlineKeyboardButton("✅ Список групп заданий", callback_data="view_groups")],
        [InlineKeyboardButton("➕ Добавить пользователя", callback_data="add_user")],
        [InlineKeyboardButton("➕ Добавить группу заданий", callback_data="add_group")],
        [InlineKeyboardButton("⬅️ Назад к сезонам", callback_data="back_to_seasons")]
    ]
    reply_markup = InlineKeyboardMarkup(buttons)
    await query.edit_message_text(f"*Сезон:* {name}\nВыберите действие:", 
                                  reply_markup=reply_markup, parse_mode='Markdown')

@restricted
async def back_to_seasons(update: Update, context: ContextTypes.DEFAULT_TYPE):
    """Возврат к списку сезонов."""
    await show_admin_menu(update, context)

@restricted
async def view_users(update: Update, context: ContextTypes.DEFAULT_TYPE):
    """Показать список пользователей выбранного сезона."""
    query = update.callback_query
    await query.answer()
    season_id = context.user_data.get("selected_season")
    users = get_users(season_id)

    if not users:
        buttons = [[InlineKeyboardButton("⬅️ Назад", callback_data="season_"+str(season_id))]]
        await query.edit_message_text("В этом сезоне нет пользователей.", 
                                      reply_markup=InlineKeyboardMarkup(buttons))
        return

    buttons = [[InlineKeyboardButton(u["name"], callback_data=f"user_{u['id']}")] for u in users]
    buttons.append([InlineKeyboardButton("⬅️ Назад", callback_data="season_"+str(season_id))])
    reply_markup = InlineKeyboardMarkup(buttons)
    await query.edit_message_text("👥 *Пользователи сезона:*", 
                                  reply_markup=reply_markup, parse_mode='Markdown')

@restricted
async def view_groups(update: Update, context: ContextTypes.DEFAULT_TYPE):
    """Показать список групп заданий сезона."""
    query = update.callback_query
    await query.answer()
    season_id = context.user_data.get("selected_season")
    groups = get_task_groups(season_id)

    text_lines = ["✅ *Группы заданий:*"]
    if groups:
        for g in groups:
            text_lines.append(f"- {g['name']} ({len(g.get('tasks', []))} заданий)")
    else:
        text_lines.append("Нет групп заданий.")
    buttons = [[InlineKeyboardButton("⬅️ Назад", callback_data="season_"+str(season_id))]]
    await query.edit_message_text("\n".join(text_lines), 
                                  reply_markup=InlineKeyboardMarkup(buttons),
                                  parse_mode='Markdown')

# --- Conversation: Добавление пользователя ---
@restricted
async def admin_add_user_start(update: Update, context: ContextTypes.DEFAULT_TYPE):
    query = update.callback_query
    await query.answer()
    await query.edit_message_text("Введите Telegram юзернейм (@username) для добавления:")
    return WAITING_FOR_USERNAME

@restricted
async def admin_add_user_username(update: Update, context: ContextTypes.DEFAULT_TYPE):
    username = update.message.text.strip().lstrip("@")
    season_id = context.user_data.get("selected_season")
    add_user_to_season(season_id, username)
    await update.message.reply_text(f"✅ Пользователь @{username} добавлен в сезон.")
    await show_admin_menu(update, context)
    return ConversationHandler.END

# --- Conversation: Добавление группы заданий ---
@restricted
async def admin_add_group_start(update: Update, context: ContextTypes.DEFAULT_TYPE):
    query = update.callback_query
    await query.answer()
    await query.edit_message_text("Введите название новой группы заданий:")
    return WAITING_FOR_GROUP_NAME

@restricted
async def admin_add_group_name(update: Update, context: ContextTypes.DEFAULT_TYPE):
    name = update.message.text.strip()
    season_id = context.user_data.get("selected_season")
    grp = create_task_group(season_id, name)
    context.user_data["new_group_id"] = grp["id"]
    await update.message.reply_text(
        f"✅ Группа \"{name}\" создана.\n"
        "Теперь введите задания в формате:\n"
        "`Название; Описание; Локация; Дедлайн; Награда; Мин.уровень`\n"
        "Или введите `/done` для завершения.", parse_mode='Markdown'
    )
    return ADDING_TASKS

@restricted
async def admin_add_group_task(update: Update, context: ContextTypes.DEFAULT_TYPE):
    text = update.message.text.strip()
    if text.lower() == "/done":
        await update.message.reply_text("✅ Добавление заданий завершено.")
        await show_admin_menu(update, context)
        return ConversationHandler.END

    parts = text.split(";")
    if len(parts) < 6:
        await update.message.reply_text(
            "⚠️ Неверный формат. Укажите 6 полей через ';'. Попробуйте ещё раз или `/done` для завершения.",
            parse_mode='Markdown'
        )
        return ADDING_TASKS

    name, description, location, deadline, reward, min_level = [p.strip() for p in parts]
    group_id = context.user_data["new_group_id"]
    task_data = {
        "name": name,
        "description": description,
        "location": location,
        "deadline": deadline,
        "reward": reward,
        "min_level": int(min_level)
    }
    add_task_to_group(group_id, task_data)
    await update.message.reply_text(f"✅ Задание \"{name}\" добавлено.")
    return ADDING_TASKS
