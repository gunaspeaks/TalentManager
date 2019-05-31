using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Agilisium.TalentManager.Web.Models
{
    public class EmployeeViewModel : ViewModelBase
    {
        public EmployeeViewModel()
        {
            Employees = new List<EmployeeModel>();
        }

        public IEnumerable<EmployeeModel> Employees { get; set; }

        public PagingInfo PagingInfo { get; set; }

        public string SearchText { get; set; }

        public int PID { get; set; }

        public int SID { get; set; }
    }
}