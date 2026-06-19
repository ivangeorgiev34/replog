using System.ComponentModel.DataAnnotations;

namespace replog.Data.Models
{
    public class MuscleGroup : BaseDbModel
    {
        public MuscleGroup()
        {
            this.Exercises = new();
        }

        [Required(ErrorMessage = "Muscle group name is required")]
        [Length(1, 15, ErrorMessage = "Muscle group name should be between 1 and 15 characters")]
        public required string Name { get; set; }

        public HashSet<Exercise>? Exercises { get; set; }
    }
}
