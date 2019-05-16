using Agilisium.TalentManager.Model.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Agilisium.TalentManager.Model.Configuration
{
    public class VendorEntityConfiguration : EntityTypeConfiguration<Vendor>
    {
        public VendorEntityConfiguration()
        {
            HasKey(p => p.VendorID);

            Property(p => p.VendorID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.VendorName).HasMaxLength(100).IsRequired();
            Property(p=>p.Address).HasMaxLength(250);
            Property(p => p.CEO).HasMaxLength(100).IsRequired();
            Property(p => p.Location).HasMaxLength(50);
            Property(p => p.PoC1).HasMaxLength(100).IsRequired();
            Property(p => p.PoC2).HasMaxLength(100);
            Property(p => p.PoCEmail1).HasMaxLength(150);
            Property(p => p.PoCEmail2).HasMaxLength(150);
            Property(p => p.PoCPhone1).HasMaxLength(50).IsRequired();
            Property(p => p.PoCPhone2).HasMaxLength(50);
            Property(p => p.PrimarySkills).HasMaxLength(150).IsRequired();
            Property(p => p.SecondarySkills).HasMaxLength(150);

            ToTable("Vendor");
        }
    }
}
