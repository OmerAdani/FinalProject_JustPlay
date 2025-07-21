using Final_project1.DAL;

namespace Final_project1.BL
{
    public class Payment
    {
        int Payment_ID;
        int Player_ID;
        string Payment_Method;
        string player_Name;
        int Number_of_payments;
        int Number_of_months_to_pay;
        int Total_Payment;
        bool Is_Paid;

        public Payment(int payment_ID, int player_ID, string payment_Method, int number_of_payments, int number_of_months_to_pay,
            int total_Payment, bool is_Paid, string Player_Name)
        {
            Payment_ID = payment_ID;
            Player_ID = player_ID;
            Payment_Method = payment_Method;
            Number_of_payments = number_of_payments;
            Number_of_months_to_pay = number_of_months_to_pay;
            Total_Payment = total_Payment;
            Is_Paid = is_Paid;
            player_Name = Player_Name;
        }

        public int payment_ID { get => Payment_ID; set => Payment_ID = value; }
        public int player_ID { get => Player_ID; set => Player_ID = value; }
        public string payment_Method { get => Payment_Method; set => Payment_Method = value; }
        public string Player_Name { get=> player_Name; set => player_Name=value; }
        public int number_of_payments { get => Number_of_payments; set => Number_of_payments = value; }
        public int number_of_months_to_pay { get => Number_of_months_to_pay; set => Number_of_months_to_pay = value; }
        public int total_Payment { get => Total_Payment; set => Total_Payment = value; }
        public bool is_Paid { get => Is_Paid; set => Is_Paid = value; }



        public Payment() { }

        
        public static List<Payment> ReadPayments()
        {

            DBservices_Get dbs = new DBservices_Get();
            return dbs.ReadPayments();
        }


        public int addPayment(Payment newPayment)

        {
            DBservices_Post dbs = new DBservices_Post();
            return dbs.addNewPayment(newPayment);

        }


        public Payment UpdatePayment(Payment payment)
        {
            DBservices_put dbs = new DBservices_put();
            return dbs.UpdatePayment(payment);
        }


        public bool DeletePayment(int PaymentId)
        {
            DBservices_delete db = new DBservices_delete();
            return db.DeletePayment(PaymentId);
        }
    }
}
