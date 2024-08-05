using ChristanCrush.DataServices;
using ChristanCrush.Models;
using ChristanCrush.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChristanCrush.Tests
{
    public class ProfileServiceTest
    {
        private ProfileService profileService = new ProfileService();
        private UserService userService = new UserService();

        [Fact]
        public void InsertProfile_ShouldReturnTrue()
        {
            string email = "registerme@example.com";

            var user = new UserModel
            {
                first_name = "John",
                last_name = "Doe",
                email = email,
                password = "Password123$",
                confirm_password = "Password123$",
                date_of_birth = new DateTime(2015, 12, 31),
                gender = "M"
            };
            userService.RegisterUserValid(user);

            int userid = userService.FindUserIdByEmail(user);

            var profile = new ProfileModel
            {
                UserId = userid,
                Bio = "Test Bio",
                Image1Data = new byte[] { 1, 2, 3 },
                Image2Data = new byte[] { 4, 5, 6 },
                Image3Data = new byte[] { 7, 8, 9 },
                Occupation = "Test Occupation",
                Hobbies = "Test Hobbies"
            };

            bool result = profileService.InsertProfile(profile);

            Assert.True(result);

            profile = profileService.GetProfileByUserId(userid);

            profileService.DeleteProfile(profile.ProfileId);

            userService.DeleteUserByEmail(user);
        }

        [Fact]
        public void InsertProfile_ShouldReturnFalse()
        {
            string email = "registerme@example.com";

            var user = new UserModel
            {
                first_name = "John",
                last_name = "Doe",
                email = email,
                password = "Password123$",
                confirm_password = "Password123$",
                date_of_birth = new DateTime(2015, 12, 31),
                gender = "M"
            };
            userService.RegisterUserValid(user);

            int userid = userService.FindUserIdByEmail(user);

            var profile = new ProfileModel
            {
                UserId = userid,
                Bio = null,
                Image1Data = null,
                Image2Data = null,
                Image3Data = null,
                Occupation = null,
                Hobbies = null
            };

            bool result = profileService.InsertProfile(profile);

            Assert.False(result);

            userService.DeleteUserByEmail(user);
        }

        [Fact]
        public void DeleteProfile_ShouldReturnTrue()
        {

            string email = "registerme@example.com";

            var user = new UserModel
            {
                first_name = "John",
                last_name = "Doe",
                email = email,
                password = "Password123$",
                confirm_password = "Password123$",
                date_of_birth = new DateTime(2015, 12, 31),
                gender = "M"
            };
            userService.RegisterUserValid(user);

            int userid = userService.FindUserIdByEmail(user);

            var profile = new ProfileModel
            {
                UserId = userid,
                Bio = "Test Bio",
                Image1Data = new byte[] { 1, 2, 3 },
                Image2Data = new byte[] { 4, 5, 6 },
                Image3Data = new byte[] { 7, 8, 9 },
                Occupation = "Test Occupation",
                Hobbies = "Test Hobbies"
            };

            profileService.InsertProfile(profile);


            profile = profileService.GetProfileByUserId(userid);

            bool result = profileService.DeleteProfile(profile.ProfileId);

            Assert.True(result);

            userService.DeleteUserByEmail(user);
        }

        [Fact]
        public void GetRandomProfile_ShouldReturnTrue()
        {
            string email = "registerme@example.com";

            var user = new UserModel
            {
                first_name = "John",
                last_name = "Doe",
                email = email,
                password = "Password123$",
                confirm_password = "Password123$",
                date_of_birth = new DateTime(2015, 12, 31),
                gender = "M"
            };
            userService.RegisterUserValid(user);

            int userid = userService.FindUserIdByEmail(user);

            var profile = new ProfileModel
            {
                UserId = userid,
                Bio = "Test Bio",
                Image1Data = new byte[] { 1, 2, 3 },
                Image2Data = new byte[] { 4, 5, 6 },
                Image3Data = new byte[] { 7, 8, 9 },
                Occupation = "Test Occupation",
                Hobbies = "Test Hobbies"
            };

            profileService.InsertProfile(profile);

            profile = profileService.GetProfileByUserId(userid);

            for(int i = 0; i < 100; i++)
            {
                if(profileService.GetRandomProfile(userid) == profile)
                {
                    Assert.False(true);
                }
                else
                {
                    Assert.True(true);
                }
            }
            
            profileService.DeleteProfile(profile.ProfileId);

            userService.DeleteUserByEmail(user);
        }
    }
}
