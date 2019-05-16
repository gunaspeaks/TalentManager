using Agilisium.TalentManager.Model.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Agilisium.TalentManager.Model.Configuration
{
    public class ProjectEntityConfiguration : EntityTypeConfiguration<Project>
    {
        public ProjectEntityConfiguration()
        {
            HasKey(p => p.ProjectID);

            Property(e => e.EndDate).IsRequired();
            Property(e => e.ProjectCode).HasMaxLength(25).IsRequired();
            Property(e => e.ProjectID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(e => e.ProjectName).IsRequired().HasMaxLength(100);
            Property(e => e.ProjectTypeID).IsRequired();
            Property(e => e.Remarks).IsOptional().HasMaxLength(500);
            Property(e => e.StartDate).IsRequired();
            Property(e => e.DeliveryManagerID).IsOptional();
            Property(e => e.ProjectManagerID).IsRequired();
            Property(e => e.SubPracticeID).IsOptional();
            Property(e => e.PracticeID).IsRequired();
            Property(e => e.IsReserved).IsRequired();

            ToTable("Project");
        }
    }
}
