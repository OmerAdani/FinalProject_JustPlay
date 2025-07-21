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


    public class DBservices_delete
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

        //-------------------------------------------------------------------------מחיקה של שחקן מכלל הטבלאות
        public bool DeletePlayer(int playerId)
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
                return false;
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>
                        {
                            { "@player_id", playerId }
                        };

            cmd = CreateCommandWithStoredProcedureGeneral("DeletePlayerById_JPlay", con, paramDic);

            try
            {
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                return false;
            }
            finally
            {
                if (con != null && con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }

        //---------------------------------------------------------------------------------------מחיקה של מאמן מכלל הטבלאות
        public bool DeleteCoach(int coachId)
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
                return false;
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>
                        {
                            { "@coach_id", coachId }
                        };

            cmd = CreateCommandWithStoredProcedureGeneral("DeleteCoachById_JPlay", con, paramDic);

            try
            {
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                return false;
            }
            finally
            {
                if (con != null && con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }

        //------------------------------------------------------------------מחיקה של משחק לפי ID

        public bool DeleteGame(int gameId)
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
                return false;
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>
                    {
                        { "@game_id", gameId }
                    };

            cmd = CreateCommandWithStoredProcedureGeneral("DeleteGameById_JPlay", con, paramDic);

            try
            {
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                return false;
            }
            finally
            {
                if (con != null && con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }

        //-------------------------------------------------------------------------------מחיקה של תשלום לפי ID

        public bool DeletePayment(int paymentId)
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
                return false;
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>
                    {
                        { "@payment_id", paymentId }
                    };

            cmd = CreateCommandWithStoredProcedureGeneral("DeletePaymentById_JPlay", con, paramDic);

            try
            {
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                return false;
            }
            finally
            {
                if (con != null && con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }

        //-------------------------------------------------------------------------מחיקה של כרטיס שחקן

        public bool DeletePlayerCard(int playerCardId)
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
                return false;
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>
                    {
                        { "@player_card_id", playerCardId }
                    };

            cmd = CreateCommandWithStoredProcedureGeneral("DeletePlayerCardById_JPlay", con, paramDic);

            try
            {
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                return false;
            }
            finally
            {
                if (con != null && con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }

        //-------------------------------------------------------------------------מחיקה של אימון

        public bool DeleteTraining(int trainingId)
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
                return false;
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>
                {
                    { "@training_id", trainingId }
                };

            cmd = CreateCommandWithStoredProcedureGeneral("DeleteTrainingById_JPlay", con, paramDic);

            try
            {
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                return false;
            }
            finally
            {
                if (con != null && con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }

        //-------------------------------------------------------------------------------------מחיקה של קבוצה
        public bool DeleteTeam(string teamName)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("igroup8_prod"); // Fix the connection method call
            }
            catch (Exception ex)
            {
                Console.WriteLine("Connection failed: " + ex.Message);
                return false;
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>
                {
                    { "@team_name", teamName }
                };

            cmd = CreateCommandWithStoredProcedureGeneral("DeleteTeamByName_JPlay", con, paramDic); // Fix the command creation method call

            try
            {
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                return false;
            }
            finally
            {
                if (con != null && con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }

       // ------------------------------------------------------------------מחיקת נתוני שחקן במשחק

        public bool DeletePlayerFromGame(int playerId, int gameId)
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
                return false;
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>
    {
        {"@player_id", playerId},
        {"@game_id", gameId}
    };

            cmd = CreateCommandWithStoredProcedureGeneral("DeletePlayerFromGame_JPlay", con, paramDic);

            try
            {
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                return false;
            }
            finally
            {
                if (con != null && con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }


    }
}