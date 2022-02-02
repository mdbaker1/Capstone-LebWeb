CREATE TABLE [dbo].[Employees]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [HireDate] DATETIME2 NULL, 
    [TermDate] DATETIME2 NULL,
    [PhotoLink] NVARCHAR(MAX) NULL,
    [IsActive] BIT NULL
)
