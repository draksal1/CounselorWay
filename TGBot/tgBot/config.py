import yaml
from pydantic import BaseModel

class BotSettings(BaseModel):
    token: str
    admin_id: int

class APISettings(BaseModel):
    base_url: str

class Settings(BaseModel):
    bot: BotSettings
    api: APISettings

# Читаем конфигурацию из YAML
with open("appsettings.yaml", "r", encoding="utf-8") as f:
    data = yaml.safe_load(f)

settings = Settings(**data)
