using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Final_project1;
using Final_project1.BL;





namespace Final_project1.DAL
{


    public class DBservices_Post
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

       





        //-----------------------------------------------------------------------------יצרית משתמש מסוג שחקן
        public int addNewPlayer(Player newPlayer)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("igroup8_prod");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Connection failed: " + ex.Message);
                return 0;
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>
    {
        { "@role", newPlayer.role },
        { "@fullname", newPlayer.name },
        { "@date_of_birth", newPlayer.date_Of_Birth },
        { "@id_number", newPlayer.id },
        { "@email", newPlayer.email },
        { "@passworduser", newPlayer.password },
        { "@phone_number", newPlayer.phone_Number },
        { "@position", newPlayer.position },
        { "@team_Name", newPlayer.team },
        { "@gender", newPlayer.gender}
    };

            cmd = CreateCommandWithStoredProcedureGeneral("AddPlayerUser_JPlay", con, paramDic);

            try
            {
                cmd.ExecuteNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                return 0;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        //-----------------------------------------------------------------------יצירת משתמש מסוג מאמן
        public int addNewCoach(Coach newCoach)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("igroup8_prod");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Connection failed: " + ex.Message);
                return 0;
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>
            {
                { "@id_number", newCoach.id },
                { "@fullname", newCoach.name},
                { "@date_of_birth", newCoach.date_Of_Birth},
                { "@email", newCoach.email},
                { "@role", newCoach.role},
                { "@passworduser", newCoach.password},
                { "@phone_number", newCoach.phone_Number},
                { "@is_manager", newCoach.is_Manager},
                { "@gender", newCoach.gender} 
            };

            cmd = CreateCommandWithStoredProcedureGeneral("AddCoachUser_JPlay", con, paramDic);

            try
            {
                cmd.ExecuteNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                return 0;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        //------------------------------------------------------------------יצירת משחק
        public int addNewGame(Game newGame)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("igroup8_prod");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Connection failed: " + ex.Message);
                return 0;
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>
    {
        { "@team_name", newGame.team },
        { "@opponent_team_name", newGame.opposing_team },
        { "@result", newGame.game_Result },
        { "@round_number", newGame.matchweek },
        { "@game_date", newGame.gameDate },
        { "@goals_scored", newGame.team_Scoring },
        { "@goals_conceded", newGame.goal_conceded }
    };

            cmd = CreateCommandWithStoredProcedureGeneral("AddGame_JPlay", con, paramDic);

            try
            {
                cmd.ExecuteNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                return 0;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        //---------------------------------------------------------יצירת תשלום חדש
        public int addNewPayment(Payment newPayment)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("igroup8_prod");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Connection failed: " + ex.Message);
                return 0;
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>
    {
        { "@player_id", newPayment.player_ID },
        { "@payment_method", newPayment.payment_Method },
        { "@number_of_payments", newPayment.number_of_payments },
        { "@months_to_pay", newPayment.number_of_months_to_pay },
        { "@total_payment", newPayment.total_Payment },
        { "@is_paid", newPayment.is_Paid }
    };

            cmd = CreateCommandWithStoredProcedureGeneral("AddPayment_JPlay", con, paramDic);

            try
            {
                cmd.ExecuteNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                return 0;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        //-------------------------------------------------------------------------יצירה של כרטיס שחקן חדש
        public int addNewPlayerCard(PlayerCard newCard)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("igroup8_prod");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Connection failed: " + ex.Message);
                return 0;
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>
    {
        { "@player_id", newCard.player_ID },
        { "@Medical_Examination", newCard.medical_Examination },
        { "@ID_Document", newCard.id_Document },
        { "@Passport_Photo", newCard.passport_Photo },
        { "@English_Name", newCard.english_Name },
        { "@Insurance_Confirmation", newCard.insurance_Confirmation },
        { "@Is_there_a_player_card", newCard.is_there_a_player_card }
    };

            cmd = CreateCommandWithStoredProcedureGeneral("AddPlayerCard_JPlay", con, paramDic);

            try
            {
                cmd.ExecuteNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                return 0;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        //--------------------------------------------------------------יצירת אימון חדש
        public int addNewTraining(Training newTraining)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("igroup8_prod");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Connection failed: " + ex.Message);
                return 0;
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>
    {
        { "@training_date", newTraining.training_Date }
    };

            cmd = CreateCommandWithStoredProcedureGeneral("AddTraining_JPlay", con, paramDic);

            try
            {
                cmd.ExecuteNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                return 0;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        //-----------------------------------------------------------------------יצירת קבוצה חדשה
        public int addNewTeam(Team newTeam)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("igroup8_prod");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Connection failed: " + ex.Message);
                return 0;
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>
    {
        { "@birth_year", newTeam.yearbook },
        { "@team_name", newTeam.name },
        { "@id_coach", newTeam.coach_id }
    };

            cmd = CreateCommandWithStoredProcedureGeneral("AddTeam_JPlay", con, paramDic);

            try
            {
                cmd.ExecuteNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                return 0;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        //------------------------------------------------------------------------הכנסה של נתוני שחקן במשחק
        public int Insert(Game_Data newGameData)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("igroup8_prod");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Connection failed: " + ex.Message);
                return 0;
            }

            cmd = new SqlCommand("InsertParticipatesInGame_JPlay", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@player_id", newGameData.playerId);
            cmd.Parameters.AddWithValue("@game_id", newGameData.gameId);
            cmd.Parameters.AddWithValue("@total_passes", newGameData.totalPasses);
            cmd.Parameters.AddWithValue("@total_attempts_to_score", newGameData.totalAttemptsToScore);
            cmd.Parameters.AddWithValue("@total_air_attempts", newGameData.totalAirAttempts);
            cmd.Parameters.AddWithValue("@num_of_accurate_passes", newGameData.numOfAccuratePasses);
            cmd.Parameters.AddWithValue("@num_of_goals", newGameData.numOfGoals);
            cmd.Parameters.AddWithValue("@num_of_assists", newGameData.numOfAssists);
            cmd.Parameters.AddWithValue("@shots_on_target", newGameData.shotsOnTarget);
            cmd.Parameters.AddWithValue("@key_passes", newGameData.keyPasses);
            cmd.Parameters.AddWithValue("@num_of_lost_ball", newGameData.numOfLostBall);
            cmd.Parameters.AddWithValue("@num_of_ball_recoveries", newGameData.numOfBallRecoveries);

            try
            {
                int affected = cmd.ExecuteNonQuery();
                return affected;
            }
            catch (Exception ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                return 0;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
    }
}

