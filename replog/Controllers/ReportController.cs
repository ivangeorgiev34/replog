using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using replog.Core.Services.Interfaces;
using replog.Data;
using replog.Data.Dtos.Report;
using replog.Models.Home;
using replog.Models.Report;

namespace replog.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;
        private readonly IUserService _userService;

        public ReportController(IReportService reportService, IUserService _userService)
        {
            this._reportService = reportService;
            this._userService = _userService;
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        [AllowAnonymous]
        public IActionResult Create()
        {
            ReportViewModel model = new();
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        [AllowAnonymous]
        public async Task<IActionResult> Create(ReportViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            CreateReportDto dto = new()
            {
                Title = model.Title,
                Description = model.Description,
            };

            await this._reportService.CreateAsync(dto);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Complete(ReportItemViewModel model)
        {
            ApplicationUser? user = await this._userService.FindByName(User.Identity?.Name ?? string.Empty);

            if (user == null)
                return View("Error");

            if (!await this._reportService.Complete(model.Id, user.Id))
                return View("Error");

            return RedirectToAction("Index", "Home");
        }
    }
}
