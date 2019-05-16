using Agilisium.TalentManager.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agilisium.TalentManager.Service.Abstract
{
    public interface IProjectAccountService
    {
        void Add(ProjectAccountDto account);

        void Delete(ProjectAccountDto account);

        bool Exists(string accountName);

        bool Exists(int id);

        bool Exists(string accountName, int accountID);

        List<ProjectAccountDto> GetAll(int pageSize = -1, int pageNo = -1);

        ProjectAccountDto GetByID(int accountID);

        bool IsDuplicateName(int accountID, string accountName);

        void Update(ProjectAccountDto entity);

        int TotalRecordsCount();

        bool CanBeDeleted(int accountID);

        bool IsDuplicateShortName(string shortName);

        bool IsDuplicateShortName(int accountID, string shortName);
    }
}
