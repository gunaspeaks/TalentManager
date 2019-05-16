using Agilisium.TalentManager.Dto;
using System.Collections.Generic;

namespace Agilisium.TalentManager.Service.Abstract
{
    public interface IDropDownCategoryService
    {
        bool Exists(string categoryName);

        bool Exists(int id);

        bool Exists(string categoryName, int id);

        IEnumerable<DropDownCategoryDto> GetCategories(int pageSize = 0, int pageNo = -1);

        DropDownCategoryDto GetCategory(int id);

        void CreateCategory(DropDownCategoryDto category);

        void UpdateCategory(DropDownCategoryDto category);

        void DeleteCategory(DropDownCategoryDto category);

        int TotalRecordsCount();

        bool CanBeDeleted(int id);

        bool IsReservedEntry(int categoryID);

        string GetCategoryName(int categoryID);
    }
}
