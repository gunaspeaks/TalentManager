using Agilisium.TalentManager.Repository.Repositories;
using Agilisium.TalentManager.Dto;
using Agilisium.TalentManager.Service.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace Agilisium.TalentManager.Service.Concreate
{
    public class DropDownSubCategoryService : IDropDownSubCategoryService
    {
        private readonly IDropDownSubCategoryRepository repository;

        public DropDownSubCategoryService(IDropDownSubCategoryRepository repository)
        {
            this.repository = repository;
        }

        public void CreateSubCategory(DropDownSubCategoryDto subCategory)
        {
            repository.Add(subCategory);
        }

        public bool Exists(string subCategoryName)
        {
            return repository.Exists(subCategoryName);
        }

        public bool Exists(int id)
        {
            return repository.Exists(id);
        }

        public bool Exists(string subCategoryName, int id)
        {
            return repository.Exists(subCategoryName, id);
        }

        public IEnumerable<DropDownSubCategoryDto> GetAll(int pageSize = -1, int pageNo = -1)
        {
            return repository.GetAll(pageSize, pageNo);
        }

        public IEnumerable<DropDownSubCategoryDto> GetSubCategories(int categoryID, int pageSize = -1, int pageNo = -1)
        {
            return repository.GetSubCategories(categoryID, pageSize, pageNo).ToList();
        }

        public IEnumerable<DropDownSubCategoryDto> GetSubCategories(int categoryID)
        {
            return repository.GetSubCategories(categoryID);
        }

        public DropDownSubCategoryDto GetSubCategory(int id)
        {
            return repository.GetByID(id);
        }

        public void UpdateSubCategory(DropDownSubCategoryDto category)
        {
            repository.Update(category);
        }

        public void DeleteSubCategory(DropDownSubCategoryDto category)
        {
            repository.Delete(category);
        }

        public int TotalRecordsCount()
        {
            return repository.TotalRecordsCount();
        }

        public int TotalRecordsCountByCategoryID(int categoryID)
        {
            return repository.TotalRecordsCountByCategoryID(categoryID);
        }

        public bool CanBeDeleted(int id)
        {
            return repository.CanBeDeleted(id);
        }

        public bool IsReservedEntry(int categoryID)
        {
            return repository.IsReservedEntry(categoryID);
        }
    }
}
