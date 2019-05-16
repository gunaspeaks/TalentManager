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
    public class ProjectAccountEntityConfiguration : EntityTypeConfiguration<ProjectAccount>
    {
        public ProjectAccountEntityConfiguration()
        {
            HasKey(p => p.AccountID);

            Property(p => p.AccountID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.AccountName).HasMaxLength(100).IsRequired();
            Property(p => p.ShortName).HasMaxLength(5);
            Property(p => p.OffshoreManagerID).IsRequired();
            Property(p => p.OnshoreManager).HasMaxLength(100).IsRequired();
            Property(p => p.PartnerManager).HasMaxLength(100);
            Property(p => p.CountryID).IsRequired();

            ToTable("ProjectAccount");

        }
    }
}
