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
    public class AllocationController : BaseController
    {
        private readonly IEmployeeService empService;
        private readonly IDropDownSubCategoryService subCategoryService;
        private readonly IProjectService projectService;
        private readonly IAllocationService allocationService;

        public AllocationController(IEmployeeService empService,
            IDropDownSubCategoryService subCategoryService,
            IAllocationService allocationService, IProjectService projectService)
        {
            this.empService = empService;
            this.subCategoryService = subCategoryService;
            this.allocationService = allocationService;
            this.projectService = projectService;
        }

        // GET: Project
        public ActionResult List(string filterType, string filterValue, string sortBy = "EmpName", string sortType = "asc", int page = 1)
        {
            AllocationViewModel viewModel = new AllocationViewModel()
            {
                FilterType = filterType,
                FilterValue = filterValue,
                SortBy = sortBy,
                SortType = sortType
            };

            try
            {
                InitializeListPage(viewModel);
                int.TryParse(filterValue, out int filterValueID);
                viewModel.PagingInfo = new PagingInfo
                {
                    TotalRecordsCount = allocationService.TotalRecordsCount(filterType, filterValueID),
                    RecordsPerPage = RecordsPerPage,
                    CurentPageNo = page
                };

                if (viewModel.PagingInfo.TotalRecordsCount > 0)
                {
                    viewModel.Allocations = GetAllocations(filterType, filterValueID, sortBy, sortType, page);
                }
                else
                {
                    DisplayWarningMessage("There are no Project Allocations to display");
                }
            }
            catch (Exception exp)
            {
                DisplayLoadErrorMessage(exp);
            }

            return View(viewModel);
        }

        public ActionResult AllAllocations(int id)
        {
            List<AllocationModel> allocations = null;
            try
            {
                List<ProjectAllocationDto> modelsDto = allocationService.GetAllAllocationsByProjectID(id);
                allocations = Mapper.Map<List<ProjectAllocationDto>, List<AllocationModel>>(modelsDto);
            }
            catch (Exception exp)
            {
                DisplayLoadErrorMessage(exp);
            }

            return View(allocations);
        }

        public ActionResult AllocationHistory(string filterType = "Please Select", string filterValue = "0", string sortBy = "EmployeeName", string sortType = "asc", int page = 1)
        {
            AllocationViewModel viewModel = new AllocationViewModel()
            {
                FilterType = filterType,
                FilterValue = filterValue,
                SortBy = sortBy,
                SortType = sortType
            };

            int.TryParse(filterValue, out int filterValueID);
            try
            {
                viewModel.PagingInfo = new PagingInfo
                {
                    TotalRecordsCount = allocationService.GetTotalRecordsCountForAllocationHistory(filterType, filterValueID),
                    RecordsPerPage = RecordsPerPage,
                    CurentPageNo = page
                };

                InitializeListPage(viewModel);
                if (viewModel.PagingInfo.TotalRecordsCount > 0)
                {
                    viewModel.Allocations = GetAllocationsHistory(filterType, filterValueID, sortBy, sortType, page);
                }
                else
                {
                    DisplayWarningMessage("There are no Project Allocations History to display");
                }
            }
            catch (Exception exp)
            {
                DisplayLoadErrorMessage(exp);
            }

            return View(viewModel);
        }

        // GET: Project/Create
        public ActionResult Create()
        {
            AllocationModel project = new AllocationModel
            {
                AllocationStartDate = DateTime.Now,
                AllocationEndDate = DateTime.Now,
                PercentageOfAllocation = 100
            };

            try
            {
                InitializePageData();
            }
            catch (Exception exp)
            {
                DisplayLoadErrorMessage(exp);
            }

            return View(project);
        }

        // POST: Project/Create
        [HttpPost]
        public ActionResult Create(AllocationModel allocation)
        {
            try
            {
                InitializePageData();

                if (ModelState.IsValid)
                {
                    if (IsValidAllocation(allocation) == false)
                    {
                        return View(allocation);
                    }

                    if (allocation.AllocationEndDate <= allocation.AllocationStartDate)
                    {
                        DisplayWarningMessage("The End date should be greater than the Start date");
                        return View(allocation);
                    }

                    if (allocationService.AnyActiveAllocationInBenchProject(allocation.EmployeeID))
                    {
                        DisplayWarningMessage("There is an active allocation in Bench project for this employee. Please end the allocation for that project.");
                        return View(allocation);
                    }

                    ProjectAllocationDto projectDto = Mapper.Map<AllocationModel, ProjectAllocationDto>(allocation);
                    allocationService.Add(projectDto);
                    DisplaySuccessMessage($"New project allocation has been created for {allocation.EmployeeName}");
                    return RedirectToAction("List");
                }
            }
            catch (Exception exp)
            {
                DisplayUpdateErrorMessage(exp);
            }
            return View(allocation);
        }

        // GET: Project/Edit/5
        public ActionResult Edit(int? id)
        {
            AllocationModel empModel = new AllocationModel();

            try
            {
                InitializePageData();

                if (!id.HasValue)
                {
                    DisplayWarningMessage("Looks like, the required data is not available with your request");
                    return RedirectToAction("List");
                }

                if (!allocationService.Exists(id.Value))
                {
                    DisplayWarningMessage($"Sorry, we couldn't find the allocation details with ID: {id.Value}");
                    return RedirectToAction("List");
                }

                ProjectAllocationDto emp = allocationService.GetByID(id.Value);
                empModel = Mapper.Map<ProjectAllocationDto, AllocationModel>(emp);
            }
            catch (Exception exp)
            {
                DisplayReadErrorMessage(exp);
            }

            return View(empModel);
        }

        // POST: Project/Edit/5
        [HttpPost]
        public ActionResult Edit(AllocationModel allocation)
        {
            try
            {
                InitializePageData();

                if (ModelState.IsValid)
                {
                    if (IsValidAllocation(allocation) == false)
                    {
                        return View(allocation);
                    }

                    if (allocation.AllocationEndDate <= allocation.AllocationStartDate)
                    {
                        DisplayWarningMessage("The End date should be greater than the Start date");
                        return View(allocation);
                    }

                    ProjectAllocationDto projectDto = Mapper.Map<AllocationModel, ProjectAllocationDto>(allocation);
                    allocationService.Update(projectDto);
                    DisplaySuccessMessage($"Project allocation details have been updated for {allocation.EmployeeName}");
                    return RedirectToAction("List");
                }
            }
            catch (Exception exp)
            {
                DisplayUpdateErrorMessage(exp);
            }
            return View(allocation);
        }

        // GET: Project/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                DisplayWarningMessage("Looks like, the Allocation ID is missing from your request");
                return RedirectToAction("List");
            }

            try
            {
                allocationService.Delete(new ProjectAllocationDto { AllocationEntryID = id.Value });
                DisplaySuccessMessage("Allocation details have been removed successfully");
                return RedirectToAction("List");
            }
            catch (Exception exp)
            {
                DisplayDeleteErrorMessage(exp);
            }

            return RedirectToAction("List");
        }

        // GET: Project/Delete/5
        public ActionResult DeAllocate(int? id)
        {
            if (!id.HasValue)
            {
                DisplayWarningMessage("Looks like, the Allocation ID is missing from your request");
                return RedirectToAction("List");
            }

            try
            {
                allocationService.EndAllocation(id.Value);
                DisplaySuccessMessage("The selected allocation has been closed successfully");
                return RedirectToAction("List");
            }
            catch (Exception exp)
            {
                DisplayDeleteErrorMessage(exp);
            }

            return RedirectToAction("List");
        }

        [HttpPost]
        public JsonResult GetProjectDetails(int projectID)
        {
            ProjectDto project = projectService.GetByID(projectID);
            JsonResult dat = Json(project);
            return dat;
        }

        [HttpPost]
        public JsonResult GetPercentageOfAllocation(int empID)
        {
            int val = allocationService.GetPercentageOfAllocation(empID);
            JsonResult res = Json(val);
            return res;
        }

        [HttpGet]
        public PartialViewResult EmployeeAllocations(int empID)
        {
            IEnumerable<CustomAllocationDto> projects = allocationService.GetAllocatedProjectsByEmployeeID(empID).ToList();
            IEnumerable<CustomAllocationModel> projectModels = Mapper.Map<IEnumerable<CustomAllocationDto>, IEnumerable<CustomAllocationModel>>(projects);
            return PartialView("EmployeeAllocations", projectModels);
        }

        [HttpPost]
        public JsonResult LoadFilterValueListItems(string filterType)
        {
            List<SelectListItem> filterValues = new List<SelectListItem>();
            switch (filterType.ToLower())
            {
                case "emp":
                    filterValues = GetEmployeesList();
                    break;
                case "prj":
                    filterValues = GetProjectsList();
                    break;
                case "pm":
                    filterValues = GetReportingManagersList();
                    break;
            }


            filterValues.Insert(0, new SelectListItem
            {
                Text = "Please Select",
                Value = "0",
            });
            Session["FilterValueListItemsAllocation"] = filterValues;
            return Json(filterValues);
        }

        #region Private Methods

        private IEnumerable<AllocationModel> GetAllocations(string filterType, int filterValueID, string sortBy, string sortType, int pageNo)
        {
            IEnumerable<ProjectAllocationDto> allocations = allocationService.GetAll(filterType, filterValueID, sortBy, sortType, RecordsPerPage, pageNo);
            IEnumerable<AllocationModel> projectModels = Mapper.Map<IEnumerable<ProjectAllocationDto>, IEnumerable<AllocationModel>>(allocations);
            return projectModels;
        }

        private IEnumerable<AllocationModel> GetAllocationsHistory(string filterType, int filterValue, string sortBy, string sortType, int pageNo)
        {
            IEnumerable<ProjectAllocationDto> allocations = allocationService.GetAllocationHistory(filterType, filterValue, sortBy, sortType, RecordsPerPage, pageNo);
            IEnumerable<AllocationModel> projectModels = Mapper.Map<IEnumerable<ProjectAllocationDto>, IEnumerable<AllocationModel>>(allocations);
            return projectModels;
        }

        private void InitializePageData()
        {
            ViewData["IsNewProject"] = true;
            GetOtherDropDownItems();
            ViewBag.EmployeeListItems = GetEmployeesList();
            ViewBag.ProjectListItems = GetProjectsList();
        }

        private void GetOtherDropDownItems()
        {
            IEnumerable<DropDownSubCategoryDto> buList = subCategoryService.GetSubCategories((int)CategoryType.UtilizationCode);

            List<SelectListItem> projectTypeItems = (from c in buList
                                                     orderby c.SubCategoryName
                                                     where c.CategoryID == (int)CategoryType.UtilizationCode
                                                     select new SelectListItem
                                                     {
                                                         Text = c.SubCategoryName,
                                                         Value = c.SubCategoryID.ToString()
                                                     }).ToList();

            ViewBag.ProjectTypeListItems = projectTypeItems;
        }

        private List<SelectListItem> GetEmployeesList()
        {
            List<EmployeeDto> employees = empService.GetAllEmployees("");

            List<SelectListItem> pmList = (from e in employees
                                           select new SelectListItem
                                           {
                                               Text = $"{e.FirstName} {e.LastName}",
                                               Value = e.EmployeeEntryID.ToString()
                                           }).OrderBy(i => i.Text).ToList();

            return pmList;
        }

        private List<SelectListItem> GetProjectsList()
        {
            IEnumerable<ProjectDto> projects = projectService.GetAll();

            List<SelectListItem> projectList = (from p in projects
                                                orderby p.ProjectCode
                                                select new SelectListItem
                                                {
                                                    Text = $"{p.ProjectCode}-{p.ProjectName}",
                                                    Value = p.ProjectID.ToString()
                                                }).OrderBy(i => i.Text).ToList();

            return projectList;
        }

        private bool IsValidAllocation(AllocationModel allocation)
        {
            ProjectDto project = projectService.GetByID(allocation.ProjectID);
            if (allocation.AllocationStartDate < project.StartDate)
            {
                DisplayWarningMessage("Allocation Start Date should be equal to or above the Project Start Date");
                return false;
            }

            if (allocation.AllocationEndDate > project.EndDate)
            {
                DisplayWarningMessage("Allocation End Date should be within the Project End Date");
                return false;
            }

            if (allocation.AllocationEndDate < allocation.AllocationStartDate || allocation.AllocationEndDate < project.StartDate)
            {
                DisplayWarningMessage("Allocation End Date should be within the range of Project Start & End Dates");
                return false;
            }

            EmployeeDto emp = empService.GetEmployee(allocation.EmployeeID);
            if (allocation.AllocationStartDate < emp.DateOfJoin)
            {
                DisplayWarningMessage($"Selected Employee's DoJ is {emp.DateOfJoin.ToString("MM/dd/yyyy")}. Allocation Start Date should be above that");
                return false;
            }

            return true;
        }

        private void InitializeListPage(AllocationViewModel viewModel)
        {
            viewModel.FilterTypeDropDownItems = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Employee",
                    Value = "emp"
                },
                new SelectListItem
                {
                    Text = "Project",
                    Value = "prj"
                },
                new SelectListItem
                {
                    Text = "Project Manager",
                    Value = "pm"
                },
            };

            if (string.IsNullOrEmpty(viewModel.FilterType) == false && Session["FilterValueListItemsAllocation"] != null)
            {
                viewModel.FilterValueDropDownItems = (List<SelectListItem>)Session["FilterValueListItemsAllocation"];
            }
        }

        private List<SelectListItem> GetReportingManagersList()
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

            return empDDList;
        }

        #endregion
    }
}
