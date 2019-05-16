using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agilisium.TalentManager.Model.Entities
{
    public class SystemSetting : EntityBase
    {
        public int SettingEntryID { get; set; }

        public string SettingName { get; set; }

        public string SettingValue { get; set; }
    }
}
