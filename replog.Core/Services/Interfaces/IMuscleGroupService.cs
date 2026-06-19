using replog.Data.Dtos.Exercise;
using replog.Data.Models;

namespace replog.Core.Services.Interfaces
{
    public interface IMuscleGroupService
    {
        IEnumerable<MuscleGroupDto> GetAll();

        IEnumerable<MuscleGroup> GetByIds(IEnumerable<Guid> ids);
    }
}
