using Microsoft.EntityFrameworkCore;
using replog.Core.Services.Interfaces;
using replog.Data;
using replog.Data.Common;
using replog.Data.Dtos.Exercise;
using replog.Data.Dtos.Workout;
using replog.Data.Models;

namespace replog.Core.Services
{
    public class WorkoutService : IWorkoutService
    {
        private readonly IRepository _repository;

        public WorkoutService(IRepository repository)
        {
            this._repository = repository;
        }

        public async Task Create(string username, List<ExerciseDto> exercises)
        {
            ApplicationUser? user = await this._repository
                .AllReadonly<ApplicationUser>()
                .FirstOrDefaultAsync(x => x.UserName == username);

            if (user == null)
                throw new Exception("User not found");

            HashSet<Set> sets = new HashSet<Set>();
            foreach (var exercise in exercises)
            {
                foreach (var set in exercise.Sets)
                {
                    sets.Add(new Set()
                    {
                        ExerciseId = exercise.Id,
                        Kilograms = set.Kilograms,
                        Repetitions = set.Repetitions,
                    });
                }
            }

            await this._repository.AddRangeAsync(sets);
            await this._repository.SaveChangesAsync();

            Workout workout = new()
            {
                Date = DateTime.Now,
                UserId = user.Id,
                Sets = sets
            };

            await this._repository.AddAsync(workout);
            await this._repository.SaveChangesAsync();
        }

        public Task<List<Workout>> GetByYear(int year)
        {
            return this._repository.All<Workout>()
                .Where(x => x.Date.Year == year)
                .ToListAsync();
        }

        public async Task<List<WorkoutDto>> GetWorkoutsByUsername(string username)
        {
            ApplicationUser? user = await this._repository
                 .AllReadonly<ApplicationUser>()
                 .FirstOrDefaultAsync(x => x.UserName == username);

            if (user == null)
                throw new Exception("User not found");

            return await this._repository
                .All<Workout>()
                .Include(x => x.Sets)
                .ThenInclude(x => x.Exercise)
                .Where(x => x.UserId == user.Id)
                .Select(x => new WorkoutDto
                {
                    Id = x.Id,
                    Date = x.Date,
                    Sets = x.Sets.Select(s => new SetDto
                    {
                        Id = s.Id,
                        Kilograms = s.Kilograms,
                        Repetitions = s.Repetitions,
                        Exercise = new ExerciseDto
                        {
                            Id = s.Exercise.Id,
                            Name = s.Exercise.Name,
                        }
                    })
                    .ToList()
                })
                .ToListAsync();
        }
    }
}
