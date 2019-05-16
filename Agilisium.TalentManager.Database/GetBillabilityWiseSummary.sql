
CREATE PROCEDURE dbo.GetBillabilityWiseSummary
AS
BEGIN
	SELECT PA.AllocationTypeID, SC.SubCategoryName AS 'AllocationType', COUNT(E.EmployeeEntryID) AS 'NumberOfEmployees'
	FROM Employee E
	LEFT OUTER JOIN ProjectAllocation PA ON PA.EmployeeID = E.EmployeeEntryID
	LEFT OUTER JOIN Project P ON P.ProjectID = PA.ProjectID
	LEFT OUTER JOIN ProjectAccount AC ON AC.AccountID = P.ProjectAccountID
	LEFT OUTER JOIN DropDownSubCategory SC ON SC.SubCategoryID = PA.AllocationTypeID
	WHERE P.EndDate >= GETDATE()
	GROUP BY SC.SubCategoryName, PA.AllocationTypeID
END
GO
