using Microsoft.AspNetCore.Mvc.Rendering;
using replog.Data.Dtos.Exercise;
namespace replog.Core.Services.Interfaces
{
    public interface IExerciseService
    {
        Task Create(string name, string description, List<MuscleGroupDto> muscleGroups);

        Task<List<SelectListItem>> GetExercises();
    }
}
