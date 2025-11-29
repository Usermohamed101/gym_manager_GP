namespace GymManagmentSystem.DataBase
{
    public class MemberMembership
    {
        public string MemberId { get; set; }
        public Member Member { get; set; }

        public int MembershipId { get; set; }
        public Membership Membership { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
    }
}
