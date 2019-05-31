using Agilisium.TalentManager.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agilisium.TalentManager.Model.Configuration
{
    public class SubPracticeEntityConfiguration : EntityTypeConfiguration<SubPractice>
    {
        public SubPracticeEntityConfiguration()
        {
            ToTable("SubPractice");
            HasKey(g => g.SubPracticeID);
            Property(g => g.SubPracticeID).IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(g => g.PracticeID).IsRequired();
            Property(g => g.SubPracticeName).IsRequired().HasMaxLength(100);
            Property(g => g.ShortName).IsRequired().HasMaxLength(10);
            Property(g => g.ManagerID).IsOptional();
        }
    }
}
