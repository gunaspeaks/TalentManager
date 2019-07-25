using Agilisium.TalentManager.Dto;
using Agilisium.TalentManager.Service.Abstract;
using Agilisium.TalentManager.Web.Helpers;
using Agilisium.TalentManager.Web.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Agilisium.TalentManager.Web.Controllers
{
    public class EmployeeController : BaseController
    {
        private readonly IEmployeeService empService;
        private readonly IDropDownSubCategoryService subCategoryService;
        private readonly IPracticeService practiceService;
        private readonly ISubPracticeService subPracticeService;
        private const string NO_VALID_VISA = "No Valid Visa";

        public EmployeeController(IEmployeeService empService,
            IDropDownSubCategoryService subCategoryService,
            IPracticeService practiceService,
            ISubPracticeService subPracticeService)
        {
            this.empService = empService;
            this.subCategoryService = subCategoryService;
            this.practiceService = practiceService;
            this.subPracticeService = subPracticeService;
        }

        // GET: Employee
        public ActionResult List(string searchText, int page = 1)
        {
            EmployeeViewModel viewModel = new EmployeeViewModel
            {
                SearchText = searchText
            };

            try
            {
                viewModel.PagingInfo = new PagingInfo
                {
                    TotalRecordsCount = empService.TotalRecordsCount(searchText),
                    RecordsPerPage = RecordsPerPage,
                    CurentPageNo = page
                };

                if (viewModel.PagingInfo.TotalRecordsCount > 0)
                {
                    viewModel.Employees = GetEmployees(searchText, page);
                }
                else
                {
                    DisplayWarningMessage("There are no Employees to display");
                }
            }
            catch (Exception exp)
            {
                DisplayLoadErrorMessage(exp);
            }

            return View(viewModel);
        }

        // GET: Employee
        public ActionResult PastEmployeeList(int page = 1)
        {
            EmployeeViewModel viewModel = new EmployeeViewModel();

            try
            {
                viewModel.PagingInfo = new PagingInfo
                {
                    TotalRecordsCount = empService.GetPastEmployeesCount(),
                    RecordsPerPage = RecordsPerPage,
                    CurentPageNo = page
                };

                if (viewModel.PagingInfo.TotalRecordsCount > 0)
                {
                    viewModel.Employees = GetPastEmployees(page);
                }
                else
                {
                    DisplayWarningMessage("There are no Employees to display");
                }
            }
            catch (Exception exp)
            {
                DisplayLoadErrorMessage(exp);
            }

            return View(viewModel);
        }

        // GET: Employee
        public ActionResult PracticeWiseList(int pid, int page = 1)
        {
            EmployeeViewModel viewModel = new EmployeeViewModel
            {
                SearchText = "",
                PID = pid
            };

            try
            {
                viewModel.PagingInfo = new PagingInfo
                {
                    TotalRecordsCount = empService.PracticeWiseRecordsCount(pid),
                    RecordsPerPage = RecordsPerPage,
                    CurentPageNo = page
                };

                if (viewModel.PagingInfo.TotalRecordsCount > 0)
                {
                    viewModel.Employees = GetPracticeWiseEmployees(pid, page);
                }
                else
                {
                    DisplayWarningMessage("There are no Employees to display");
                }
            }
            catch (Exception exp)
            {
                DisplayLoadErrorMessage(exp);
            }

            return View(viewModel);
        }

        // GET: Employee
        public ActionResult SubPracticeWiseList(int sid, int page = 1)
        {
            EmployeeViewModel viewModel = new EmployeeViewModel
            {
                SearchText = "",
                SID = sid
            };

            try
            {
                viewModel.PagingInfo = new PagingInfo
                {
                    TotalRecordsCount = empService.SubPracticeWiseRecordsCount(sid),
                    RecordsPerPage = RecordsPerPage,
                    CurentPageNo = page
                };

                if (viewModel.PagingInfo.TotalRecordsCount > 0)
                {
                    viewModel.Employees = GetSubPracticeWiseEmployees(sid, page);
                }
                else
                {
                    DisplayWarningMessage("There are no Employees to display");
                }
            }
            catch (Exception exp)
            {
                DisplayLoadErrorMessage(exp);
            }

            return View(viewModel);
        }

        // GET: Employe/Create
        public ActionResult Create()
        {
            EmployeeModel emp = new EmployeeModel()
            {
                DateOfJoin = DateTime.Now,
            };

            try
            {
                InitializePageData(-1);
                emp.VisaCategoryID = int.Parse(ViewBag.NoVisaCategoryID);
            }
            catch (Exception exp)
            {
                DisplayLoadErrorMessage(exp);
            }

            return View(emp);
        }

        // POST: Employe/Create
        [HttpPost]
        public ActionResult Create(EmployeeModel employee)
        {
            try
            {
                InitializePageData(-1);

                if (ModelState.IsValid)
                {
                    if (empService.IsDuplicateName(employee.FirstName, employee.LastName))
                    {
                        DisplayWarningMessage("There is already an Employee with the same First and Last Name");
                        return View(employee);
                    }

                    if (empService.IsDuplicateEmployeeID(employee.EmployeeID))
                    {
                        DisplayWarningMessage("This Employee ID is already exists");
                        return View(employee);
                    }

                    EmployeeDto employeeDto = Mapper.Map<EmployeeModel, EmployeeDto>(employee);
                    empService.Create(employeeDto);
                    DisplaySuccessMessage("New Employee details have been stored successfully");
                    return RedirectToAction("List");
                }
            }
            catch (Exception exp)
            {
                DisplayUpdateErrorMessage(exp);
            }
            return View(employee);
        }

        // GET: Employe/Edit/5
        public ActionResult Edit(int? id)
        {
            EmployeeModel empModel = new EmployeeModel();
            InitializePageData(empModel.PracticeID, id ?? -1);

            if (!id.HasValue)
            {
                DisplayWarningMessage("Looks like, the employee ID is missing in your request");
                return View(empModel);
            }

            try
            {

                if (!empService.Exists(id.Value))
                {
                    DisplayWarningMessage($"Sorry, we couldn't find the Employee with ID: {id.Value}");
                    return View(empModel);
                }

                EmployeeDto emp = empService.GetEmployee(id.Value);
                GetSubPracticeList(emp.PracticeID);
                empModel = Mapper.Map<EmployeeDto, EmployeeModel>(emp);
                if(String.IsNullOrWhiteSpace(empModel.TravelledCountries))
                {
                    empModel.TravelledCountries = "None";
                }

                if (empModel.VisaCategoryID.HasValue == false)
                {
                    empModel.VisaCategoryID = int.Parse(ViewBag.NoVisaCategoryID);
                }
            }
            catch (Exception exp)
            {
                DisplayReadErrorMessage(exp);
            }
            return View(empModel);
        }

        public ActionResult View(int? id)
        {
            EmployeeModel empModel = new EmployeeModel();

            if (!id.HasValue)
            {
                DisplayWarningMessage("Looks like, the employee ID is missing in your request");
                return View(empModel);
            }

            try
            {

                if (!empService.Exists(id.Value))
                {
                    DisplayWarningMessage($"Sorry, we couldn't find the Employee with ID: {id.Value}");
                    return View(empModel);
                }

                EmployeeDto emp = empService.GetEmployee(id.Value);
                empModel = Mapper.Map<EmployeeDto, EmployeeModel>(emp);
            }
            catch (Exception exp)
            {
                DisplayReadErrorMessage(exp);
            }
            return View(empModel);
        }

        // POST: Employe/Edit/5
        [HttpPost]
        public ActionResult Edit(EmployeeModel employee)
        {
            try
            {
                InitializePageData(employee.PracticeID, employee.EmployeeEntryID);
                if (ModelState.IsValid)
                {
                    if (empService.IsDuplicateName(employee.EmployeeEntryID, employee.FirstName, employee.LastName))
                    {
                        DisplayWarningMessage("There is already an Employee with the same First and Last Name");
                        return View(employee);
                    }

                    if (empService.IsDuplicateEmployeeID(employee.EmployeeEntryID, employee.EmployeeID))
                    {
                        DisplayWarningMessage("This Employee ID is already exists");
                        return View(employee);
                    }

                    EmployeeDto employeeDto = Mapper.Map<EmployeeModel, EmployeeDto>(employee);
                    empService.Update(employeeDto);
                    DisplaySuccessMessage("Employee details have been Updated successfully");
                    return RedirectToAction("List");
                }
            }
            catch (Exception exp)
            {
                DisplayUpdateErrorMessage(exp);
            }
            return View(employee);
        }

        // GET: Employe/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                DisplayWarningMessage("Looks like, the employee ID is missing in your request");
                return RedirectToAction("List");
            }

            try
            {
                empService.Delete(new EmployeeDto { EmployeeEntryID = id.Value });
                DisplaySuccessMessage("Employee details have been deleted successfully");
                return RedirectToAction("List");
            }
            catch (Exception exp)
            {
                DisplayDeleteErrorMessage(exp);
                return RedirectToAction("List");
            }
        }

        public ActionResult ChangeReportingManager()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetSubPracticeList(int id)
        {
            return Json(GetSubPractices(id));
        }

        [HttpPost]
        public JsonResult GenerateNewEmployeeID(int id)
        {
            return Json(empService.GenerateNewEmployeeID(id));
        }

        [HttpPost]
        public JsonResult GetEmployeeDetails(int id)
        {
            EmployeeDto emp = empService.GetEmployee(id);
            return Json(emp);
        }

        public FileStreamResult DownloadAllEmployees(string filterType, string filterValue)
        {
            StringBuilder recordString = new StringBuilder($"Employee ID,Employee Name,Employee Type,Business Unit,POD,Competency,Date of Join,Last Working Day,Primary Skills,Secondary Skills,Reporting Manager{Environment.NewLine}");
            try
            {
                List<EmployeeDto> employees = empService.GetAllEmployees("");
                foreach (EmployeeDto dto in employees)
                {
                    recordString.Append($"{dto.EmployeeID},");
                    recordString.Append($"{dto.FirstName} {dto.LastName},");
                    recordString.Append($"{dto.EmploymentTypeName},");
                    recordString.Append($"{dto.BusinessUnitName},");
                    recordString.Append($"{dto.PracticeName},");
                    recordString.Append($"{dto.SubPracticeName},");
                    recordString.Append($"{dto.DateOfJoin.ToString("dd/MMM/yyyy")},");
                    recordString.Append($"{dto.LastWorkingDay?.ToString("dd/MMM/yyyy")},");
                    recordString.Append($"{dto.PrimarySkills},");
                    recordString.Append($"{dto.SecondarySkills},");
                    recordString.Append($"{dto.ReportingManagerName}{Environment.NewLine}");
                }

            }
            catch (Exception exp)
            {
                DisplayLoadErrorMessage(exp);
            }
            byte[] byteArr = Encoding.ASCII.GetBytes(recordString.ToString());
            MemoryStream stream = new MemoryStream(byteArr);
            return File(stream, "application/vnd.ms-excel", $"Employees As On{DateTime.Now.Year - DateTime.Now.Month - DateTime.Now.Day}.csv");
        }

        private IEnumerable<EmployeeModel> GetEmployees(string searchText, int pageNo = 1)
        {
            IEnumerable<EmployeeDto> employees = empService.GetAllEmployees(searchText, RecordsPerPage, pageNo);
            IEnumerable<EmployeeModel> employeeModels = Mapper.Map<IEnumerable<EmployeeDto>, IEnumerable<EmployeeModel>>(employees);

            return employeeModels;
        }

        private IEnumerable<EmployeeModel> GetPastEmployees(int pageNo)
        {
            IEnumerable<EmployeeDto> employees = empService.GetAllPastEmployees(RecordsPerPage, pageNo);
            IEnumerable<EmployeeModel> employeeModels = Mapper.Map<IEnumerable<EmployeeDto>, IEnumerable<EmployeeModel>>(employees);

            return employeeModels;
        }

        private IEnumerable<EmployeeModel> GetSubPracticeWiseEmployees(int subPracticeID, int pageNo = 1)
        {
            IEnumerable<EmployeeDto> employees = empService.GetAllBySubPractice(subPracticeID, RecordsPerPage, pageNo);
            IEnumerable<EmployeeModel> employeeModels = Mapper.Map<IEnumerable<EmployeeDto>, IEnumerable<EmployeeModel>>(employees);

            return employeeModels;
        }

        private IEnumerable<EmployeeModel> GetPracticeWiseEmployees(int practiceID, int pageNo = 1)
        {
            IEnumerable<EmployeeDto> employees = empService.GetAllByPractice(practiceID, RecordsPerPage, pageNo);
            IEnumerable<EmployeeModel> employeeModels = Mapper.Map<IEnumerable<EmployeeDto>, IEnumerable<EmployeeModel>>(employees);

            return employeeModels;
        }

        private void InitializePageData(int subPracticeID = -1, int employeeID = -1)
        {
            ViewBag.IsNewEntry = true;
            GetPracticeList();
            GetReportingManagersList(employeeID);
            GetSubPractices(subPracticeID);
            GetOtherDropDownItems();
        }

        private void GetOtherDropDownItems()
        {
            List<DropDownSubCategoryDto> buList = subCategoryService.GetAll().ToList();

            List<SelectListItem> buListItems = (from c in buList
                                                orderby c.SubCategoryName
                                                where c.CategoryID == (int)CategoryType.BusinessUnit
                                                select new SelectListItem
                                                {
                                                    Text = c.SubCategoryName,
                                                    Value = c.SubCategoryID.ToString()
                                                }).ToList();

            ViewBag.BuListItems = buListItems;

            List<SelectListItem> ucListItems = (from c in buList
                                                orderby c.SubCategoryName
                                                where c.CategoryID == (int)CategoryType.UtilizationCode
                                                select new SelectListItem
                                                {
                                                    Text = c.SubCategoryName,
                                                    Value = c.SubCategoryID.ToString()
                                                }).ToList();

            ViewBag.UtilizationTypeListItems = ucListItems;

            List<SelectListItem> empTypeList = (from c in buList
                                                orderby c.SubCategoryName
                                                where c.CategoryID == (int)CategoryType.EmploymentType
                                                select new SelectListItem
                                                {
                                                    Text = c.SubCategoryName,
                                                    Value = c.SubCategoryID.ToString()
                                                }).ToList();

            ViewBag.EmploymentTypeListItems = empTypeList;

            List<SelectListItem> visaListItems = (from c in buList
                                                  orderby c.SubCategoryName
                                                  where c.CategoryID == (int)CategoryType.VisaCategory
                                                  select new SelectListItem
                                                  {
                                                      Text = c.SubCategoryName,
                                                      Value = c.SubCategoryID.ToString()
                                                  }).ToList();
            ViewBag.NoVisaCategoryID = visaListItems.FirstOrDefault(i => i.Text == NO_VALID_VISA).Value;
            ViewBag.VisaCategoryListItems = visaListItems;

            List<SelectListItem> gradeListItems = new List<SelectListItem>();
            for (int i = 10; i >= 1; i--)
            {
                gradeListItems.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            }
            ViewBag.GradeListItems = gradeListItems;

        }

        private void GetPracticeList()
        {
            List<PracticeDto> practices = practiceService.GetPractices().ToList();
            List<SelectListItem> practiceItems = (from p in practices
                                                  orderby p.PracticeName
                                                  select new SelectListItem
                                                  {
                                                      Text = p.PracticeName,
                                                      Value = p.PracticeID.ToString()
                                                  }).ToList();
            ViewBag.PracticeListItems = practiceItems;
        }

        private List<SelectListItem> GetSubPractices(int practiceID)
        {
            IEnumerable<SubPracticeDto> subPracticeList = subPracticeService.GetAllByPracticeID(practiceID);
            List<SelectListItem> ddList = (from c in subPracticeList
                                           orderby c.SubPracticeName
                                           select new SelectListItem
                                           {
                                               Text = c.SubPracticeName,
                                               Value = c.SubPracticeID.ToString()
                                           }).ToList();

            ViewBag.SubPracticeListItems = ddList;
            return ddList;
        }

        private void GetReportingManagersList(int employeeID)
        {
            List<EmployeeDto> managers = empService.GetAllManagers();

            List<SelectListItem> empDDList = new List<SelectListItem>();

            if (managers != null)
            {
                empDDList = (from e in managers
                             where e.EmployeeEntryID != employeeID
                             select new SelectListItem
                             {
                                 Text = $"{e.FirstName} {e.LastName}",
                                 Value = e.EmployeeEntryID.ToString()
                             }).OrderBy(i => i.Text).ToList();
            }

            ViewBag.ReportingManagerListItems = empDDList;
        }
    }
}
