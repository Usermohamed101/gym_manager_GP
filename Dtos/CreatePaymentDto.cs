namespace GymManagmentSystem.Dtos
{
    public class CreatePaymentDto
    {
        public string MemberId { get; set; }
        public int MembershipId { get; set; }
        public decimal Amount { get; set; }
        public string Method { get; set; }
    }
}
