using System.ComponentModel.DataAnnotations;

namespace replog.Models.Exercise
{
    public class CreateExerciseViewModel
    {
        [Required(ErrorMessage = "Please enter an exercise name.")]
        [StringLength(100, ErrorMessage = "The name cannot exceed 100 characters.")]
        [Display(Name = "Exercise Name")]
        public string Name { get; set; }

        [StringLength(100, ErrorMessage = "The description cannot exceed 100 characters.")]
        [Display(Name = "Description / Instructions")]
        public string Description { get; set; }

        public List<MuscleGroupSelection> MuscleGroups { get; set; } = new List<MuscleGroupSelection>();
    }

    public class MuscleGroupSelection
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }
}