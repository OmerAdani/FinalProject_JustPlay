using Final_project1.DAL;

namespace Final_project1.BL
{
    public class Training
    {
        int Training_ID;
        DateTime Training_Date;

        public Training(int training_ID, DateTime training_Date)
        {
            Training_ID = training_ID;
            Training_Date = training_Date;
        }

        public int training_ID { get => Training_ID; set => Training_ID = value; }
        public DateTime training_Date { get => Training_Date; set => Training_Date = value; }


        public Training() { }


        private static List<Training> tarining_list = new List<Training>();

        public static List<Training> ReadTraining()
        {

            DBservices_Get dbs = new DBservices_Get();
            return dbs.ReadTrainings();
        }

        public int addTraining(Training newTraining)

        {
            DBservices_Post dbs = new DBservices_Post();
            return dbs.addNewTraining(newTraining);

        }



        public Training UpdateTraining(Training training)
        {
            DBservices_put dbs_put = new DBservices_put();
            return dbs_put.UpdateTraining(training);
        }

        public bool DeleteTarining(int trainingID)
        {
            DBservices_delete db = new DBservices_delete();
            return db.DeleteTraining(trainingID);
        }
    }
}
