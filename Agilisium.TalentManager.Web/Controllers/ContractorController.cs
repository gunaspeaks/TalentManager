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
    public class ContractorController : BaseController
    {
        private readonly IContractorService contractService;
        private readonly IProjectService projectService;
        private readonly IEmployeeService empService;
        private readonly IDropDownSubCategoryService subCategoryService;
        private readonly IVendorService vendorService;

        public ContractorController(IContractorService contractService, IProjectService projectService,
            IEmployeeService empService, IDropDownSubCategoryService subCategoryService,
            IVendorService vendorService)
        {
            this.contractService = contractService;
            this.projectService = projectService;
            this.empService = empService;
            this.subCategoryService = subCategoryService;
            this.vendorService = vendorService;
        }

        // GET: Contracts
        public ActionResult List(int page = 1)
        {
            ContractorViewModel viewModel = new ContractorViewModel();

            try
            {
                viewModel.PagingInfo = new PagingInfo
                {
                    TotalRecordsCount = contractService.TotalRecordsCount(),
                    RecordsPerPage = RecordsPerPage,
                    CurentPageNo = page
                };

                if (viewModel.PagingInfo.TotalRecordsCount > 0)
                {
                    viewModel.Contractors = GetAllContractors(page);
                }
                else
                {
                    DisplayWarningMessage("There are no Contractors to display");
                }
            }
            catch (Exception exp)
            {
                DisplayLoadErrorMessage(exp);
            }

            return View(viewModel);
        }

        public ActionResult Create()
        {
            ContractorModel model = new ContractorModel
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(30)
            };

            try
            {
                InitializePageData();
            }
            catch (Exception exp)
            {
                DisplayLoadErrorMessage(exp);
            }

            return View(model);
        }

        public ActionResult CIndex()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult ContractorsDashboard()
        {
            var model = new ContractorWidgetModel();

            try
            {
                model.ActiveContractors = contractService.GetActiveContractorsCount();
            }
            catch (Exception exp) { }

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Create(ContractorModel contractor)
        {
            try
            {
                InitializePageData();

                if (ModelState.IsValid)
                {
                    if (contractService.IsDuplicateContractorName(contractor.ContractorName))
                    {
                        DisplayWarningMessage("There is already a Contractor with the same Name");
                        return View(contractor);
                    }

                    ContractorDto contractorDto = Mapper.Map<ContractorModel, ContractorDto>(contractor);
                    contractService.CreateNewContractor(contractorDto);
                    DisplaySuccessMessage("New Contractor details have been stored successfully");
                    return RedirectToAction("List");
                }
            }
            catch (Exception exp)
            {
                DisplayUpdateErrorMessage(exp);
            }
            return View(contractor);
        }


        // GET: Employe/Edit/5
        public ActionResult Edit(int? id)
        {
            ContractorModel contractorModel = new ContractorModel();
            InitializePageData();

            if (!id.HasValue)
            {
                DisplayWarningMessage("Looks like, the Contractor ID is missing in your request");
                return View(contractorModel);
            }

            try
            {

                if (!contractService.Exists(id.Value))
                {
                    DisplayWarningMessage($"Sorry, we couldn't find the Employee with ID: {id.Value}");
                    return View(contractorModel);
                }

                ContractorDto contractor = contractService.GetByID(id.Value);
                contractorModel = Mapper.Map<ContractorDto, ContractorModel>(contractor);
            }
            catch (Exception exp)
            {
                DisplayReadErrorMessage(exp);
            }
            return View(contractorModel);
        }

        // POST: Employe/Edit/5
        [HttpPost]
        public ActionResult Edit(ContractorModel contractor)
        {
            try
            {
                InitializePageData();
                if (ModelState.IsValid)
                {
                    if (contractService.IsDuplicateContractorName(contractor.ContractorID, contractor.ContractorName))
                    {
                        DisplayWarningMessage("There is already a Contractor with the same Name");
                        return View(contractor);
                    }

                    ContractorDto contractorDto = Mapper.Map<ContractorModel, ContractorDto>(contractor);
                    contractService.UpdateContractor(contractorDto);
                    DisplaySuccessMessage("Contractor details have been Updated successfully");
                    return RedirectToAction("List");
                }
            }
            catch (Exception exp)
            {
                DisplayUpdateErrorMessage(exp);
            }
            return View(contractor);
        }

        // GET: Employe/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                DisplayWarningMessage("Looks like, the ID is missing in your request");
                return RedirectToAction("List");
            }

            try
            {
                contractService.Delete(new ContractorDto { ContractorID = id.Value });
                DisplaySuccessMessage("Contractor details have been deleted successfully");
                return RedirectToAction("List");
            }
            catch (Exception exp)
            {
                DisplayDeleteErrorMessage(exp);
                return RedirectToAction("List");
            }
        }

        private List<ContractorModel> GetAllContractors(int pageNo = 1)
        {
            List<ContractorDto> contracts = contractService.GetAll(RecordsPerPage, pageNo);
            List<ContractorModel> contractModels = Mapper.Map<List<ContractorDto>, List<ContractorModel>>(contracts);

            return contractModels;
        }

        private void InitializePageData()
        {
            GetProjectsList();
            GetContractPeriodList();
            GetVendorsList();
        }

        private void GetProjectsList()
        {
            List<ProjectDto> projects = projectService.GetAll().ToList();

            List<SelectListItem> projectList = (from p in projects
                                                select new SelectListItem
                                                {
                                                    Text = p.ProjectName,
                                                    Value = p.ProjectID.ToString()
                                                }).OrderBy(p=>p.Text).ToList();

            ViewBag.ProjectListItems = projectList;
        }

        private void GetVendorsList()
        {
            List<VendorDto> vendors = vendorService.GetAllVendors().ToList();

            List<SelectListItem> vendorList = (from p in vendors
                                               select new SelectListItem
                                               {
                                                   Text = p.VendorName,
                                                   Value = p.VendorID.ToString()
                                               }).ToList();

            ViewBag.VendorListItems = vendorList;
        }

        private void GetContractPeriodList()
        {
            List<DropDownSubCategoryDto> buList = subCategoryService.GetSubCategories((int)CategoryType.ContractPeriod).ToList();

            List<SelectListItem> contractPeriodItems = (from c in buList
                                                        orderby c.SubCategoryID
                                                        select new SelectListItem
                                                        {
                                                            Text = c.SubCategoryName,
                                                            Value = c.SubCategoryID.ToString()
                                                        }).ToList();

            ViewBag.ContractPeriodListItems = contractPeriodItems;
        }

        private void GetManagersList()
        {
            List<EmployeeDto> employees = empService.GetAllManagers().ToList();

            List<SelectListItem> employeeList = (from p in employees
                                                 select new SelectListItem
                                                 {
                                                     Text = p.LastName + ", " + p.FirstName,
                                                     Value = p.EmployeeEntryID.ToString()
                                                 }).ToList();

            ViewBag.ManagerListItems = employeeList;
        }

    }
}