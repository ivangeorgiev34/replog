using replog.Data;

namespace replog.Core.Services.Interfaces
{
    public interface IUserService
    {
        Task<ApplicationUser?> FindByName(string name);
    }
}
