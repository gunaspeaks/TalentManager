using Agilisium.TalentManager.Model.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Agilisium.TalentManager.Model.Configuration
{
    public  class EmployeeEntityConfiguration : EntityTypeConfiguration<Employee>
    {
        public EmployeeEntityConfiguration()
        {
            // Key
            HasKey(e => e.EmployeeEntryID);

            Property(e => e.EmployeeEntryID).HasColumnName("EmployeeEntryID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(e => e.DateOfJoin).HasColumnName("DateOfJoin").IsRequired();
            Property(e => e.EmailID).HasColumnName("EmailID").HasMaxLength(100).IsOptional();
            Property(e => e.EmployeeID).HasColumnName("EmployeeID").IsRequired().HasMaxLength(10);
            Property(e => e.FirstName).HasColumnName("FirstName").HasMaxLength(100).IsRequired();
            Property(e => e.LastName).HasColumnName("LastName").HasMaxLength(100).IsRequired();
            Property(e => e.LastWorkingDay).IsOptional();
            Property(e => e.PrimarySkills).IsRequired().HasMaxLength(100);
            Property(e => e.SecondarySkills).IsOptional().HasMaxLength(100);
            Property(e => e.ReportingManagerID).IsOptional();
            Property(e => e.UtilizationTypeID).IsOptional();
            Property(e => e.SubPracticeID).IsRequired();
            Property(e => e.EmploymentTypeID).IsRequired();

            ToTable("Employee");
        }
    }
}
