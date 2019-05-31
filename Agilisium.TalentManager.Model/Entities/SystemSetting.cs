using System.ComponentModel.DataAnnotations;

namespace Agilisium.TalentManager.Model.Entities
{
    public class SystemSetting : EntityBase
    {
        [Key]
        public int SettingEntryID { get; set; }

        public string SettingName { get; set; }

        public string SettingValue { get; set; }
    }
}
