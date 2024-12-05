using SupplierService.Interfaces;
using SupplierService.Repositories;

namespace SupplierService.DependencyInjection
{
    public static class ContainerConfiguration
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            //services.AddTransient<ITimebooking, TimebookingRepository>();
            services.AddTransient<ISupplierService, SupplierRepository>();

        }
    }
}
