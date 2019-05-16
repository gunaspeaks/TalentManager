using Agilisium.TalentManager.Dto;
using System.Collections.Generic;

namespace Agilisium.TalentManager.Service.Abstract
{
    public interface IDropDownSubCategoryService
    {
        bool Exists(string subCategoryName);

        bool Exists(int id);

        bool Exists(string subCategoryName, int id);

        IEnumerable<DropDownSubCategoryDto> GetAll(int pageSize = -1, int pageNo = -1);

        IEnumerable<DropDownSubCategoryDto> GetSubCategories(int categoryID, int pageSize = -1, int pageNo = -1);

        DropDownSubCategoryDto GetSubCategory(int id);

        void CreateSubCategory(DropDownSubCategoryDto subCategory);

        void UpdateSubCategory(DropDownSubCategoryDto category);

        void DeleteSubCategory(DropDownSubCategoryDto category);

        int TotalRecordsCount();

        int TotalRecordsCountByCategoryID(int categoryID);

        bool CanBeDeleted(int id);

        bool IsReservedEntry(int categoryID);
    }
}
