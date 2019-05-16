using Agilisium.TalentManager.Repository.Abstract;
using Agilisium.TalentManager.Dto;
using Agilisium.TalentManager.Model.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Agilisium.TalentManager.Repository.Repositories
{
    public class DropDownSubCategoryRepository : RepositoryBase<DropDownSubCategory>, IDropDownSubCategoryRepository
    {
        public void Add(DropDownSubCategoryDto entity)
        {
            DropDownSubCategory subCategory = CreateBusinessEntity(entity, true);
            Entities.Add(subCategory);
            DataContext.Entry(subCategory).State = EntityState.Added;
            DataContext.SaveChanges();
        }

        public void Delete(DropDownSubCategoryDto entity)
        {
            DropDownSubCategory subCategory = Entities.FirstOrDefault(e => e.SubCategoryID == entity.SubCategoryID);
            subCategory.IsDeleted = true;
            subCategory.UpdateTimeStamp(entity.LoggedInUserName);
            Entities.Add(subCategory);
            DataContext.Entry(subCategory).State = EntityState.Modified;
            DataContext.SaveChanges();
        }

        public bool Exists(string itemName, int id)
        {
            return Entities.Any(c => c.SubCategoryName.ToLower() == itemName.ToLower() &&
            c.SubCategoryID != id && c.IsDeleted == false);
        }

        public bool Exists(string subCategoryName)
        {
            return Entities.Any(c => c.SubCategoryName.ToLower() == subCategoryName.ToLower() && c.IsDeleted == false);
        }

        public bool Exists(int id)
        {
            return Entities.Any(c => c.SubCategoryID == id && c.IsDeleted == false);

        }

        public IEnumerable<DropDownSubCategoryDto> GetAll(int pageSize = -1, int pageNo = -1)
        {
            IQueryable<DropDownSubCategoryDto> subCategories = from s in Entities
                                                               join c in DataContext.DropDownCategories on s.CategoryID equals c.CategoryID into ce
                                                               from cd in ce.DefaultIfEmpty()
                                                               orderby s.SubCategoryName
                                                               where s.IsDeleted == false
                                                               select new DropDownSubCategoryDto
                                                               {
                                                                   SubCategoryID = s.SubCategoryID,
                                                                   SubCategoryName = s.SubCategoryName,
                                                                   CategoryID = s.CategoryID,
                                                                   Description = s.Description,
                                                                   ShortName = s.ShortName,
                                                                   CategoryName = cd.CategoryName,
                                                                   IsReserved = cd.IsReserved
                                                               };

            if (pageSize <= 0 || pageNo < 1)
            {
                return subCategories;
            }

            return subCategories.Skip((pageNo - 1) * pageSize).Take(pageSize);
        }

        public DropDownSubCategoryDto GetByID(int id)
        {
            return (from s in Entities
                    join c in DataContext.DropDownCategories on s.CategoryID equals c.CategoryID into ce
                    from cd in ce.DefaultIfEmpty()
                    where s.SubCategoryID == id && s.IsDeleted == false && cd.IsDeleted == false
                    select new DropDownSubCategoryDto
                    {
                        SubCategoryID = s.SubCategoryID,
                        SubCategoryName = s.SubCategoryName,
                        CategoryID = s.CategoryID,
                        Description = s.Description,
                        ShortName = s.ShortName,
                        CategoryName = cd.CategoryName
                    }).FirstOrDefault();
        }

        public DropDownSubCategoryDto GetByName(string name, int categoryID)
        {
            return (from s in Entities
                    where s.CategoryID== categoryID &&
                    (s.SubCategoryName.ToLower() == name.ToLower() || s.ShortName.ToLower() == name.ToLower()) 
                    && s.IsDeleted == false 
                    select new DropDownSubCategoryDto
                    {
                        SubCategoryID = s.SubCategoryID,
                        SubCategoryName = s.SubCategoryName,
                        CategoryID = s.CategoryID,
                        Description = s.Description,
                        ShortName = s.ShortName,
                    }).FirstOrDefault();
        }

        public IEnumerable<DropDownSubCategoryDto> GetSubCategories(int categoryID, int pageSize = -1, int pageNo = -1)
        {
            return (from s in Entities
                    join c in DataContext.DropDownCategories on s.CategoryID equals c.CategoryID into ce
                    from cd in ce.DefaultIfEmpty()
                    where s.CategoryID == categoryID && s.IsDeleted == false && cd.IsDeleted == false
                    select new DropDownSubCategoryDto
                    {
                        SubCategoryID = s.SubCategoryID,
                        SubCategoryName = s.SubCategoryName,
                        CategoryID = s.CategoryID,
                        Description = s.Description,
                        ShortName = s.ShortName,
                        CategoryName = cd.CategoryName,
                        IsReserved = cd.IsReserved
                    });
        }

        public void Update(DropDownSubCategoryDto entity)
        {
            DropDownSubCategory buzEntity = Entities.FirstOrDefault(e => e.SubCategoryID == entity.SubCategoryID);
            MigrateEntity(entity, buzEntity);
            buzEntity.UpdateTimeStamp(entity.LoggedInUserName);
            Entities.Add(buzEntity);
            DataContext.Entry(buzEntity).State = EntityState.Modified;
            DataContext.SaveChanges();
        }

        public override bool CanBeDeleted(int id)
        {
            if (DataContext.Employees.Any(c => c.IsDeleted == false
                && (c.BusinessUnitID == id || c.EmploymentTypeID == id || c.UtilizationTypeID == id))
                || DataContext.Projects.Any(p => p.IsDeleted == false && p.ProjectTypeID == id)
                || DataContext.ProjectAllocations.Any(p => p.IsDeleted == false && p.AllocationTypeID == id))
            {
                return false;
            }

            return true;
        }

        public bool IsReservedEntry(int subCategoryID)
        {
            return Entities.Any(c => c.IsDeleted == false &&
            c.SubCategoryID == subCategoryID &&
            c.IsReserved == true);
        }

        public int TotalRecordsCountByCategoryID(int categoryID)
        {
            return Entities.Count(c => c.CategoryID == categoryID && c.IsDeleted == false);
        }

        private DropDownSubCategory CreateBusinessEntity(DropDownSubCategoryDto subCategoryDto, bool isNewEntity = false)
        {
            DropDownSubCategory category = new DropDownSubCategory
            {
                CategoryID = subCategoryDto.CategoryID,
                SubCategoryID = subCategoryDto.SubCategoryID,
                SubCategoryName = subCategoryDto.SubCategoryName,
                Description = subCategoryDto.Description,
                ShortName = subCategoryDto.ShortName,
            };

            category.UpdateTimeStamp(subCategoryDto.LoggedInUserName, true);
            return category;
        }

        private void MigrateEntity(DropDownSubCategoryDto sourceEntity, DropDownSubCategory targetEntity)
        {
            targetEntity.CategoryID = sourceEntity.CategoryID;
            targetEntity.Description = sourceEntity.Description;
            targetEntity.ShortName = sourceEntity.ShortName;
            targetEntity.SubCategoryID = sourceEntity.SubCategoryID;
            targetEntity.SubCategoryName = sourceEntity.SubCategoryName;
            targetEntity.UpdateTimeStamp(sourceEntity.LoggedInUserName);
        }
    }

    public interface IDropDownSubCategoryRepository : IRepository<DropDownSubCategoryDto>
    {
        IEnumerable<DropDownSubCategoryDto> GetSubCategories(int categoryID, int pageSize = -1, int pageNo = -1);

        bool Exists(string itemName, int id);

        bool IsReservedEntry(int subCategoryID);

        int TotalRecordsCountByCategoryID(int categoryID);

        DropDownSubCategoryDto GetByName(string name, int categoryID);
    }
}
