using Agilisium.TalentManager.Dto;
using System.Collections.Generic;

namespace Agilisium.TalentManager.Service.Abstract
{
    public interface IProjectService
    {
        void Create(ProjectDto project);

        void Update(ProjectDto project);

        void Delete(ProjectDto project);

        bool Exists(string projectName, int id);

        bool Exists(string itemName);

        bool Exists(int id);

        IEnumerable<ProjectDto> GetAll(int pageSize = -1, int pageNo = -1);

        ProjectDto GetByID(int id);

        bool IsDuplicateProjectCode(string projectCode);

        bool IsDuplicateProjectCode(string projectCode, int projectID);

        int TotalRecordsCount();

        string GenerateProjectCode(int accountID);

        List<ProjectDto> GetAll(string filterType, int filterValue, int pageSize = -1, int pageNo = -1);

        int TotalRecordsCount(string filterType, int filterValue);

        bool IsReservedEntry(int projectID);

        List<ProjectDto> GetAllByManagerID(int managerID);
    }
}
