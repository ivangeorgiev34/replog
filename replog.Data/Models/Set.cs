using System.ComponentModel.DataAnnotations;

namespace replog.Data.Models
{
    public class Set : BaseDbModel
    {
        [Required(ErrorMessage = "Set repetitions are required")]
        [Range(1, int.MaxValue, ErrorMessage = "Repetitions must be at least 1")]
        public int Repetitions { get; set; }

        [Required(ErrorMessage = "Set kilograms are required")]
        public double Kilograms { get; set; }

        public Guid ExerciseId { get; set; }
        public Exercise Exercise { get; set; } = null!;

        public Guid? WorkoutId { get; set; }
        public Workout Workout { get; set; } = null!;
    }
}
