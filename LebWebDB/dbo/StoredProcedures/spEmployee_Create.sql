CREATE PROCEDURE [dbo].[spEmployee_Create]
	@FirstName nvarchar(50),
	@LastName nvarchar(50),
	@HireDate datetime2(7),
	@PhotoLink nvarchar(max),
	@TermDate datetime2(7),
	@IsActive bit

AS

begin 
	set nocount on

	DECLARE @NextId INT
	SET @NextId = (SELECT MAX(Id) From Employees);
	IF @NextId IS NUll SET @NextId = 109100100
	ELSE SET @NextId += 1;

	insert into dbo.Employees(Id, FirstName, LastName, HireDate, PhotoLink, IsActive)
	values (@NextId, @FirstName, @LastName, @HireDate, @PhotoLink, @IsActive)

end
