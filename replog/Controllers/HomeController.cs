using Microsoft.AspNetCore.Mvc;
using replog.Core.Services.Interfaces;
using replog.Models;
using replog.Models.Home;
using System.Diagnostics;

namespace replog.Controllers
{
    public class HomeController : Controller
    {
        private readonly int CurrentYear = DateTime.Now.Year;

        private const int MinYear = 1990;
        private const int MaxYear = 2050;

        private readonly IReportService _reportService;
        private readonly IWorkoutService _workoutService;

        public HomeController(IReportService reportService, IWorkoutService workoutService)
        {
            this._reportService = reportService;
            this._workoutService = workoutService;
        }
        public async Task<IActionResult> Index(int year)
        {
            int calendarYear = year == 0 ? CurrentYear
                : year < MinYear ? MinYear
                : year > MaxYear ? MaxYear
                : year;

            IEnumerable<ReportItemViewModel> reports = (User.Identity != null && User.Identity.IsAuthenticated && User.IsInRole("Admin"))
                ? this._reportService.GetAll()
                .Where(x => x.CompletedBy == null)
                .Select(x => new ReportItemViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                })
                : new List<ReportItemViewModel>();

            var workouts = (User.Identity != null && User.Identity.IsAuthenticated && User.IsInRole("User"))
                ? (await this._workoutService.GetWorkoutsByUsername(User.Identity!.Name!))
                .Select(x => x.Date)
                .ToHashSet()
                : new HashSet<DateTime>();

            CalendarViewModel model = new()
            {
                SelectedYear = calendarYear,
                Reports = reports,
                WorkoutDates = workouts
            };

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
