using Agilisium.TalentManager.Model.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Agilisium.TalentManager.Model.Configuration
{
    public class DropDownCategoryConfiguration : EntityTypeConfiguration<DropDownCategory>
    {
        public DropDownCategoryConfiguration()
        {
            ToTable("DropDownCategory");
            HasKey(g => g.CategoryID);
            Property(g => g.CategoryID).IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(g => g.CategoryName).IsRequired().HasMaxLength(100);
            Property(g => g.Description).HasMaxLength(500);
            Property(g => g.ShortName).IsRequired().HasMaxLength(10);
        }
    }
}
