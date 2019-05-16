using Agilisium.TalentManager.Dto;
using Agilisium.TalentManager.Repository.Repositories;
using Agilisium.TalentManager.Service.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace Agilisium.TalentManager.Service.Concreate
{
    public class ProjectAccountService : IProjectAccountService
    {
        private readonly IProjectAccountRepository repository;

        public ProjectAccountService(IProjectAccountRepository repository)
        {
            this.repository = repository;
        }

        public void Add(ProjectAccountDto account)
        {
            repository.Add(account);
        }

        public void Delete(ProjectAccountDto account)
        {
            repository.Delete(account);
        }

        public bool Exists(string accountName)
        {
            return repository.Exists(accountName);
        }

        public bool Exists(int id)
        {
            return repository.Exists(id);
        }

        public List<ProjectAccountDto> GetAll(int pageSize = -1, int pageNo = -1)
        {
            return repository.GetAll(pageSize, pageNo).ToList();
        }

        public ProjectAccountDto GetByID(int accountID)
        {
            return repository.GetByID(accountID);
        }

        public bool IsDuplicateName(int accountID, string accountName)
        {
            return repository.IsDuplicateName(accountID, accountName);
        }

        public void Update(ProjectAccountDto entity)
        {
            repository.Update(entity);
        }

        public int TotalRecordsCount()
        {
            return repository.TotalRecordsCount();
        }

        public bool Exists(string accountName, int accountID)
        {
            return repository.IsDuplicateName(accountID, accountName);
        }

        public bool CanBeDeleted(int accountID)
        {
            return repository.CanBeDeleted(accountID);
        }

        public bool IsDuplicateShortName(string shortName)
        {
            return repository.IsDuplicateShortName(shortName);
        }

        public bool IsDuplicateShortName(int accountID, string shortName)
        {
            return repository.IsDuplicateShortName(accountID, shortName);
        }
    }
}
