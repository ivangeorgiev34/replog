using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using replog.Core.Services.Interfaces;
using replog.Data.Common;
using replog.Data.Dtos.Exercise;
using replog.Data.Dtos.Workout;
using replog.Data.Models;
using replog.Models.Workout;

namespace replog.Controllers
{
    public class WorkoutController : Controller
    {
        private readonly IExerciseService _exerciseService;
        private readonly IRepository _repository;
        private readonly IWorkoutService _workoutService;

        public WorkoutController(IExerciseService exerciseService, IRepository repository, IWorkoutService workoutService)
        {
            this._exerciseService = exerciseService;
            this._repository = repository;
            this._workoutService = workoutService;
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Index()
        {
            string currentUsername = User.Identity?.Name;

            if (string.IsNullOrEmpty(currentUsername))
            {
                return Challenge();
            }

            try
            {
                var workouts = await _workoutService.GetWorkoutsByUsername(currentUsername);

                return View(workouts);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Could not load workouts: {ex.Message}");
                return View(new List<WorkoutDto>());
            }
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Create()
        {
            List<SelectListItem> exercises = await this._exerciseService.GetExercises();

            ViewBag.AvailableExercises = exercises;
            return View(new CreateWorkoutViewModel());
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Create(CreateWorkoutViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.AvailableExercises = await this._repository.All<Exercise>()
                    .Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name })
                    .ToListAsync();

                return View(model);
            }

            List<ExerciseDto> exercises = model.Exercises
                .Select(x => new ExerciseDto
                {
                    Id = x.ExerciseId,
                    Name = x.ExerciseName,
                    Sets = x.Sets.Select(s => new SetDto
                    {
                        Kilograms = s.Weight,
                        Repetitions = s.Reps
                    })
                    .ToList()
                })
                .ToList();

            await this._workoutService.Create(User.Identity!.Name!, exercises);

            return RedirectToAction("Index", "Home");
        }
    }
}
