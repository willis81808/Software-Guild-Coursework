USE Master;
GO

CREATE DATABASE SGRoster;
GO

USE SGRoster;
GO

CREATE TABLE Cohort (
	CohortID INT IDENTITY(1,1) PRIMARY KEY,
	StartDate DATE NOT NULL,
	[Subject] VARCHAR(30) NOT NULL,
	[Location] VARCHAR(30) NOT NULL
)
GO

CREATE TABLE Apprentice (
	ApprenticeID INT IDENTITY(1,1) PRIMARY KEY,
	FirstName VARCHAR(30) NOT NULL,
	LastName VARCHAR(30) NOT NULL
)
GO

CREATE TABLE ApprenticeCohort (
	CohortID INT NOT NULL,
	ApprenticeID INT NOT NULL,
	CONSTRAINT PK_ApprenticeCohort
		PRIMARY KEY (ApprenticeID, CohortID),
	CONSTRAINT FK_Cohort_ApprenticeCohort
		FOREIGN KEY (CohortID)
		REFERENCES Cohort(CohortID),
	CONSTRAINT FK_Apprentice_ApprenticeCohort
		FOREIGN KEY (ApprenticeID)
		REFERENCES Apprentice(ApprenticeID)
)
GO