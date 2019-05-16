using Agilisium.TalentManager.Dto;
using Agilisium.TalentManager.Service.Abstract;
using Agilisium.TalentManager.Web.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Agilisium.TalentManager.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeService empService;

        public HomeController(IEmployeeService empService)
        {
            this.empService = empService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [ChildActionOnly]
        public ActionResult EmployeesDashboard()
        {
            EmployeeWidgetModel model = new EmployeeWidgetModel();

            try
            {
                EmployeeWidgetDto dto = empService.GetEmployeesCountSummary();
                model = Mapper.Map<EmployeeWidgetDto, EmployeeWidgetModel>(dto);
            }
            catch (Exception) { }
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult AllocationsDashboard()
        {
            return PartialView(new List<AllocationWidgetModel>());
        }

        [ChildActionOnly]
        public ActionResult PracticeHeadCount()
        {
            List<PracticeHeadCountModel> headCountModel = new List<PracticeHeadCountModel>();
            try
            {
                List<PracticeHeadCountDto> headCount = empService.GetPracticeWiseHeadCount();
                headCountModel = Mapper.Map<List<PracticeHeadCountDto>, List<PracticeHeadCountModel>>(headCount);
            }
            catch (Exception) { }
            return PartialView(headCountModel);
        }

        public ActionResult SubPracticeHeadCount()
        {
            List<SubPracticeHeadCountDto> subHeadCount = empService.GetSubPracticeWiseHeadCount();

            List<IGrouping<string, SubPracticeHeadCountDto>> groupedItems = (from s in subHeadCount
                                                                             group s by s.Practice into sg
                                                                             select sg).ToList();

            List<SubPracticeHeadCountModel> records = new List<SubPracticeHeadCountModel>();
            for (int i = 0; i < groupedItems.Count; i++)
            {
                SubPracticeHeadCountModel subPractice = new SubPracticeHeadCountModel
                {
                    Practice = groupedItems[i].Key,
                    HeadCount = 0
                };
                IOrderedEnumerable<SubPracticeHeadCountDto> items = groupedItems[i].ToList().OrderBy(p => p.SubPractice);
                foreach (SubPracticeHeadCountDto item in items)
                {
                    subPractice.HeadCount += item.HeadCount;
                    subPractice.SubPractices.Add(new SubPracticeWiseCountModel
                    {
                        HeadCount = item.HeadCount,
                        SubPractice = item.SubPractice ?? "Un-Assigned",
                        SubPracticeID = item.SubPracticeID
                    });
                }

                records.Add(subPractice);
            }

            List<SubPracticeHeadCountModel> headCountModel = new List<SubPracticeHeadCountModel>();
            try
            {
                headCountModel = Mapper.Map<List<SubPracticeHeadCountDto>, List<SubPracticeHeadCountModel>>(subHeadCount);
            }
            catch (Exception) { }
            return View(records);
        }
    }
}