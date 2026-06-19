using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using replog.Data;
using replog.Extensions;

namespace replog
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
                options.UseAsyncSeeding(async (dbContext, _, ct) =>
                {
                    await MuscleGroupSeeder.SeedMuscleGroups(dbContext);
                    await ExercisesSeeder.SeedExercises(dbContext);

                    await dbContext.SaveChangesAsync();
                });

                options.UseSeeding((dbContext, _) =>
                {
                    MuscleGroupSeeder.SeedMuscleGroups(dbContext).GetAwaiter().GetResult();
                    ExercisesSeeder.SeedExercises(dbContext).GetAwaiter().GetResult();

                    dbContext.SaveChanges();
                });
            });

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole<Guid>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddControllersWithViews();

            builder.Services.AddServices();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                await db.Database.EnsureCreatedAsync();
            }

            await SeedUserRoles(app);
            await SeedAdmin(app);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();
            app.MapRazorPages()
               .WithStaticAssets();

            app.Run();
        }

        private static async Task SeedUserRoles(WebApplication? app)
        {
            if (app == null)
                throw new ArgumentNullException("Could not create roles");

            await using (AsyncServiceScope scope = app.Services.CreateAsyncScope())
            {
                RoleManager<IdentityRole<Guid>> roleManager = scope.ServiceProvider
                     .GetRequiredService<RoleManager<IdentityRole<Guid>>>();

                await RoleSeeder.SeedRoles(roleManager);
            }
        }

        private static async Task SeedAdmin(WebApplication? app)
        {
            if (app == null)
                throw new ArgumentNullException("Could not create roles");

            await using (AsyncServiceScope scope = app.Services.CreateAsyncScope())
            {
                UserManager<ApplicationUser> userManager = scope.ServiceProvider
                     .GetRequiredService<UserManager<ApplicationUser>>();

                ApplicationUser? user = await userManager.FindByNameAsync("admin@abv.bg");
                if (user != null)
                {
                    if (!await userManager.IsInRoleAsync(user, "Admin"))
                        await userManager.AddToRoleAsync(user, "Admin");

                    return;
                }

                ApplicationUser admin = new()
                {
                    Email = "admin@abv.bg",
                    UserName = "admin@abv.bg",
                };

                PasswordHasher<ApplicationUser> passwordHasher = new();
                admin.PasswordHash = passwordHasher.HashPassword(admin, "Admin1!");

                await userManager.CreateAsync(admin);
                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }
}
