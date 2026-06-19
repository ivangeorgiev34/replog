using System.ComponentModel.DataAnnotations;

namespace replog.Data.Models
{
    public class BaseDbModel
    {
        [Required(ErrorMessage = "Every entity should have an identifier")]
        [Key]
        public Guid Id { get; set; }
    }
}
