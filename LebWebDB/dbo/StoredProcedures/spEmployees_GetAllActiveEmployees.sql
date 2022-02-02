CREATE PROCEDURE [dbo].[spEmployees_GetAllActiveEmployees]

	AS

begin

	select [Id], [FirstName], [LastName], [HireDate], [TermDate], [PhotoLink], [IsActive] 
	from dbo.Employees
	where [IsActive] = 1

end
