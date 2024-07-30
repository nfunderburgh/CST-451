using ChristanCrush.DataServices;
using ChristanCrush.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChristanCrush.Tests
{
    public class LikeDAOTest
    {
        private readonly LikeDAO LikeDao = new LikeDAO();

        [Fact]
        public void CheckIfMutualLikeExists_ShouldReturnTrue()
        {
            var like1 = new LikeModel { LikerId = 4, LikedId = 5, LikedAt = DateTime.Now };
            var like2 = new LikeModel { LikerId = 5, LikedId = 4, LikedAt = DateTime.Now };

            int LikeId1 = LikeDao.InsertLikeInt(like1);
            int LikeId2 = LikeDao.InsertLikeInt(like2);

            bool result = LikeDao.CheckIfMutualLikeExists(4, 5);

            Assert.True(result);

            LikeDao.DeleteLike(LikeId1);
            LikeDao.DeleteLike(LikeId2);
        }

        [Fact]
        public void CheckIfMutualLikeExists_ShouldReturnFalse()
        {
            var like = new LikeModel { LikerId = 4, LikedId = 5, LikedAt = DateTime.Now };
            var like1 = new LikeModel { LikerId = 3, LikedId = 5, LikedAt = DateTime.Now };

            int LikeId = LikeDao.InsertLikeInt(like);
            int LikeId1 = LikeDao.InsertLikeInt(like1);

            bool result = LikeDao.CheckIfMutualLikeExists(4, 5);

            Assert.False(result);

            LikeDao.DeleteLike(LikeId);
            LikeDao.DeleteLike(LikeId1);
        }

        [Fact]
        public void DeleteLike_ShouldReturnTrue()
        {
            var like = new LikeModel { LikerId = 5, LikedId = 4, LikedAt = DateTime.Now };
            int LikeId = LikeDao.InsertLikeInt(like);

            bool result = LikeDao.DeleteLike(LikeId);

            Assert.True(result);
        }

        [Fact]
        public void InsertLike_ShouldReturnFalse()
        {
            // Arrange
            var like = new LikeModel
            {
                LikerId = 4,
                LikedId = 5,
                LikedAt = DateTime.Now
            };

            int likeId= LikeDao.InsertLikeInt(like);

            Assert.False(string.IsNullOrEmpty(likeId.ToString()));

            LikeDao.DeleteLike(likeId);
        }
    }
}
