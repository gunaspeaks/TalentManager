using Agilisium.TalentManager.Model.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Agilisium.TalentManager.Model.Configuration
{
    public class EmployeeIDTrackerEntityConfiguration : EntityTypeConfiguration<EmployeeIDTracker>
    {
        public EmployeeIDTrackerEntityConfiguration()
        {
            HasKey(p => p.TrackerID);

            Property(p => p.TrackerID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.EmploymentTypeID).IsRequired();
            Property(p => p.IDPrefix).IsRequired().HasMaxLength(5);
            Property(p => p.RunningID).IsRequired();

            ToTable("EmployeeIDTracker");
        }
    }
}
