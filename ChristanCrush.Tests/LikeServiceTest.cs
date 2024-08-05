using ChristanCrush.Models;
using ChristanCrush.Services;


namespace ChristanCrush.Tests
{
    public class LikeServiceTest
    {
        private readonly LikeService likeService = new LikeService();

        [Fact]
        public void CheckIfMutualLikeExists_ShouldReturnTrue()
        {
            var like1 = new LikeModel { LikerId = 4, LikedId = 5, LikedAt = DateTime.Now };
            var like2 = new LikeModel { LikerId = 5, LikedId = 4, LikedAt = DateTime.Now };

            int LikeId1 = likeService.InsertLikeInt(like1);
            int LikeId2 = likeService.InsertLikeInt(like2);

            bool result = likeService.CheckIfMutualLikeExists(4, 5);

            Assert.True(result);

            likeService.DeleteLike(LikeId1);
            likeService.DeleteLike(LikeId2);
        }

        [Fact]
        public void CheckIfMutualLikeExists_ShouldReturnFalse()
        {
            var like = new LikeModel { LikerId = 4, LikedId = 5, LikedAt = DateTime.Now };
            var like1 = new LikeModel { LikerId = 3, LikedId = 5, LikedAt = DateTime.Now };

            int LikeId = likeService.InsertLikeInt(like);
            int LikeId1 = likeService.InsertLikeInt(like1);

            bool result = likeService.CheckIfMutualLikeExists(4, 5);

            Assert.False(result);

            likeService.DeleteLike(LikeId);
            likeService.DeleteLike(LikeId1);
        }

        [Fact]
        public void DeleteLike_ShouldReturnTrue()
        {
            var like = new LikeModel { LikerId = 5, LikedId = 4, LikedAt = DateTime.Now };
            int LikeId = likeService.InsertLikeInt(like);

            bool result = likeService.DeleteLike(LikeId);

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

            int likeId= likeService.InsertLikeInt(like);

            Assert.False(string.IsNullOrEmpty(likeId.ToString()));

            likeService.DeleteLike(likeId);
        }

        [Fact]
        public void CheckIfLikeExists_ShouldReturnTrue()
        {
            var like = new LikeModel
            {
                LikerId = 4,
                LikedId = 5,
                LikedAt = DateTime.Now
            };

            int likeId = likeService.InsertLikeInt(like);

            bool result = likeService.CheckIfLikeExists(like.LikerId, like.LikedId);

            Assert.True(result);

            likeService.DeleteLike(likeId);
        }

        [Fact]
        public void CheckIfLikeExists_ShouldReturnFalse()
        {
            var like = new LikeModel
            {
                LikerId = -1,
                LikedId = -1,
                LikedAt = DateTime.Now
            };

            bool result = likeService.CheckIfLikeExists(like.LikerId, like.LikedId);

            Assert.False(result);
        }

    }
}
