using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Agilisium.TalentManager.Web.Models
{
    public class VendorSpecializedPartnerModel
    {
        [DisplayName("Specialized Partner")]
        public string SpecializedPartner { get; set; }

        [DisplayName("Vendors Count")]
        public int VendorsCount { get; set; }
    }
}