using Agilisium.TalentManager.Dto;
using Agilisium.TalentManager.Model.Entities;
using Agilisium.TalentManager.Repository.Abstract;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Agilisium.TalentManager.Repository.Repositories
{
    public class SubPracticeRepository : RepositoryBase<SubPractice>, ISubPracticeRepository
    {
        public void Add(SubPracticeDto entity)
        {
            SubPractice subPractice = CreateBusinessEntity(entity, true);
            Entities.Add(subPractice);
            DataContext.Entry(subPractice).State = EntityState.Added;
            DataContext.SaveChanges();
        }

        public void Delete(SubPracticeDto entity)
        {
            SubPractice buzEntity = Entities.FirstOrDefault(e => e.SubPracticeID == entity.SubPracticeID);
            buzEntity.IsDeleted = true;
            Entities.Add(buzEntity);
            buzEntity.UpdateTimeStamp(entity.LoggedInUserName);
            DataContext.Entry(buzEntity).State = EntityState.Modified;
            DataContext.SaveChanges();
        }

        public bool Exists(string subPracticeName, int id, int practiceID)
        {
            return Entities.Any(c => c.SubPracticeName.ToLower() == subPracticeName.ToLower() &&
            c.SubPracticeID != id && c.PracticeID == practiceID && c.IsDeleted == false);
        }

        public bool Exists(string itemName)
        {
            return Entities.Any(c => c.SubPracticeName.ToLower() == itemName.ToLower() && c.IsDeleted == false);
        }

        public bool Exists(string itemName, int practiceID)
        {
            return Entities.Any(c => c.SubPracticeName.ToLower() == itemName.ToLower() && c.PracticeID == practiceID && c.IsDeleted == false);
        }

        public bool Exists(int id)
        {
            return Entities.Any(c => c.SubPracticeID == id && c.IsDeleted == false);
        }

        public IEnumerable<SubPracticeDto> GetAll(int pageSize = -1, int pageNo = -1)
        {
            IQueryable<SubPracticeDto> practices = from c in Entities
                                                   join p in DataContext.Practices on c.PracticeID equals p.PracticeID
                                                   join e in DataContext.Employees on c.ManagerID equals e.EmployeeEntryID into ee
                                                   from ed in ee.DefaultIfEmpty()
                                                   orderby c.SubPracticeName
                                                   where c.IsDeleted == false
                                                   select new SubPracticeDto
                                                   {
                                                       PracticeID = c.PracticeID,
                                                       PracticeName = p.PracticeName,
                                                       SubPracticeID = c.SubPracticeID,
                                                       SubPracticeName = c.SubPracticeName,
                                                       ShortName = c.ShortName,
                                                       ManagerID = c.ManagerID,
                                                       ManagerName = string.IsNullOrEmpty(ed.FirstName) ? "" : ed.LastName + ", " + ed.FirstName,
                                                       HeadCount = DataContext.Employees.Count(h => h.SubPracticeID == c.SubPracticeID)
                                                   };

            if (pageSize <= 0 || pageNo < 1)
            {
                return practices;
            }

            return practices.Skip((pageNo - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<SubPracticeDto> GetAllByPracticeID(int practiceID, int pageSize = -1, int pageNo = -1)
        {
            IQueryable<SubPracticeDto> practices = from c in Entities
                                                   join e in DataContext.Employees on c.ManagerID equals e.EmployeeEntryID into ee
                                                   from ed in ee.DefaultIfEmpty()
                                                   join p in DataContext.Practices on c.PracticeID equals p.PracticeID into pr
                                                   from prd in pr.DefaultIfEmpty()
                                                   where c.PracticeID == practiceID && c.IsDeleted == false
                                                   orderby c.SubPracticeName
                                                   select new SubPracticeDto
                                                   {
                                                       PracticeID = c.PracticeID,
                                                       PracticeName = prd.PracticeName,
                                                       SubPracticeID = c.SubPracticeID,
                                                       SubPracticeName = c.SubPracticeName,
                                                       ShortName = c.ShortName,
                                                       ManagerID = c.ManagerID,
                                                       ManagerName = string.IsNullOrEmpty(ed.FirstName) ? "" : ed.LastName + ", " + ed.FirstName,
                                                       HeadCount = DataContext.Employees.Count(h => h.SubPracticeID == c.SubPracticeID)
                                                   };

            if (pageSize <= 0 || pageNo < 1)
            {
                return practices;
            }

            return practices.Skip((pageNo - 1) * pageSize).Take(pageSize);

        }

        public SubPracticeDto GetByID(int id)
        {
            return (from c in Entities
                    join e in DataContext.Employees on c.ManagerID equals e.EmployeeEntryID into ee
                    from ed in ee.DefaultIfEmpty()
                    where c.SubPracticeID == id && c.IsDeleted == false
                    select new SubPracticeDto
                    {
                        PracticeID = c.PracticeID,
                        SubPracticeID = c.SubPracticeID,
                        SubPracticeName = c.SubPracticeName,
                        ShortName = c.ShortName,
                        ManagerID = c.ManagerID,
                        ManagerName = string.IsNullOrEmpty(ed.FirstName) ? "" : ed.LastName + ", " + ed.FirstName
                    }).FirstOrDefault();
        }

        public SubPracticeDto GetByName(string name, int practiceID)
        {
            return (from c in Entities
                    where c.PracticeID == practiceID && c.SubPracticeName.ToLower() == name.ToLower() && c.IsDeleted == false
                    select new SubPracticeDto
                    {
                        PracticeID = c.PracticeID,
                        SubPracticeID = c.SubPracticeID,
                        SubPracticeName = c.SubPracticeName,
                        ShortName = c.ShortName,
                    }).FirstOrDefault();
        }

        public void Update(SubPracticeDto entity)
        {
            SubPractice buzEntity = Entities.FirstOrDefault(s => s.SubPracticeID == entity.SubPracticeID);
            MigrateEntity(entity, buzEntity);
            buzEntity.UpdateTimeStamp(entity.LoggedInUserName);
            Entities.Add(buzEntity);
            DataContext.Entry(buzEntity).State = EntityState.Modified;
            DataContext.SaveChanges();
        }

        public override bool CanBeDeleted(int id)
        {
            // are there any depending employees
            if (DataContext.Employees.Any(c => c.IsDeleted == false && c.SubPracticeID == id)
                || DataContext.Projects.Any(c => c.IsDeleted == false && c.SubPracticeID == id))
            {
                return false;
            }

            return true;
        }

        public string GetManagerName(int subPracticeID)
        {
            return (from p in Entities
                    join e in DataContext.Employees on p.ManagerID equals e.EmployeeEntryID into ee
                    from ed in ee.DefaultIfEmpty()
                    where p.SubPracticeID == subPracticeID
                    select string.IsNullOrEmpty(ed.FirstName) ? "" : ed.LastName + ", " + ed.FirstName).FirstOrDefault();
        }

        public int TotalRecordsCountByPracticeID(int practiceID)
        {
            return Entities.Count(p => p.PracticeID == practiceID && p.IsDeleted == false);
        }

        private SubPractice CreateBusinessEntity(SubPracticeDto subPracticeDto, bool isNewEntity = false)
        {
            SubPractice subPractice = new SubPractice
            {
                PracticeID = subPracticeDto.PracticeID,
                SubPracticeName = subPracticeDto.SubPracticeName,
                ShortName = subPracticeDto.ShortName,
                SubPracticeID = subPracticeDto.SubPracticeID,
                ManagerID = subPracticeDto.ManagerID
            };

            subPractice.UpdateTimeStamp(subPracticeDto.LoggedInUserName, true);
            return subPractice;
        }

        private void MigrateEntity(SubPracticeDto sourceEntity, SubPractice targetEntity)
        {
            targetEntity.PracticeID = sourceEntity.PracticeID;
            targetEntity.SubPracticeName = sourceEntity.SubPracticeName;
            targetEntity.ShortName = sourceEntity.ShortName;
            targetEntity.SubPracticeID = sourceEntity.SubPracticeID;
            targetEntity.ManagerID = sourceEntity.ManagerID;
            targetEntity.UpdateTimeStamp(sourceEntity.LoggedInUserName);
        }
    }

    public interface ISubPracticeRepository : IRepository<SubPracticeDto>
    {
        IEnumerable<SubPracticeDto> GetAllByPracticeID(int practiceID, int pageSize = -1, int pageNo = -1);

        bool Exists(string subPracticeName, int id, int practiceID);

        bool Exists(string itemName, int practiceID);

        int TotalRecordsCountByPracticeID(int practiceID);

        string GetManagerName(int subPracticeID);
    }
}
