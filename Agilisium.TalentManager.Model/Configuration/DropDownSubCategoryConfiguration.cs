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
    public class DropDownSubCategoryConfiguration:EntityTypeConfiguration<DropDownSubCategory>
    {
        public DropDownSubCategoryConfiguration()
        {
            HasKey(g => g.SubCategoryID);

            Property(g => g.SubCategoryID).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(g => g.SubCategoryName).IsRequired().HasMaxLength(100);
            Property(g => g.ShortName).HasMaxLength(10).IsRequired();
            Property(g => g.Description).HasMaxLength(500);
            Property(g => g.CategoryID).IsRequired();

            ToTable("DropDownSubCategory");
        }
    }
}
