using System;
using ServerCore.Models;

namespace ServerCore.Services
{
    public interface IUserService
    {
        Guid RegisterUser(string name, int age);
        User GetUser(Guid id);
        bool UpdateCharacteristics(Guid userId, Action<Characteristics> updateAction);
    }
}