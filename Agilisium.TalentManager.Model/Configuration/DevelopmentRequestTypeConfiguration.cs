using Agilisium.TalentManager.Model.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Agilisium.TalentManager.Model.Configuration
{
    public class DevelopmentRequestTypeConfiguration : EntityTypeConfiguration<DevelopmentRequest>
    {
        public DevelopmentRequestTypeConfiguration()
        {
            ToTable("DevelopmentRequest");
            HasKey(p => p.RequestID);
            Property(g => g.RequestID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(g => g.RequestedBy).IsOptional().HasMaxLength(100);
            Property(g => g.Remarks).IsOptional().HasMaxLength(500);
            Property(g => g.RequestTitle).IsOptional().HasMaxLength(250);
        }
    }
}
