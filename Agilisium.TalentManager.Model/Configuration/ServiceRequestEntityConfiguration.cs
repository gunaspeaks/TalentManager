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
    public class ServiceRequestEntityConfiguration : EntityTypeConfiguration<ServiceRequest>
    {
        public ServiceRequestEntityConfiguration()
        {
            HasKey(p => p.ServiceRequestID);

            Property(p => p.ServiceRequestID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.RequestedSkill).HasMaxLength(50);
            Property(p => p.EmailMessage).HasMaxLength(500);

            ToTable("ServiceRequest");
        }
    }
}
