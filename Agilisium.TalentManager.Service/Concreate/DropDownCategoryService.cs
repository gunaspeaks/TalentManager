using Agilisium.TalentManager.Repository.Repositories;
using Agilisium.TalentManager.Dto;
using Agilisium.TalentManager.Service.Abstract;
using System.Collections.Generic;

namespace Agilisium.TalentManager.Service.Concreate
{
    public class DropDownCategoryService : IDropDownCategoryService
    {
        private readonly IDropDownCategoryRepository repository;

        public DropDownCategoryService(IDropDownCategoryRepository repository)
        {
            this.repository = repository;
        }

        public void CreateCategory(DropDownCategoryDto category)
        {
            repository.Add(category);
        }

        public void DeleteCategory(DropDownCategoryDto category)
        {
            repository.Delete(category);
        }

        public bool Exists(string categoryName)
        {
            return repository.Exists(categoryName);
        }

        public bool Exists(int id)
        {
            return repository.Exists(id);
        }

        public bool Exists(string categoryName, int id)
        {
            return repository.Exists(categoryName, id);
        }

        public IEnumerable<DropDownCategoryDto> GetCategories(int pageSize, int pageNo = -1)
        {
            return repository.GetAll(pageSize, pageNo);
        }

        public DropDownCategoryDto GetCategory(int id)
        {
            return repository.GetByID(id);
        }

        public void UpdateCategory(DropDownCategoryDto category)
        {
            repository.Update(category);
        }

        public int TotalRecordsCount()
        {
            return repository.TotalRecordsCount();
        }

        public bool CanBeDeleted(int id)
        {
            return repository.CanBeDeleted(id);
        }

        public bool IsReservedEntry(int categoryID)
        {
            return repository.IsReservedEntry(categoryID);
        }

        public string GetCategoryName(int categoryID)
        {
            return repository.GetCategoryName(categoryID);
        }
    }
}
