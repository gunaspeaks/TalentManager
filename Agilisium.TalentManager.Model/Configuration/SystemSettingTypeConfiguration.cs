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
    public class SystemSettingTypeConfiguration : EntityTypeConfiguration<SystemSetting>
    {
        public SystemSettingTypeConfiguration()
        {
            HasKey(p => p.SettingEntryID);

            Property(p => p.SettingEntryID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.SettingName).HasMaxLength(100).IsRequired();
            Property(p => p.SettingValue).HasMaxLength(250).IsRequired();

            ToTable("SystemSettings");
        }
    }
}
