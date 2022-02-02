CREATE PROCEDURE [dbo].[spEmployee_Get]
	@Id int
AS

begin

	select [Id], [FirstName], [LastName], [HireDate], [TermDate], [PhotoLink], [IsActive]
	from dbo.Employees
	where Id = @Id

end