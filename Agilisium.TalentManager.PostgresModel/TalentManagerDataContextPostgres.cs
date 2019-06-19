using Agilisium.TalentManager.Model.Configuration;
using Agilisium.TalentManager.Model.Entities;
using System.Data.Entity;

namespace Agilisium.TalentManager.PostgresModel
{
    public class TalentManagerDataContext : DbContext
    {
        public bool IsPostgresDB { get; set; }

        public TalentManagerDataContext():base(nameOrConnectionString: "TalentDataContextPostgres") {
            IsPostgresDB = true;
            //Database.SetInitializer<TalentManagerDataContext>(null);
        }

        public DbSet<Practice> Practices { get; set; }

        public DbSet<SubPractice> SubPractices { get; set; }

        public DbSet<DropDownCategory> DropDownCategories { get; set; }

        public DbSet<DropDownSubCategory> DropDownSubCategories { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<ProjectAllocation> ProjectAllocations { get; set; }

        public DbSet<EmployeeIDTracker> EmployeeIDTrackers { get; set; }

        public DbSet<Vendor> Vendors { get; set; }

        public DbSet<Contractor> Contractors { get; set; }

        public DbSet<ServiceRequest> ServiceRequests { get; set; }

        public DbSet<SystemSetting> SystemSettings { get; set; }

        public DbSet<ProjectAccount> ProjectAccounts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contractor>().ToTable("Contractor", "TalentManager");
            modelBuilder.Entity<DropDownCategory>().ToTable("DropDownCategory", "TalentManager");
            modelBuilder.Entity<DropDownSubCategory>().ToTable("DropDownSubCategory", "TalentManager");
            modelBuilder.Entity<Employee>().ToTable("Employee", "TalentManager");
            modelBuilder.Entity<EmployeeIDTracker>().ToTable("EmployeeIDTracker", "TalentManager");
            modelBuilder.Entity<Practice>().ToTable("Practice", "TalentManager");
            modelBuilder.Entity<Project>().ToTable("Project", "TalentManager");
            modelBuilder.Entity<ProjectAccount>().ToTable("ProjectAccount", "TalentManager");
            modelBuilder.Entity<ProjectAllocation>().ToTable("ProjectAllocation", "TalentManager");
            modelBuilder.Entity<ServiceRequest>().ToTable("ServiceRequest", "TalentManager");
            modelBuilder.Entity<SubPractice>().ToTable("SubPractice", "TalentManager");
            modelBuilder.Entity<SystemSetting>().ToTable("SystemSettings", "TalentManager");
            modelBuilder.Entity<Vendor>().ToTable("Vendor", "TalentManager");
        }
    }
}
