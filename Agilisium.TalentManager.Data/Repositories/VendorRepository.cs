using Agilisium.TalentManager.Dto;
using Agilisium.TalentManager.Model.Entities;
using Agilisium.TalentManager.Repository.Abstract;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Agilisium.TalentManager.Repository.Repositories
{
    public class VendorRepository : RepositoryBase<Vendor>, IVendorRepository
    {
        public bool Exists(string itemName)
        {
            return Entities.Any(e => e.VendorName.ToLower() == itemName.ToLower() && e.IsDeleted == false);
        }

        public bool Exists(int id)
        {
            return Entities.Any(e => e.VendorID == id && e.IsDeleted == false);
        }

        public VendorDto GetByID(int id)
        {
            return (from v in Entities
                    where v.VendorID == id
                    select new VendorDto
                    {
                        Address = v.Address,
                        CEO = v.CEO,
                        Location = v.Location,
                        PoC1 = v.PoC1,
                        PoC2 = v.PoC2,
                        PoCEmail1 = v.PoCEmail1,
                        PoCEmail2 = v.PoCEmail2,
                        PoCPhone1 = v.PoCPhone1,
                        PoCPhone2 = v.PoCPhone2,
                        PrimarySkills = v.PrimarySkills,
                        SecondarySkills = v.SecondarySkills,
                        SpecializedPartnerID = v.SpecializedPartnerID,
                        VendorID = v.VendorID,
                        VendorName = v.VendorName,
                    }).FirstOrDefault();
        }

        public IEnumerable<VendorDto> GetAll(int pageSize = -1, int pageNo = -1)
        {
            IEnumerable<VendorDto> vendors = GetAllActiveVendors("");

            if (pageSize <= 0 || pageNo < 1)
            {
                return vendors;
            }

            return vendors.Skip((pageNo - 1) * pageSize).Take(pageSize);
        }

        public VendorSearchResultDto SearchVendors(string searchText)
        {
            VendorSearchResultDto searchResult = new VendorSearchResultDto();

            string[] filters = searchText.Split(',');
            foreach (string filter in filters)
            {
                searchResult.SearchResults.Add(new VendorSearchDto
                {
                    SearchedFor = filter,
                    MatchingVendors = (from v in Entities
                                       where v.IsDeleted == false &&
                                       (v.PrimarySkills.ToLower().Contains(filter.ToLower()) || v.SecondarySkills.Contains(filter.ToLower()))
                                       select new VendorDto
                                       {
                                           IsSelected = true,
                                           VendorID = v.VendorID,
                                           VendorName = v.VendorName,
                                           PrimarySkills = v.PrimarySkills,
                                           SecondarySkills = v.SecondarySkills,
                                           PoCEmail1 = v.PoCEmail1,
                                           PoCEmail2 = v.PoCEmail2
                                       }).ToList()
                });
            }

            return searchResult;
        }

        public bool IsDuplicateName(int vendorID, string vendorName)
        {
            return Entities.Any(e =>
                e.VendorID != vendorID &&
                e.VendorName.ToLower() == vendorName.ToLower() && e.IsDeleted == false);
        }

        public void Add(VendorDto entity)
        {
            Vendor vendor = CreateBusinessEntity(entity, true);
            Entities.Add(vendor);
            DataContext.Entry(vendor).State = EntityState.Added;
            DataContext.SaveChanges();
        }

        public void Update(VendorDto entity)
        {
            Vendor buzEntity = Entities.FirstOrDefault(e => e.VendorID == entity.VendorID);
            MigrateEntity(entity, buzEntity);
            buzEntity.UpdateTimeStamp(entity.LoggedInUserName);
            Entities.Add(buzEntity);
            DataContext.Entry(buzEntity).State = EntityState.Modified;
            DataContext.SaveChanges();
        }

        public void Delete(VendorDto entity)
        {
            Vendor buzEntity = Entities.FirstOrDefault(e => e.VendorID == entity.VendorID);
            buzEntity.IsDeleted = true;
            buzEntity.UpdateTimeStamp(entity.LoggedInUserName);
            Entities.Add(buzEntity);
            DataContext.Entry(buzEntity).State = EntityState.Modified;
            DataContext.SaveChanges();
        }

        public int TotalRecordsCount(string searchText)
        {
            if (string.IsNullOrEmpty(searchText))
            {
                return TotalRecordsCount();
            }
            else
            {
                return Entities.Count(e => e.IsDeleted == false
                && (e.PrimarySkills.ToLower().StartsWith(searchText.ToLower())
                || e.SecondarySkills.ToLower().StartsWith(searchText.ToLower())));
            }
        }

        public IEnumerable<VendorSpecializedPartnerWto> GetVendorSpecialityPartnersList()
        {
            DbCommand cmd = DataContext.Database.Connection.CreateCommand();
            cmd.CommandText = "dbo.GetVendorsCountBasedOnSpcializedPartner";
            cmd.CommandType = CommandType.StoredProcedure;
            DataContext.Database.Connection.Open();
            DbDataReader reader = cmd.ExecuteReader();
            ObjectResult<VendorSpecializedPartnerWto> items = ((IObjectContextAdapter)DataContext).ObjectContext.Translate<VendorSpecializedPartnerWto>(reader);
            List<VendorSpecializedPartnerWto> listItems = items.ToList();
            DataContext.Database.Connection.Close();

            return listItems;
        }

        private IEnumerable<VendorDto> GetAllActiveVendors(string searchText)
        {
            IQueryable<VendorDto> vendors = from v in Entities
                                            where v.IsDeleted == false
                                            orderby v.VendorID
                                            select new VendorDto
                                            {
                                                Address = v.Address,
                                                CEO = v.CEO,
                                                Location = v.Location,
                                                PoC1 = v.PoC1,
                                                PoC2 = v.PoC2,
                                                PoCEmail1 = v.PoCEmail1,
                                                PoCEmail2 = v.PoCEmail2,
                                                PoCPhone1 = v.PoCPhone1,
                                                PoCPhone2 = v.PoCPhone2,
                                                PrimarySkills = v.PrimarySkills,
                                                SpecializedPartnerID = v.SpecializedPartnerID,
                                                VendorID = v.VendorID,
                                                VendorName = v.VendorName
                                            };

            if (string.IsNullOrEmpty(searchText) == false)
            {
                vendors = vendors.Where(e => e.PrimarySkills.ToLower().StartsWith(searchText) || e.SecondarySkills.ToLower().StartsWith(searchText));
            }

            return vendors;
        }

        private Vendor CreateBusinessEntity(VendorDto vendorDto, bool isNewEntity = false)
        {
            Vendor vendor = new Vendor
            {
                Address = vendorDto.Address,
                CEO = vendorDto.CEO,
                Location = vendorDto.Location,
                PoC1 = vendorDto.PoC1,
                PoC2 = vendorDto.PoC2,
                PoCEmail1 = vendorDto.PoCEmail1,
                PoCEmail2 = vendorDto.PoCEmail2,
                PoCPhone1 = vendorDto.PoCPhone1,
                PoCPhone2 = vendorDto.PoCPhone2,
                PrimarySkills = vendorDto.PrimarySkills,
                SecondarySkills = vendorDto.SecondarySkills,
                SpecializedPartnerID = vendorDto.SpecializedPartnerID,
                VendorID = vendorDto.VendorID,
                VendorName = vendorDto.VendorName
            };

            vendor.UpdateTimeStamp(vendorDto.LoggedInUserName, true);
            return vendor;
        }

        private void MigrateEntity(VendorDto sourceEntity, Vendor targetEntity)
        {
            targetEntity.Address = sourceEntity.Address;
            targetEntity.CEO = sourceEntity.CEO;
            targetEntity.Location = sourceEntity.Location;
            targetEntity.PoC1 = sourceEntity.PoC1;
            targetEntity.PoC2 = sourceEntity.PoC2;
            targetEntity.PoCEmail1 = sourceEntity.PoCEmail1;
            targetEntity.PoCEmail2 = sourceEntity.PoCEmail2;
            targetEntity.PoCPhone1 = sourceEntity.PoCPhone1;
            targetEntity.PoCPhone2 = sourceEntity.PoCPhone2;
            targetEntity.PrimarySkills = sourceEntity.PrimarySkills;
            targetEntity.SpecializedPartnerID = sourceEntity.SpecializedPartnerID;
            targetEntity.SecondarySkills = sourceEntity.SecondarySkills;
            targetEntity.VendorID = sourceEntity.VendorID;
            targetEntity.VendorName = sourceEntity.VendorName;

            targetEntity.UpdateTimeStamp(sourceEntity.LoggedInUserName);
        }
    }

    public interface IVendorRepository : IRepository<VendorDto>
    {
        bool IsDuplicateName(int vendorID, string vendorName);

        int TotalRecordsCount(string searchText);

        VendorSearchResultDto SearchVendors(string searchText);

        IEnumerable<VendorSpecializedPartnerWto> GetVendorSpecialityPartnersList();
    }
}
