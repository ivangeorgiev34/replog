using System.ComponentModel.DataAnnotations.Schema;

namespace replog.Data.Models
{
    public class Workout : BaseDbModel
    {
        public Workout()
        {
            this.Sets = new();
        }

        public DateTime Date { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = null!;

        public HashSet<Set> Sets { get; set; }
    }
}
