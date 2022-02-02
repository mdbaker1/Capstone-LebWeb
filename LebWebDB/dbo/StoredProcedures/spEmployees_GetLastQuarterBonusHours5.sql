CREATE PROCEDURE [dbo].[spEmployees_GetLastQuarterBonusHours5]
	
AS
	
-- Get todays date
        declare @today datetime = GetDate();
        -- Get the first day of the previous quarter
        declare @firstDayOfPreviousQuarter datetime = dateadd(qq, datediff(qq, 0, @today) - 1, 0);
        declare @lastDayOfPreviousQuarter datetime = dateadd(dd, -1, dateadd(qq, datediff(qq, 0, @today), 0));

        -- Get 5 year hire date from last day of previous quarter
        declare @fiveYearHireDate datetime = dateadd(year, -5, @firstDayOfPreviousQuarter);

        -- Get 8 year hire date from last day of previous quarter
        declare @eightYearHireDate datetime = dateadd(year, -8, @firstDayOfPreviousQuarter);

select Id, FirstName, LastName, HireDate, 
	DATEDIFF(YEAR,HireDate,@firstDayOfPreviousQuarter -1) -
	(CASE WHEN DATEADD(YY,DATEDIFF(YEAR,HireDate,@firstDayOfPreviousQuarter -1),HireDate) > @firstDayOfPreviousQuarter -1
		THEN 1
		ELSE 0 
	END) As YrsOfService
from Employees 
where IsActive = 1
and HireDate < @fiveYearHireDate
and HireDate > @eightYearHireDate
order by HireDate, LastName, FirstName