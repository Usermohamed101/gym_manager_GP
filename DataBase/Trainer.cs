namespace GymManagmentSystem.DataBase
{
    public class Trainer:User
    {


        public ICollection<WorkOutPlan> WorkOutPlans { get; set; }
        public ICollection<Member> Members { get; set; }

    }
}
