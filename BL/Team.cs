using Final_project1.DAL;

namespace Final_project1.BL
{
    public class Team
    {
        int Team_id;
        int Coach_id;
        string Coach_Name;
        string Name;
        int Yearbook;



        

        public int team_id { get => Team_id; set => Team_id = value; }
        public int coach_id { get => Coach_id; set => Coach_id = value; }

        public string coach_name { get => Coach_Name; set => Coach_Name = value; }
        public string name { get => Name; set => Name = value; }

        public int yearbook { get => Yearbook; set => Yearbook = value; }


        public Team(int team_id,int coach_id,string coach_name, string name,int yearbook)
        {
            Team_id = team_id;
            Coach_id = coach_id;
            Coach_Name = coach_name;
            Name = name;
            Yearbook = yearbook;
        }

        public Team() { }

      


        public static List<Team> ReadTeam()
        {

            DBservices_Get dbs = new DBservices_Get();
            return dbs.ReadTeams();
        }

        public int addNewTeam(Team newTeam)

        {
            DBservices_Post dbs = new DBservices_Post();
            return dbs.addNewTeam(newTeam);

        }


        public Team UpdateTeam(Team team)
        {
            DBservices_put dbs = new DBservices_put();
            return dbs.UpdateTeam(team);
        }



        public bool DeleteTeam(string TeamName)
        {
            DBservices_delete db = new DBservices_delete();
            return db.DeleteTeam(TeamName);
        }
    }

}
