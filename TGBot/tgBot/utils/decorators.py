from functools import wraps
from telegram import Update
from telegram.ext import ContextTypes
from config import settings
from utils.logger import logger

def restricted(func):
    """Декоратор для ограниченного доступа админа."""
    @wraps(func)
    async def wrapper(update: Update, context: ContextTypes.DEFAULT_TYPE, *args, **kwargs):
        user_id = update.effective_user.id
        if user_id != settings.bot.admin_id:
            try:
                await update.effective_message.reply_text("⛔ Эта команда доступна только администратору.")
            except:
                pass
            logger.warning(f"Unauthorized access denied for user_id={user_id}")
            return
        return await func(update, context, *args, **kwargs)
    return wrapper
