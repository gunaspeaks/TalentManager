using Agilisium.TalentManager.Dto;
using Agilisium.TalentManager.Repository.Repositories;
using Agilisium.TalentManager.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agilisium.TalentManager.Service.Concreate
{
    public class SystemSettingsService : ISystemSettingsService
    {
        private readonly ISystemSettingRepository repository;

        public SystemSettingsService(ISystemSettingRepository repository)
        {
            this.repository = repository;
        }

        public string GetSystemSettingValue(string settingName)
        {
            return repository.GetSystemSettingValue(settingName);
        }

        public List<SystemSettingDto> GetAll()
        {
            return repository.GetAll().ToList();
        }
    }
}
