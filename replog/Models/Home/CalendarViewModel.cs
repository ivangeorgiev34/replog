namespace replog.Models.Home
{
    public class CalendarViewModel
    {
        public int SelectedYear { get; set; }
        public HashSet<DateTime> WorkoutDates { get; set; } = new HashSet<DateTime>();
        public IEnumerable<ReportItemViewModel> Reports { get; set; } = new List<ReportItemViewModel>();
    }
}
