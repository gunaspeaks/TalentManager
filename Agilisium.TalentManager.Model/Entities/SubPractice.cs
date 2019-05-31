using System.ComponentModel.DataAnnotations;

namespace Agilisium.TalentManager.Model.Entities
{
    public class SubPractice : EntityBase
    {
        [Key]
        public int SubPracticeID { get; set; }

        public string SubPracticeName { get; set; }

        public string ShortName { get; set; }

        public int PracticeID { get; set; }

        public int? ManagerID { get; set; }
    }
}
