using Agilisium.TalentManager.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agilisium.TalentManager.Service.Abstract
{
    public interface IServiceRequestService
    {
        void Add(ServiceRequestDto entity);

        void Add(IEnumerable<ServiceRequestDto> serviceRequests);

        void Delete(ServiceRequestDto entity);

        bool Exists(int id);

        bool Exists(string itemName);

        List<ServiceRequestDto> GetAll(int pageSize = -1, int pageNo = -1);

        ServiceRequestDto GetByID(int id);

        void Update(ServiceRequestDto entity);

        int TotalRecordsCount();

        List<ServiceRequestDto> GetAllEmailPendingRequests();

        void UpdateEmailSentStatus(int requestID);
    }
}
