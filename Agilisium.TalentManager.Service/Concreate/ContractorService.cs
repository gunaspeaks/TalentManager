using Agilisium.TalentManager.Dto;
using Agilisium.TalentManager.Repository.Repositories;
using Agilisium.TalentManager.Service.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace Agilisium.TalentManager.Service.Concreate
{
    public class ContractorService : IContractorService
    {
        private IContractorRepository repository;

        public ContractorService(IContractorRepository repository)
        {
            this.repository = repository;
        }

        public int TotalRecordsCount()
        {
            return repository.TotalRecordsCount();
        }

        public List<ContractorDto> GetAll(int pageSize = -1, int pageNo = -1)
        {
            return repository.GetAll(pageSize, pageNo).ToList();
        }

        public void CreateNewContractor(ContractorDto contractor)
        {
            repository.Add(contractor);
        }

        public void UpdateContractor(ContractorDto contractor)
        {
            repository.Update(contractor);
        }

        public void Delete(ContractorDto contractor)
        {
            repository.Delete(contractor);
        }

        public bool IsDuplicateContractorName(string contractorName)
        {
            return repository.Exists(contractorName);
        }

        public bool IsDuplicateContractorName(int contractorID, string contractorName)
        {
            return repository.Exists(contractorName, contractorID);
        }

        public bool Exists(int contractorID)
        {
            return repository.Exists(contractorID);
        }

        public ContractorDto GetByID(int contractorID)
        {
            return repository.GetByID(contractorID);
        }

        public int GetActiveContractorsCount()
        {
            return repository.GetActiveContractorsCount();
        }
    }
}
