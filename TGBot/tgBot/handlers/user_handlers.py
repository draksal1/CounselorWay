# handlers/user_handlers.py

from telegram import Update, InlineKeyboardButton, InlineKeyboardMarkup
from telegram.ext import ContextTypes
from config import settings
from services.api_client import get_user_profile_by_telegram_id  # и, при необходимости, другие функции

# --- Главное меню пользователя ---
async def show_user_menu(update: Update, context: ContextTypes.DEFAULT_TYPE):
    if update.callback_query:
        query = update.callback_query
        await query.answer()
        target = query.message
    else:
        target = update.message

    telegram_id = update.effective_user.id
    profile = get_user_profile_by_telegram_id(telegram_id)
    is_registered = bool(profile)

    buttons = []
    if is_registered:
        buttons.append([InlineKeyboardButton("👤 Мой профиль", callback_data="my_profile")])
    buttons.append([InlineKeyboardButton("🌐 Гостевая страница", callback_data="guest_page")])

    reply_markup = InlineKeyboardMarkup(buttons)
    await target.reply_text("Главное меню:", reply_markup=reply_markup)


# --- Просмотр своего профиля ---
async def user_view_profile(update: Update, context: ContextTypes.DEFAULT_TYPE):
    query = update.callback_query
    await query.answer()

    telegram_id = update.effective_user.id
    user = get_user_profile_by_telegram_id(telegram_id)
    if not user:
        await query.edit_message_text("Вы не зарегистрированы. Обратитесь к администратору.")
        return

    text = (
        f"*Имя:* {user.get('name')}\n"
        f"*Уровень:* {user.get('level')}\n"
        f"*Стаж:* {user.get('staj', 0)}\n"
        "*Характеристики:* " +
        ", ".join(f"{k}: {v}" for k,v in user.get("stats", {}).items()) + "\n"
        f"*Текущие задания:* {len(user.get('current_tasks', []))}\n"
        f"*Выполненные задания:* {len(user.get('completed_tasks', []))}"
    )
    photo = user.get("photo_url")
    if photo:
        await context.bot.send_photo(
            chat_id=update.effective_chat.id,
            photo=photo,
            caption=text,
            parse_mode='Markdown'
        )
    else:
        await query.edit_message_text(text, parse_mode='Markdown')

    # Кнопка 'Назад в меню'
    buttons = [[InlineKeyboardButton("⬅️ Меню", callback_data="back_to_user_menu")]]
    await context.bot.send_message(
        chat_id=update.effective_chat.id,
        text="Что дальше?",
        reply_markup=InlineKeyboardMarkup(buttons)
    )


# --- Гостевая страница ---
async def user_guest_page(update: Update, context: ContextTypes.DEFAULT_TYPE):
    query = update.callback_query
    await query.answer()

    # TODO: заменить на реальный вызов API
    all_users = []  # get_all_users()

    if not all_users:
        await query.edit_message_text("Нет данных о пользователях.")
        return

    buttons = [
        [InlineKeyboardButton(u["name"], callback_data=f"user_{u['id']}")]
        for u in all_users
    ]
    buttons.append([InlineKeyboardButton("⬅️ Меню", callback_data="back_to_user_menu")])
    await query.edit_message_text(
        "🌐 *Гостевая страница*: выберите пользователя",
        reply_markup=InlineKeyboardMarkup(buttons),
        parse_mode='Markdown'
    )


# --- Возврат в меню пользователя (без circular import) ---
async def user_back_to_menu(update: Update, context: ContextTypes.DEFAULT_TYPE):
    # Здесь просто повторно рисуем меню пользователя
    await show_user_menu(update, context)
