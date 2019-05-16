using Agilisium.TalentManager.Dto;
using Agilisium.TalentManager.Repository.Repositories;
using Agilisium.TalentManager.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Agilisium.TalentManager.Service.Concreate
{
    public class VendorService : IVendorService
    {
        private readonly IVendorRepository repository;

        public VendorService(IVendorRepository repository)
        {
            this.repository = repository;
        }

        public void Create(VendorDto vendor)
        {
            repository.Add(vendor);
        }

        public void Delete(VendorDto vendor)
        {
            repository.Delete(vendor);
        }

        public bool Exists(int vendorID)
        {
            return repository.Exists(vendorID);
        }

        public bool Exists(string vendorName)
        {
            return repository.Exists(vendorName);
        }

        public VendorSearchResultDto SearchVendors(string searchText)
        {
            return repository.SearchVendors(searchText);
        }

        public List<VendorDto> GetAllVendors(int pageSize = -1, int pageNo = -1)
        {
            return repository.GetAll(pageSize, pageNo).ToList();
        }

        public VendorDto GetVendor(int id)
        {
            return repository.GetByID(id);
        }

        public bool IsDuplicateName(int vendorID, string vendorName)
        {
            return repository.IsDuplicateName(vendorID, vendorName);
        }

        public int TotalRecordsCount()
        {
            return repository.TotalRecordsCount();
        }

        public void Update(VendorDto vendor)
        {
            repository.Update(vendor);
        }

        public List<VendorSpecializedPartnerWto> GetVendorSpecialityPartnersList()
        {
            return repository.GetVendorSpecialityPartnersList().ToList();
        }
    }
}
