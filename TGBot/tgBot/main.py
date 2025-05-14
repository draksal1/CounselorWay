import asyncio
from config import settings
from utils.logger import logger
from telegram.ext import ApplicationBuilder, CallbackQueryHandler
from handlers.start import start_handler
from handlers.admin_handlers import (
    season_selected, back_to_seasons, view_users, view_groups
)
from handlers.user_handlers import (
    user_view_profile, user_guest_page, user_back_to_menu
)
from handlers.conversation import register as register_conversation



import ssl, certifi
from telegram import Bot
from telegram.request import HTTPXRequest
from config import settings
import certifi_win32




def main():
    logger.info("Запуск бота...")
    certifi_win32.wincerts.add_to_certifi()
    request = HTTPXRequest(
        connection_pool_size=8,
        httpx_kwargs={
            # HTTPX примет строку с путём к файлу сертификатов
            "verify": certifi.where()
        }
    )

    # 2) Инициализируем Bot с нашим кастомным request
    bot = Bot(
        token=settings.bot.token,
        request=request
    )

    # 3) Строим приложение на основе готового Bot
    app = ApplicationBuilder().bot(bot).build()

    # Команда /start
    app.add_handler(start_handler)

    # Admin callbacks
    app.add_handler(CallbackQueryHandler(season_selected, pattern="^season_"))
    app.add_handler(CallbackQueryHandler(back_to_seasons, pattern="^back_to_seasons$"))
    app.add_handler(CallbackQueryHandler(view_users, pattern="^view_users$"))
    app.add_handler(CallbackQueryHandler(view_groups, pattern="^view_groups$"))

    # User callbacks
    app.add_handler(CallbackQueryHandler(user_view_profile, pattern="^my_profile$"))
    app.add_handler(CallbackQueryHandler(user_guest_page, pattern="^guest_page$"))
    app.add_handler(CallbackQueryHandler(user_back_to_menu, pattern="^back_to_user_menu$"))

    # Conversation handlers (add user / add group)
    register_conversation(app)

    # Запускаем polling
    app.run_polling()

if __name__ == "__main__":
    asyncio.run(main())
