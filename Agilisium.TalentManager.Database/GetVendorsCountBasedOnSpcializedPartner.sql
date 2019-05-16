
CREATE PROCEDURE [dbo].[GetVendorsCountBasedOnSpcializedPartner]
AS
BEGIN
	SELECT SubCategoryName As SpecializedPartner, COUNT(SpecializedPartnerID) As VendorsCount 
	FROM dbo.Vendor V
	LEFT OUTER JOIN dbo.DropDownSubCategory C ON C.SubCategoryID = SpecializedPartnerID
	WHERE V.IsDeleted=0
	GROUP BY SpecializedPartnerID, SubCategoryName
	ORDER BY VendorsCount DESC
END