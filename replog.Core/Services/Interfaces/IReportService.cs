using replog.Data.Dtos.Report;

namespace replog.Core.Services.Interfaces
{
    public interface IReportService
    {
        Task CreateAsync(CreateReportDto dto);

        IEnumerable<ReportDto> GetAll();

        Task<bool> Complete(Guid reportId, Guid userId);
    }
}
