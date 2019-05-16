using Agilisium.TalentManager.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agilisium.TalentManager.Service.Abstract
{
    public interface IContractorService
    {
        int TotalRecordsCount();

        List<ContractorDto> GetAll(int pageSize = -1, int pageNo = -1);

        void CreateNewContractor(ContractorDto contractor);

        void UpdateContractor(ContractorDto contractor);

        void Delete(ContractorDto contractor);

        bool IsDuplicateContractorName(string contractorName);

        bool IsDuplicateContractorName(int contractorID, string contractorName);

        bool Exists(int contractorID);

        ContractorDto GetByID(int contractorID);

        int GetActiveContractorsCount();
    }
}
