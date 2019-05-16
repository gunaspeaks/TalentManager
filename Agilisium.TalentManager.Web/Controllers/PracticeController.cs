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
    public class PracticeController : BaseController
    {
        private readonly IPracticeService practiceService;
        private readonly IEmployeeService empService;
        private readonly IDropDownSubCategoryService subCategoryService;

        public PracticeController(IPracticeService practiceService, IEmployeeService empService,
            IDropDownSubCategoryService subCategoryService)
        {
            this.practiceService = practiceService;
            this.empService = empService;
            this.subCategoryService = subCategoryService;
        }

        // GET: Practice
        public ActionResult List(int page = 1)
        {
            PracticeViewModel viewModel = new PracticeViewModel();

            try
            {
                viewModel.PagingInfo = new PagingInfo
                {
                    TotalRecordsCount = practiceService.TotalRecordsCount(),
                    RecordsPerPage = RecordsPerPage,
                    CurentPageNo = page
                };

                if (viewModel.PagingInfo.TotalRecordsCount > 0)
                {
                    viewModel.Practices = GetPractices(page);
                }
                else
                {
                    DisplayWarningMessage("There are no PODs to display");
                }
            }
            catch (Exception exp)
            {
                DisplayLoadErrorMessage(exp);
            }

            return View(viewModel);
        }

        // GET: Practice/Create
        public ActionResult Create()
        {
            try
            {
                InitializePageData();
            }
            catch (Exception exp)
            {
                DisplayLoadErrorMessage(exp);
            }

            return View(new PracticeModel());
        }

        // POST: Practice/Create
        [HttpPost]
        public ActionResult Create(PracticeModel practice)
        {
            try
            {
                InitializePageData();

                if (ModelState.IsValid)
                {
                    if (practiceService.Exists(practice.PracticeName))
                    {
                        DisplayWarningMessage($"The POD Name '{practice.PracticeName}' is duplicate");
                        return View(practice);
                    }
                    PracticeDto practiceModel = Mapper.Map<PracticeModel, PracticeDto>(practice);
                    practiceService.CreatePractice(practiceModel);
                    DisplaySuccessMessage($"New POD '{practice.PracticeName}' has been stored successfully");
                    return RedirectToAction("List");
                }
            }
            catch (Exception exp)
            {
                DisplayLoadErrorMessage(exp);
            }
            return View(practice);
        }

        // GET: Practice/Edit/5
        public ActionResult Edit(int? id)
        {
            PracticeModel practice = new PracticeModel();

            InitializePageData();

            try
            {
                if (!id.HasValue)
                {
                    DisplayWarningMessage("Looks like, the ID is missing in your request");
                    return RedirectToAction("List");
                }

                if (!practiceService.Exists(id.Value))
                {
                    DisplayWarningMessage($"Sorry, We couldn't find the POD with ID: {id.Value}");
                    return RedirectToAction("List");
                }

                PracticeDto practiceDto = practiceService.GetPractice(id.Value);
                practice = Mapper.Map<PracticeDto, PracticeModel>(practiceDto);
            }
            catch (Exception exp)
            {
                DisplayReadErrorMessage(exp);
            }

            return View(practice);
        }

        // POST: Practice/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, PracticeModel practice)
        {
            try
            {
                InitializePageData();

                if (ModelState.IsValid)
                {
                    if (practiceService.Exists(practice.PracticeName, practice.PracticeID))
                    {
                        DisplayWarningMessage($"POD Name '{practice.PracticeName}' is duplicate");
                        return View(practice);
                    }

                    PracticeDto practiceModel = Mapper.Map<PracticeModel, PracticeDto>(practice);
                    practiceService.UpdatePractice(practiceModel);
                    DisplaySuccessMessage($"POD '{practice.PracticeName}' details have been modified successfully");
                    return RedirectToAction("List");
                }
            }
            catch (Exception exp)
            {
                DisplayUpdateErrorMessage(exp);
            }
            return View(practice);
        }

        // POST: Practice/Delete/5
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
                if (practiceService.IsReservedEntry(id.Value))
                {
                    DisplayWarningMessage("Hey, why do you want to delete a Reserved POD. Please check with the system administrator.");
                    return RedirectToAction("List");
                }

                if (practiceService.CanBeDeleted(id.Value) == false)
                {
                    DisplayWarningMessage("There are some dependencies with this POD. So, you can't delete this for now");
                    return RedirectToAction("List");
                }

                practiceService.DeletePractice(new PracticeDto { PracticeID = id.Value });
                DisplaySuccessMessage("POD has been deleted successfully");
            }
            catch (Exception exp)
            {
                DisplayDeleteErrorMessage(exp);
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        public JsonResult GetPracticesByBuID(int buID)
        {
            IEnumerable<PracticeDto> practices = practiceService.GetPracticesByBU(buID).ToList();

            List<SelectListItem> practiceItems = (from p in practices
                                                  select new SelectListItem
                                                  {
                                                      Value = p.PracticeID.ToString(),
                                                      Text = p.PracticeName
                                                  }).ToList();
            return Json(practiceItems);
        }

        private IEnumerable<PracticeModel> GetPractices(int pageNo)
        {
            IEnumerable<PracticeDto> practices = practiceService.GetPractices(RecordsPerPage, pageNo);
            IEnumerable<PracticeModel> practiceModels = Mapper.Map<IEnumerable<PracticeDto>, IEnumerable<PracticeModel>>(practices);
            return practiceModels;
        }

        private void InitializePageData()
        {
            List<EmployeeDto> managers = empService.GetAllManagers();
            List<SelectListItem> empDDList = new List<SelectListItem>();

            if (managers != null)
            {
                empDDList = (from e in managers
                             select new SelectListItem
                             {
                                 Text = $"{e.FirstName} {e.LastName}",
                                 Value = e.EmployeeEntryID.ToString()
                             }).OrderBy(i => i.Text).ToList();
            }

            ViewBag.ManagerListItems = empDDList;
            LoadBusinessUnitItems();
        }

        private void LoadBusinessUnitItems()
        {
            IEnumerable<DropDownSubCategoryDto> buList = subCategoryService.GetSubCategories((int)CategoryType.BusinessUnit);

            List<SelectListItem> buListItems = (from c in buList
                                                orderby c.SubCategoryName
                                                where c.CategoryID == (int)CategoryType.BusinessUnit
                                                select new SelectListItem
                                                {
                                                    Text = c.SubCategoryName,
                                                    Value = c.SubCategoryID.ToString()
                                                }).ToList();

            ViewBag.BuListItems = buListItems;
        }
    }
}
