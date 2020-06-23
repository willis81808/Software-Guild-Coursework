CREATE DATABASE SWCCorp
GO


USE SWCCorp
GO


CREATE TABLE MgmtTraining (
	ClassID int PRIMARY KEY IDENTITY(1,1),
	ClassName varchar(50) NOT NULL,
	ClassDurationHours int NULL
)
GO

CREATE TABLE Location (
	LocationID INT not null PRIMARY KEY,
	Street VARCHAR(30) null,
	City VARCHAR(20) null,
	[State] CHAR(2) null
)
GO

CREATE TABLE Employee(
	EmpID INT PRIMARY KEY not null,
	LastName VARCHAR(30) null,
	FirstName VARCHAR(20) null,
	HireDate DATETIME null,
	LocationID INT null FOREIGN KEY REFERENCES Location(LocationID),
	ManagerID INT null FOREIGN KEY REFERENCES Employee(EmpID),
	[Status] CHAR(12) null
)
GO

CREATE TABLE PayRates (
	EmpID INT PRIMARY KEY FOREIGN KEY REFERENCES Employee(EmpID),
	YearlySalary DECIMAL(9,2) NULL,
	MonthlySalary DECIMAL(9,2) NULL,
	HourlyRate DECIMAL(9,2) NULL
)
GO

CREATE TABLE [Grant] (
	GrantID CHAR(3) NOT NULL PRIMARY KEY,
	GrantName NVARCHAR(50) NOT NULL,
	EmpID INT NULL FOREIGN KEY REFERENCES Employee(EmpID),
	Amount DECIMAL(9,2) NOT NULL
)
GO

INSERT INTO MgmtTraining VALUES('Embracing Diversity',12)
INSERT INTO MgmtTraining VALUES('Interviewing',6)
INSERT INTO MgmtTraining VALUES('Difficult Negotiations',30)

INSERT INTO Location VALUES(1,'111 First ST','Seattle', 'WA')
INSERT INTO Location VALUES(2,'222 Second AVE', 'Boston','MA')
INSERT INTO Location VALUES(3,'333 Third PL','Chicago','IL')
INSERT INTO Location VALUES(4,'444 Ruby ST','Spokane', 'WA')

INSERT INTO Employee (EmpID, LastName, FirstName, HireDate, LocationID, ManagerID, [Status])
VALUES (1,'Adams','Alex','1/1/01',1, NULL, NULL),
(2,'Brown','Barry','8/12/02',1,NULL, NULL),
(3,'Osako','Lee','9/1/99',2,NULL, NULL),
(4,'Kennson','David','3/16/96',1,NULL, NULL),
(5,'Bender','Eric','5/17/07',1,NULL, NULL),
(6,'Kendall','Lisa','11/15/01',4,NULL, NULL),
(7,'Lonning','David','1/1/00',1,NULL, NULL),
(8,'Marshbank','John','11/15/01',null,NULL, NULL),
(9,'Newton','James','9/30/03',2,NULL, NULL),
(10,'O''Haire','Terry','10/04/04',2,NULL, NULL),
(11,'Smith','Sally','4/1/89',1,NULL, NULL),
(12,'O''Neil','Barbara','5/26/95',4,NULL, NULL)

INSERT INTO PayRates VALUES (1,75000,null,null)
INSERT INTO PayRates VALUES (2,78000,null,null)
INSERT INTO PayRates VALUES (3,null,null,45)
INSERT INTO PayRates VALUES (4,null,6500,null)
INSERT INTO PayRates VALUES (5,null,5800,null)
INSERT INTO PayRates VALUES (6,52000,null,null)
INSERT INTO PayRates VALUES (7,null,6100,null)
INSERT INTO PayRates VALUES (8,null,null,32)
INSERT INTO PayRates VALUES (9,null,null,18)
INSERT INTO PayRates VALUES (10,Null,null,17)
INSERT INTO PayRates VALUES (11,115000,null,null)
INSERT INTO PayRates VALUES (12,null,null,21)

GO


--Update all Seattle Employees to Report to Sally Smith (except herself)
UPDATE Employee SET ManagerID = 11
WHERE LocationID =  1 AND EmpID != 11

--Update all Boston Employee to report to Lee Osako (except himself)
UPDATE Employee SET ManagerID = 3
WHERE LocationID = 2 AND EmpID != 3

--Update all Spokane Employees to report directly to David Kennson
UPDATE Employee SET ManagerID = 4
WHERE locationID = 4

--Set all sales people with no office location to work for David Kennson
UPDATE Employee SET ManagerID = 4
WHERE LocationID IS NULL

--Set Lee Osako to work for Sally Smith
UPDATE Employee SET ManagerID = 11
WHERE EmpID = 3

UPDATE Employee SET Status = 'On Leave' WHERE EmpID = 7
UPDATE Employee SET Status = 'Has Tenure' WHERE EmpID in (12,4)


INSERT INTO [Grant] VALUES ('001','92 Purr_Scents %% team',7,4750)
INSERT INTO [Grant] VALUES ('002','K_Land fund trust',2,15750)
INSERT INTO [Grant] VALUES ('003','Robert@BigStarBank.com',7,18100)
INSERT INTO [Grant] VALUES ('004','Norman''s Outreach',null,21000)
INSERT INTO [Grant] VALUES ('005','BIG 6''s Foundation%',4,21000)
INSERT INTO [Grant] VALUES ('006','TALTA_Kishan International',3,18100)
INSERT INTO [Grant] VALUES ('007','Ben@MoreTechnology.com',010,41000)
INSERT INTO [Grant] VALUES ('008','@Last-U-Can-Help',7,25000)
INSERT INTO [Grant] VALUES ('009','Thank you @.com',11,21500)
INSERT INTO [Grant] VALUES ('010','Call Mom @Com',5,7500)
GO

