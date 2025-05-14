import requests
from config import settings

def get_seasons():
    url = f"{settings.api.base_url}/seasons"
    r = requests.get(url)
    r.raise_for_status()
    return r.json()

def create_season(name: str):
    url = f"{settings.api.base_url}/seasons"
    r = requests.post(url, json={"name": name})
    r.raise_for_status()
    return r.json()

def get_users(season_id: int):
    url = f"{settings.api.base_url}/seasons/{season_id}/users"
    r = requests.get(url)
    r.raise_for_status()
    return r.json()

def add_user_to_season(season_id: int, username: str):
    url = f"{settings.api.base_url}/seasons/{season_id}/users"
    r = requests.post(url, json={"username": username})
    r.raise_for_status()
    return r.json()

def get_task_groups(season_id: int):
    url = f"{settings.api.base_url}/seasons/{season_id}/task-groups"
    r = requests.get(url)
    r.raise_for_status()
    return r.json()

def create_task_group(season_id: int, name: str):
    url = f"{settings.api.base_url}/seasons/{season_id}/task-groups"
    r = requests.post(url, json={"name": name})
    r.raise_for_status()
    return r.json()

def add_task_to_group(group_id: int, task_data: dict):
    url = f"{settings.api.base_url}/task-groups/{group_id}/tasks"
    r = requests.post(url, json=task_data)
    r.raise_for_status()
    return r.json()

def get_user_profile_by_id(user_id: int):
    url = f"{settings.api.base_url}/users/{user_id}"
    r = requests.get(url)
    r.raise_for_status()
    return r.json()

def get_user_profile_by_telegram_id(telegram_id: int):
    url = f"{settings.api.base_url}/users"
    r = requests.get(url, params={"telegram_id": telegram_id})
    r.raise_for_status()
    return r.json()
