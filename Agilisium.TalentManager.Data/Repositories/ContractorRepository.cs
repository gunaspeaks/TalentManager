using Agilisium.TalentManager.Dto;
using Agilisium.TalentManager.Model.Entities;
using Agilisium.TalentManager.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Agilisium.TalentManager.Repository.Repositories
{
    public class ContractorRepository : RepositoryBase<Contractor>, IContractorRepository
    {
        private const string SPECIALITY_PARTNER_CATEGORY = "Specialized Partner";

        public void Add(ContractorDto entity)
        {
            Contractor employee = CreateBusinessEntity(entity, true);
            Entities.Add(employee);
            DataContext.Entry(employee).State = EntityState.Added;
            DataContext.SaveChanges();
        }

        public void Delete(ContractorDto entity)
        {
            Contractor buzEntity = Entities.FirstOrDefault(e => e.ContractorID == entity.ContractorID);
            buzEntity.IsDeleted = true;
            buzEntity.UpdateTimeStamp(entity.LoggedInUserName);
            Entities.Add(buzEntity);
            DataContext.Entry(buzEntity).State = EntityState.Modified;
            DataContext.SaveChanges();
        }

        public bool Exists(string itemName)
        {
            return Entities.Any(c => c.ContractorName.ToLower() == itemName.ToLower() && c.IsDeleted == false);
        }

        public bool Exists(string contractorName, int contractorID)
        {
            return Entities.Any(c => c.ContractorName.ToLower() == contractorName.ToLower()
            && c.ContractorID != contractorID && c.IsDeleted == false);
        }

        public bool Exists(int id)
        {
            return Entities.Any(c => c.ContractorID == id);
        }

        public IEnumerable<ContractorDto> GetAll(int pageSize = -1, int pageNo = -1)
        {
            IQueryable<ContractorDto> allContractors = from c in Entities
                                                       join v in DataContext.Vendors on c.VendorID equals v.VendorID into ve
                                                       from vd in ve.DefaultIfEmpty()
                                                       join p in DataContext.Projects on c.ProjectID equals p.ProjectID into pe
                                                       from pd in pe.DefaultIfEmpty()
                                                       join e in DataContext.Employees on pd.ProjectManagerID equals e.EmployeeEntryID into ee
                                                       from ed in ee.DefaultIfEmpty()
                                                       join d in DataContext.DropDownSubCategories on c.ContractPeriodID equals d.SubCategoryID into de
                                                       from dd in de.DefaultIfEmpty()
                                                       where c.IsDeleted == false
                                                       orderby c.ContractorName
                                                       select new ContractorDto
                                                       {
                                                           AgilisiumManagerID = c.AgilisiumManagerID,
                                                           AgilisiumManagerName = ed.LastName + ", " + ed.FirstName,
                                                           BillingRate = c.BillingRate,
                                                           ClientRate = c.ClientRate,
                                                           ContractorID = c.ContractorID,
                                                           ContractorName = c.ContractorName,
                                                           ContractPeriodID = c.ContractPeriodID,
                                                           ContractPeriod = dd.SubCategoryName,
                                                           EndDate = c.EndDate,
                                                           OnshoreRate = c.OnshoreRate,
                                                           ProjectID = c.ProjectID,
                                                           ProjectName = pd.ProjectName,
                                                           SkillSet = c.SkillSet,
                                                           StartDate = c.StartDate,
                                                           VendorID = c.VendorID,
                                                           VendorName = vd.VendorName
                                                       };

            if (pageSize <= 0 || pageNo < 1)
            {
                return allContractors;
            }

            return allContractors.Skip((pageNo - 1) * pageSize).Take(pageSize);
        }

        public ContractorDto GetByID(int id)
        {
            return (from c in Entities
                    where c.ContractorID == id
                    select new ContractorDto
                    {
                        AgilisiumManagerID = c.AgilisiumManagerID,
                        BillingRate = c.BillingRate,
                        ClientRate = c.ClientRate,
                        ContractorID = c.ContractorID,
                        ContractorName = c.ContractorName,
                        ContractPeriodID = c.ContractPeriodID,
                        EndDate = c.EndDate,
                        OnshoreRate = c.OnshoreRate,
                        ProjectID = c.ProjectID,
                        SkillSet = c.SkillSet,
                        StartDate = c.StartDate,
                        VendorID = c.VendorID
                    }).FirstOrDefault();
        }

        public void Update(ContractorDto entity)
        {
            Contractor buzEntity = Entities.FirstOrDefault(e => e.ContractorID == entity.ContractorID);
            MigrateEntity(entity, buzEntity);
            buzEntity.UpdateTimeStamp(entity.LoggedInUserName);
            Entities.Add(buzEntity);
            DataContext.Entry(buzEntity).State = EntityState.Modified;
            DataContext.SaveChanges();
        }

        public int GetActiveContractorsCount()
        {
            return Entities.Where(p => p.IsDeleted == false && p.EndDate >= DateTime.Now).Count();
        }

        private Contractor CreateBusinessEntity(ContractorDto contractorDto, bool isNewEntity = false)
        {
            Contractor contractor = new Contractor
            {
                AgilisiumManagerID = contractorDto.AgilisiumManagerID,
                BillingRate = contractorDto.BillingRate,
                ClientRate = contractorDto.ClientRate,
                ContractorID = contractorDto.ContractorID,
                ContractorName = contractorDto.ContractorName,
                ContractPeriodID = contractorDto.ContractPeriodID,
                EndDate = contractorDto.EndDate,
                OnshoreRate = contractorDto.OnshoreRate,
                ProjectID = contractorDto.ProjectID,
                SkillSet = contractorDto.SkillSet,
                StartDate = contractorDto.StartDate,
                VendorID = contractorDto.VendorID,
            };

            contractor.UpdateTimeStamp(contractorDto.LoggedInUserName, true);
            return contractor;
        }

        private void MigrateEntity(ContractorDto sourceEntity, Contractor targetEntity)
        {
            targetEntity.AgilisiumManagerID = sourceEntity.AgilisiumManagerID;
            targetEntity.BillingRate = sourceEntity.BillingRate;
            targetEntity.ClientRate = sourceEntity.ClientRate;
            targetEntity.ContractorID = sourceEntity.ContractorID;
            targetEntity.ContractorName = sourceEntity.ContractorName;
            targetEntity.ContractPeriodID = sourceEntity.ContractPeriodID;
            targetEntity.EndDate = sourceEntity.EndDate;
            targetEntity.OnshoreRate = sourceEntity.OnshoreRate;
            targetEntity.ProjectID = sourceEntity.ProjectID;
            targetEntity.SkillSet = sourceEntity.SkillSet;
            targetEntity.StartDate = sourceEntity.StartDate;
            targetEntity.VendorID = sourceEntity.VendorID;

            targetEntity.UpdateTimeStamp(sourceEntity.LoggedInUserName);
        }
    }

    public interface IContractorRepository : IRepository<ContractorDto>
    {
        bool Exists(string contractorName, int contractorID);

        int GetActiveContractorsCount();
    }
}
