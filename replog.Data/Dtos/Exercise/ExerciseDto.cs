using replog.Data.Dtos.Workout;

namespace replog.Data.Dtos.Exercise
{
    public class ExerciseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<SetDto> Sets { get; set; }
    }
}
