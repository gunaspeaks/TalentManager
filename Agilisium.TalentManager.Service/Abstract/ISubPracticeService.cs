using Agilisium.TalentManager.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agilisium.TalentManager.Service.Abstract
{
    public interface ISubPracticeService
    {
        bool Exists(string subPracticeName, int practiceID);

        bool Exists(int id);

        bool Exists(string subPracticeName, int id, int practiceID);

        IEnumerable<SubPracticeDto> GetSubPractices(int pageSize = -1, int pageNo = -1);

        IEnumerable<SubPracticeDto> GetAllByPracticeID(int practiceID, int pageSize = -1, int pageNo = -1);

        SubPracticeDto GetByID(int subPracticeID);

        void CreateSubPractice(SubPracticeDto subPractice);

        void UpdateSubPractice(SubPracticeDto subPractice);

        void DeleteSubPractice(SubPracticeDto subPractice);

        int TotalRecordsCount();

        int TotalRecordsCountByPracticeID(int practiceID);

        bool CanBeDeleted(int id);

        string GetManagerName(int subPracticeID);
    }
}
