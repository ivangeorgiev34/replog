using System.ComponentModel.DataAnnotations;

namespace replog.Models.Report
{
    public class ReportViewModel
    {
        [Required(ErrorMessage = "A title is required.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "The title must be between 5 and 50 characters.")]
        [Display(Name = "Report Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "A description is required.")]
        [StringLength(200, MinimumLength = 10, ErrorMessage = "The description must be between 10 and 200 characters.")]
        [Display(Name = "Report Description")]
        public string Description { get; set; }
    }
}
