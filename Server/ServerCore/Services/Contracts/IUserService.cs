using ServerCore.Models.Api.Users;
using ServerCore.Models.Database;

namespace ServerCore.Services;

public interface IUserService
{
    User Create(UserCreate userCreate);
    User Update(UserUpdate userUpdate);
    User Get(Guid id);
    void Delete(Guid id);
    void UpdateCharacteristics(Guid userId, Characteristics newCharacteristics);
}