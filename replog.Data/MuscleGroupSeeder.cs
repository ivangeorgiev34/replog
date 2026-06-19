using Microsoft.EntityFrameworkCore;
using replog.Data.Models;

namespace replog.Data
{
    public class MuscleGroupSeeder
    {
        private readonly static HashSet<string> _muscleGroupNames = ["Chest", "Back", "Biceps", "Triceps", "Shoulder", "Legs"];
        public static async Task SeedMuscleGroups(DbContext dbContext)
        {
            foreach (string muscleGroupName in _muscleGroupNames)
            {
                if (dbContext.Set<MuscleGroup>().Any(x => x.Name == muscleGroupName))
                    continue;

                await dbContext.AddAsync(new MuscleGroup() { Name = muscleGroupName });
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
