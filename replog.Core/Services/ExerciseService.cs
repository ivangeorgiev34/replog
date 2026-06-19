using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using replog.Core.Services.Interfaces;
using replog.Data.Common;
using replog.Data.Dtos.Exercise;
using replog.Data.Models;

namespace replog.Core.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly IRepository _repository;
        private readonly IMuscleGroupService _muscleGroupService;

        public ExerciseService(IRepository repository, IMuscleGroupService muscleGroupService)
        {
            this._repository = repository;
            this._muscleGroupService = muscleGroupService;
        }

        public async Task Create(string name, string description, List<MuscleGroupDto> groups)
        {
            IEnumerable<Guid> muscleGroupIds = groups
                .Select(x => x.Id);

            HashSet<MuscleGroup> muscleGroups = this._muscleGroupService.GetByIds(muscleGroupIds).ToHashSet();

            Exercise exercise = new()
            {
                Name = name,
                Description = description,
                MuscleGroups = muscleGroups
            };

            await _repository.AddAsync(exercise);
            await _repository.SaveChangesAsync();
        }

        public Task<List<SelectListItem>> GetExercises()
        {
            return this._repository.All<Exercise>()
                .OrderBy(e => e.Name)
                .Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Name
                })
                .ToListAsync();
        }
    }
}
