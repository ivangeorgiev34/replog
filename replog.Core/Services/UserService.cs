
using Microsoft.EntityFrameworkCore;
using replog.Core.Services.Interfaces;
using replog.Data;
using replog.Data.Common;

namespace replog.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository _repository;

        public UserService(IRepository repository)
        {
            this._repository = repository;
        }

        public Task<ApplicationUser?> FindByName(string name)
        {
            return this._repository.AllReadonly<ApplicationUser>()
                .FirstOrDefaultAsync(x => x.UserName == name);
        }
    }
}
