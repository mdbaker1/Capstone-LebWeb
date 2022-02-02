CREATE PROCEDURE [dbo].[spEmployees_GetAllInactiveEmployees]

AS

begin

	select [Id], [FirstName], [LastName], [HireDate], [TermDate], [PhotoLink], [IsActive] 
	from dbo.Employees
	where [IsActive] = 0

end