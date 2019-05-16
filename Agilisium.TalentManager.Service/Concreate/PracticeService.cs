using Agilisium.TalentManager.Repository.Repositories;
using Agilisium.TalentManager.Dto;
using Agilisium.TalentManager.Service.Abstract;
using System.Collections.Generic;

namespace Agilisium.TalentManager.Service.Concreate
{
    public class PracticeService : IPracticeService
    {
        private readonly IPracticeRepository repository;

        public PracticeService(IPracticeRepository repository)
        {
            this.repository = repository;
        }

        public void CreatePractice(PracticeDto practice)
        {
            repository.Add(practice);
        }

        public void DeletePractice(PracticeDto practice)
        {
            repository.Delete(practice);
        }

        public bool Exists(string practice)
        {
            return repository.Exists(practice);
        }

        public bool Exists(int id)
        {
            return repository.Exists(id);
        }

        public bool Exists(string practiceName, int id)
        {
            return repository.Exists(practiceName, id);
        }

        public IEnumerable<PracticeDto> GetPractices(int pageSize = -1, int pageNo = -1)
        {
            return repository.GetAll(pageSize, pageNo);
        }

        public PracticeDto GetPractice(int id)
        {
            return repository.GetByID(id);
        }

        public void UpdatePractice(PracticeDto practice)
        {
            repository.Update(practice);
        }

        public int TotalRecordsCount()
        {
            return repository.TotalRecordsCount();
        }

        public bool CanBeDeleted(int id)
        {
            return repository.CanBeDeleted(id);
        }

        public bool IsReservedEntry(int id)
        {
            return repository.IsReservedEntry(id);
        }

        public string GetPracticeName(int practiceID)
        {
            return repository.GetPracticeName(practiceID);
        }

        public string GetManagerName(int practiceID)
        {
            return repository.GetManagerName(practiceID);
        }

        public IEnumerable<PracticeDto> GetPracticesByBU(int buID)
        {
            return repository.GetPracticesByBU(buID);
        }
    }
}
