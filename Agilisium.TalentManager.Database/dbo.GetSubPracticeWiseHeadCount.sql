CREATE PROCEDURE [dbo].[GetSubPracticeWiseHeadCount]
AS
BEGIN
	SELECT p.PracticeID,p.PracticeName AS Practice, sp.SubPracticeID,sp.SubPracticeName AS SubPractice, COUNT(e.EmployeeEntryID) AS HeadCount FROM Employee e
	LEFT OUTER JOIN Practice AS p ON p.PracticeID=e.PracticeID
	LEFT OUTER JOIN SubPractice AS sp ON sp.SubPracticeID=e.SubPracticeID
	WHERE e.IsDeleted = 0
	GROUP BY p.PracticeID, sp.SubPracticeID,p.PracticeName,sp.SubPracticeName, e.PracticeID
	ORDER BY p.PracticeName, HeadCount DESC
END
