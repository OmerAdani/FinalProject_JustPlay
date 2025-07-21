using Final_project1.DAL;

namespace Final_project1.BL
{
    public class ParticipatesInTraining
    {
        int Training_ID;
        string Team_Name;
        int Player_ID;
        string Player_Name;
        DateTime Training_Date;
        bool Was_in_training;
        bool Took_part_in_training;
        string Reason_for_not_training;

        public ParticipatesInTraining(int training_ID, string team_Name, int player_ID, string player_Name, DateTime training_Date,
            bool was_in_training, bool took_part_in_training, string reason_for_not_training)
        {
            Training_ID = training_ID;
            Team_Name = team_Name;
            Player_ID = player_ID;
            Player_Name = player_Name;
            Training_Date = training_Date;
            Was_in_training = was_in_training;
            Took_part_in_training = took_part_in_training;
            Reason_for_not_training = reason_for_not_training;
        }

        public int training_ID { get => Training_ID; set => Training_ID = value; }
        public string team_Name { get => Team_Name; set => Team_Name = value; }
        public int player_ID { get => Player_ID; set => Player_ID = value; }
        public string player_Name { get => Player_Name; set => Player_Name = value; }
        public DateTime training_Date { get => Training_Date; set => Training_Date = value; }
        public bool was_in_training { get => Was_in_training; set => Was_in_training = value; }
        public bool took_part_in_training { get => Took_part_in_training; set => Took_part_in_training = value; }
        public string reason_for_not_training { get => Reason_for_not_training; set => Reason_for_not_training = value; }


       public ParticipatesInTraining() { }

        public static List<ParticipatesInTraining> ReadByCoachId(int coachId)
        {
            DBservices_Get dbs = new DBservices_Get();
            return dbs.ReadTrainingParticipationsByCoachId(coachId);
        }



    }

    
}
