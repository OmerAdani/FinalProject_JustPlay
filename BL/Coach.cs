using Final_project1.DAL;
using Microsoft.AspNetCore.Mvc;

namespace Final_project1.BL
{
    public class Coach : Users
    {
        bool Is_Manager;

        public Coach(int system_id,string role,string name, DateTime dateOfBirth, int id, string email, string password,
            string phoneNumber,string gender, bool is_active, bool is_Manager)
            : base(system_id,role,name, dateOfBirth, id, email, password, phoneNumber,gender, is_active)
        {
            Is_Manager = is_Manager;
        }

        public bool is_Manager { get => Is_Manager; set => Is_Manager = value; }

        public Coach() { }

        public static List<Coach> ReadCoach()
        {

            DBservices_Get dbs = new DBservices_Get();
            return dbs.ReadCoach();
        }
        public int Register_Coach(Coach newCoach)

        {
            DBservices_Post dbs = new DBservices_Post();
            return dbs.addNewCoach(newCoach);

        }

        public Coach UpdateCoach(Coach userCoach)
        {
            DBservices_put dbs_put = new DBservices_put();
            Coach result = dbs_put.UpdateCoach(userCoach);

            if (result != null)
            {
                return userCoach;
            }

            return null;
        }

        public bool DeleteCoach(int coachId)
        {
            DBservices_delete dbs_delete = new DBservices_delete();
            return dbs_delete.DeleteCoach(coachId);
        }


    }
}
