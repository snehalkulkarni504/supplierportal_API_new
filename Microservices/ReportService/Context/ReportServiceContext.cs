using Microsoft.EntityFrameworkCore;

namespace ReportService.Context
{
    public class ReportServiceContext: DbContext
    {
        protected readonly IConfiguration Configuration;

        public ReportServiceContext(DbContextOptions options) : base(options)
        {
            // TODO: This seems a bit too long. Why has this been set this high?
            //Database.SetCommandTimeout(180);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
        }
    }
}
