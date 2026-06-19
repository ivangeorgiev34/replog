namespace replog.Models.Workout
{
    public class CreateWorkoutViewModel
    {
        public List<WorkoutExerciseViewModel> Exercises { get; set; } = new();
    }

    public class WorkoutExerciseViewModel
    {
        public Guid ExerciseId { get; set; }
        public string ExerciseName { get; set; } = string.Empty;
        public List<ExerciseSetViewModel> Sets { get; set; } = new();
    }

    public class ExerciseSetViewModel
    {
        public int Reps { get; set; }
        public double Weight { get; set; }
    }
}
