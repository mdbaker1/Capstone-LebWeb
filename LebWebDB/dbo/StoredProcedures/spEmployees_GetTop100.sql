CREATE PROCEDURE [dbo].[spEmployees_GetTop100]

AS

begin

	select top 100 [Id], [FirstName], [LastName], [HireDate], [PhotoLink]
	from dbo.Employees
	where IsActive = 1
	order by HireDate

end
