CREATE PROCEDURE [dbo].[GetManagerWiseProjectsSummary]
AS
BEGIN
	SELECT P.ProjectManagerID, E.FirstName + ' ' + E.LastName AS 'ManagerName', COUNT(P.ProjectID) As 'ProjectCount'
	FROM dbo.Project P
	LEFT OUTER JOIN dbo.Employee E ON P.ProjectManagerID = E.EmployeeEntryID
	GROUP By P.ProjectManagerID, E.FirstName + ' ' + E.LastName
	ORDER BY COUNT(P.ProjectID) DESC
END
