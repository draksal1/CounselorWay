from pydantic import BaseModel
from typing import Dict, Optional, List

class Season(BaseModel):
    id: int
    name: str

class Task(BaseModel):
    id: int
    name: str
    description: str
    location: str
    deadline: str
    reward: Dict[str, int]
    min_level: int

class TaskGroup(BaseModel):
    id: int
    name: str
    tasks: List[Task] = []

class User(BaseModel):
    id: int
    name: str
    level: int
    staj: Optional[int]
    stats: Dict[str, int]
    photo_url: Optional[str]
    current_tasks: List[Task] = []
    completed_tasks: List[Task] = []
    season_name: Optional[str]
