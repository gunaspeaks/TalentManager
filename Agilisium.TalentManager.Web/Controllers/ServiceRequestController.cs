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
    public class ServiceRequestController : BaseController
    {
        private readonly IServiceRequestService requestService;
        private readonly IDropDownSubCategoryService subCategoryService;

        public ServiceRequestController(IServiceRequestService requestRequest,
            IDropDownSubCategoryService subCategoryService)
        {
            this.requestService = requestRequest;
            this.subCategoryService = subCategoryService;
        }

        // GET: ServiceRequest
        public ActionResult List(int page = 1)
        {
            ServiceRequestViewModel viewModel = new ServiceRequestViewModel();

            try
            {
                viewModel.PagingInfo = new PagingInfo
                {
                    TotalRecordsCount = requestService.TotalRecordsCount(),
                    RecordsPerPage = RecordsPerPage,
                    CurentPageNo = page
                };

                if (viewModel.PagingInfo.TotalRecordsCount > 0)
                {
                    viewModel.ServiceRequests = GetServiceRequests(page);
                }
                else
                {
                    DisplayWarningMessage("There are no Service Requests to display");
                }
            }
            catch (Exception exp)
            {
                DisplayLoadErrorMessage(exp);
            }

            return View(viewModel);
        }

        // GET: ServiceRequest/Edit/5
        public ActionResult Edit(int? id)
        {
            ServiceRequestModel request = new ServiceRequestModel();

            InitializePageData();

            try
            {
                if (!id.HasValue)
                {
                    DisplayWarningMessage("Looks like, the ID is missing in your request");
                    return RedirectToAction("List");
                }

                if (!requestService.Exists(id.Value))
                {
                    DisplayWarningMessage($"Sorry, We couldn't find the Service Request with ID: {id.Value}");
                    return RedirectToAction("List");
                }

                ServiceRequestDto requestDto = requestService.GetByID(id.Value);
                request = Mapper.Map<ServiceRequestDto, ServiceRequestModel>(requestDto);
            }
            catch (Exception exp)
            {
                DisplayReadErrorMessage(exp);
            }

            return View(request);
        }

        // POST: ServiceRequest/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ServiceRequestModel practice)
        {
            try
            {
                InitializePageData();

                if (ModelState.IsValid)
                {
                    ServiceRequestDto practiceDto = Mapper.Map<ServiceRequestModel, ServiceRequestDto>(practice);
                    requestService.Update(practiceDto);
                    DisplaySuccessMessage("Request status has been modified successfully");
                    return RedirectToAction("List");
                }
            }
            catch (Exception exp)
            {
                DisplayUpdateErrorMessage(exp);
            }
            return View(practice);
        }

        private void InitializePageData()
        {
            List<DropDownSubCategoryDto> managers = subCategoryService.GetSubCategories((int)CategoryType.ServiceRequestType).ToList();
            List<SelectListItem> empDDList = new List<SelectListItem>();

            if (managers != null)
            {
                empDDList = (from e in managers
                             select new SelectListItem
                             {
                                 Text = e.SubCategoryName,
                                 Value = e.SubCategoryID.ToString()
                             }).ToList();
            }

            ViewBag.RequestStatusListItems = empDDList;
        }

        private List<ServiceRequestModel> GetServiceRequests(int page)
        {
            List<ServiceRequestDto> requestsDto = requestService.GetAll(RecordsPerPage, page);
            List<ServiceRequestModel> requests = Mapper.Map<List<ServiceRequestDto>, List<ServiceRequestModel>>(requestsDto);
            return requests;
        }
    }
}
