using Microsoft.EntityFrameworkCore;
using ReportService.Models;
using System.Reflection.Emit;

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


        public virtual DbSet<TRN_Deletion_Details> TRN_Deletion_Details { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TRN_Deletion_Details>(entity =>
            {
                entity
                    .ToTable("TRN_Deletion_Details")
                    .HasKey(x => x.ID);
            });
        }
    }
}
