using Microsoft.EntityFrameworkCore;
using replog.Core.Services.Interfaces;
using replog.Data.Common;
using replog.Data.Dtos.Report;
using replog.Data.Models;

namespace replog.Core.Services
{
    public class ReportService : IReportService
    {
        private readonly IRepository _repository;

        public ReportService(IRepository repository)
        {
            this._repository = repository;
        }

        public async Task<bool> Complete(Guid reportId, Guid userId)
        {
            Report? report = await this._repository.All<Report>()
                .FirstOrDefaultAsync(x => x.Id == reportId);

            if (report == null)
                return false;

            report.CompletedById = userId;

            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task CreateAsync(CreateReportDto dto)
        {
            Report report = new()
            {
                Title = dto.Title,
                Description = dto.Description,
            };

            await this._repository.AddAsync(report);
            await this._repository.SaveChangesAsync();
        }

        public IEnumerable<ReportDto> GetAll()
        {
            return _repository.AllReadonly<Report>()
                .Select(x => new ReportDto()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    CompletedBy = x.CompletedBy,
                })
                .ToList()
                .AsEnumerable();
        }
    }
}
