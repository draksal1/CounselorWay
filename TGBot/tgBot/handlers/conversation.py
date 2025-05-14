from telegram.ext import ConversationHandler, MessageHandler, CallbackQueryHandler, CommandHandler, filters
from models.states import WAITING_FOR_USERNAME, WAITING_FOR_GROUP_NAME, ADDING_TASKS
from handlers.admin_handlers import (
    admin_add_user_start, admin_add_user_username,
    admin_add_group_start, admin_add_group_name, admin_add_group_task
)

def register(app):
    # Conversation: добавить пользователя
    conv_add_user = ConversationHandler(
        entry_points=[CallbackQueryHandler(admin_add_user_start, pattern="^add_user$")],
        states={
            WAITING_FOR_USERNAME: [
                MessageHandler(filters.TEXT & ~filters.COMMAND, admin_add_user_username)
            ]
        },
        fallbacks=[CommandHandler("cancel", lambda u, c: ConversationHandler.END)]
    )
    app.add_handler(conv_add_user)

    # Conversation: добавить группу заданий
    conv_add_group = ConversationHandler(
        entry_points=[CallbackQueryHandler(admin_add_group_start, pattern="^add_group$")],
        states={
            WAITING_FOR_GROUP_NAME: [
                MessageHandler(filters.TEXT & ~filters.COMMAND, admin_add_group_name)
            ],
            ADDING_TASKS: [
                MessageHandler(filters.TEXT & ~filters.COMMAND, admin_add_group_task)
            ]
        },
        fallbacks=[
            CommandHandler("done", lambda u, c: ConversationHandler.END),
            CommandHandler("cancel", lambda u, c: ConversationHandler.END)
        ]
    )
    app.add_handler(conv_add_group)
