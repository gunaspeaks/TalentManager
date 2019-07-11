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
    public class VendorController : BaseController
    {
        private readonly IVendorService vendorService;
        private readonly IDropDownSubCategoryService subCategoryService;
        private readonly IServiceRequestService requestService;
        private readonly ISystemSettingsService settingsService;

        public VendorController(IVendorService vendorService,
            IDropDownSubCategoryService subCategoryService,
            IServiceRequestService requestService, ISystemSettingsService settingsService)
        {
            this.vendorService = vendorService;
            this.subCategoryService = subCategoryService;
            this.requestService = requestService;
            this.settingsService = settingsService;
        }

        // GET: Vendor
        public ActionResult List(int page = 1)
        {
            VendorViewModel viewModel = new VendorViewModel();

            try
            {
                viewModel.PagingInfo = new PagingInfo
                {
                    TotalRecordsCount = vendorService.TotalRecordsCount(),
                    RecordsPerPage = RecordsPerPage,
                    CurentPageNo = page
                };

                if (viewModel.PagingInfo.TotalRecordsCount > 0)
                {
                    viewModel.Vendors = GetVendors(page);
                }
                else
                {
                    DisplayWarningMessage("There are no Vendors to display");
                }
            }
            catch (Exception exp)
            {
                DisplayLoadErrorMessage(exp);
            }

            return View(viewModel);
        }

        public ActionResult VIndex()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult VendorsDashboard()
        {
            List<VendorSpecializedPartnerModel> result = new List<VendorSpecializedPartnerModel>();
            try
            {
                List<VendorSpecializedPartnerWto> wtoResult = vendorService.GetVendorSpecialityPartnersList();
                result = Mapper.Map< List<VendorSpecializedPartnerWto>, List<VendorSpecializedPartnerModel>>(wtoResult);
            }
            catch (Exception exp) { }
            return PartialView(result);
        }

        public ActionResult Search(string by)
        {
            VendorSearchResultViewModel viewModel = new VendorSearchResultViewModel();

            try
            {
                if (string.IsNullOrEmpty(by))
                {
                    DisplayWarningMessage("Hey, I am not sure about what you are searching for!");
                    return View(viewModel);
                }

                string[] filters = by.Split(',');
                bool anyValidText = filters.Any(s => s.Length > 0);
                if (anyValidText == false)
                {
                    DisplayWarningMessage("Hey, I am not sure about what you are searching for!");
                    return View(viewModel);
                }

                VendorSearchResultDto resultDto = vendorService.SearchVendors(by);
                viewModel = Mapper.Map<VendorSearchResultDto, VendorSearchResultViewModel>(resultDto);
            }
            catch (Exception exp)
            {
                DisplayLoadErrorMessage(exp);
            }

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult PrepareServiceRequests(string emailMessage, IEnumerable<VendorModel> matchingVendors)
        {
            List<ServiceRequestModel> requests = new List<ServiceRequestModel>();

            if (matchingVendors == null ||
                (matchingVendors != null && matchingVendors.Count() == 0))
            {
                DisplayWarningMessage("Looks like, there are no records to display");
                return RedirectToAction("Search");
            }

            if (matchingVendors.Any(r => r.IsSelected == true) == false)
            {
                DisplayWarningMessage("Please select one or more Vendors from search result");
                return RedirectToAction("Search");
            }

            foreach (VendorModel item in matchingVendors)
            {
                if (item.IsSelected == false)
                {
                    continue;
                }

                requests.Add(new ServiceRequestModel
                {
                    EmailMessage = emailMessage,
                    RequestedDate = DateTime.Now,
                    RequestedSkill = item.RequestedSkill,
                    VendorID = item.VendorID,
                    VendorName = item.VendorName
                });
            }

            return View("prepare", requests);
        }

        [HttpPost]
        public ActionResult SendRequests(IEnumerable<ServiceRequestModel> serviceRequests)
        {
            if (serviceRequests == null || (serviceRequests != null && serviceRequests.Count() == 0))
            {
                DisplayWarningMessage("There are no requests to process.");
                return View();
            }

            try
            {
                List<ServiceRequestDto> requests = Mapper.Map<IEnumerable<ServiceRequestModel>, List<ServiceRequestDto>>(serviceRequests);
                requestService.Add(requests);

                string templatePath = EmailTemplatesFolderPath + "ContractorRequestEmailTemplate.html";
                //ContractorRequestProcessor emailProcessor = new ContractorRequestProcessor(requestService, settingsService);
                //emailProcessor.ProcessPendingServiceRequests(templatePath);

                return RedirectToAction("List", "ServiceRequest");
            }
            catch (Exception exp)
            {
                DisplayUpdateErrorMessage(exp);
            }

            return View();
        }

        // GET: Employe/Create
        public ActionResult Create()
        {
            VendorModel emp = new VendorModel();

            try
            {
                InitializePageData();
            }
            catch (Exception exp)
            {
                DisplayLoadErrorMessage(exp);
            }

            return View(emp);
        }

        // POST: Employe/Create
        [HttpPost]
        public ActionResult Create(VendorModel vendor)
        {
            try
            {
                InitializePageData();

                if (ModelState.IsValid)
                {
                    if (vendorService.Exists(vendor.VendorName))
                    {
                        DisplayWarningMessage("There is already a Vendor with the same Name");
                        return View(vendor);
                    }

                    VendorDto VendorDto = Mapper.Map<VendorModel, VendorDto>(vendor);
                    vendorService.Create(VendorDto);
                    DisplaySuccessMessage("New Vendor details have been stored successfully");
                    return RedirectToAction("List");
                }
            }
            catch (Exception exp)
            {
                DisplayUpdateErrorMessage(exp);
            }
            return View(vendor);
        }

        // GET: Employe/Edit/5
        public ActionResult Edit(int? id)
        {
            VendorModel empModel = new VendorModel();
            InitializePageData();

            if (!id.HasValue)
            {
                DisplayWarningMessage("Looks like, the Vendor ID is missing in your request");
                return View(empModel);
            }

            try
            {

                if (!vendorService.Exists(id.Value))
                {
                    DisplayWarningMessage($"Sorry, we couldn't find the Vendor with ID: {id.Value}");
                    return View(empModel);
                }

                VendorDto emp = vendorService.GetVendor(id.Value);
                empModel = Mapper.Map<VendorDto, VendorModel>(emp);
            }
            catch (Exception exp)
            {
                DisplayReadErrorMessage(exp);
            }
            return View(empModel);
        }

        // POST: Employe/Edit/5
        [HttpPost]
        public ActionResult Edit(VendorModel vendor)
        {
            try
            {
                InitializePageData();
                if (ModelState.IsValid)
                {
                    if (vendorService.IsDuplicateName(vendor.VendorID, vendor.VendorName))
                    {
                        DisplayWarningMessage("There is already an Vendor with the same Name");
                        return View(vendor);
                    }

                    VendorDto VendorDto = Mapper.Map<VendorModel, VendorDto>(vendor);
                    vendorService.Update(VendorDto);
                    DisplaySuccessMessage("Vendor details have been Updated successfully");
                    return RedirectToAction("List");
                }
            }
            catch (Exception exp)
            {
                DisplayUpdateErrorMessage(exp);
            }
            return View(vendor);
        }

        // GET: Employe/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                DisplayWarningMessage("Looks like, the Vendor ID is missing in your request");
                return RedirectToAction("List");
            }

            try
            {
                vendorService.Delete(new VendorDto { VendorID = id.Value });
                DisplaySuccessMessage("Vendor details have been deleted successfully");
                return RedirectToAction("List");
            }
            catch (Exception exp)
            {
                DisplayDeleteErrorMessage(exp);
                return RedirectToAction("List");
            }
        }

        private List<VendorModel> GetVendors(int pageNo = 1)
        {
            List<VendorDto> vendors = vendorService.GetAllVendors(RecordsPerPage, pageNo).ToList();
            List<VendorModel> vendorModels = Mapper.Map<List<VendorDto>, List<VendorModel>>(vendors);

            return vendorModels;
        }

        private void InitializePageData()
        {
            ViewBag.IsNewEntry = true;
            GetOtherDropDownItems();
        }

        private void GetOtherDropDownItems()
        {
            IEnumerable<DropDownSubCategoryDto> buList = subCategoryService.GetSubCategories((int)CategoryType.SpecializedPartner);

            List<SelectListItem> partnerListItems = (from c in buList
                                                     orderby c.SubCategoryName
                                                     select new SelectListItem
                                                     {
                                                         Text = c.SubCategoryName,
                                                         Value = c.SubCategoryID.ToString()
                                                     }).ToList();

            ViewBag.PartnerListItems = partnerListItems;
        }
    }
}
