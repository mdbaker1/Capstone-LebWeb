CREATE PROCEDURE [dbo].[spEmployee_Update]
	@Id int,
	@FirstName nvarchar(50),
	@LastName nvarchar(50),
	@HireDate datetime2(7),
	@TermDate datetime2(7),
	@PhotoLink nvarchar(max),
	@IsActive bit
AS
	
begin

	set nocount on;
	
	update dbo.Employees
	set FirstName = @FirstName,
		LastName = @LastName,
		HireDate = @HireDate,
		TermDate = @TermDate,
		PhotoLink = @PhotoLink,
		IsActive = @IsActive
	where Id = @Id;

end
