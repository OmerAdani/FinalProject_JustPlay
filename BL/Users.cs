using Final_project1.DAL;
using System.Reflection;
namespace Final_project1.BL
{
    public class Users
    {
        protected int System_id;
        protected string Role;
        protected string Name;
        protected DateTime Date_Of_Birth;
        protected int ID;
        protected string Email;
        protected string Password;
        protected string Phone_Number;
        protected string Gender;
        protected bool Is_Active;

        public int system_id { get => System_id; set => System_id = value; }
        public string role { get => Role; set => Role = value; }
        public string name { get => Name; set => Name = value; }
        public DateTime date_Of_Birth { get => Date_Of_Birth; set => Date_Of_Birth = value; }
        public int id { get => ID; set => ID = value; }
        public string email { get => Email; set => Email = value; }
        public string password { get => Password; set => Password = value; }
        public string phone_Number { get => Phone_Number; set => Phone_Number = value; }
        public string gender { get => Gender; set => Gender = value; }
        public bool is_active { get => Is_Active; set => Is_Active = value; }

        public Users() { }

        public Users(int system_id, string role, string name, DateTime date_Of_Birth, int id, string email, string password,
            string phone_Number, string gender, bool is_active)
        {
            System_id = system_id;
            Role = role;
            Name = name;
            Date_Of_Birth = date_Of_Birth;
            ID = id;
            Email = email;
            Password = password;
            Phone_Number = phone_Number;
            Gender = gender;
            Is_Active = is_active;
        }

        public static List<Users> ReadUsers()
        {
            DBservices_Get dbs = new DBservices_Get();
            return dbs.ReadUsers();
        }

        public (int id, string team_name,string name, bool isActive,string role,bool isManager) GetUserIdByEmail(string Email)
        {
            DBservices_Get dbs = new DBservices_Get();
            return dbs.GetUserIdByEmail(Email);
        }

        public bool Login(string Email, string Password)
        {
            DBservices_Get dbs = new DBservices_Get();

            foreach (Users tempuser in dbs.ReadUsers())
            {
                if (tempuser.email == Email && tempuser.password == Password)
                {
                    Console.WriteLine("user exists");
                    return true;
                }
            }
            return false;
        }




    }
}
