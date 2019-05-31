using Agilisium.TalentManager.Model.Configuration;
using Agilisium.TalentManager.Model.Entities;
using System.Data.Entity;

namespace Agilisium.TalentManager.Model
{
    public class TalentManagerDataContext : DbContext
    {
        public TalentManagerDataContext() : base("TalentDataContext")
        {
            //// Uu-comment the below line to re-create the database freshly
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

        public DbSet<DevelopmentRequest> DevelopmentRequests { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PracticeEntityConfiguration());
            modelBuilder.Configurations.Add(new SubPracticeEntityConfiguration());
            modelBuilder.Configurations.Add(new DropDownCategoryConfiguration());
            modelBuilder.Configurations.Add(new DropDownSubCategoryConfiguration());
            modelBuilder.Configurations.Add(new EmployeeEntityConfiguration());
            modelBuilder.Configurations.Add(new ProjectEntityConfiguration());
            modelBuilder.Configurations.Add(new ProjectAllocationEntityConfiguration());
            modelBuilder.Configurations.Add(new EmployeeIDTrackerEntityConfiguration());
            modelBuilder.Configurations.Add(new VendorEntityConfiguration());
            modelBuilder.Configurations.Add(new ContractorTypeConfiguration());
            modelBuilder.Configurations.Add(new ServiceRequestEntityConfiguration());
            modelBuilder.Configurations.Add(new SystemSettingTypeConfiguration());
            modelBuilder.Configurations.Add(new ProjectAccountEntityConfiguration());
            modelBuilder.Configurations.Add(new DevelopmentRequestTypeConfiguration());
        }
    }
}
