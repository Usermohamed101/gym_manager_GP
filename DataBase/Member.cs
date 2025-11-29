namespace GymManagmentSystem.DataBase
{
    public class Member:User
    {

    
        public ICollection<MemberMembership> MemberMemberships { get; set; }

        public ICollection<Payment> Payments { get; set; }

        public ICollection<WorkOutPlan> TrainingPlans { get; set; }

        public ICollection<Trainer> Trainers { get; set; }
    }
}
