using Final_project1.DAL;

namespace Final_project1.BL
{
    public class Player : Users
    {
       
      
        string Position;
        string Team;//לשים לב שזה שם הקבוצ ולא התז שלה , שם הקבוצה צריך להיות המפתח הראשי של קבוצה

        public Player(int system_id,string role, string name, DateTime dateOfBirth, int id, string email, string password,
            string phoneNumber,string gender, bool is_active, string position,string team)
            : base(system_id,role,name, dateOfBirth, id, email, password, phoneNumber, gender, is_active)
        {
            
            Position = position;

        }

        
        public string position { get => Position; set => Position = value; }
        public string team { get => Team; set => Team = value; }

        
        public Player() { }


        public static List<Player> ReadPlayersByCoachId(int coachId)
        {
            DBservices_Get dbs = new DBservices_Get();
            return dbs.ReadPlayersByCoachId(coachId);
        }
        public int Register(Player newPlayer)

        {
           

            DBservices_Post dbs = new DBservices_Post();
            return dbs.addNewPlayer(newPlayer);

        }
        public Player UpdatePlayer(Player userPlayer)
        {
            DBservices_put dbs_put = new DBservices_put();
            Player result = dbs_put.UpdatePlayer(userPlayer);

            if (result != null)
            {
                return userPlayer;
            }

            return null; 
        }


        public bool DeletePlayer(int playerId)
        {
            DBservices_delete dbs_delete = new DBservices_delete();
            return dbs_delete.DeletePlayer(playerId);
        }
    }
}
