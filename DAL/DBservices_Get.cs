using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Final_project1;
using Final_project1.BL;
using System.Xml.Linq;





namespace Final_project1.DAL
{


    public class DBservices_Get
    {





        //
        // TODO: Add constructor logic here
        //


        //--------------------------------------------------------------------------------------------------
        // This method creates a connection to the database according to the connectionString name in the web.config 
        //--------------------------------------------------------------------------------------------------
        public SqlConnection connect(String conString)
        {

            // read the connection string from the configuration file
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json").Build();
            string cStr = configuration.GetConnectionString(conString);
            SqlConnection con = new SqlConnection(cStr);
            con.Open();
            return con;
        }
        private SqlCommand CreateCommandWithStoredProcedureGeneral(String spName, SqlConnection con, Dictionary<string, object> paramDic)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            if (paramDic != null)
                foreach (KeyValuePair<string, object> param in paramDic)
                {
                    cmd.Parameters.AddWithValue(param.Key, param.Value);

                }


            return cmd;
        }




        //------------------------------------------------------------------------------החזרת תקין או לא חטובת הLogin 
        public (int id, string team_name, string name, bool isActive, string role,bool isManager) GetUserIdByEmail(string email)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("igroup8_prod");
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            try
            {
                Dictionary<string, object> paramDic = new Dictionary<string, object>();
                paramDic.Add("@Email", email);

                cmd = CreateCommandWithStoredProcedureGeneral("GetUserIdByEmail_JPlay", con, paramDic);

                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                if (dataReader.Read())
                {
                    int id = Convert.ToInt32(dataReader["id_number"]);
                    string name = dataReader["fullname"].ToString();
                    string team_name = dataReader["team_Name"].ToString();
                    bool isActive = Convert.ToBoolean(dataReader["is_active"]);
                    string role = dataReader["Role"] != DBNull.Value ? dataReader["Role"].ToString() : null;
                    bool isManager = Convert.ToBoolean(dataReader["is_manager"]);

                    return (id, team_name, name, isActive, role,isManager);
                }

                return (-1, null, null, false, null,false); // Fix: Ensure the tuple matches the expected structure.  
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }






        //---------------------------------------------------------החזרת רשימת משתמשים 
        public List<Users> ReadUsers()
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("igroup8_prod"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw; // CA2200: Re-throwing caught exception changes stack information
            }

            List<Users> users = new List<Users>();

