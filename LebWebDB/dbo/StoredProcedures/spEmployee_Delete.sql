CREATE PROCEDURE [dbo].[spEmployee_Delete]
	@Id int
AS

begin
	set nocount on;

	delete from dbo.Employees
	where Id = @Id;

end