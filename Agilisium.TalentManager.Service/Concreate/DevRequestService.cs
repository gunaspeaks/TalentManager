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
    public class DevRequestService : IDevRequestService
    {
        private readonly IDevRequestRepository repository;

        public DevRequestService(IDevRequestRepository repository)
        {
            this.repository = repository;
        }

        public void Add(DevelopmentRequestDto entity)
        {
            repository.Add(entity);
        }

        public void Delete(DevelopmentRequestDto entity)
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

        public List<DevelopmentRequestDto> GetAll(int pageSize = -1, int pageNo = -1)
        {
            return repository.GetAll(pageSize, pageNo).ToList();
        }

        public List<DevelopmentRequestDto> GetAllByOwner(string ownerName)
        {
            return repository.GetAllByOwner(ownerName).ToList();
        }

        public DevelopmentRequestDto GetByID(int id)
        {
            return repository.GetByID(id);
        }

        public int TotalRecordsCount()
        {
            return repository.TotalRecordsCount();
        }

        public void Update(DevelopmentRequestDto entity)
        {
            repository.Update(entity);
        }
    }
}
