using ChristanCrush.DataServices;
using ChristanCrush.Models;

namespace ChristanCrush.Services
{
    public class UserService
    {
        private DBConnection database = new DBConnection();
        private UserDAO UserDao = new UserDAO();

        public bool FindUserByEmailAndPasswordValid(UserModel user)
        {
            return UserDao.FindUserByEmailAndPasswordValid(user, database.DbConnection());
        }

        public string GetUserNameByUserId(int userId)
        {
            return UserDao.GetUserNameByUserId(userId, database.DbConnection());
        }

        public int FindUserIdByEmail(UserModel user)
        {
            return UserDao.FindUserIdByEmail(user, database.DbConnection());
        }

        public bool RegisterUserValid(UserModel user)
        {
            return UserDao.RegisterUserValid(user, database.DbConnection());
        }

        public bool DeleteUserByEmail(UserModel user)
        {
            return UserDao.DeleteUserByEmail(user, database.DbConnection());
        }
        public bool IsEmailRegistered(UserModel user)
        {
            return UserDao.IsEmailRegistered(user, database.DbConnection());
        }
    }
}
