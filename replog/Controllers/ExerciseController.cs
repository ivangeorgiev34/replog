using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using replog.Core.Services.Interfaces;
using replog.Data.Dtos.Exercise;
using replog.Models.Exercise;

namespace replog.Controllers
{
    public class ExerciseController : Controller
    {
        private readonly IMuscleGroupService _muscleGroupService;
        private readonly IExerciseService _exerciseService;

        public ExerciseController(IMuscleGroupService muscleGroupService, IExerciseService exerciseService)
        {
            this._muscleGroupService = muscleGroupService;
            this._exerciseService = exerciseService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            List<MuscleGroupSelection> muscleGroups = _muscleGroupService.GetAll()
                .Select(x => new MuscleGroupSelection
                {
                    Id = x.Id,
                    Name = x.Name,
                    IsSelected = false
                })
                .ToList();

            CreateExerciseViewModel model = new()
            {
                MuscleGroups = muscleGroups
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateExerciseViewModel model)
        {
            if (!ModelState.IsValid)
                return View();

            List<MuscleGroupDto> muscleGroups = model.MuscleGroups
                .Where(x => x.IsSelected)
                .Select(x => new MuscleGroupDto
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToList();

            await this._exerciseService.Create(model.Name, model.Description, muscleGroups);
            return RedirectToAction("Index", "Home");
        }
    }
}
