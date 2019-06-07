using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Agilisium.TalentManager.Web.Models
{
    public class ResourceCountModel
    {
        [DisplayName("Total Strength")]
        public int TotalCount { get; set; }

        [DisplayName("Delivery")]
        public int DeliveryCount { get; set; }

        [DisplayName("Business Operations")]
        public int BoCount { get; set; }

        [DisplayName("Business Development")]
        public int BdCount { get; set; }
    }
}