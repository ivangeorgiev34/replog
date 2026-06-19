namespace replog.Data.Dtos.Workout
{
    public class WorkoutDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public List<SetDto> Sets { get; set; } = new List<SetDto>();
    }
}
