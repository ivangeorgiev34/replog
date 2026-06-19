using System.ComponentModel.DataAnnotations;

namespace replog.Data.Models
{
    public class Report : BaseDbModel
    {
        [Required(ErrorMessage = "Title is required")]
        [Length(5, 50, ErrorMessage = "Title should be between 5 and 50 characters")]
        public required string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [Length(10, 200, ErrorMessage = "Description should be between 10 and 200 characters")]
        public required string Description { get; set; }

        public Guid? CompletedById { get; set; }
        public ApplicationUser CompletedBy { get; set; } = null!;
    }
}
