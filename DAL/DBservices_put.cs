using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Final_project1;
using Final_project1.BL;
using Microsoft.AspNetCore.Mvc.Routing;





namespace Final_project1.DAL
{


    public class DBservices_put
    {
        // TODO: Add constructor logic here

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
            cmd.CommandText = spName;          // can be Select, Insert, Update, Delete 
            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds
            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            if (paramDic != null)
            {
                foreach (KeyValuePair<string, object> param in paramDic)
                {
                    cmd.Parameters.AddWithValue(param.Key, param.Value);
                }
            }

            return cmd;
        }
        //---------------------------------------------------------------------------------עדכון משתמש מסוג שחקן
        public Player UpdatePlayer(Player player)
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
                return null;
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>
    {
        { "@system_id", player.system_id },
        { "@new_id_number", player.id },
        { "@fullname", player.name },
        { "@gender", player.gender },
        { "@date_of_birth", player.date_Of_Birth },
        { "@email", player.email },
        { "@role", player.role },
        { "@passworduser", player.password },
        { "@phone_number", player.phone_Number },
        { "@is_active", player.is_active },
        { "@position", player.position },
        { "@team_name", player.team }
    };

            cmd = CreateCommandWithStoredProcedureGeneral("UpdatePlayerBySystemId_JPlay", con, paramDic);

