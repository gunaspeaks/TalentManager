using Agilisium.TalentManager.Dto;
using System.Collections.Generic;

namespace Agilisium.TalentManager.Service.Abstract
{
    public interface IVendorService
    {
        VendorSearchResultDto SearchVendors(string searchText);

        VendorDto GetVendor(int id);

        bool Exists(int vendorID);

        bool Exists(string vendorName);

        void Create(VendorDto Vendor);

        void Update(VendorDto Vendor);

        void Delete(VendorDto Vendor);

        bool IsDuplicateName(int vendorID, string vendorName);

        int TotalRecordsCount();

        List<VendorDto> GetAllVendors(int pageSize = -1, int pageNo = -1);

        List<VendorSpecializedPartnerWto> GetVendorSpecialityPartnersList();
    }
}
