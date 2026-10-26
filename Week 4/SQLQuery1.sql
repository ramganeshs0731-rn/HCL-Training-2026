USE master;
GO
 
IF DB_ID(N'LearnerManagementAssessment') IS NOT NULL
BEGIN
    ALTER DATABASE LearnerManagementAssessment SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE LearnerManagementAssessment;
END;
GO

CREATE DATABASE LearnerManagementAssessment;
GO
USE LearnerManagementAssessment;
GO

CREATE TABLE Departments
(
    DepartmentId INT IDENTITY(1,1) CONSTRAINT PK_Departments PRIMARY KEY,
    DepartmentName NVARCHAR(100) NOT NULL CONSTRAINT UQ_Departments_Name UNIQUE,
    Location NVARCHAR(100) NOT NULL
);
GO

CREATE TABLE Students
(
    StudentId INT IDENTITY(1,1) CONSTRAINT PK_Students PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(255) NOT NULL CONSTRAINT UQ_Students_Email UNIQUE,
    Age INT NOT NULL CONSTRAINT CK_Students_Age CHECK (Age BETWEEN 17 AND 60),
    DepartmentId INT NULL,
    AdmissionDate DATE NOT NULL,
    IsActive BIT NOT NULL CONSTRAINT DF_Students_IsActive DEFAULT (1),
    CONSTRAINT FK_Students_Departments FOREIGN KEY (DepartmentId)
        REFERENCES Departments(DepartmentId)
);
GO

CREATE TABLE Courses
(
    CourseId INT IDENTITY(1,1) CONSTRAINT PK_Courses PRIMARY KEY,
    CourseName NVARCHAR(120) NOT NULL CONSTRAINT UQ_Courses_Name UNIQUE,
    Category NVARCHAR(60) NOT NULL,
    Credits INT NOT NULL CONSTRAINT CK_Courses_Credits CHECK (Credits BETWEEN 1 AND 6),
    Fee DECIMAL(10,2) NOT NULL CONSTRAINT CK_Courses_Fee CHECK (Fee >= 0),
    IsActive BIT NOT NULL CONSTRAINT DF_Courses_IsActive DEFAULT (1)
);
GO

CREATE TABLE Enrollments
(
    EnrollmentId INT IDENTITY(1,1) CONSTRAINT PK_Enrollments PRIMARY KEY,
    StudentId INT NOT NULL,
    CourseId INT NOT NULL,
    EnrolledAt DATE NOT NULL,
    EnrollmentStatus NVARCHAR(20) NOT NULL,
    CONSTRAINT CK_Enrollments_Status CHECK (EnrollmentStatus IN ('Active','Completed','Withdrawn')),
    CONSTRAINT UQ_Enrollments_Student_Course UNIQUE (StudentId, CourseId),
    CONSTRAINT FK_Enrollments_Students FOREIGN KEY (StudentId) REFERENCES Students(StudentId),
    CONSTRAINT FK_Enrollments_Courses FOREIGN KEY (CourseId) REFERENCES Courses(CourseId)
);
GO

CREATE TABLE Assessments
(
    AssessmentId INT IDENTITY(1,1) CONSTRAINT PK_Assessments PRIMARY KEY,
    CourseId INT NOT NULL,
    AssessmentName NVARCHAR(120) NOT NULL,
    AssessmentDate DATE NOT NULL,
    MaxMarks INT NOT NULL CONSTRAINT CK_Assessments_MaxMarks CHECK (MaxMarks > 0),
    CONSTRAINT FK_Assessments_Courses FOREIGN KEY (CourseId) REFERENCES Courses(CourseId)
);
GO

CREATE TABLE AssessmentResults
(
    ResultId INT IDENTITY(1,1) CONSTRAINT PK_AssessmentResults PRIMARY KEY,
    StudentId INT NOT NULL,
    AssessmentId INT NOT NULL,
    MarksObtained DECIMAL(5,2) NULL,
    SubmissionStatus NVARCHAR(20) NOT NULL,
    CONSTRAINT CK_Results_Marks CHECK (MarksObtained IS NULL OR MarksObtained BETWEEN 0 AND 100),
    CONSTRAINT CK_Results_Status CHECK (SubmissionStatus IN ('Submitted','Pending','Absent')),
    CONSTRAINT UQ_Results_Student_Assessment UNIQUE (StudentId, AssessmentId),
    CONSTRAINT FK_Results_Students FOREIGN KEY (StudentId) REFERENCES Students(StudentId),
    CONSTRAINT FK_Results_Assessments FOREIGN KEY (AssessmentId) REFERENCES Assessments(AssessmentId)
);
GO

CREATE TABLE Payments
(
    PaymentId INT IDENTITY(1,1) CONSTRAINT PK_Payments PRIMARY KEY,
    StudentId INT NOT NULL,
    Amount DECIMAL(10,2) NOT NULL CONSTRAINT CK_Payments_Amount CHECK (Amount >= 0),
    PaymentDate DATE NOT NULL,
    PaymentStatus NVARCHAR(20) NOT NULL,
    PaymentMethod NVARCHAR(30) NOT NULL,
    CONSTRAINT CK_Payments_Status CHECK (PaymentStatus IN ('Paid','Pending','Failed','Refunded')),
    CONSTRAINT FK_Payments_Students FOREIGN KEY (StudentId) REFERENCES Students(StudentId)
);
GO



