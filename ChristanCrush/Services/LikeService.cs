using ChristanCrush.DataServices;
using ChristanCrush.Models;

namespace ChristanCrush.Services
{
    public class LikeService
    {
        private DBConnection database = new DBConnection();
        private LikeDAO LikeDao = new LikeDAO();

        public bool InsertLike(LikeModel like)
        {
            return LikeDao.InsertLike(like, database.DbConnection());
        }

        public int InsertLikeInt(LikeModel like)
        {
            return LikeDao.InsertLikeInt(like, database.DbConnection());
        }

        public bool CheckIfMutualLikeExists(int userId1, int userId2)
        {
            return LikeDao.CheckIfMutualLikeExists(userId1, userId2, database.DbConnection());
        }

        public bool DeleteLike(int likeId)
        {
            return LikeDao.DeleteLike(likeId, database.DbConnection());
        }

        public bool CheckIfLikeExists(int likerId, int likedId)
        {
            return LikeDao.CheckIfLikeExists(likerId, likedId, database.DbConnection());
        }
    }
}
