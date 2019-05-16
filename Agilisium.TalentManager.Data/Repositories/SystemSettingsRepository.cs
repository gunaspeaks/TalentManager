using Agilisium.TalentManager.Repository.Abstract;
using Agilisium.TalentManager.Dto;
using Agilisium.TalentManager.Model.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Agilisium.TalentManager.Repository.Repositories
{
    public class SystemSettingRepository : RepositoryBase<SystemSetting>, ISystemSettingRepository
    {
        public void Add(SystemSettingDto entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(SystemSettingDto entity)
        {
            throw new System.NotImplementedException();
        }

        public bool Exists(string itemName)
        {
            return Entities.Any(e => e.SettingName == itemName);
        }

        public bool Exists(int id)
        {
            return true;
        }

        public IEnumerable<SystemSettingDto> GetAll(int pageSize = -1, int pageNo = -1)
        {
            return (from e in Entities
                    where e.IsDeleted == false
                    select new SystemSettingDto
                    {
                        SettingEntryID = e.SettingEntryID,
                        SettingName = e.SettingName,
                        SettingValue = e.SettingValue
                    });
        }

        public SystemSettingDto GetByID(int id)
        {
            return (from e in Entities
                    where e.IsDeleted == false
                    select new SystemSettingDto
                    {
                        SettingEntryID = e.SettingEntryID,
                        SettingName = e.SettingName,
                        SettingValue = e.SettingValue
                    }).FirstOrDefault();
        }

        public string GetSystemSettingValue(string settingName)
        {
            return Entities.FirstOrDefault(e => e.SettingName == settingName)?.SettingValue;
        }

        public void Update(SystemSettingDto entity)
        {
            throw new System.NotImplementedException();
        }
    }

    public interface ISystemSettingRepository : IRepository<SystemSettingDto>
    {
        string GetSystemSettingValue(string settingName);
    }
}
