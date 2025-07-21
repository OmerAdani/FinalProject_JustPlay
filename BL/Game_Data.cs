using Final_project1.DAL;

namespace Final_project1.BL
{
    public class Game_Data
    {
        int PlayerId;
        string PlayerName;
        string PlayerPosition;
        string PlayerTeam;
        string Opposing_team;
        int GameId;
        DateTime GameDate;
        int TotalPasses;
        int TotalAttemptsToScore;
        int TotalAirAttempts;
        int NumOfAccuratePasses;
        int NumOfGoals;
        int NumOfAssists;
        int ShotsOnTarget;
        int KeyPasses;
        int NumOfLostBall;
        int NumOfPasses;
        int NumOfBallRecoveries;

        public Game_Data(int playerId, string playerName, string playerPosition, string playerTeam, string opposing_team, int gameId, DateTime gameDate,
            int totalPasses, int totalAttemptsToScore, int totalAirAttempts, int numOfAccuratePasses, int numOfGoals, int numOfAssists,
            int shotsOnTarget, int keyPasses, int numOfLostBall, int numOfPasses, int numOfBallRecoveries)
        {
            PlayerId = playerId;
            PlayerName = playerName;
            PlayerPosition = playerPosition;
            PlayerTeam = playerTeam;
            Opposing_team = opposing_team;
            GameId = gameId;
            GameDate = gameDate;
            TotalPasses = totalPasses;
            TotalAttemptsToScore = totalAttemptsToScore;
            TotalAirAttempts = totalAirAttempts;
            NumOfAccuratePasses = numOfAccuratePasses;
            NumOfGoals = numOfGoals;
            NumOfAssists = numOfAssists;
            ShotsOnTarget = shotsOnTarget;
            KeyPasses = keyPasses;
            NumOfLostBall = numOfLostBall;
            NumOfPasses = numOfPasses;
            NumOfBallRecoveries = numOfBallRecoveries;
        }

        public int playerId { get => PlayerId; set => PlayerId = value; }

        public string playerPosition { get => PlayerPosition; set => PlayerPosition = value; }
        public string playerName { get => PlayerName; set => PlayerName = value; }
        public string playerTeam { get => PlayerTeam; set => PlayerTeam = value; }
        public string opposing_team { get => Opposing_team; set => Opposing_team = value; }
        public int gameId { get => GameId; set => GameId = value; }
        public DateTime gameDate { get => GameDate; set => GameDate = value; }
        public int totalPasses { get => TotalPasses; set => TotalPasses = value; }
        public int totalAttemptsToScore { get => TotalAttemptsToScore; set => TotalAttemptsToScore = value; }
        public int totalAirAttempts { get => TotalAirAttempts; set => TotalAirAttempts = value; }
        public int numOfAccuratePasses { get => NumOfAccuratePasses; set => NumOfAccuratePasses = value; }
        public int numOfGoals { get => NumOfGoals; set => NumOfGoals = value; }
        public int numOfAssists { get => NumOfAssists; set => NumOfAssists = value; }
        public int shotsOnTarget { get => ShotsOnTarget; set => ShotsOnTarget = value; }
        public int keyPasses { get => KeyPasses; set => KeyPasses = value; }
        public int numOfLostBall { get => NumOfLostBall; set => NumOfLostBall = value; }
        public int numOfPasses { get => NumOfPasses; set => NumOfPasses = value; }
        public int numOfBallRecoveries { get => NumOfBallRecoveries; set => NumOfBallRecoveries = value; }



        public Game_Data() { }



        public static List<Game_Data> ReadDataByCoachId(int coachId)
        {
            DBservices_Get dbs = new DBservices_Get();
            return dbs.ReadGameDataByCoachId(coachId);
        }


        public int InsertGameData(Game_Data newGameData)
        {
            DBservices_Post db = new DBservices_Post();
            return db.Insert(newGameData);
        }




        public Game_Data UpdateStats(Game_Data gameData)
        {
            DBservices_put dbServices = new DBservices_put();
            return dbServices.UpdatePlayerGameStats(gameData);
        }


        public bool RemovePlayerFromGame(int playerId, int gameId)
        {
            DBservices_delete db = new DBservices_delete();
            return db.DeletePlayerFromGame(playerId, gameId);
        }
    }






}
