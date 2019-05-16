using Agilisium.TalentManager.Dto;
using Agilisium.TalentManager.Repository.Repositories;
using Agilisium.TalentManager.Service.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace Agilisium.TalentManager.Service.Concreate
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository repository;

        public ProjectService(IProjectRepository repository)
        {
            this.repository = repository;
        }

        public void Create(ProjectDto project)
        {
            repository.Add(project);
        }

        public void Delete(ProjectDto project)
        {
            repository.Delete(project);
        }

        public bool Exists(string projectName, int id)
        {
            return repository.Exists(id, projectName);
        }

        public bool Exists(string projectName)
        {
            return repository.Exists(projectName);
        }

        public bool Exists(int id)
        {
            return repository.Exists(id);
        }

        public IEnumerable<ProjectDto> GetAll(int pageSize = -1, int pageNo = -1)
        {
            return repository.GetAll(pageSize, pageNo).ToList();
        }

        public ProjectDto GetByID(int id)
        {
            return repository.GetByID(id);
        }

        public void Update(ProjectDto project)
        {
            repository.Update(project);
        }

        public bool IsDuplicateProjectCode(string projectCode)
        {
            return repository.IsDuplicateProjectCode(projectCode);
        }

        public bool IsDuplicateProjectCode(string projectCode, int projectID)
        {
            return repository.IsDuplicateProjectCode(projectCode, projectID);
        }

        public int TotalRecordsCount()
        {
            return repository.TotalRecordsCount();
        }

        public string GenerateProjectCode(int accountID)
        {
            return repository.GenerateProjectCode(accountID);
        }

        public List<ProjectDto> GetAll(string filterType, int filterValue, int pageSize = -1, int pageNo = -1)
        {
            return repository.GetAll(filterType, filterValue, pageSize, pageNo).ToList();
        }

        public int TotalRecordsCount(string filterType, int filterValue)
        {
            return repository.TotalRecordsCount(filterType, filterValue);
        }

        public bool IsReservedEntry(int projectID)
        {
            return repository.IsReservedEntry(projectID);
        }

        public List<ProjectDto> GetAllByManagerID(int managerID)
        {
            return repository.GetAllByManagerID(managerID).ToList();
        }
    }
}
