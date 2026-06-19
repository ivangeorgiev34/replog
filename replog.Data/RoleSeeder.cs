using Microsoft.AspNetCore.Identity;

namespace replog.Data
{
    public class RoleSeeder
    {
        private readonly static HashSet<string> _roles = ["Admin", "User"];

        public static async Task SeedRoles(RoleManager<IdentityRole<Guid>> roleManager)
        {
            foreach (string role in _roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole<Guid>(role));
            }
        }
    }
}
