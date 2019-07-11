using Agilisium.TalentManager.Dto;
using Agilisium.TalentManager.Model.Entities;
using Agilisium.TalentManager.Repository.Abstract;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Agilisium.TalentManager.Repository.Repositories
{
    public class PracticeRepository : RepositoryBase<Practice>, IPracticeRepository
    {
        private const string DEFAULT_PRACTICE_NAME = "Not Mapped";

        public void Add(PracticeDto entity)
        {
            Practice practice = CreateBusinessEntity(entity, true);
            Entities.Add(practice);
            DataContext.Entry(practice).State = EntityState.Added;
            DataContext.SaveChanges();
        }

        public void Delete(PracticeDto entity)
        {
            Practice buzEntity = Entities.FirstOrDefault(e => e.PracticeID == entity.PracticeID);
            buzEntity.IsDeleted = true;
            buzEntity.UpdateTimeStamp(entity.LoggedInUserName);
            Entities.Add(buzEntity);
            DataContext.Entry(buzEntity).State = EntityState.Modified;
            DataContext.SaveChanges();
        }

        public bool Exists(string practiceName, int id)
        {
            return Entities.Any(c => c.PracticeName.ToLower() == practiceName.ToLower() &&
            c.PracticeID != id && c.IsDeleted == false);
        }

        public bool Exists(string itemName)
        {
            return Entities.Any(c => c.PracticeName.ToLower() == itemName.ToLower() && c.IsDeleted == false);
        }

        public bool Exists(int id)
        {
            return Entities.Any(c => c.PracticeID == id && c.IsDeleted == false);
        }

        public IEnumerable<PracticeDto> GetAll(int pageSize = -1, int pageNo = -1)
        {
            IQueryable<PracticeDto> practices = from p in Entities
                                                join e in DataContext.Employees on p.ManagerID equals e.EmployeeEntryID into ee
                                                from ed in ee.DefaultIfEmpty()
                                                join s in DataContext.DropDownSubCategories on p.BusinessUnitID equals s.SubCategoryID into se
                                                from sd in se.DefaultIfEmpty()
                                                orderby sd.SubCategoryName, p.PracticeName
                                                where p.IsDeleted == false
                                                select new PracticeDto
                                                {
                                                    BusinessUnitID = p.BusinessUnitID,
                                                    BusinessUnitName = sd.SubCategoryName,
                                                    PracticeID = p.PracticeID,
                                                    PracticeName = p.PracticeName,
                                                    ShortName = p.ShortName,
                                                    IsReserved = p.IsReserved,
                                                    ManagerID = p.ManagerID,
                                                    ManagerName = string.IsNullOrEmpty(ed.FirstName) ? "" : ed.LastName + ", " + ed.FirstName,
                                                    HeadCount = DataContext.Employees.Count(h => h.PracticeID == p.PracticeID && h.IsDeleted == false && h.LastWorkingDay.HasValue == false)
                                                };

            if (pageSize <= 0 || pageNo < 1)
            {
                return practices;
            }

            return practices.Skip((pageNo - 1) * pageSize).Take(pageSize);

        }

        public IEnumerable<PracticeDto> GetPracticesByBU(int buID)
        {
            IQueryable<PracticeDto> practices = from p in Entities
                                                join e in DataContext.Employees on p.ManagerID equals e.EmployeeEntryID into ee
                                                from ed in ee.DefaultIfEmpty()
                                                join s in DataContext.DropDownSubCategories on p.BusinessUnitID equals s.SubCategoryID into se
                                                from sd in se.DefaultIfEmpty()
                                                orderby p.PracticeName
                                                where p.IsDeleted == false && p.BusinessUnitID == buID
                                                select new PracticeDto
                                                {
                                                    BusinessUnitID = p.BusinessUnitID,
                                                    BusinessUnitName = sd.SubCategoryName,
                                                    PracticeID = p.PracticeID,
                                                    PracticeName = p.PracticeName,
                                                    ShortName = p.ShortName,
                                                    IsReserved = p.IsReserved,
                                                    ManagerID = p.ManagerID,
                                                    ManagerName = string.IsNullOrEmpty(ed.FirstName) ? "" : ed.LastName + ", " + ed.FirstName
                                                };

            return practices;
        }

        public PracticeDto GetByID(int id)
        {
            return (from p in Entities
                    join e in DataContext.Employees on p.ManagerID equals e.EmployeeEntryID into ee
                    from ed in ee.DefaultIfEmpty()
                    where p.PracticeID == id && p.IsDeleted == false
                    select new PracticeDto
                    {
                        BusinessUnitID = p.BusinessUnitID,
                        PracticeID = p.PracticeID,
                        PracticeName = p.PracticeName,
                        ShortName = p.ShortName,
                        ManagerID = p.ManagerID,
                        ManagerName = string.IsNullOrEmpty(ed.FirstName) ? "" : ed.LastName + ", " + ed.FirstName
                    }).FirstOrDefault();
        }

        public PracticeDto GetByNameOrDefault(string name)
        {
            if (Entities.Any(p => p.PracticeName.ToLower() == name.ToLower() && p.IsDeleted == false))
            {
                return (from p in Entities
                        where p.PracticeName.ToLower() == name.ToLower() && p.IsDeleted == false
                        select new PracticeDto
                        {
                            BusinessUnitID = p.BusinessUnitID,
                            PracticeID = p.PracticeID,
                            PracticeName = p.PracticeName,
                            ShortName = p.ShortName,
                        }).FirstOrDefault();
            }
            else
            {
                return (from p in Entities
                        where p.PracticeName == DEFAULT_PRACTICE_NAME && p.IsDeleted == false
                        select new PracticeDto
                        {
                            BusinessUnitID = p.BusinessUnitID,
                            PracticeID = p.PracticeID,
                            PracticeName = p.PracticeName,
                            ShortName = p.ShortName,
                        }).FirstOrDefault();
            }
        }

        public void Update(PracticeDto entity)
        {
            Practice buzEntity = Entities.FirstOrDefault(e => e.PracticeID == entity.PracticeID);
            MigrateEntity(entity, buzEntity);
            buzEntity.UpdateTimeStamp(entity.LoggedInUserName);
            Entities.Add(buzEntity);
            DataContext.Entry(buzEntity).State = EntityState.Modified;
            DataContext.SaveChanges();
        }

        public override bool CanBeDeleted(int id)
        {
            // are there any depending sub practices
            if (DataContext.SubPractices.Any(c => c.IsDeleted == false && c.PracticeID == id)
                || DataContext.Employees.Any(c => c.IsDeleted == false && c.PracticeID == id)
                || DataContext.Projects.Any(c => c.IsDeleted == false && c.PracticeID == id))
            {
                return false;
            }

            return true;
        }

        public bool IsReservedEntry(int practiceID)
        {
            return Entities.Any(c => c.IsDeleted == false &&
            c.PracticeID == practiceID &&
            c.IsReserved == true);
        }

        public string GetManagerName(int practiceID)
        {
            return (from p in Entities
                    join e in DataContext.Employees on p.ManagerID equals e.EmployeeEntryID into ee
                    from ed in ee.DefaultIfEmpty()
                    where p.PracticeID == practiceID
                    select string.IsNullOrEmpty(ed.FirstName) ? "" : ed.LastName + ", " + ed.FirstName).FirstOrDefault();
        }

        public string GetPracticeName(int practiceID)
        {
            return Entities.FirstOrDefault(c => c.PracticeID == practiceID
            && c.IsDeleted == false)?.PracticeName;
        }

        private Practice CreateBusinessEntity(PracticeDto categoryDto, bool isNewEntity = false)
        {
            Practice practice = new Practice
            {
                BusinessUnitID = categoryDto.BusinessUnitID,
                PracticeName = categoryDto.PracticeName,
                ShortName = categoryDto.ShortName,
                PracticeID = categoryDto.PracticeID,
                ManagerID = categoryDto.ManagerID
            };

            practice.UpdateTimeStamp(categoryDto.LoggedInUserName, true);

            return practice;
        }

        private void MigrateEntity(PracticeDto sourceEntity, Practice targetEntity)
        {
            targetEntity.BusinessUnitID = sourceEntity.BusinessUnitID;
            targetEntity.PracticeName = sourceEntity.PracticeName;
            targetEntity.ShortName = sourceEntity.ShortName;
            targetEntity.PracticeID = sourceEntity.PracticeID;
            targetEntity.ManagerID = sourceEntity.ManagerID;
            targetEntity.UpdateTimeStamp(sourceEntity.LoggedInUserName);
        }
    }

    public interface IPracticeRepository : IRepository<PracticeDto>
    {
        bool Exists(string practiceName, int id);

        bool IsReservedEntry(int practiceID);

        string GetPracticeName(int practiceID);

        string GetManagerName(int practiceID);

        IEnumerable<PracticeDto> GetPracticesByBU(int buID);
    }
}
