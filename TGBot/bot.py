import argparse
import json
import requests
import asyncio
import nest_asyncio
from telegram import Update
from telegram.ext import Application, CommandHandler, MessageHandler, ContextTypes, filters

# Применяем nest_asyncio для поддержки уже запущенного событийного цикла (например, в PyCharm)
nest_asyncio.apply()


def load_config(config_path: str) -> dict:
    """Загружает конфигурацию из указанного JSON-файла."""
    with open(config_path, 'r', encoding='utf-8') as config_file:
        return json.load(config_file)


async def start(update: Update, context: ContextTypes.DEFAULT_TYPE):
    """Обработчик команды /start."""
    await update.message.reply_text(
        'Привет! Отправь сообщение, и я передам его на сервер. '
        'Также можно запросить время командой /time.'
    )


async def time_command(update: Update, context: ContextTypes.DEFAULT_TYPE):
    """Обработчик команды /time.

    Отправляет сообщение '/time' на сервер, а сервер возвращает текущее время.
    """
    payload = {'message': '/time'}
    try:
        response = requests.post(SERVER_URL, json=payload)
        if response.ok:
            result = response.json().get('result')
            await update.message.reply_text(result)
        else:
            await update.message.reply_text('Ошибка при обработке запроса на сервере.')
    except Exception as e:
        await update.message.reply_text(f'Произошла ошибка: {e}')


async def handle_message(update: Update, context: ContextTypes.DEFAULT_TYPE):
    """Обработчик текстовых сообщений, отличных от команд."""
    user_text = update.message.text
    payload = {'message': user_text}
    try:
        response = requests.post(SERVER_URL, json=payload)
        if response.ok:
            result = response.json().get('result')
            await update.message.reply_text(result)
        else:
            await update.message.reply_text('Ошибка при обработке запроса на сервере.')
    except Exception as e:
        await update.message.reply_text(f'Произошла ошибка: {e}')


async def main():
    # Парсинг аргументов командной строки для получения пути к файлу конфигурации
    parser = argparse.ArgumentParser(description='Telegram бот с конфигурационным файлом')
    parser.add_argument('--config', type=str, default='config.json',
                        help='Путь к файлу конфигурации (по умолчанию config.json)')
    args = parser.parse_args()

    # Загрузка конфигурации
    config = load_config(args.config)
    global TOKEN, SERVER_URL
    TOKEN = config['token']
    SERVER_URL = config['server_url']

    # Создание приложения бота с использованием новой асинхронной архитектуры
    application = Application.builder().token(TOKEN).build()

    # Регистрация обработчиков: отдельный для /start, /time и общий для остальных сообщений
    application.add_handler(CommandHandler("start", start))
    application.add_handler(CommandHandler("time", time_command))
    application.add_handler(MessageHandler(filters.TEXT & ~filters.COMMAND, handle_message))

    # Запуск бота (polling)
    await application.run_polling()


if __name__ == '__main__':
    asyncio.run(main())
