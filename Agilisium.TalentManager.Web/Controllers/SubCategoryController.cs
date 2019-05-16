using Agilisium.TalentManager.Dto;
using Agilisium.TalentManager.Service.Abstract;
using Agilisium.TalentManager.Web.Helpers;
using Agilisium.TalentManager.Web.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Agilisium.TalentManager.Web.Controllers
{
    public class SubCategoryController : BaseController
    {
        private readonly IDropDownSubCategoryService subCategoryService;
        private readonly IDropDownCategoryService categoryService;

        public SubCategoryController(IDropDownCategoryService categoryService, IDropDownSubCategoryService subCategoryService)
        {
            this.categoryService = categoryService;
            this.subCategoryService = subCategoryService;
        }

        // GET: SubCategory/1
        public ActionResult List(string selectedCategoryID, int page = 1)
        {
            SubCategoryViewModel model = new SubCategoryViewModel();

            try
            {
                model.CategoryListItems = (IEnumerable<SelectListItem>)Session["CategoryListItems"] ?? GetCategoriesDropDownList();

                if (string.IsNullOrEmpty(selectedCategoryID))
                {
                    if (Session["SelectedCategoryID"] == null
                        || (Session["SelectedCategoryID"] != null && string.IsNullOrEmpty(Session["SelectedCategoryID"].ToString())))
                    {
                        model.SelectedCategoryID = int.Parse(model.CategoryListItems.FirstOrDefault(c => c.Text != "Please Select")?.Value);
                    }
                    else
                    {
                        model.SelectedCategoryID = int.Parse(Session["SelectedCategoryID"].ToString());
                    }
                }
                else
                {
                    model.SelectedCategoryID = int.Parse(selectedCategoryID);
                }

                model.PagingInfo = new PagingInfo
                {
                    TotalRecordsCount = subCategoryService.TotalRecordsCountByCategoryID(model.SelectedCategoryID),
                    CurentPageNo = page,
                    RecordsPerPage = RecordsPerPage
                };

                if (model.PagingInfo.TotalRecordsCount > 0)
                {
                    Session["SelectedCategoryID"] = model.SelectedCategoryID.ToString();
                    model.SubCategories = GetSubCategories(model.SelectedCategoryID, page);
                }
                else
                {
                    string categoryName = categoryService.GetCategoryName(model.SelectedCategoryID);
                    if (string.IsNullOrEmpty(categoryName))
                    {
                        DisplayWarningMessage("Hey, please check whether you are trying to access the correct Category.");
                    }
                    else
                    {
                        DisplayWarningMessage($"There are no Sub-Categories found for Category '{categoryName}'");
                    }
                }
            }
            catch (Exception exp)
            {
                DisplayLoadErrorMessage(exp);
            }

            return View(model);
        }

        // GET: SubCategory/Create
        public ActionResult Create()
        {
            try
            {
                ViewBag.CategoryListItems = (IEnumerable<SelectListItem>)Session["CategoryListItems"] ?? GetCategoriesDropDownList();
            }
            catch (Exception exp)
            {
                DisplayLoadErrorMessage(exp);
            }
            return View(new SubCategoryModel());
        }

        // POST: SubCategory/Create
        [HttpPost]
        public ActionResult Create(SubCategoryModel subCategory)
        {
            try
            {
                ViewBag.CategoryListItems = (IEnumerable<SelectListItem>)Session["CategoryListItems"] ?? GetCategoriesDropDownList();

                if (ModelState.IsValid)
                {
                    if (subCategoryService.Exists(subCategory.SubCategoryName))
                    {
                        DisplayWarningMessage($"Sub-Category Name '{subCategory.SubCategoryName}' is duplicate");
                        return View(subCategory);
                    }

                    DropDownSubCategoryDto subCategoryModel = Mapper.Map<SubCategoryModel, DropDownSubCategoryDto>(subCategory);
                    subCategoryService.CreateSubCategory(subCategoryModel);
                    DisplaySuccessMessage($"New Sub-Category '{subCategory.SubCategoryName}' has been stored successfully");
                    Session["SelectedCategoryID"] = subCategory.CategoryID.ToString();
                    return RedirectToAction("List");
                }
            }
            catch (Exception exp)
            {
                DisplayUpdateErrorMessage(exp);
            }
            return View(subCategory);
        }

        // GET: SubCategory/Edit/5
        public ActionResult Edit(int? id)
        {
            SubCategoryModel uiCategory = new SubCategoryModel();

            if (!id.HasValue)
            {
                DisplayWarningMessage("Looks like, the ID is missing in your request");
                return RedirectToAction("List");
            }

            try
            {
                if (!subCategoryService.Exists(id.Value))
                {
                    DisplayWarningMessage($"Sorry, We couldn't find the Sub-Category with ID: {id.Value}");
                    return RedirectToAction("List");
                }

                ViewBag.CategoryListItems = (IEnumerable<SelectListItem>)Session["CategoryListItems"] ?? GetCategoriesDropDownList();

                DropDownSubCategoryDto category = subCategoryService.GetSubCategory(id.Value);
                uiCategory = Mapper.Map<DropDownSubCategoryDto, SubCategoryModel>(category);
            }
            catch (Exception exp)
            {
                DisplayReadErrorMessage(exp);
            }

            return View(uiCategory);
        }

        // POST: SubCategory/Edit/5
        [HttpPost]
        public ActionResult Edit(SubCategoryModel subCategory)
        {
            try
            {
                ViewBag.CategoryListItems = (IEnumerable<SelectListItem>)Session["CategoryListItems"] ?? GetCategoriesDropDownList();

                // || (!ModelState.IsValid && ModelState.Values.Count(p => p.Errors.Count > 0) == 1)
                if (ModelState.IsValid)
                {
                    if (subCategoryService.Exists(subCategory.SubCategoryName, subCategory.SubCategoryID))
                    {
                        DisplayWarningMessage($"Sub-Category Name '{subCategory.SubCategoryName}' is duplicate");
                        return View(subCategory);
                    }

                    DropDownSubCategoryDto subCategoryDto = Mapper.Map<SubCategoryModel, DropDownSubCategoryDto>(subCategory);
                    subCategoryService.UpdateSubCategory(subCategoryDto);
                    DisplaySuccessMessage("Sub-Category has been updated successfully");
                    Session["SelectedCategoryID"] = subCategory.CategoryID.ToString();
                    return RedirectToAction("List");
                }
            }
            catch (Exception exp)
            {
                DisplayUpdateErrorMessage(exp);
            }
            return View(subCategory);
        }

        // GET: SubCategory/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                DisplayWarningMessage("Looks like, the ID is missing in your request");
                return RedirectToAction("List");
            }

            try
            {
                if (subCategoryService.IsReservedEntry(id.Value))
                {
                    DisplayWarningMessage("Hey, why do you want to delete a Reserved Sub-Category. Please check with the system administrator.");
                    return RedirectToAction("List");
                }

                if (subCategoryService.CanBeDeleted(id.Value) == false)
                {
                    DisplayWarningMessage("There are some dependencies with this Sub-Category. So, you can't delete this for now.");
                    return RedirectToAction("List");
                }

                subCategoryService.DeleteSubCategory(new DropDownSubCategoryDto { SubCategoryID = id.Value });
                DisplaySuccessMessage("Sub-Category has been deleted successfully");
            }
            catch (Exception exp)
            {
                DisplayDeleteErrorMessage(exp);
            }
            return RedirectToAction("List");
        }

        private IEnumerable<SelectListItem> GetCategoriesDropDownList()
        {
            IEnumerable<DropDownCategoryDto> categories = categories = categoryService.GetCategories();
            List<SelectListItem> categoriesList = (from cat in categories
                                                   orderby cat.CategoryName
                                                   select new SelectListItem
                                                   {
                                                       Text = cat.CategoryName,
                                                       Value = $"{cat.CategoryID}"
                                                   }).ToList();

            Session["CategoryListItems"] = categoriesList;
            return categoriesList;
        }

        private IEnumerable<SubCategoryModel> GetSubCategories(int categoryID, int pageNo)
        {
            IEnumerable<DropDownSubCategoryDto> subCategories = subCategoryService.GetSubCategories(categoryID, RecordsPerPage, pageNo);
            IEnumerable<SubCategoryModel> uiCategories = Mapper.Map<IEnumerable<DropDownSubCategoryDto>, IEnumerable<SubCategoryModel>>(subCategories);
            return uiCategories;
        }
    }
}
