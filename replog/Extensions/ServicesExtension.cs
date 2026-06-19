using replog.Core.Services;
using replog.Core.Services.Interfaces;
using replog.Data.Common;

namespace replog.Extensions
{
    public static class ServicesExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IWorkoutService, WorkoutService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMuscleGroupService, MuscleGroupService>();
            services.AddScoped<IExerciseService, ExerciseService>();
        }
    }
}
