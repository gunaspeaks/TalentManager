using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agilisium.TalentManager.Dto
{
    public class UtilizedDaysDetailDto
    {
        public int EmployeeEntryID { get; set; }

        public string EmployeeID { get; set; }

        public string EmployeeName { get; set; }

        public int PracticeID { get; set; }

        public string PracticeName { get; set; }

        public DateTime From { get; set; }

        public DateTime Upto { get; set; }
    }
}
