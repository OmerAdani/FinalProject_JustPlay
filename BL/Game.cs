using Final_project1.DAL;
using System.Numerics;

namespace Final_project1.BL
{
    public class Game
    {
        int GameID;
        DateTime GameDate;
        int Matchweek;
        string Team;
        string Opposing_team;
        string Game_Result;
        int Team_Scoring;
        int Goal_conceded;

        public int gameID { get => GameID; set => GameID = value; }
        public DateTime gameDate { get => GameDate; set => GameDate = value; }
        public int matchweek { get => Matchweek; set => Matchweek = value; }
        public string team { get => Team; set => Team = value; }
        public string opposing_team { get => Opposing_team; set => Opposing_team = value; }
        public string game_Result { get => Game_Result; set => Game_Result = value; }
        public int team_Scoring { get => Team_Scoring; set => Team_Scoring = value; }
        public int goal_conceded { get => Goal_conceded; set => Goal_conceded = value; }

        public Game() { }

        public Game(int gameID, DateTime gameDate, int matchweek, string team, string opposing_team,
            string game_Result, int team_Scoring, int goal_conceded)
        {
            GameID = gameID;
            GameDate = gameDate;
            Matchweek = matchweek;
            Team = team;
            Opposing_team = opposing_team;
            Game_Result = game_Result;
            Team_Scoring = team_Scoring;
            Goal_conceded = goal_conceded;
        }

        public static List<Game> ReadGamesByCoachId(int coachId)
        {
            DBservices_Get dbs = new DBservices_Get();
            return dbs.ReadGamesByCoachId(coachId);
        }

        public int addNewGame(Game newGame)

        {
            DBservices_Post dbs = new DBservices_Post();
            return dbs.addNewGame(newGame);

        }

        public Game UpdateGame(Game updatedGame)
        {
            DBservices_put dbs = new DBservices_put();
            Game result = dbs.UpdateGame(updatedGame);

            if (result != null)
            {
                return result;
            }

            return null;
        }


        public bool DeleteGame(int GameId)
        {
            DBservices_delete db = new DBservices_delete();
            return db.DeleteGame(GameId);
        }
    }
}
