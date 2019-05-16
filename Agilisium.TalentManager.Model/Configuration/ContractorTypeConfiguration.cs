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
    public class ContractorTypeConfiguration : EntityTypeConfiguration<Contractor>
    {
        public ContractorTypeConfiguration()
        {
            HasKey(p => p.ContractorID);

            Property(p => p.ContractorID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.ContractorName).HasMaxLength(100);
            Property(p => p.SkillSet).HasMaxLength(150);

            ToTable("Contractor");
        }
    }
}
