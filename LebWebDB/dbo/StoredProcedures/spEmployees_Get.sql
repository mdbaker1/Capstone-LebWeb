CREATE PROCEDURE [dbo].[spEmployees_Get]
	
AS

begin

	select [Id], [FirstName], [LastName], [HireDate], [TermDate], [PhotoLink], [IsActive] 
	from dbo.Employees

end