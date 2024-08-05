using ChristanCrush.DataServices;
using ChristanCrush.Models;

namespace ChristanCrush.Services
{
    public class ProfileService
    {
        private DBConnection database = new DBConnection();
        private ProfileDAO ProfileDao = new ProfileDAO();

        public bool InsertProfile(ProfileModel profile)
        {
            return ProfileDao.InsertProfile(profile, database.DbConnection());
        }

        public ProfileModel GetProfileByUserId(int userId)
        {
            return ProfileDao.GetProfileByUserId(userId, database.DbConnection());
        }

        public ProfileModel GetRandomProfile(int currentUserId)
        {
            return ProfileDao.GetRandomProfile(currentUserId, database.DbConnection());
        }

        public ProfileModel GetProfileByProfileId(int profileId)
        {
            return ProfileDao.GetProfileByProfileId(profileId, database.DbConnection());   
        }

        public List<ProfileModel> GetProfilesMatchedWithUser(int userId)
        {
           return ProfileDao.GetProfilesMatchedWithUser(userId, database.DbConnection());
        }

        public bool DeleteProfile(int profileId)
        {
            return ProfileDao.DeleteProfile(profileId, database.DbConnection());   
        }
    }
}
