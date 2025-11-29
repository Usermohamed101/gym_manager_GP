namespace GymManagmentSystem.DataBase
{
    public class WorkOut
    {
        public int Id { get; set; }
        public string ExerciseName { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public decimal? Weight { get; set; } // nullable

        public int WorkOutPlanId { get; set; }
        public WorkOutPlan WorkOutPlan { get; set; }

    }
}
