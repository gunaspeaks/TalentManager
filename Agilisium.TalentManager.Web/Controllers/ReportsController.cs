using Agilisium.TalentManager.Dto;
using Agilisium.TalentManager.Service.Abstract;
using Agilisium.TalentManager.Web.Helpers;
using Agilisium.TalentManager.Web.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Agilisium.TalentManager.Web.Controllers
{
    public class ReportsController : BaseController
    {
        private readonly IAllocationService allocationService;
        private readonly IProjectService projectService;

        public ReportsController(IAllocationService allocationService, IProjectService projectService)
        {
            this.allocationService = allocationService;
            this.projectService = projectService;
        }

        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ManagerWiseAllocations()
        {
            List<ManagerWiseAllocationModel> managerSummary = new List<ManagerWiseAllocationModel>();
            try
            {
                List<ManagerWiseAllocationDto> summaryDtos = allocationService.GetManagerWiseAllocationSummary();
                managerSummary = Mapper.Map<List<ManagerWiseAllocationDto>, List<ManagerWiseAllocationModel>>(summaryDtos);
            }
            catch (Exception exp)
            {
                DisplayLoadErrorMessage(exp);
            }
            return View(managerSummary);
        }

        public ActionResult ManagerWiseProjects(int id)
        {
            ProjectViewModel model = new ProjectViewModel();
            try
            {
                List<ProjectDto> summaryDtos = projectService.GetAllByManagerID(id);
                model.Projects = Mapper.Map<List<ProjectDto>, List<ProjectModel>>(summaryDtos);
            }
            catch (Exception exp)
            {
                DisplayLoadErrorMessage(exp);
            }
            return View(model);
        }

        public ActionResult BillabilityWiseAllocationSummary()
        {
            List<BillabilityWiseAllocationSummaryModel> allocations = new List<BillabilityWiseAllocationSummaryModel>();
            try
            {
                List<BillabilityWiseAllocationSummaryDto> summaryDtos = allocationService.GetBillabilityWiseAllocationSummary();
                allocations = Mapper.Map<List<BillabilityWiseAllocationSummaryDto>, List<BillabilityWiseAllocationSummaryModel>>(summaryDtos);
            }
            catch (Exception exp)
            {
                DisplayLoadErrorMessage(exp);
            }
            return View(allocations);
        }

        public ActionResult BillabilityWiseAllocationDetail(int id)
        {
            List<BillabilityWiseAllocationDetailModel> allocations = new List<BillabilityWiseAllocationDetailModel>();
            try
            {
                List<BillabilityWiseAllocationDetailDto> summaryDtos = allocationService.GetBillabilityWiseAllocationDetail(id);
                allocations = Mapper.Map<List<BillabilityWiseAllocationDetailDto>, List<BillabilityWiseAllocationDetailModel>>(summaryDtos);
            }
            catch (Exception exp)
            {
                DisplayLoadErrorMessage(exp);
            }
            return View(allocations);
        }
    }
}