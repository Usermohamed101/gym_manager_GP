namespace GymManagmentSystem.DataBase
{
    public class WorkOutPlan
    {

        public int Id { get; set; }
        public string Name { get; set; } 

        public string MemberId { get; set; }
        public Member Member { get; set; }

        public string TrainerId { get; set; }
        public Trainer Trainer { get; set; }

        
        public ICollection<WorkOut> Workouts { get; set; }
    }
}
