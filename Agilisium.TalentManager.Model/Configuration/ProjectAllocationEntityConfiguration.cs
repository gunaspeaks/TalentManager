using Agilisium.TalentManager.Model.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agilisium.TalentManager.Model.Configuration
{
    public class ProjectAllocationEntityConfiguration:EntityTypeConfiguration<ProjectAllocation>
    {
        public ProjectAllocationEntityConfiguration()
        {
            HasKey(p => p.AllocationEntryID);

            Property(p => p.AllocationEndDate).IsRequired();
            Property(p => p.AllocationEntryID).HasColumnName("AllocationEntryID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.AllocationStartDate).IsRequired();
            Property(p => p.AllocationTypeID).HasColumnName("AllocationTypeID");
            Property(p => p.EmployeeID).IsRequired().HasColumnName("EmployeeID");
            Property(p => p.ProjectID).IsRequired().HasColumnName("ProjectID");
            Property(p => p.IsActive).IsRequired().HasColumnName("IsActive");
            Property(p => p.Remarks).IsOptional().HasMaxLength(250);

            ToTable("ProjectAllocation");
        }
    }
}
