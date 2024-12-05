using AdminService.Models;
using Microsoft.EntityFrameworkCore;
using SupplierService.Models;

namespace AdminService.Context
{
    public partial class AdminServiceContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public AdminServiceContext(DbContextOptions options) : base(options)
        {
            // TODO: This seems a bit too long. Why has this been set this high?
            //Database.SetCommandTimeout(180);
        }

        public virtual DbSet<GetMenu> MST_Menu { get; set; }
        public virtual DbSet<RELRoleMenu> RELRoleMenu { get; set; }
        public virtual DbSet<GetRolesDetails> Roleresults { get; set; }
      
        public virtual DbSet<ValidateUserLogin> MST_Users { get; set; }
        public virtual DbSet<Usermaster> userMaster {  get; set; }

        public virtual DbSet<Getrole> Getroles { get; set; }
        public virtual DbSet<Adduser> Addusers { get; set; }
        public virtual DbSet<GetSupplierDetails> supplierDetails {  get; set; }
        public virtual DbSet<GetCountryDetails> CountryDetails { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GetRolesDetails>(entity => entity.HasNoKey());

            modelBuilder.Entity<ValidateUserLogin>(entity => entity.HasNoKey());

            modelBuilder.Entity<Usermaster>(entity => entity.HasNoKey());

            modelBuilder.Entity<Getrole>(entity => entity.HasNoKey());
            modelBuilder.Entity<Adduser>(entity => entity.HasNoKey());
            modelBuilder.Entity<GetSupplierDetails>(entity => entity.HasNoKey());

            modelBuilder.Entity<GetMenu>(entity =>
            {
                entity
                    .ToTable("MST_Menu")
                    .HasKey(x => x.MenuId);
            });


            modelBuilder.Entity<RELRoleMenu>(entity =>
            {
                entity
                    .ToTable("REL_RoleMenu")
                    .HasKey(x => x.Rel_RoleMenuId);
            });

            modelBuilder.Entity<GetCountryDetails>(entity =>
            {
                entity
                    .ToTable("MST_Country")
                    .HasKey(x => x.CountryId);
            });
        }
    }
}