            try
            {
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (reader.Read())
                {
                    // בונה מחדש את האובייקט מהנתונים המעודכנים מה-DB
                    Player updatedPlayer = new Player
                    {
                        system_id = Convert.ToInt32(reader["System ID"]),
                        id = Convert.ToInt32(reader["Player ID"]),
                        name = reader["Full Name"].ToString(),
                        gender = reader["Gender"].ToString(),
                        date_Of_Birth = Convert.ToDateTime(reader["Date of Birth"]),
                        email = reader["Email"].ToString(),
                        password = reader["Password"]?.ToString() ?? string.Empty,
                        phone_Number = reader["Phone Number"]?.ToString() ?? string.Empty,
                        role = reader["Role"].ToString(),
                        is_active = Convert.ToBoolean(reader["Is Active"]),
                        position = reader["Position"].ToString(),
                        team = reader["Team"].ToString()
                    };

                    return updatedPlayer;
                }
                else
                {
                    Console.WriteLine("No data returned from procedure.");
                    return null;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                return null;
            }
            finally
            {
                if (con != null && con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }

        //----------------------------------------------------------------------------------------עדכון משתמש מסוג מאמן והחזרה של הנתונים אחרי עדכון
        public Coach UpdateCoach(Coach coach)
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
                return null;
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>
    {
        { "@system_id", coach.system_id },
        { "@new_id_number", coach.id },
        { "@fullname", coach.name },
        { "@gender", coach.gender },
        { "@date_of_birth", coach.date_Of_Birth },
        { "@email", coach.email },
        { "@role", coach.role },
        { "@passworduser", coach.password },
        { "@phone_number", coach.phone_Number },
        { "@is_active", coach.is_active },
        { "@is_manager", coach.is_Manager }
    };

            cmd = CreateCommandWithStoredProcedureGeneral("UpdateCoachBySystemId_JPlay", con, paramDic);

            try
            {
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (reader.Read())
                {
                    Coach updatedCoach = new Coach
                    {
                        system_id = Convert.ToInt32(reader["System ID"]),
                        id = Convert.ToInt32(reader["Coach ID"]),
                        name = reader["Full Name"].ToString(),
                        gender = reader["Gender"].ToString(),
                        date_Of_Birth = Convert.ToDateTime(reader["Date of Birth"]),
                        email = reader["Email"].ToString(),
                        password = reader["Password"]?.ToString() ?? string.Empty,
                        phone_Number = reader["Phone Number"]?.ToString() ?? string.Empty,
                        role = reader["Role"].ToString(),
                        is_active = Convert.ToBoolean(reader["Is Active"]),
                        is_Manager = Convert.ToBoolean(reader["Is Manager"])
                    };

                    return updatedCoach;
                }
                else
                {
                    Console.WriteLine("No data returned from procedure.");
                    return null;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                return null;
            }
            finally
            {
                if (con != null && con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }


        //-----------------------------------------------------------------------עדכון משחקים והחזרה של המשחק לאחר עריכה

        public Game UpdateGame(Game updatedGame)
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
                return null;
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>
    {
        {"@game_id", updatedGame.gameID},
        {"@team_name", updatedGame.team},
        {"@opponent_team_name", updatedGame.opposing_team},
        {"@result", updatedGame.game_Result},
        {"@round_number", updatedGame.matchweek},
        {"@game_date", updatedGame.gameDate},
        {"@goals_scored", updatedGame.team_Scoring},
        {"@goals_conceded", updatedGame.goal_conceded}
    };

            cmd = CreateCommandWithStoredProcedureGeneral("UpdateGameById_JPlay", con, paramDic);

            try
            {
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                if (reader.Read())
                {
                    Game g = new Game
                    {
                        gameID = Convert.ToInt32(reader["Game ID"]),
                        gameDate = Convert.ToDateTime(reader["Game Date"]),
                        team = reader["Team"].ToString(),
                        opposing_team = reader["Opponent Team"].ToString(),
                        matchweek = Convert.ToInt32(reader["Round Number"]),
                        game_Result = reader["Result"].ToString(),
                        team_Scoring = Convert.ToInt32(reader["Goals Scored"]),
                        goal_conceded = Convert.ToInt32(reader["Goals Conceded"])
                    };

                    return g;
                }
                else
                {
                    Console.WriteLine("No game returned from update procedure.");
                    return null;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                return null;
            }
            finally
            {
                if (con != null && con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }

        //--------------------------------------------------------------------------פונקצייה לעריכת תשלום והחזרה של התשלום החדש
        public Payment UpdatePayment(Payment payment)
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
                return null;
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>
    {
        {"@payment_id", payment.payment_ID},
        {"@player_id", payment.player_ID},
        {"@payment_method", payment.payment_Method},
        {"@number_of_payments", payment.number_of_payments},
        {"@months_to_pay", payment.number_of_months_to_pay},
        {"@total_payment", payment.total_Payment},
        {"@is_paid", payment.is_Paid}
    };

            cmd = CreateCommandWithStoredProcedureGeneral("UpdatePaymentById_JPlay", con, paramDic);

            try
            {
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (reader.Read())
                {
                    Payment p = new Payment
                    {
                        payment_ID = Convert.ToInt32(reader["Payment ID"]),
                        player_ID = Convert.ToInt32(reader["Player ID"]),
                        payment_Method = reader["Payment Method"].ToString(),
                        number_of_payments = Convert.ToInt32(reader["Number of Payments"]),
                        number_of_months_to_pay = Convert.ToInt32(reader["Months to Pay"]),
                        total_Payment = Convert.ToInt32(reader["Total Payment"]),
                        is_Paid = Convert.ToBoolean(reader["Is Paid"])
                    };

                    return p;
                }
                else
                {
                    Console.WriteLine("No data returned from procedure.");
                    return null;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                return null;
            }
            finally
            {
                if (con != null && con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }

        //--------------------------------------------------------------------------עדכון של כרטיס שחקן והחזרה של החדש
        public PlayerCard UpdatePlayerCard(PlayerCard card)
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
                return null;
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>
    {
        { "@Player_Card_ID", card.card_ID },
        { "@player_id", card.player_ID },
        { "@Medical_Examination", card.medical_Examination },
        { "@ID_Document", card.id_Document },
        { "@Passport_Photo", card.passport_Photo },
        { "@English_Name", card.english_Name },
        { "@Insurance_Confirmation", card.insurance_Confirmation },
        { "@Is_there_a_player_card", card.is_there_a_player_card }
    };

            cmd = CreateCommandWithStoredProcedureGeneral("UpdatePlayerCard_JPlay", con, paramDic);

            try
            {
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                if (reader.Read())
                {
                    return new PlayerCard
                    {
                        card_ID = Convert.ToInt32(reader["card_ID"]),
                        player_ID = Convert.ToInt32(reader["player_ID"]),
                        medical_Examination = reader["medical_Examination"].ToString(),
                        id_Document = reader["id_Document"].ToString(),
                        passport_Photo = reader["passport_Photo"].ToString(),
                        english_Name = reader["english_Name"].ToString(),
                        insurance_Confirmation = reader["insurance_Confirmation"].ToString(),
                        is_there_a_player_card = Convert.ToBoolean(reader["is_there_a_player_card"])
                    };
                }

                return null;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                return null;
            }
            finally
            {
                if (con != null && con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }

        //----------------------------------------------------------------------------------עדכון אימון והחזרה של האימון החדש
        public Training UpdateTraining(Training training)
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
                return null;
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>
        {
            { "@training_id", training.training_ID },
            { "@training_date", training.training_Date }
        };

            cmd = CreateCommandWithStoredProcedureGeneral("UpdateTraining_JPlay", con, paramDic);

            try
            {
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (reader.Read())
                {
                    return new Training
                    {
                        training_ID = Convert.ToInt32(reader["Training ID"]),
                        training_Date = Convert.ToDateTime(reader["Training Date"])
                    };
                }

                return null;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                return null;
            }
            finally
            {
                if (con != null && con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }

        //-----------------------------------------------------------------------------עריכת קבוצה והחזרה של הקבוצה החדשה
        public Team UpdateTeam(Team team)
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
                return null;
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>
        {
            { "@team_id", team.team_id },
            { "@birth_year", team.yearbook },
            { "@team_name", team.name },
            { "@id_coach", team.coach_id }
        };

            cmd = CreateCommandWithStoredProcedureGeneral("UpdateTeam_JPlay", con, paramDic);

            try
            {
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (reader.Read())
                {
                    return new Team
                    {
                        team_id = Convert.ToInt32(reader["Team ID"]),
                        yearbook = Convert.ToInt32(reader["Birth Year"]),
                        name = reader["Team Name"].ToString(),
                        coach_id = Convert.ToInt32(reader["Coach ID"]),
                        coach_name = reader["Coach Name"].ToString()
                    };
                }

                return null;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                return null;
            }
            finally
            {
                if (con != null && con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }

        //-----------------------------------------------------------------------------עריכה של נתוני שחקן במשחק


        public Game_Data UpdatePlayerGameStats(Game_Data stats)
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
                return null;
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>
    {
        { "@player_id", stats.playerId },
        { "@game_id", stats.gameId },
        { "@num_of_goals", stats.numOfGoals },
        { "@num_of_assists", stats.numOfAssists },
        { "@total_attempts_to_score", stats.totalAttemptsToScore },
        { "@shots_on_target", stats.shotsOnTarget },
        { "@total_passes", stats.totalPasses },
        { "@num_of_accurate_passes", stats.numOfAccuratePasses },
        { "@num_of_lost_ball", stats.numOfLostBall },
        { "@key_passes", stats.keyPasses },
        { "@total_air_attempts", stats.totalAirAttempts },
        { "@num_of_ball_recoveries", stats.numOfBallRecoveries }
    };

            cmd = CreateCommandWithStoredProcedureGeneral("UpdatePlayerGameStats_JPlay", con, paramDic);

            try
            {
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (reader.Read())
                {
                    Game_Data updatedStats = new Game_Data
                    {
                        playerId = Convert.ToInt32(reader["player_id"]),
                        gameId = Convert.ToInt32(reader["game_id"]),
                        numOfGoals = Convert.ToInt32(reader["num_of_goals"]),
                        numOfAssists = Convert.ToInt32(reader["num_of_assists"]),
                        totalAttemptsToScore = Convert.ToInt32(reader["total_attempts_to_score"]),
                        shotsOnTarget = Convert.ToInt32(reader["shots_on_target"]),
                        totalPasses = Convert.ToInt32(reader["total_passes"]),
                        numOfAccuratePasses = Convert.ToInt32(reader["num_of_accurate_passes"]),
                        numOfLostBall = Convert.ToInt32(reader["num_of_lost_ball"]),
                        keyPasses = Convert.ToInt32(reader["key_passes"]),
                        totalAirAttempts = Convert.ToInt32(reader["total_air_attempts"]),
                        numOfBallRecoveries = Convert.ToInt32(reader["num_of_ball_recoveries"])
                    };
                    return updatedStats;
                }
                else
                {
                    return null;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                return null;
            }
            finally
            {
                if (con != null && con.State != ConnectionState.Closed)
                    con.Close();
            }
        }
    }

    }
    



