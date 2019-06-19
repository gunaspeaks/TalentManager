using System.Configuration;

namespace Agilisium.TalentManager.PostgresDbHelper
{
    public static class PostgresSqlQueries
    {

        public static string CONNECTION_STRING = ConfigurationManager.ConnectionStrings["TalentDataContextPostgres"].ConnectionString;

        public static string GET_ALLOCATION_DETAIL_FOR_VALID_ALLOCATIONS = "SELECT DISTINCT \"E\".\"EmployeeEntryID\", \"E\".\"EmployeeID\", CONCAT(\"E\".\"FirstName\", ' ', \"E\".\"LastName\") AS \"EmployeeName\","
                    + "\"E\".\"PrimarySkills\", \"E\".\"SecondarySkills\", \"PA\".\"AllocationTypeID\", \"DS\".\"SubCategoryName\" AS \"AllocationType\","
                    + "\"P\".\"BusinessUnitID\", \"BU\".\"SubCategoryName\" AS \"BusinessUnit\","
                    + "\"P\".\"PracticeID\", \"POD\".\"PracticeName\" AS \"POD\","
                    + "\"PA\".\"AllocationStartDate\", \"PA\".\"AllocationEndDate\", \"P\".\"ProjectID\", \"P\".\"ProjectName\", \"ACC\".\"AccountName\", "
                    + "\"P\".\"ProjectManagerID\", CONCAT(\"PM\".\"FirstName\", ' ', \"PM\".\"LastName\") AS \"ProjectManager\", \"PA\".\"Remarks\""
                    + " FROM \"TalentManager\".\"Employee\" AS \"E\""
                    + " LEFT OUTER JOIN \"TalentManager\".\"ProjectAllocation\" AS \"PA\" ON \"PA\".\"EmployeeID\" = \"E\".\"EmployeeEntryID\""
                    + " LEFT OUTER JOIN \"TalentManager\".\"Project\" AS \"P\" ON \"P\".\"ProjectID\" = \"PA\".\"ProjectID\""
                    + " LEFT OUTER JOIN \"TalentManager\".\"ProjectAccount\" AS \"ACC\" ON \"ACC\".\"AccountID\" = \"P\".\"ProjectAccountID\""
                    + " LEFT OUTER JOIN \"TalentManager\".\"Employee\" AS \"PM\" ON \"PM\".\"EmployeeEntryID\" = \"P\".\"ProjectManagerID\""
                    + " LEFT OUTER JOIN \"TalentManager\".\"DropDownSubCategory\" AS \"DS\" ON \"DS\".\"SubCategoryID\" = \"PA\".\"AllocationTypeID\""
                    + " LEFT OUTER JOIN \"TalentManager\".\"DropDownSubCategory\" AS \"BU\" ON \"BU\".\"SubCategoryID\" = \"P\".\"BusinessUnitID\""
                    + " LEFT OUTER JOIN \"TalentManager\".\"Practice\" AS \"POD\" ON \"POD\".\"PracticeID\" = \"P\".\"PracticeID\""
                    + " WHERE \"E\".\"IsDeleted\" = False AND \"PA\".\"IsDeleted\" = False AND \"E\".\"LastWorkingDay\" IS NULL"
                    + " AND \"PA\".\"AllocationTypeID\" = __ALLOCATION_TYPE_ID__ AND \"PA\".\"AllocationEndDate\" >= '__CURRENT_DATE__'"
                    + "ORDER BY 3";
        public static string GET_ALLOCATION_DETAIL_FOR_ALL_NON_ALLOCATED_DELIVERY_BU_RESOURCES = "SELECT DISTINCT \"E\".\"EmployeeEntryID\", \"E\".\"EmployeeID\", CONCAT(\"E\".\"FirstName\",' ', \"E\".\"LastName\") AS \"EmployeeName\","
                    + "\"E\".\"PrimarySkills\", \"E\".\"SecondarySkills\", -1 AS \"AllocationTypeID\", 'Not Allocated Yet (Delivery)' AS \"AllocationType\","
                    + "\"E\".\"BusinessUnitID\", \"BU\".\"SubCategoryName\" AS \"BusinessUnit\","
                    + "\"E\".\"PracticeID\", \"POD\".\"PracticeName\" AS \"POD\","
                    + "NULL AS \"AllocationStartDate\", NULL AS \"AllocationEndDate\", NUll AS \"ProjectID\", NUll AS \"ProjectName\", NUll AS \"AccountName\", "
                    + "NUll AS \"ProjectManagerID\", NUll AS \"ProjectManager\", ' ' AS \"Remarks\""
                    + " FROM \"TalentManager\".\"Employee\" AS \"E\""
                    + " LEFT OUTER JOIN \"TalentManager\".\"Employee\" AS \"PM\" ON \"PM\".\"EmployeeEntryID\" = \"E\".\"ReportingManagerID\""
                    + " LEFT OUTER JOIN \"TalentManager\".\"DropDownSubCategory\" AS \"BU\" ON \"BU\".\"SubCategoryID\" = 3"
                    + " LEFT OUTER JOIN \"TalentManager\".\"Practice\" AS \"POD\" ON \"POD\".\"PracticeID\" = \"E\".\"PracticeID\" "
                    + " WHERE \"E\".\"IsDeleted\" = False AND \"E\".\"BusinessUnitID\" = 3 AND \"E\".\"LastWorkingDay\" IS NULL"
                    + " AND(SELECT COUNT(1) FROM \"TalentManager\".\"ProjectAllocation\" WHERE \"EmployeeID\" = \"E\".\"EmployeeEntryID\" AND \"IsDeleted\"=False AND \"AllocationEndDate\" >= '__CURRENT_DATE__') = 0"
                    + " ORDER BY 3";
        public static string GET_ALLOCATION_DETAIL_FOR_ALL_NON_ALLOCATED_OTHER_BU_RESOURCES = "SELECT DISTINCT \"E\".\"EmployeeEntryID\", \"E\".\"EmployeeID\", CONCAT(\"E\".\"FirstName\",' ', \"E\".\"LastName\") AS \"EmployeeName\","
                    + "\"E\".\"PrimarySkills\", \"E\".\"SecondarySkills\", -2 AS \"AllocationTypeID\", 'Not Allocated Yet (BO/BD)' AS \"AllocationType\","
                    + "\"E\".\"BusinessUnitID\", \"BU\".\"SubCategoryName\" AS \"BusinessUnit\","
                    + "\"E\".\"PracticeID\", \"POD\".\"PracticeName\" AS \"POD\","
                    + "NULL AS \"AllocationStartDate\", NULL AS \"AllocationEndDate\", NUll AS \"ProjectID\", NUll AS \"ProjectName\", NUll AS \"AccountName\", "
                    + "NUll AS \"ProjectManagerID\", NUll AS \"ProjectManager\", ' ' AS \"Remarks\""
                    + " FROM \"TalentManager\".\"Employee\" AS \"E\""
                    + " LEFT OUTER JOIN \"TalentManager\".\"Employee\" AS \"PM\" ON \"PM\".\"EmployeeEntryID\" = \"E\".\"ReportingManagerID\""
                    + " LEFT OUTER JOIN \"TalentManager\".\"DropDownSubCategory\" AS \"BU\" ON \"BU\".\"SubCategoryID\" = 3"
                    + " LEFT OUTER JOIN \"TalentManager\".\"Practice\" AS \"POD\" ON \"POD\".\"PracticeID\" = \"E\".\"PracticeID\""
                    + " WHERE \"E\".\"IsDeleted\" = False AND \"E\".\"BusinessUnitID\" != 3 AND \"E\".\"LastWorkingDay\" IS NULL"
                    + " AND(SELECT COUNT(1) FROM \"TalentManager\".\"ProjectAllocation\" WHERE \"EmployeeID\" = \"E\".\"EmployeeEntryID\" AND \"IsDeleted\"=False AND \"AllocationEndDate\" >= '__CURRENT_DATE__') = 0"
                    + " ORDER BY 3";
        public static string GET_COUNT_OF_NON_ALLOCATED_EMPLOYEES_FROM_DELIVERY = "SELECT COUNT(1)"
                    + " FROM \"TalentManager\".\"Employee\" AS \"E\""
                    + " WHERE \"E\".\"IsDeleted\" = False AND \"E\".\"LastWorkingDay\" IS NULL AND \"E\".\"BusinessUnitID\" = 3"
                    + " AND (SELECT COUNT(1) FROM \"TalentManager\".\"ProjectAllocation\" WHERE \"EmployeeID\" = \"E\".\"EmployeeEntryID\" "
                    + " AND \"IsDeleted\" = False AND \"AllocationEndDate\" >= '__CURRENT_DATE__') = 0";
        public static string GET_COUNT_OF_NON_ALLOCATED_EMPLOYEES_FROM_NON_DELIVERY = "SELECT COUNT(1)"
                    + " FROM \"TalentManager\".\"Employee\" AS \"E\""
                    + " WHERE \"E\".\"IsDeleted\" = False AND \"E\".\"LastWorkingDay\" IS NULL AND \"E\".\"BusinessUnitID\" != 3"
                    + " AND(SELECT COUNT(1) FROM \"TalentManager\".\"ProjectAllocation\" WHERE \"EmployeeID\" = \"E\".\"EmployeeEntryID\" "
                    + " AND \"IsDeleted\" = False AND \"AllocationEndDate\" >= '__CURRENT_DATE__') = 0";
        public static string GET_MANAGER_WISE_PROJECTS_SUMMARY = "SELECT \"P\".\"ProjectManagerID\", CONCAT(\"E\".\"FirstName\", ' ', \"E\".\"LastName\") AS \"ManagerName\", COUNT(\"P\".\"ProjectID\") As \"ProjectCount\""
                    + " FROM \"TalentManager\".\"Project\" AS \"P\""
                    + " LEFT OUTER JOIN \"TalentManager\".\"Employee\" AS \"E\" ON \"P\".\"ProjectManagerID\" = \"E\".\"EmployeeEntryID\""
                    + " WHERE \"P\".\"IsDeleted\" = False AND \"P\".\"EndDate\" >= '__CURRENT_DATE__' AND \"E\".\"IsDeleted\" = False AND \"E\".\"LastWorkingDay\" IS NULL"
                    + " GROUP BY \"P\".\"ProjectManagerID\", CONCAT(\"E\".\"FirstName\", ' ', \"E\".\"LastName\")"
                    + "ORDER BY COUNT(\"P\".\"ProjectID\") DESC";
        public static string GET_PRACTICE_WISE_HEAD_COUNT = "SELECT \"E\".\"PracticeID\",\"P\".\"PracticeName\" AS \"Practice\", COUNT(\"EmployeeID\") AS \"HeadCount\""
                    + " FROM \"TalentManager\".\"Employee\" AS \"E\""
                    + " LEFT OUTER JOIN \"TalentManager\".\"Practice\" AS \"P\" ON \"P\".\"PracticeID\" = \"E\".\"PracticeID\""
                    + " WHERE \"E\".\"LastWorkingDay\" IS NULL AND \"E\".\"IsDeleted\" = False"
                    + " GROUP BY \"E\".\"PracticeID\", \"P\".\"PracticeName\""
                    + " ORDER BY \"HeadCount\" DESC";
        public static string GET_SUB_PRACTICE_WISE_HEAD_COUNT = "SELECT \"p\".\"PracticeID\",\"p\".\"PracticeName\" AS \"Practice\", \"sp\".\"SubPracticeID\",\"sp\".\"SubPracticeName\" AS \"SubPractice\", COUNT(\"e\".\"EmployeeEntryID\") AS \"HeadCount\" "
                    + " FROM \"TalentManager\".\"Employee\" AS \"e\""
                    + " LEFT OUTER JOIN \"TalentManager\".\"Practice\" AS \"p\" ON \"p\".\"PracticeID\"=\"e\".\"PracticeID\""
                    + " LEFT OUTER JOIN \"TalentManager\".\"SubPractice\" AS \"sp\" ON \"sp\".\"SubPracticeID\"=\"e\".\"SubPracticeID\""
                    + " WHERE \"e\".\"IsDeleted\" = False AND \"e\".\"LastWorkingDay\" IS NULL"
                    + " GROUP BY \"p\".\"PracticeID\", \"sp\".\"SubPracticeID\",\"p\".\"PracticeName\",\"sp\".\"SubPracticeName\", \"e\".\"PracticeID\""
                    + " ORDER BY \"p\".\"PracticeName\", \"sp\".\"SubPracticeName\", \"HeadCount\" DESC";
    }
}
