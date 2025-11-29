namespace GymManagmentSystem.Dtos
{
    public class AddWorkoutDto
    {

        public string Name { get; set; }
        public int Reps { get; set; }
        public int Sets { get; set; }
        public decimal? Weight { get; set; }

    }
}
