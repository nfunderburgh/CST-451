using ChristanCrush.DataServices;
using ChristanCrush.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChristanCrush.Tests
{
    public class ProfileDAOTest
    {
        private ProfileDAO profileDAO = new ProfileDAO();

        [Fact]
        public void InsertProfile_ShouldReturnTrueWhenSuccessful()
        {
            var profile = new ProfileModel
            {
                UserId = 4,
                Bio = "Test Bio",
                Image1Data = new byte[] { 1, 2, 3 },
                Image2Data = new byte[] { 4, 5, 6 },
                Image3Data = new byte[] { 7, 8, 9 },
                Occupation = "Test Occupation",
                Hobbies = "Test Hobbies"
            };

            bool result = profileDAO.InsertProfile(profile);

            Assert.True(result);
        }
    }
}
