using replog.Data.Dtos.Exercise;
using replog.Data.Dtos.Workout;
using replog.Data.Models;

namespace replog.Core.Services.Interfaces
{
    public interface IWorkoutService
    {
        Task<List<Workout>> GetByYear(int year);

        Task Create(string username, List<ExerciseDto> exercises);

        Task<List<WorkoutDto>> GetWorkoutsByUsername(string username);
    }
}
