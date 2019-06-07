using Agilisium.TalentManager.Dto;
using System.Collections.Generic;

namespace Agilisium.TalentManager.Service.Abstract
{
    public interface IEmployeeService
    {
        List<EmployeeDto> GetAllEmployees(string searchText, int pageSize = 0, int pageNo = -1);

        EmployeeDto GetEmployee(int id);

        bool Exists(int employeeEntryID);

        void Create(EmployeeDto employee);

        void Update(EmployeeDto employee);

        void Delete(EmployeeDto employee);

        bool IsDuplicateName(string firstName, string lastName);

        bool IsDuplicateName(int employeeEntryID, string firstName, string lastName);

        bool IsDuplicateEmployeeID(string employeeID);

        bool IsDuplicateEmployeeID(int employeeEntryID, string employeeID);

        string GenerateNewEmployeeID(int trackerID);

        List<EmployeeDto> GetAllManagers();

        List<EmployeeDto> GetAllAccountManagers();

        int TotalRecordsCount(string searchText);

        IEnumerable<EmployeeDto> GetAllPastEmployees(int pageSize = -1, int pageNo = -1);

        int GetPastEmployeesCount();

        List<PracticeHeadCountDto> GetPracticeWiseHeadCount();

        List<SubPracticeHeadCountDto> GetSubPracticeWiseHeadCount();

        List<EmployeeDto> GetAllByPractice(int practiceID, int pageSize = -1, int pageNo = -1);

        List<EmployeeDto> GetAllBySubPractice(int subPracticeID, int pageSize = -1, int pageNo = -1);

        int PracticeWiseRecordsCount(int practiceID);

        int SubPracticeWiseRecordsCount(int subPracticeID);

        ResourceCountDto GetEmployeesCountSummary();

        string GetNameByEmployeeID(string empID);

        BillabilityWiseResourceCountDto GetBillabilityCountSummary();
    }
}
