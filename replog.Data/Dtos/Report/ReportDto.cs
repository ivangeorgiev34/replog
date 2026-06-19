namespace replog.Data.Dtos.Report
{
    public class ReportDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ApplicationUser CompletedBy { get; set; } = null!;
    }
}
