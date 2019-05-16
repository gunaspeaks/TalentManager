using Agilisium.TalentManager.Dto;
using Agilisium.TalentManager.Model.Entities;
using Agilisium.TalentManager.Repository.Abstract;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Agilisium.TalentManager.Repository.Repositories
{
    public class ProjectAccountRepository : RepositoryBase<ProjectAccount>, IProjectAccountRepository
    {
        public void Add(ProjectAccountDto entity)
        {
            ProjectAccount account = CreateBusinessEntity(entity, true);
            Entities.Add(account);
            DataContext.Entry(account).State = EntityState.Added;
            DataContext.SaveChanges();
        }

        public void Delete(ProjectAccountDto entity)
        {
            ProjectAccount buzEntity = Entities.FirstOrDefault(e => e.AccountID == entity.AccountID);
            buzEntity.IsDeleted = true;
            buzEntity.UpdateTimeStamp(entity.LoggedInUserName);
            Entities.Add(buzEntity);
            DataContext.Entry(buzEntity).State = EntityState.Modified;
            DataContext.SaveChanges();
        }

        public bool Exists(string itemName)
        {
            return Entities.Any(e => e.AccountName.ToLower() == itemName.ToLower() && e.IsDeleted == false);
        }

        public bool Exists(int id)
        {
            return Entities.Any(e => e.AccountID == id && e.IsDeleted == false);
        }

        public IEnumerable<ProjectAccountDto> GetAll(int pageSize = -1, int pageNo = -1)
        {
            IQueryable<ProjectAccountDto> accounts = from v in Entities
                                                     join e in DataContext.Employees on v.OffshoreManagerID equals e.EmployeeEntryID into ee
                                                     from ed in ee.DefaultIfEmpty()
                                                     join c in DataContext.DropDownSubCategories on v.CountryID equals c.SubCategoryID into ce
                                                     from cd in ce.DefaultIfEmpty()
                                                     where v.IsDeleted == false
                                                     orderby v.AccountName
                                                     select new ProjectAccountDto
                                                     {
                                                         AccountID = v.AccountID,
                                                         AccountName = v.AccountName,
                                                         CountryID = v.CountryID,
                                                         OffshoreManagerID = v.OffshoreManagerID,
                                                         OnshoreManager = v.OnshoreManager,
                                                         PartnerManager = v.PartnerManager,
                                                         ShortName = v.ShortName,
                                                         OffshoreManager = ed.LastName + ", " + ed.FirstName,
                                                         Country = cd.SubCategoryName
                                                     };

            if (pageSize <= 0 || pageNo < 1)
            {
                return accounts;
            }

            return accounts.Skip((pageNo - 1) * pageSize).Take(pageSize);
        }

        public ProjectAccountDto GetByID(int id)
        {
            return (from v in Entities
                    where v.AccountID == id
                    select new ProjectAccountDto
                    {
                        AccountID = v.AccountID,
                        AccountName = v.AccountName,
                        CountryID = v.CountryID,
                        OffshoreManagerID = v.OffshoreManagerID,
                        OnshoreManager = v.OnshoreManager,
                        PartnerManager = v.PartnerManager,
                        ShortName = v.ShortName
                    }).FirstOrDefault();
        }

        public bool IsDuplicateName(int accountID, string accountName)
        {
            return Entities.Any(e =>
                e.AccountID != accountID &&
                e.AccountName.ToLower() == accountName.ToLower() && e.IsDeleted == false);
        }

        public void Update(ProjectAccountDto entity)
        {
            ProjectAccount buzEntity = Entities.FirstOrDefault(e => e.AccountID == entity.AccountID);
            MigrateEntity(entity, buzEntity);
            buzEntity.UpdateTimeStamp(entity.LoggedInUserName);
            Entities.Add(buzEntity);
            DataContext.Entry(buzEntity).State = EntityState.Modified;
            DataContext.SaveChanges();
        }

        public override bool CanBeDeleted(int accountID)
        {
            return DataContext.Projects.Any(p => p.IsDeleted == false && p.ProjectAccountID == accountID);
        }

        public bool IsDuplicateShortName(string shortName)
        {
            return Entities.Any(e => e.ShortName.ToLower() == shortName.ToLower() && e.IsDeleted == false);
        }

        public bool IsDuplicateShortName(int accountID, string shortName)
        {
            return Entities.Any(e => e.AccountID != accountID && e.ShortName.ToLower() == shortName.ToLower() && e.IsDeleted == false);
        }

        private ProjectAccount CreateBusinessEntity(ProjectAccountDto accDto, bool isNewEntity = false)
        {
            ProjectAccount account = new ProjectAccount
            {
                AccountID = accDto.AccountID,
                AccountName = accDto.AccountName.Trim(),
                CountryID = accDto.CountryID,
                OffshoreManagerID = accDto.OffshoreManagerID,
                OnshoreManager = accDto.OnshoreManager,
                PartnerManager = accDto.PartnerManager,
                ShortName = accDto.ShortName.Trim()
            };

            account.UpdateTimeStamp(accDto.LoggedInUserName, true);
            return account;
        }

        private void MigrateEntity(ProjectAccountDto sourceEntity, ProjectAccount targetEntity)
        {
            targetEntity.AccountID = sourceEntity.AccountID;
            targetEntity.AccountName = sourceEntity.AccountName;
            targetEntity.CountryID = sourceEntity.CountryID;
            targetEntity.OffshoreManagerID = sourceEntity.OffshoreManagerID;
            targetEntity.OnshoreManager = sourceEntity.OnshoreManager;
            targetEntity.PartnerManager = sourceEntity.PartnerManager;
            targetEntity.ShortName = sourceEntity.ShortName;

            targetEntity.UpdateTimeStamp(sourceEntity.LoggedInUserName);
        }
    }

    public interface IProjectAccountRepository : IRepository<ProjectAccountDto>
    {
        bool IsDuplicateName(int accountID, string accountName);

        bool IsDuplicateShortName(string shortName);

        bool IsDuplicateShortName(int accountID, string shortName);
    }
}
