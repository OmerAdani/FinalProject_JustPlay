using Final_project1.DAL;
using System.Data.SqlTypes;

namespace Final_project1.BL
{
    public class PlayerCard
    {
        int Card_ID;
        int Player_ID;
        string Player_Name;
        string Team_Name;
        string Medical_Examination;
        string ID_Document;
        string Passport_Photo;
        bool Is_there_a_player_card;
        string English_Name;
        string Insurance_Confirmation;

        public PlayerCard(int card_ID,string player_Name, int player_ID, string team_Name, string medical_Examination, string iD_Document, string passport_Photo, 
            bool is_there_a_player_card, string english_Name, string insurance_Confirmation)
        {
            Card_ID = card_ID;
            Player_ID = player_ID;
            Player_Name = player_Name;
            Team_Name = team_Name;
           Medical_Examination = medical_Examination;
            ID_Document = iD_Document;
            Passport_Photo = passport_Photo;
            Is_there_a_player_card = is_there_a_player_card;
            English_Name = english_Name;
            Insurance_Confirmation = insurance_Confirmation;
        }

        public int card_ID { get => Card_ID; set => Card_ID = value; }
        public int player_ID { get => Player_ID; set => Player_ID = value; }
        public string player_Name { get => Player_Name; set => Player_Name = value; }
        public string team_Name { get => Team_Name; set => Team_Name = value; }
        public string medical_Examination { get => Medical_Examination; set => Medical_Examination = value; }
        public string id_Document { get => ID_Document; set => ID_Document = value; }
        public string passport_Photo { get => Passport_Photo; set => Passport_Photo = value; }
        public bool is_there_a_player_card { get => Is_there_a_player_card; set => Is_there_a_player_card = value; }
        public string english_Name { get => English_Name; set => English_Name = value; }
        public string insurance_Confirmation { get => Insurance_Confirmation; set => Insurance_Confirmation = value; }


        public PlayerCard() { }


        public static List<PlayerCard> ReadCardsByCoachId(int coachId)
        {
            DBservices_Get dbs = new DBservices_Get();
            return dbs.ReadPlayerCardsByCoachId(coachId);
        }


        public int addPlayerCard(PlayerCard newPlayerCard)

        {
            DBservices_Post dbs = new DBservices_Post();
            return dbs.addNewPlayerCard(newPlayerCard);

        }



        public PlayerCard UpdatePlayerCard(PlayerCard card)
        {
            DBservices_put dbs_put = new DBservices_put();
            return dbs_put.UpdatePlayerCard(card);
        }



        public bool DeletePlayerCard(int cardID)
        {
            DBservices_delete db = new DBservices_delete();
            return db.DeletePlayerCard(cardID);
        }
    }
}
