using replog.Core.Services.Interfaces;
using replog.Data.Common;
using replog.Data.Dtos.Exercise;
using replog.Data.Models;

namespace replog.Core.Services
{
    public class MuscleGroupService : IMuscleGroupService
    {
        private readonly IRepository _repository;

        public MuscleGroupService(IRepository repository)
        {
            this._repository = repository;
        }
        public IEnumerable<MuscleGroupDto> GetAll()
        {
            return this._repository.AllReadonly<MuscleGroup>()
                .Select(x => new MuscleGroupDto
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .AsEnumerable()
                .ToList();
        }

        public IEnumerable<MuscleGroup> GetByIds(IEnumerable<Guid> ids)
        {
            return this._repository.All<MuscleGroup>()
                .Where(x => ids.Contains(x.Id))
                .ToList();
        }
    }
}
