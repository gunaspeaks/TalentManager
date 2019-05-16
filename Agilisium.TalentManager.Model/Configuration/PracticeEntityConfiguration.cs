using Agilisium.TalentManager.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agilisium.TalentManager.Model.Configuration
{
    class PracticeEntityConfiguration : EntityTypeConfiguration<Practice>
    {
        public PracticeEntityConfiguration()
        {
            ToTable("Practice");
            HasKey(g => g.PracticeID);
            Property(g => g.PracticeID).IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(g => g.PracticeName).IsRequired().HasMaxLength(100);
            Property(g => g.ShortName).IsRequired().HasMaxLength(10);
            Property(g => g.ManagerID).IsOptional();
        }
    }
}
