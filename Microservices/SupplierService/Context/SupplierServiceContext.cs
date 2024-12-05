using Microsoft.EntityFrameworkCore;

namespace SupplierService.Context
{
    public class SupplierServiceContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public SupplierServiceContext(DbContextOptions options) : base(options)
        {
            // TODO: This seems a bit too long. Why has this been set this high?
            //Database.SetCommandTimeout(180);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
        }
    }
}
