using ChristanCrush.Models;
using ChristanCrush.Services;


namespace ChristanCrush.Tests
{
    public class UserServiceTest
    {
        private UserService userService = new UserService();

        [Fact]
        public void RegisterUserValid()
        {
            var user = new UserModel
            {
                first_name = "John",
                last_name = "Doe",
                email = "registerme@example.com",
                password = "Password123$",
                confirm_password = "Password123$",
                date_of_birth = new DateTime(2015, 12, 31),
                gender = "M"
            };

            bool result = userService.RegisterUserValid(user);

            Assert.True(result);
            Assert.True(userService.IsEmailRegistered(user));
            userService.DeleteUserByEmail(user);
        }

        [Fact]
        public void DeleteUserByEmail()
        {
            var user = new UserModel
            {
                first_name = "John",
                last_name = "Doe",
                email = "registerme@example.com",
                password = "Password123$",
                confirm_password = "Password123$",
                date_of_birth = new DateTime(2015, 12, 31),
                gender = "M"
            };

            userService.RegisterUserValid(user);

            bool result = userService.DeleteUserByEmail(user);

            Assert.True(result);
            Assert.False(userService.IsEmailRegistered(user));
        }

        [Fact]
        public void FindUserByEmailAndPasswordValid()
        {
            var user = new UserModel
            {
                first_name = "John",
                last_name = "Doe",
                email = "registerme@example.com",
                password = "Password123$",
                confirm_password = "Password123$",
                date_of_birth = new DateTime(2015, 12, 31),
                gender = "M"
            };

            userService.RegisterUserValid(user);
            bool result = userService.FindUserByEmailAndPasswordValid(user);

            Assert.True(result);
            userService.DeleteUserByEmail(user);
        }

        [Fact]
        public void GetUserNameByUserId()
        {
            var user = new UserModel
            {
                first_name = "John",
                last_name = "Doe",
                email = "registerme@example.com",
                password = "Password123$",
                confirm_password = "Password123$",
                date_of_birth = new DateTime(2015, 12, 31),
                gender = "M"
            };

            userService.RegisterUserValid(user);
            int userId = userService.FindUserIdByEmail(user);

            string userInfo = userService.GetUserNameByUserId(userId);

            Assert.Equal("John Doe", userInfo);
            userService.DeleteUserByEmail(user);
        }

        [Fact]
        public void IsEmailRegistered()
        {
            var user = new UserModel
            {
                first_name = "John",
                last_name = "Doe",
                email = "registerme@example.com",
                password = "Password123$",
                confirm_password = "Password123$",
                date_of_birth = new DateTime(2015, 12, 31),
                gender = "M"
            };

            userService.RegisterUserValid(user);
            bool isRegistered = userService.IsEmailRegistered(user);

            Assert.True(isRegistered);
            userService.DeleteUserByEmail(user);
        }
    }
}
