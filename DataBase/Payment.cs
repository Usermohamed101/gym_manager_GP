namespace GymManagmentSystem.DataBase
{
    public class Payment
    {

        public int Id { get; set; }
        public string MemberId { get; set; }
        public int MembershipId { get; set; }
        public Member Member { get; set; }

        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
      

        public string PaymentMethod { get; set; }

        public Membership Membership { get; set; }
    }
}
