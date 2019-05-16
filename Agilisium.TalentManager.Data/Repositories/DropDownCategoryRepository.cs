using Agilisium.TalentManager.Repository.Abstract;
using Agilisium.TalentManager.Dto;
using Agilisium.TalentManager.Model.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Agilisium.TalentManager.Repository.Repositories
{
    public class DropDownCategoryRepository : RepositoryBase<DropDownCategory>, IDropDownCategoryRepository
    {
        public void Add(DropDownCategoryDto entity)
        {
            DropDownCategory category = CreateBusinessEntity(entity, true);
            Entities.Add(category);
            DataContext.Entry(category).State = EntityState.Added;
            DataContext.SaveChanges();
        }

        public void Delete(DropDownCategoryDto entity)
        {
            DropDownCategory dbEntity = Entities.FirstOrDefault(e => e.CategoryID == entity.CategoryID);
            dbEntity.IsDeleted = true;
            dbEntity.UpdateTimeStamp(entity.LoggedInUserName);
            Entities.Add(dbEntity);
            DataContext.Entry(dbEntity).State = EntityState.Modified;
            DataContext.SaveChanges();
        }

        public bool Exists(string categoryName, int currentCategoryID)
        {
            return Entities.Any(c => c.CategoryName.ToLower() == categoryName.ToLower() &&
            c.CategoryID != currentCategoryID && c.IsDeleted == false);
        }

        public bool Exists(string categoryName)
        {
            return Entities.Any(c => c.CategoryName.ToLower() == categoryName.ToLower() && c.IsDeleted == false);
        }

        public bool Exists(int categoryID)
        {
            return Entities.Any(c => c.CategoryID == categoryID && c.IsDeleted == false);
        }

        public IEnumerable<DropDownCategoryDto> GetAll(int pageSize = -1, int pageNo = -1)
        {
            IQueryable<DropDownCategoryDto> categories = from c in Entities
                                                         orderby c.CategoryName
                                                         where c.IsDeleted == false
                                                         select new DropDownCategoryDto
                                                         {
                                                             CategoryID = c.CategoryID,
                                                             CategoryName = c.CategoryName,
                                                             Description = c.Description,
                                                             ShortName = c.ShortName,
                                                             IsReserved = c.IsReserved
                                                         };

            if (pageSize <= 0 || pageNo < 1)
            {
                return categories;
            }

            return categories.Skip((pageNo - 1) * pageSize).Take(pageSize);
        }

        public DropDownCategoryDto GetByID(int id)
        {
            return (from c in Entities
                    where c.CategoryID == id && c.IsDeleted == false
                    select new DropDownCategoryDto
                    {
                        CategoryID = c.CategoryID,
                        CategoryName = c.CategoryName,
                        Description = c.Description,
                        ShortName = c.ShortName,
                        IsReserved = c.IsReserved
                    }).FirstOrDefault();
        }

        public void Update(DropDownCategoryDto entity)
        {
            DropDownCategory buzEntity = Entities.FirstOrDefault(c => c.CategoryID == entity.CategoryID);
            MigrateEntity(entity, buzEntity);
            Entities.Add(buzEntity);
            DataContext.Entry(buzEntity).State = EntityState.Modified;
            DataContext.SaveChanges();
        }

        private DropDownCategory CreateBusinessEntity(DropDownCategoryDto categoryDto, bool isNewEntity = false)
        {
            DropDownCategory category = new DropDownCategory
            {
                CategoryName = categoryDto.CategoryName,
                Description = categoryDto.Description,
                ShortName = categoryDto.ShortName,
                CategoryID = categoryDto.CategoryID
            };

            category.UpdateTimeStamp(categoryDto.LoggedInUserName, isNewEntity: true);
            return category;
        }

        private void MigrateEntity(DropDownCategoryDto sourceEntity, DropDownCategory targetEntity)
        {
            targetEntity.CategoryID = sourceEntity.CategoryID;
            targetEntity.CategoryName = sourceEntity.CategoryName;
            targetEntity.Description = sourceEntity.Description;
            targetEntity.IsReserved = sourceEntity.IsReserved;
            targetEntity.ShortName = sourceEntity.ShortName;
            targetEntity.UpdateTimeStamp(sourceEntity.LoggedInUserName);
        }

        public override bool CanBeDeleted(int id)
        {
            // are there any depending sub categories
            if (DataContext.DropDownSubCategories.Count(c => c.IsDeleted == false && c.CategoryID == id) > 0)
            {
                return false;
            }

            return true;
        }

        public bool IsReservedEntry(int categoryID)
        {
            return Entities.Any(c => c.IsDeleted == false &&
            c.CategoryID == categoryID &&
            c.IsReserved == true);
        }

        public string GetCategoryName(int categoryID)
        {
            return Entities.FirstOrDefault(c => c.CategoryID == categoryID)?.CategoryName;
        }
    }

    public interface IDropDownCategoryRepository : IRepository<DropDownCategoryDto>
    {
        bool Exists(string itemName, int id);

        bool IsReservedEntry(int categoryID);

        string GetCategoryName(int categoryID);
    }
}
