CREATE PROCEDURE GetBillabilityWiseDetails
@AllocationType INT 
AS
BEGIN
	SELECT E.EmployeeEntryID, E.FirstName + ' ' + E.LastName AS 'EmployeeName', P.ProjectName, 
	SPT.SubCategoryName AS 'ProjectType', P.ProjectID, AC.AccountName, 
	SC.SubCategoryName AS 'AllocationType', P.AllocationStartDate, P.AllocationEndDate
	FROM Employee E
	LEFT OUTER JOIN ProjectAllocation PA ON PA.EmployeeID = E.EmployeeEntryID
	LEFT OUTER JOIN Project P ON P.ProjectID = PA.ProjectID
	LEFT OUTER JOIN ProjectAccount AC ON AC.AccountID = P.ProjectAccountID
	LEFT OUTER JOIN DropDownSubCategory SC ON SC.SubCategoryID = PA.AllocationTypeID
	LEFT OUTER JOIN DropDownSubCategory SPT ON SPT.SubCategoryID = P.ProjectTypeID
	WHERE P.EndDate >= GETDATE() AND PA.AllocationTypeID = @AllocationType
END
GO
