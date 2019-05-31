using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Agilisium.TalentManager.Model.Entities
{
    public class ProjectAllocation : EntityBase
    {
        [Key]
        public int AllocationEntryID { get; set; }

        public int EmployeeID { get; set; }

        public DateTime AllocationStartDate { get; set; }

        public DateTime AllocationEndDate { get; set; }

        public int AllocationTypeID { get; set; }

        public int ProjectID { get; set; }

        public string Remarks { get; set; }

        [DefaultValue(0)]
        public int PercentageOfAllocation { get; set; }

        [DefaultValue(true)]
        public bool IsActive { get; set; }
    }
}
