using replog.Data.Dtos.Exercise;

namespace replog.Data.Dtos.Workout
{
    public class SetDto
    {
        public Guid Id { get; set; }
        public int Repetitions { get; set; }
        public double Kilograms { get; set; }
        public ExerciseDto Exercise { get; set; }
    }
}
