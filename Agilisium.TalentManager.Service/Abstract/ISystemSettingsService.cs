using Agilisium.TalentManager.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agilisium.TalentManager.Service.Abstract
{
    public interface ISystemSettingsService
    {
        string GetSystemSettingValue(string settingName);

        List<SystemSettingDto> GetAll();
    }
}