            cmd = CreateCommandWithStoredProcedureGeneral("GetAllUsers", con, new Dictionary<string, object>()); // CS8625: Cannot convert null literal to non-nullable reference type.

            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                Users u = new Users();
                u.id = Convert.ToInt32(dataReader["id_number"]);
                u.name = dataReader["fullname"].ToString() ?? string.Empty;
                u.gender = dataReader["Gender"].ToString() ?? string.Empty;
                u.role = dataReader["Role"].ToString() ?? string.Empty;
                u.date_Of_Birth = Convert.ToDateTime(dataReader["date_of_birth"]);
                u.email = dataReader["email"].ToString() ?? string.Empty;
                u.password = dataReader["passworduser"].ToString() ?? string.Empty;
                u.phone_Number = dataReader["phone_number"].ToString() ?? string.Empty;
                u.is_active = Convert.ToBoolean(dataReader["is_active"]);
                users.Add(u);
            }
            return users;
        }
        //-----------------------------------------------------------------החזרת רשימת שחקנים לפי מאמן או מנהל 
        public List<Player> ReadPlayersByCoachId(int coachId)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("igroup8_prod");
            }
            catch (Exception ex)
            {
                throw;
            }

            List<Player> players = new List<Player>();
            Dictionary<string, object> paramDic = new Dictionary<string, object>
    {
        { "@coach_id", coachId }
    };

            cmd = CreateCommandWithStoredProcedureGeneral("GetPlayersByCoachId_JPlay", con, paramDic);
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                Player p = new Player();
                p.id = Convert.ToInt32(dataReader["Player id"]);
                p.name = dataReader["Full Name"].ToString() ?? string.Empty;
                p.date_Of_Birth = Convert.ToDateTime(dataReader["Date of Birth"]);
                p.gender = dataReader["Gender"].ToString() ?? string.Empty;
                p.email = dataReader["Email"].ToString() ?? string.Empty;
                p.password = dataReader["Password"].ToString() ?? string.Empty;
                p.phone_Number = dataReader["Phone Number"].ToString() ?? string.Empty;
                p.role = dataReader["Role"].ToString() ?? string.Empty;
                p.position = dataReader["position"].ToString() ?? string.Empty;
                p.team = dataReader["Team"].ToString() ?? string.Empty;
                p.is_active = Convert.ToBoolean(dataReader["Is Active"]);
                p.system_id = Convert.ToInt32(dataReader["System ID"]);

                players.Add(p);
            }

            return players;
        }

        //-----------------------------------------------------------------החזרת רשימת מאמנים
        public List<Coach> ReadCoach()
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("igroup8_prod"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw; // CA2200: Re-throwing caught exception changes stack information
            }

            List<Coach> coach = new List<Coach>();

            cmd = CreateCommandWithStoredProcedureGeneral("GetAllCoachs", con, new Dictionary<string, object>()); // CS8625: Cannot convert null literal to non-nullable reference type.

            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                Coach c = new Coach();
                c.id = Convert.ToInt32(dataReader["Coach ID"]);
                c.is_Manager = Convert.ToBoolean(dataReader["Is Manager"]);
                c.name = dataReader["Full Name"].ToString() ?? string.Empty;
                c.gender = dataReader["Gender"].ToString() ?? string.Empty;
                c.date_Of_Birth = Convert.ToDateTime(dataReader["Date of Birth"]);
                c.role = dataReader["Role"].ToString() ?? string.Empty;
                c.email = dataReader["Email"].ToString() ?? string.Empty;
                c.password = dataReader["Password"].ToString() ?? string.Empty;
                c.phone_Number = dataReader["Phone Number"].ToString() ?? string.Empty;
                c.is_active = Convert.ToBoolean(dataReader["Is Active"]);
                c.system_id = Convert.ToInt32(dataReader["System ID"]);
                coach.Add(c);
            }
            return coach;
        }
        //--------------------------------------------------------------- לפי מאמן או מנהל החזרת רשימת משחקים
        public List<Game> ReadGamesByCoachId(int coachId)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("igroup8_prod");
            }
            catch (Exception ex)
            {
                throw;
            }

            List<Game> games = new List<Game>();

            Dictionary<string, object> paramDic = new Dictionary<string, object>
    {
        { "@coach_id", coachId }
    };

            cmd = CreateCommandWithStoredProcedureGeneral("GetGamesByCoachId_JPlay", con, paramDic);
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (reader.Read())
            {
                Game g = new Game();
                g.gameID = Convert.ToInt32(reader["Game ID"]);
                g.gameDate = Convert.ToDateTime(reader["Game Date"]);
                g.team = reader["Team"].ToString() ?? string.Empty;
                g.opposing_team = reader["Opponent Team"].ToString() ?? string.Empty;
                g.matchweek = Convert.ToInt32(reader["Round Number"]);
                g.game_Result = reader["Result"].ToString() ?? string.Empty;
                g.team_Scoring = Convert.ToInt32(reader["Goals Scored"]);
                g.goal_conceded = Convert.ToInt32(reader["Goals Conceded"]);

                games.Add(g);
            }

            return games;
        }

        //---------------------------------------------------------------------החזרת רשימת תשלומים


        public List<Payment> ReadPayments()
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("igroup8_prod"); // יצירת חיבור
            }
            catch (Exception ex)
            {
                // כתיבה ללוג (אם קיים)
                throw;
            }

            List<Payment> payments = new List<Payment>();

            cmd = CreateCommandWithStoredProcedureGeneral("GetAllPayments", con, new Dictionary<string, object>());

            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                Payment p = new Payment();

                p.payment_ID = Convert.ToInt32(dataReader["Payment ID"]);
                p.player_ID = Convert.ToInt32(dataReader["Player ID"]);
                p.Player_Name = dataReader["Player Name"].ToString() ?? string.Empty;
                p.payment_Method = dataReader["Payment Method"].ToString() ?? string.Empty;
                p.number_of_payments = Convert.ToInt32(dataReader["Number of Payments"]);
                p.number_of_months_to_pay = Convert.ToInt32(dataReader["Months to Pay"]);
                p.total_Payment = Convert.ToInt32(dataReader["Total Payment"]);
                p.is_Paid = Convert.ToBoolean(dataReader["Is Paid"]);

                payments.Add(p);
            }

            return payments;
        }


        //--------------------------------------------------------------------------- לפי מאמן או מנהל החזרה של רשימת כרטיסי שחקן
        public List<PlayerCard> ReadPlayerCardsByCoachId(int coachId)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("igroup8_prod");
            }
            catch (Exception ex)
            {
                throw;
            }

            List<PlayerCard> playerCards = new List<PlayerCard>();

            Dictionary<string, object> paramDic = new Dictionary<string, object>
    {
        { "@coach_id", coachId }
    };

            cmd = CreateCommandWithStoredProcedureGeneral("GetPlayerCardsByCoachId_JPlay", con, paramDic);
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (reader.Read())
            {
                PlayerCard card = new PlayerCard();

                card.card_ID = Convert.ToInt32(reader["Player Card ID"]);
                card.player_ID = Convert.ToInt32(reader["Player ID"]);
                card.player_Name = reader["Player Name"].ToString() ?? string.Empty;
                card.team_Name = reader["Team Name"].ToString() ?? string.Empty;
                card.medical_Examination = reader["Medical Examination"].ToString() ?? string.Empty;
                card.id_Document = reader["ID Document"].ToString() ?? string.Empty;
                card.passport_Photo = reader["Passport Photo"].ToString() ?? string.Empty;
                card.english_Name = reader["English Name"].ToString() ?? string.Empty;
                card.insurance_Confirmation = reader["Insurance Confirmation"].ToString() ?? string.Empty;
                card.is_there_a_player_card = Convert.ToBoolean(reader["Has Player Card?"]);

                playerCards.Add(card);
            }

            return playerCards;
        }
        //-----------------------------------------------------------------------------------רשימה להחזרת אימונים

        public List<Training> ReadTrainings()
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("igroup8_prod"); // יצירת חיבור
            }
            catch (Exception ex)
            {
                throw;
            }

            List<Training> trainings = new List<Training>();

            cmd = CreateCommandWithStoredProcedureGeneral("GetAllTraining", con, new Dictionary<string, object>());

            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                Training t = new Training();

                t.training_ID = Convert.ToInt32(dataReader["Training ID"]);
                t.training_Date = Convert.ToDateTime(dataReader["Training Date"]);

                trainings.Add(t);
            }

            return trainings;
        }
        //-----------------------------------------------------------------------החזרת רשימת קבוצות
        public List<Team> ReadTeams()
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("igroup8_prod"); // יצירת חיבור
            }
            catch (Exception ex)
            {
                throw;
            }

            List<Team> teams = new List<Team>();

            cmd = CreateCommandWithStoredProcedureGeneral("GetAllTeams", con, new Dictionary<string, object>());

            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                Team team = new Team();

                team.team_id = Convert.ToInt32(dataReader["Team ID"]);
                team.yearbook = Convert.ToInt32(dataReader["Birth Year"]);
                team.name = dataReader["Team Name"].ToString() ?? string.Empty;
                team.coach_id = Convert.ToInt32(dataReader["Coach ID"]);
                team.coach_name = dataReader["Coach Full Name"].ToString() ?? string.Empty;
               

                teams.Add(team);
            }

            return teams;
        }

        //--------------------------------------------------------------------------------------------------------החזרה של נתוני המשחק

        public List<Game_Data> ReadGameDataByCoachId(int coachId)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("igroup8_prod");
            }
            catch (Exception ex)
            {
                throw;
            }

            List<Game_Data> gameDataList = new List<Game_Data>();

            Dictionary<string, object> paramDic = new Dictionary<string, object>
    {
        { "@coach_id", coachId }
    };

            cmd = CreateCommandWithStoredProcedureGeneral("GetGameDataByCoachId_JPlay", con, paramDic);
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (reader.Read())
            {
                Game_Data g = new Game_Data
                (
                    Convert.ToInt32(reader["Player ID"]),
                    reader["Player Name"].ToString(),
                    reader["Position"].ToString(),
                    reader["Team Name"].ToString(),
                    reader["Opponent Team Name"].ToString(),
                    Convert.ToInt32(reader["Game ID"]),
                    Convert.ToDateTime(reader["Game Date"]),
                    Convert.ToInt32(reader["Total Passes"]),
                    Convert.ToInt32(reader["Total Attempts to Score"]),
                    Convert.ToInt32(reader["Num of Air Attempts"]),
                    Convert.ToInt32(reader["Num of Accurate Passes"]),
                    Convert.ToInt32(reader["Goals"]),
                    Convert.ToInt32(reader["Assists"]),
                    Convert.ToInt32(reader["Shots on Target"]),
                    Convert.ToInt32(reader["Key Passes"]),
                    Convert.ToInt32(reader["Num of Lost Ball"]),
                    0, // NumOfPasses (אם כבר לא קיים יותר)
                    Convert.ToInt32(reader["Num of Ball Recoveries"])
                );

                gameDataList.Add(g);
            }

            return gameDataList;
        }



        //----------------------------------------------------------------------פרוצדורה להחזרת משתתפים באימונים

        public List<ParticipatesInTraining> ReadTrainingParticipationsByCoachId(int coachId)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("igroup8_prod");
            }
            catch (Exception ex)
            {
                throw;
            }

            List<ParticipatesInTraining> participations = new List<ParticipatesInTraining>();

            Dictionary<string, object> paramDic = new Dictionary<string, object>
    {
        { "@coach_id", coachId }
    };

            cmd = CreateCommandWithStoredProcedureGeneral("GetTrainingParticipationByCoachId_JPlay", con, paramDic);
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (reader.Read())
            {
                ParticipatesInTraining tp = new ParticipatesInTraining();

                tp.player_ID = Convert.ToInt32(reader["Player ID"]);
                tp.team_Name = reader["Team Name"].ToString() ?? string.Empty;
                tp.training_ID = Convert.ToInt32(reader["Training ID"]);
                tp.was_in_training = Convert.ToBoolean(reader["was in training?"]);
                tp.took_part_in_training = Convert.ToBoolean(reader["Participated?"]);
                tp.reason_for_not_training = reader["Reason for Absence"].ToString() ?? string.Empty;

                participations.Add(tp);
            }

            return participations;
        }


    }
}

