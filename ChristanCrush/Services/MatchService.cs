using ChristanCrush.DataServices;
using ChristanCrush.Models;

namespace ChristanCrush.Services
{
    public class MatchService
    {
        private DBConnection database = new DBConnection();
        private MatchDAO MatchDao = new MatchDAO();

        public bool InsertMatch(MatchModel match)
        {
            return MatchDao.InsertMatch(match, database.DbConnection());
        }

        public MatchModel GetMatch(int userId1, int userId2)
        {
            return MatchDao.GetMatch(userId1, userId2, database.DbConnection());
        }

        public bool DeleteMatchById(int matchId)
        {
            return MatchDao.DeleteMatchById(matchId, database.DbConnection());
        }
    }
}
