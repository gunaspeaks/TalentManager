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
    public class AccountsController : BaseController
    {
        private readonly IProjectAccountService accountsService;
        private readonly IDropDownSubCategoryService subCategoryService;
        private readonly IEmployeeService empService;

        public AccountsController(IProjectAccountService practiceService, IDropDownSubCategoryService subCategoryService,
            IEmployeeService empService)
        {
            this.accountsService = practiceService;
            this.subCategoryService = subCategoryService;
            this.empService = empService;
        }

        // GET: Accounts
        public ActionResult List(int page = 1)
        {
            ProjectAccountViewModel viewModel = new ProjectAccountViewModel();

            try
            {
                viewModel.PagingInfo = new PagingInfo
                {
                    TotalRecordsCount = accountsService.TotalRecordsCount(),
                    RecordsPerPage = RecordsPerPage,
                    CurentPageNo = page
                };

                if (viewModel.PagingInfo.TotalRecordsCount > 0)
                {
                    viewModel.ProjectAccounts = GetAllAccounts(page);
                }
                else
                {
                    DisplayWarningMessage("There are no Accounts to display");
                }
            }
            catch (Exception exp)
            {
                DisplayLoadErrorMessage(exp);
            }

            return View(viewModel);
        }

        // GET: Accounts/Create
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

            return View(new ProjectAccountModel());
        }

        // POST: Accounts/Create
        [HttpPost]
        public ActionResult Create(ProjectAccountModel account)
        {
            try
            {
                InitializePageData();

                if (ModelState.IsValid)
                {
                    if (accountsService.Exists(account.AccountName))
                    {
                        DisplayWarningMessage($"This Account Name '{account.AccountName}' is duplicate");
                        return View(account);
                    }

                    if (accountsService.IsDuplicateShortName(account.ShortName))
                    {
                        DisplayWarningMessage($"This Short Name '{account.ShortName}' is duplicate");
                        return View(account);
                    }

                    ProjectAccountDto accountModel = Mapper.Map<ProjectAccountModel, ProjectAccountDto>(account);
                    accountsService.Add(accountModel);
                    DisplaySuccessMessage($"New Account '{account.AccountName}' has been stored successfully");
                    return RedirectToAction("List");
                }
            }
            catch (Exception exp)
            {
                DisplayLoadErrorMessage(exp);
            }
            return View(account);
        }

        // GET: Accounts/Edit/5
        public ActionResult Edit(int? id)
        {
            ProjectAccountModel account = new ProjectAccountModel();

            InitializePageData();

            try
            {
                if (!id.HasValue)
                {
                    DisplayWarningMessage("Looks like, the ID is missing in your request");
                    return RedirectToAction("List");
                }

                if (!accountsService.Exists(id.Value))
                {
                    DisplayWarningMessage($"Sorry, We couldn't find the Account with ID: {id.Value}");
                    return RedirectToAction("List");
                }

                ProjectAccountDto accountDto = accountsService.GetByID(id.Value);
                account = Mapper.Map<ProjectAccountDto, ProjectAccountModel>(accountDto);
            }
            catch (Exception exp)
            {
                DisplayReadErrorMessage(exp);
            }

            return View(account);
        }

        // POST: Accounts/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ProjectAccountModel account)
        {
            try
            {
                InitializePageData();

                if (ModelState.IsValid)
                {
                    if (accountsService.Exists(account.AccountName, account.AccountID))
                    {
                        DisplayWarningMessage($"ProjectAccount Name '{account.AccountName}' is duplicate");
                        return View(account);
                    }

                    if (accountsService.IsDuplicateShortName(account.AccountID, account.ShortName))
                    {
                        DisplayWarningMessage($"This Short Name '{account.ShortName}' is duplicate");
                        return View(account);
                    }

                    ProjectAccountDto accountModel = Mapper.Map<ProjectAccountModel, ProjectAccountDto>(account);
                    accountsService.Update(accountModel);
                    DisplaySuccessMessage($"Account '{account.AccountName}' details have been modified successfully");
                    return RedirectToAction("List");
                }
            }
            catch (Exception exp)
            {
                DisplayUpdateErrorMessage(exp);
            }
            return View(account);
        }

        // POST: Accounts/Delete/5
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
                if (accountsService.CanBeDeleted(id.Value) == false)
                {
                    DisplayWarningMessage("There are some dependencies with this Account. So, you can't delete this for now");
                    return RedirectToAction("List");
                }

                accountsService.Delete(new ProjectAccountDto { AccountID = id.Value });
                DisplaySuccessMessage("Account has been deleted successfully");
            }
            catch (Exception exp)
            {
                DisplayDeleteErrorMessage(exp);
            }
            return RedirectToAction("List");
        }

        private IEnumerable<ProjectAccountModel> GetAllAccounts(int pageNo)
        {
            IEnumerable<ProjectAccountDto> accounts = accountsService.GetAll(RecordsPerPage, pageNo);
            IEnumerable<ProjectAccountModel> acctModels = Mapper.Map<IEnumerable<ProjectAccountDto>, IEnumerable<ProjectAccountModel>>(accounts);
            return acctModels;
        }

        private void InitializePageData()
        {
            List<EmployeeDto> managers = empService.GetAllAccountManagers();
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
            LoadCountriesList();
        }

        private void LoadCountriesList()
        {
            IEnumerable<DropDownSubCategoryDto> buList = subCategoryService.GetSubCategories((int)CategoryType.Country);

            List<SelectListItem> buListItems = (from c in buList
                                                orderby c.SubCategoryName
                                                where c.CategoryID == (int)CategoryType.Country
                                                select new SelectListItem
                                                {
                                                    Text = c.SubCategoryName,
                                                    Value = c.SubCategoryID.ToString()
                                                }).ToList();

            ViewBag.CountryListItems = buListItems;
        }
    }
}
