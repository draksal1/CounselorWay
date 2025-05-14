# handlers/start.py

from telegram import Update
from telegram.ext import ContextTypes, CommandHandler
from config import settings
from handlers.admin_handlers import show_admin_menu
from handlers.user_handlers import show_user_menu

async def start(update: Update, context: ContextTypes.DEFAULT_TYPE):
    if update.effective_user.id == settings.bot.admin_id:
        await show_admin_menu(update, context)
    else:
        await show_user_menu(update, context)

start_handler = CommandHandler("start", start)
