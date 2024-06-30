using Milestone.Models;

namespace Milestone.Services
{
    public interface IUserDataService
    {
        bool FindUserByEmailAndPasswordValid(UserModel user);

        bool RegisterUserValid(UserModel user);

        int FindUserIdByEmailAndPassword(UserModel user);
    }
}
