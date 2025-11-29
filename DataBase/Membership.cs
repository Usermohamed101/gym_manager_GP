namespace GymManagmentSystem.DataBase
{
    public class Membership
    {
        public int Id { get; set; }
        public string Name { get; set; } // Monthly, Yearly, Gold, etc.
        public decimal Price { get; set; }
        public int DurationInDays { get; set; } // 30, 90, 365, etc.

        // Navigation
        public ICollection<MemberMembership> MemberMemberships { get; set; }
    }
}
