using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using replog.Data.Models;

namespace replog.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Workout> Workouts { get; set; }

        public DbSet<Set> Sets { get; set; }

        public DbSet<Report> Reports { get; set; }

        public DbSet<MuscleGroup> MuscleGroup { get; set; }

        public DbSet<Exercise> Exercises { get; set; }
    }
}
