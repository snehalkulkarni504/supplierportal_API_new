using ReportService.Interfaces;
using ReportService.Repositories;

namespace ReportService.DependencyInjection
{
    public static class ContainerConfiguration
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            //services.AddTransient<ITimebooking, TimebookingRepository>();
            services.AddTransient<IReportService, ReportRepositories>();

        }
    }
}
