using ChristanCrush.Models;
using ChristanCrush.Services;
using ChristanCrush.Utility;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChristanCrush.Tests
{
    public class UserDaoTest
    {
        private UserDAO userDAO = new UserDAO();

        /// <summary>
        /// The RegisterUserValid function tests the registration of a user with valid information in a
        /// C# application.
        /// </summary>
        [Fact]
        public void RegisterUserValid()
        {
            var user = new UserModel
            {
                first_name = "John",
                last_name = "Doe",
                email = "registerme@example.com",
                password = "password123",
                confirm_password = "password123",
                date_of_birth = new DateTime(2015, 12, 31),
                gender = "M"
            };

            bool result = userDAO.RegisterUserValid(user);

            Assert.True(result);
            Assert.True(userDAO.IsEmailRegistered(user));
            userDAO.DeleteUserByEmail(user);
        }

        /// <summary>
        /// The function tests the deletion of a user by email address from a database.
        /// </summary>
        [Fact]
        public void DeleteUserByEmail()
        {
            var user = new UserModel
            {
                first_name = "John",
                last_name = "Doe",
                email = "registerme@example.com",
                password = "password123",
                confirm_password = "password123",
                date_of_birth = new DateTime(2015, 12, 31),
                gender = "M"
            };

            userDAO.RegisterUserValid(user);

            bool result = userDAO.DeleteUserByEmail(user);

            Assert.True(result);
            Assert.False(userDAO.IsEmailRegistered(user));
        }
    }
}
