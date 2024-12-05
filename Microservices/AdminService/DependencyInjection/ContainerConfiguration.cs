
using AdminService.Interfaces;
using AdminService.Repositories;

namespace SupplierService.DependencyInjection
{
    public static class ContainerConfiguration
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            //services.AddTransient<ITimebooking, TimebookingRepository>();
            services.AddTransient<IAdminService, AdminRepository>();

        }
    }
}
