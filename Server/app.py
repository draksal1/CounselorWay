from flask import Flask, request, jsonify
import datetime
import argparse
import json

def load_config(config_path: str) -> dict:
    """Загружает конфигурацию из указанного JSON-файла."""
    with open(config_path, 'r', encoding='utf-8') as config_file:
        return json.load(config_file)

app = Flask(__name__)

@app.route('/api/process', methods=['POST'])
def process():
    data = request.json
    user_message = data.get('message', '')
    # Если получено сообщение /time, возвращаем текущее время сервера
    if user_message.lower() == '/time':
        current_time = datetime.datetime.now().strftime('%Y-%m-%d %H:%M:%S')
        result = f"Текущее время на сервере: {current_time}"
    else:
        result = f"Сервер получил: {user_message}"
    return jsonify({"result": result})

if __name__ == '__main__':
    # Парсинг аргументов командной строки для получения пути к файлу конфигурации
    parser = argparse.ArgumentParser(description="Запуск Flask сервера")
    parser.add_argument('--config', type=str, default='config.json',
                        help='Путь к файлу конфигурации (по умолчанию config.json)')
    args = parser.parse_args()

    # Загрузка конфигурации
    config = load_config(args.config)

    # Получение хоста и порта из конфигурации (если не указаны, используются значения по умолчанию)
    host = config.get('host', '0.0.0.0')
    port = config.get('port', 5000)

    # Запуск сервера с указанными хостом и портом
    app.run(debug=True, host=host, port=port)
