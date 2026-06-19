using System.ComponentModel.DataAnnotations;

namespace replog.Data.Models
{
    public class Exercise : BaseDbModel
    {
        public Exercise()
        {
            this.MuscleGroups = new();
        }

        [Required(ErrorMessage = "Exercise name is required")]
        [Length(1, 100, ErrorMessage = "Exercise name should be between 1 and 100 characters")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Exercise description is required")]
        [Length(1, 100, ErrorMessage = "Exercise description should be between 1 and 100 characters")]
        public required string Description { get; set; }

        [Required(ErrorMessage = "Exercise should have at least one muscle group")]
        public HashSet<MuscleGroup> MuscleGroups { get; set; }
    }
}
