using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agilisium.TalentManager.Dto
{
    public class VendorSearchDto
    {
        public VendorSearchDto()
        {
            MatchingVendors = new List<VendorDto>();
        }

        public string SearchedFor { get; set; }

        public List<VendorDto> MatchingVendors { get; set; }
    }
}
