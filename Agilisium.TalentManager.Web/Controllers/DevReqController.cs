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
    public class DevReqController : BaseController
    {
        private readonly IDropDownSubCategoryService subCategoryService;
        private readonly IDevRequestService requestService;

        public DevReqController(IDropDownSubCategoryService subCategoryService, IDevRequestService requestService)
        {
            this.subCategoryService = subCategoryService;
            this.requestService = requestService;
        }

        // GET: DevReq
        public ActionResult List(int page = 1)
        {
            DevelopmentRequestViewModel viewModel = new DevelopmentRequestViewModel();

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
                    viewModel.DevelopmentRequests = GetRequests();
                }
                else
                {
                    DisplayWarningMessage("There are no Requests to display");
                }
            }
            catch (Exception exp)
            {
                DisplayLoadErrorMessage(exp);
            }

            return View(viewModel);
        }

        // GET: DevReq/Create
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

            return View(new DevelopmentRequestModel());
        }

        // POST: DevReq/Create
        [HttpPost]
        public ActionResult Create(DevelopmentRequestModel request)
        {
            try
            {
                InitializePageData();

                if (ModelState.IsValid)
                {
                    DevelopmentRequestDto requestModel = Mapper.Map<DevelopmentRequestModel, DevelopmentRequestDto>(request);
                    requestModel.RequestedBy = LoggedInUserName;
                    requestService.Add(requestModel);
                    DisplaySuccessMessage("New Request has been stored successfully");
                    return RedirectToAction("List");
                }
            }
            catch (Exception exp)
            {
                DisplayLoadErrorMessage(exp);
            }
            return View(request);
        }

        // GET: DevReq/Edit/5
        public ActionResult Edit(int? requestID)
        {
            DevelopmentRequestModel request = new DevelopmentRequestModel();

            InitializePageData();

            try
            {
                if (!requestID.HasValue)
                {
                    DisplayWarningMessage("Looks like, the ID is missing in your request");
                    return RedirectToAction("List");
                }

                if (!requestService.Exists(requestID.Value))
                {
                    DisplayWarningMessage($"Sorry, We couldn't find the Request with ID: {requestID.Value}");
                    return RedirectToAction("List");
                }

                DevelopmentRequestDto reqDto = requestService.GetByID(requestID.Value);
                request = Mapper.Map<DevelopmentRequestDto, DevelopmentRequestModel>(reqDto);
            }
            catch (Exception exp)
            {
                DisplayReadErrorMessage(exp);
            }

            return View(request);
        }

        // POST: DevReq/Edit/5
        [HttpPost]
        public ActionResult Edit(int requestID, DevelopmentRequestModel request)
        {
            try
            {
                InitializePageData();

                if (ModelState.IsValid)
                {
                    DevelopmentRequestDto reqModel = Mapper.Map<DevelopmentRequestModel, DevelopmentRequestDto>(request);
                    reqModel.RequestID = requestID;
                    requestService.Update(reqModel);
                    DisplaySuccessMessage("Request details have been modified successfully");
                    return RedirectToAction("List");
                }
            }
            catch (Exception exp)
            {
                DisplayUpdateErrorMessage(exp);
            }
            return View(request);
        }

        // POST: DevReq/Delete/5
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
                requestService.Delete(new DevelopmentRequestDto { RequestID = id.Value });
                DisplaySuccessMessage("Request has been deleted successfully");
            }
            catch (Exception exp)
            {
                DisplayDeleteErrorMessage(exp);
            }
            return RedirectToAction("List");
        }

        private IEnumerable<DevelopmentRequestModel> GetRequests()
        {
            DevelopmentRequestModel model = new DevelopmentRequestModel();
            IEnumerable<DevelopmentRequestDto> requests = null;
            if (LoggedInUserName.ToLower().Contains(AdminUserName.ToLower()))
            {
                requests = requestService.GetAll();
            }
            else
            {
                requests = requestService.GetAllByOwner(LoggedInUserName);
            }
            IEnumerable<DevelopmentRequestModel> requestModels = Mapper.Map<IEnumerable<DevelopmentRequestDto>, IEnumerable<DevelopmentRequestModel>>(requests);
            return requestModels;
        }

        private void InitializePageData()
        {
            LoadListItems();
        }

        private void LoadListItems()
        {
            IEnumerable<DropDownSubCategoryDto> ddlItems = subCategoryService.GetAll();

            List<SelectListItem> priorityItems = (from c in ddlItems
                                                  orderby c.SubCategoryName
                                                  where c.CategoryID == (int)CategoryType.DevelopmentPriority
                                                  select new SelectListItem
                                                  {
                                                      Text = c.SubCategoryName,
                                                      Value = c.SubCategoryID.ToString()
                                                  }).ToList();

            List<SelectListItem> statusItems = (from c in ddlItems
                                                orderby c.SubCategoryName
                                                where c.CategoryID == (int)CategoryType.DevelopmentRequestStatus
                                                select new SelectListItem
                                                {
                                                    Text = c.SubCategoryName,
                                                    Value = c.SubCategoryID.ToString()
                                                }).ToList();

            List<SelectListItem> typeItems = (from c in ddlItems
                                              orderby c.SubCategoryName
                                              where c.CategoryID == (int)CategoryType.DevelopmentRequestType
                                              select new SelectListItem
                                              {
                                                  Text = c.SubCategoryName,
                                                  Value = c.SubCategoryID.ToString()
                                              }).ToList();

            ViewBag.PriorityListItems = priorityItems;
            ViewBag.StatusListItems = statusItems;
            ViewBag.RequestTypeListItems = typeItems;
        }
    }
}
