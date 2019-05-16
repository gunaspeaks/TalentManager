using Agilisium.TalentManager.Dto;
using System.Collections.Generic;

namespace Agilisium.TalentManager.Service.Abstract
{
    public interface IPracticeService
    {
        bool Exists(string practiceName);

        bool Exists(int id);

        bool Exists(string practiceName, int id);

        IEnumerable<PracticeDto> GetPractices(int pageSize = -1, int pageNo = -1);

        PracticeDto GetPractice(int id);

        void CreatePractice(PracticeDto practice);

        void UpdatePractice(PracticeDto practice);

        void DeletePractice(PracticeDto practice);

        int TotalRecordsCount();

        bool CanBeDeleted(int id);

        bool IsReservedEntry(int id);

        string GetPracticeName(int practiceID);

        string GetManagerName(int practiceID);

        IEnumerable<PracticeDto> GetPracticesByBU(int buID);
    }
}
