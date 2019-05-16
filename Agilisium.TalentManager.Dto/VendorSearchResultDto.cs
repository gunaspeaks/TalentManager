using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agilisium.TalentManager.Dto
{
    public class VendorSearchResultDto
    {
        public VendorSearchResultDto()
        {
            SearchResults = new List<VendorSearchDto>();
        }

        public List<VendorSearchDto> SearchResults { get; set; }

        public string EmailMessage { get; set; }
    }
}
