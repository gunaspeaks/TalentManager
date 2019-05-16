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
    public class ServiceRequestService : IServiceRequestService
    {
        private readonly IServiceRequestRepository repository;

        public ServiceRequestService(IServiceRequestRepository repository)
        {
            this.repository = repository;
        }

        public void Add(IEnumerable<ServiceRequestDto> serviceRequests)
        {
            repository.Add(serviceRequests);
        }

        public void Add(ServiceRequestDto entity)
        {
            repository.Add(entity);
        }

        public void Delete(ServiceRequestDto entity)
        {
            repository.Delete(entity);
        }

        public bool Exists(int id)
        {
            return repository.Exists(id);
        }

        public bool Exists(string itemName)
        {
            return repository.Exists(itemName);
        }

        public List<ServiceRequestDto> GetAll(int pageSize = -1, int pageNo = -1)
        {
            return repository.GetAll(pageSize, pageNo).ToList();
        }

        public ServiceRequestDto GetByID(int id)
        {
            return repository.GetByID(id);
        }

        public void Update(ServiceRequestDto entity)
        {
            repository.Delete(entity);
        }

        public int TotalRecordsCount()
        {
            return repository.TotalRecordsCount();
        }

        public List<ServiceRequestDto> GetAllEmailPendingRequests()
        {
            return repository.GetAllEmailPendingRequests().ToList();
        }

        public void UpdateEmailSentStatus(int requestID)
        {
            repository.UpdateEmailSentStatus(requestID);
        }
    }
}
