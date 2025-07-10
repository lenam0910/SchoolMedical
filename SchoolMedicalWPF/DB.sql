-- Drop existing database if needed (for reset)
IF EXISTS (SELECT * FROM sys.databases WHERE name = 'SchoolMedicalDB')
    DROP DATABASE SchoolMedicalDB;
GO

CREATE DATABASE SchoolMedicalDB;
GO

USE SchoolMedicalDB;
GO

-- Students Table
CREATE TABLE Students (
    StudentId INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    DateOfBirth DATE NOT NULL,
    Gender NVARCHAR(10),
    Class NVARCHAR(20),
    EmergencyContact NVARCHAR(100),
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- Staff Table (Updated with LastLogin)
CREATE TABLE Staff (
    StaffId INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Role NVARCHAR(50) NOT NULL, -- e.g., Nurse, Doctor
    Username NVARCHAR(50) UNIQUE NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Contact NVARCHAR(100),
    LastLogin DATETIME,
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- MedicalRecords Table
CREATE TABLE MedicalRecords (
    RecordId INT PRIMARY KEY IDENTITY(1,1),
    StudentId INT NOT NULL,
    Condition NVARCHAR(200),
    Allergies NVARCHAR(200),
    Notes NVARCHAR(500),
    RecordedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (StudentId) REFERENCES Students(StudentId)
);

-- Medications Table
CREATE TABLE Medications (
    MedicationId INT PRIMARY KEY IDENTITY(1,1),
    StudentId INT NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    Dosage NVARCHAR(50),
    Frequency NVARCHAR(50),
    StartDate DATE,
    EndDate DATE,
    PrescribedBy INT,
    FOREIGN KEY (StudentId) REFERENCES Students(StudentId),
    FOREIGN KEY (PrescribedBy) REFERENCES Staff(StaffId)
);

-- Appointments Table
CREATE TABLE Appointments (
    AppointmentId INT PRIMARY KEY IDENTITY(1,1),
    StudentId INT NOT NULL,
    StaffId INT NOT NULL,
    AppointmentDate DATETIME NOT NULL,
    Reason NVARCHAR(200),
    Status NVARCHAR(20) DEFAULT 'Scheduled', -- Scheduled, Completed, Cancelled
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (StudentId) REFERENCES Students(StudentId),
    FOREIGN KEY (StaffId) REFERENCES Staff(StaffId)
);

-- HealthIncidents Table
CREATE TABLE HealthIncidents (
    IncidentId INT PRIMARY KEY IDENTITY(1,1),
    StudentId INT NOT NULL,
    StaffId INT NOT NULL,
    IncidentDate DATETIME NOT NULL,
    Description NVARCHAR(500),
    ActionTaken NVARCHAR(500),
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (StudentId) REFERENCES Students(StudentId),
    FOREIGN KEY (StaffId) REFERENCES Staff(StaffId)
);

-- AuditLogs Table (New)
CREATE TABLE AuditLogs (
    LogId INT PRIMARY KEY IDENTITY(1,1),
    StaffId INT NOT NULL,
    Action NVARCHAR(100) NOT NULL, -- e.g., Login, UpdateStudent, CreateAppointment
    Details NVARCHAR(500),
    LogDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (StaffId) REFERENCES Staff(StaffId)
);

-- Notifications Table (New)
CREATE TABLE Notifications (
    NotificationId INT PRIMARY KEY IDENTITY(1,1),
    StaffId INT NOT NULL,
    Message NVARCHAR(200) NOT NULL,
    IsRead BIT DEFAULT 0,
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (StaffId) REFERENCES Staff(StaffId)
);



-- Insert Sample Data
INSERT INTO Students (FirstName, LastName, DateOfBirth, Gender, Class, EmergencyContact)
VALUES ('John', 'Doe', '2010-05-15', 'Male', '10A', 'Jane Doe - 123-456-7890'),
       ('Sarah', 'Smith', '2011-03-22', 'Female', '9B', 'Tom Smith - 987-654-3210');

INSERT INTO Staff (FirstName, LastName, Role, Username, PasswordHash, Email, Contact)
VALUES ('Emily', 'Brown', 'Nurse', 'emily.brown', '$2a$10$3zHz3iX9jK8z3u2f4z5M8u1y7X9b0x2W3v4Y5Z6A7B8C9D0E1F2G3', 'emily.brown@school.com', '123-456-7890'),
       ('Michael', 'Lee', 'Doctor', 'michael.lee', '$2a$10$3zHz3iX9jK8z3u2f4z5M8u1y7X9b0x2W3v4Y5Z6A7B8C9D0E1F2G3', 'michael.lee@school.com', '987-654-3210');

INSERT INTO MedicalRecords (StudentId, Condition, Allergies, Notes)
VALUES (1, 'Asthma', 'Peanuts', 'Uses inhaler as needed'),
       (2, 'None', 'None', 'Healthy');

INSERT INTO Medications (StudentId, Name, Dosage, Frequency, StartDate, EndDate, PrescribedBy)
VALUES (1, 'Albuterol', '2 puffs', 'As needed', '2025-01-01', '2025-12-31', 1);

INSERT INTO Appointments (StudentId, StaffId, AppointmentDate, Reason, Status)
VALUES (1, 1, '2025-07-10 10:00:00', 'Asthma check-up', 'Scheduled'),
       (2, 2, '2025-07-11 14:00:00', 'Annual check-up', 'Scheduled');

INSERT INTO HealthIncidents (StudentId, StaffId, IncidentDate, Description, ActionTaken)
VALUES (1, 1, '2025-07-01 12:00:00', 'Minor fall during recess', 'Applied ice pack, monitored for 30 minutes');

INSERT INTO AuditLogs (StaffId, Action, Details, LogDate)
VALUES (1, 'Login', 'Nurse Emily logged in', '2025-07-09 08:00:00');

INSERT INTO Notifications (StaffId, Message, IsRead)
VALUES (1, 'Appointment with John Doe at 10:00 tomorrow', 0);


GO