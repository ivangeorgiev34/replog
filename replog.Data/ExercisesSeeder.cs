using Microsoft.EntityFrameworkCore;
using replog.Data.Models;

namespace replog.Data
{
    public class ExercisesSeeder
    {
        private readonly static HashSet<(string Name, string Description, IEnumerable<string> MuscleGroups)> _exercises =
    [
        // Chest
        (
            "Barbell Bench Press",
            "Classic compound chest exercise performed with a barbell on a flat bench.",
            ["Chest", "Triceps", "Shoulder"]
        ),
        (
            "Incline Dumbbell Press",
            "Pressing dumbbells on an incline bench to emphasize the upper chest.",
            ["Chest", "Shoulder", "Triceps"]
        ),
        (
            "Push Up",
            "Bodyweight pushing exercise performed from a plank position.",
            ["Chest", "Triceps", "Shoulder"]
        ),

        // Back
        (
            "Pull Up",
            "Standard vertical pull up using body weight.",
            ["Back", "Biceps"]
        ),
        (
            "Barbell Row",
            "Bent-over rowing movement using a barbell.",
            ["Back", "Biceps"]
        ),
        (
            "Lat Pulldown",
            "Machine-based pulling exercise targeting the latissimus dorsi.",
            ["Back", "Biceps"]
        ),

        // Biceps
        (
            "Barbell Curl",
            "Standing curl performed with a straight barbell.",
            ["Biceps"]
        ),
        (
            "Hammer Curl",
            "Neutral-grip dumbbell curl emphasizing the brachialis and biceps.",
            ["Biceps"]
        ),
        (
            "Preacher Curl",
            "Curl variation performed on a preacher bench for isolation.",
            ["Biceps"]
        ),

        // Triceps
        (
            "Triceps Pushdown",
            "Cable exercise performed by extending the elbows downward.",
            ["Triceps"]
        ),
        (
            "Skull Crusher",
            "Lying triceps extension performed with a barbell or EZ bar.",
            ["Triceps"]
        ),
        (
            "Bench Dip",
            "Bodyweight dip performed using a bench for support.",
            ["Triceps", "Chest"]
        ),

        // Shoulder
        (
            "Overhead Press",
            "Compound pressing movement performed overhead.",
            ["Shoulder", "Triceps"]
        ),
        (
            "Lateral Raise",
            "Dumbbell raise targeting the lateral deltoids.",
            ["Shoulder"]
        ),
        (
            "Face Pull",
            "Cable exercise targeting rear deltoids and upper back.",
            ["Shoulder", "Back"]
        ),

        // Legs
        (
            "Barbell Squat",
            "Fundamental lower-body exercise performed with a barbell.",
            ["Legs"]
        ),
        (
            "Romanian Deadlift",
            "Hip-hinge movement focusing on hamstrings and glutes.",
            ["Legs", "Back"]
        ),
        (
            "Leg Press",
            "Machine-based lower-body pressing exercise.",
            ["Legs"]
        )
    ];

        public static async Task SeedExercises(DbContext dbContext)
        {
            HashSet<MuscleGroup> muscleGroups = dbContext.Set<MuscleGroup>().ToHashSet();

            foreach (var exercise in _exercises)
            {
                if (dbContext.Set<Exercise>().Any(x => x.Name == exercise.Name))
                    continue;

                HashSet<MuscleGroup> exerciseMuscleGroups = muscleGroups
                    .IntersectBy(exercise.MuscleGroups, x => x.Name)
                    .ToHashSet();

                await dbContext.AddAsync(new Exercise
                {
                    Name = exercise.Name,
                    Description = exercise.Description,
                    MuscleGroups = exerciseMuscleGroups
                });
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
