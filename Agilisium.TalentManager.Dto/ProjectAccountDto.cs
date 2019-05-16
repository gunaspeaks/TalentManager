using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agilisium.TalentManager.Dto
{
    public class ProjectAccountDto : DtoBase
    {
        public int AccountID { get; set; }

        public string AccountName { get; set; }

        public string OnshoreManager { get; set; }

        public int OffshoreManagerID { get; set; }

        public string OffshoreManager { get; set; }

        public string PartnerManager { get; set; }

        public int CountryID { get; set; }

        public string Country { get; set; }

        public string ShortName { get; set; }
    }
}
