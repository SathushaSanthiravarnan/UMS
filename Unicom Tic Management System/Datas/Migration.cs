using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Datas;

namespace Unicom_Tic_Management_System.Datas
{
    internal static class Migration
    {
        public static void CreateTables()
        {
            using (var conn = DatabaseManager.GetConnection())
            {
                var cmd = conn.CreateCommand();

                cmd.CommandText = @"
                    PRAGMA foreign_keys = ON;

                   

                    CREATE TABLE IF NOT EXISTS NICDetails (
                        NIC TEXT PRIMARY KEY NOT NULL,
                        IsUsed INTEGER DEFAULT 0 
                    );

                    CREATE TABLE IF NOT EXISTS Users (
                        UserID INTEGER PRIMARY KEY AUTOINCREMENT,
                        Username TEXT NOT NULL UNIQUE,
                        Password TEXT NOT NULL,
                        NIC TEXT NOT NULL UNIQUE,
                        CreatedAt TEXT NOT NULL DEFAULT CURRENT_TIMESTAMP,
                        UpdatedAt TEXT NOT NULL DEFAULT CURRENT_TIMESTAMP,
                        Role TEXT NOT NULL DEFAULT 'Student', -- e.g., 'Student', 'Lecturer', 'Staff', 'Admin', 'Mentor'
                        FOREIGN KEY (NIC) REFERENCES NICDetails(NIC)
                    );

                    CREATE TABLE IF NOT EXISTS ActivityLogs (
                        LogID INTEGER PRIMARY KEY AUTOINCREMENT,
                        UserID INTEGER,
                        Action TEXT NOT NULL,
                        CreatedAt TEXT NOT NULL DEFAULT CURRENT_TIMESTAMP,
                        FOREIGN KEY (UserID) REFERENCES Users(UserID)
                    );


                    CREATE TABLE IF NOT EXISTS Departments (
                        DepartmentID INTEGER PRIMARY KEY AUTOINCREMENT,
                        DepartmentName TEXT NOT NULL UNIQUE
                    );

                    CREATE TABLE IF NOT EXISTS TimeSlots (
                        TimeSlotID INTEGER PRIMARY KEY AUTOINCREMENT,
                        SlotName TEXT NOT NULL UNIQUE,
                        StartTime TEXT NOT NULL,
                        EndTime TEXT NOT NULL
                    );

                    INSERT OR IGNORE INTO TimeSlots (SlotName, StartTime, EndTime) VALUES
                    ('07:00 - 08:00', '07:00', '08:00'),
                    ('08:00 - 09:00', '08:00', '09:00'),
                    ('09:00 - 10:00', '09:00', '10:00'),
                    ('10:00 - 11:00', '10:00', '11:00'),
                    ('11:00 - 12:00', '11:00', '12:00'),
                    ('12:00 - 13:00', '12:00', '13:00'),
                    ('13:00 - 14:00', '13:00', '14:00'),
                    ('14:00 - 15:00', '14:00', '15:00'),
                    ('15:00 - 16:00', '15:00', '16:00'),
                    ('16:00 - 17:00', '16:00', '17:00'),
                    ('17:00 - 18:00', '17:00', '18:00'),
                    ('18:00 - 19:00', '18:00', '19:00');


                    CREATE TABLE IF NOT EXISTS Courses (
                        CourseID INTEGER PRIMARY KEY AUTOINCREMENT,
                        CourseName TEXT NOT NULL UNIQUE,
                        Description TEXT
                    );

                    CREATE TABLE IF NOT EXISTS Subjects (
                        SubjectID INTEGER PRIMARY KEY AUTOINCREMENT,
                        SubjectName TEXT NOT NULL UNIQUE,
                        CourseID INTEGER NOT NULL,                       
                        FOREIGN KEY (CourseID) REFERENCES Courses(CourseID)
                    );                  

                    CREATE TABLE IF NOT EXISTS MainGroups (
                        MainGroupID INTEGER PRIMARY KEY AUTOINCREMENT,
                        GroupCode TEXT NOT NULL UNIQUE CHECK(GroupCode IN ('A','B')),
                        Description TEXT
                    );

                    CREATE TABLE IF NOT EXISTS SubGroups (
                        SubGroupID INTEGER PRIMARY KEY AUTOINCREMENT,
                        MainGroupID INTEGER NOT NULL,
                        SubGroupName TEXT NOT NULL UNIQUE, -- e.g., 'Group A - Presentation', 'Group B - Grammar'
                        Description TEXT,
                        FOREIGN KEY (MainGroupID) REFERENCES MainGroups(MainGroupID)
                    );

                    CREATE TABLE IF NOT EXISTS Mentors (
                        MentorID INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        NIC TEXT NOT NULL UNIQUE,
                        DepartmentID INTEGER,
                        UserID INTEGER UNIQUE,
                        Phone TEXT,
                        Email TEXT,
                        CreatedAt TEXT NOT NULL DEFAULT CURRENT_TIMESTAMP, -- Renamed from RegisteredAt
                        UpdatedAt TEXT NOT NULL DEFAULT CURRENT_TIMESTAMP,
                        FOREIGN KEY (DepartmentID) REFERENCES Departments(DepartmentID),
                        FOREIGN KEY (UserID) REFERENCES Users(UserID),
                        FOREIGN KEY (NIC) REFERENCES NICDetails(NIC)
                    );

                    CREATE TABLE IF NOT EXISTS Students (
                        StudentID INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        NIC TEXT NOT NULL UNIQUE,
                        Address TEXT,
                        ContactNo TEXT,
                        Email TEXT,
                        DateOfBirth TEXT,
                        Gender TEXT,
                        EnrollmentDate TEXT NOT NULL,
                        CourseID INTEGER NOT NULL,
                        UserID INTEGER UNIQUE NOT NULL,
                        MainGroupID INTEGER NOT NULL,
                        SubGroupID INTEGER NOT NULL,
                        CreatedAt TEXT NOT NULL DEFAULT CURRENT_TIMESTAMP,
                        UpdatedAt TEXT NOT NULL DEFAULT CURRENT_TIMESTAMP,
                        AdmissionNumber TEXT NOT NULL UNIQUE,
                        FOREIGN KEY (CourseID) REFERENCES Courses(CourseID),
                        FOREIGN KEY (UserID) REFERENCES Users(UserID),
                        FOREIGN KEY (NIC) REFERENCES NICDetails(NIC),
                        FOREIGN KEY (MainGroupID) REFERENCES MainGroups(MainGroupID),
                        FOREIGN KEY (SubGroupID) REFERENCES SubGroups(SubGroupID)
                    );

                    CREATE TABLE IF NOT EXISTS AdmissionNumbers (
                        AdmissionNumber TEXT PRIMARY KEY NOT NULL UNIQUE,
                        StudentID INTEGER UNIQUE NOT NULL,
                        FOREIGN KEY (StudentID) REFERENCES Students(StudentID)
                    );

                    CREATE TABLE IF NOT EXISTS GroupMentors (
                        SubGroupID INTEGER NOT NULL,
                        MentorID INTEGER NOT NULL,
                        AssignedDate TEXT,
                        PRIMARY KEY (SubGroupID, MentorID),
                        FOREIGN KEY (SubGroupID) REFERENCES SubGroups(SubGroupID),
                        FOREIGN KEY (MentorID) REFERENCES Mentors(MentorID)
                    );

                    CREATE TABLE IF NOT EXISTS StudentGroups (
                        StudentID INTEGER NOT NULL,
                        SubGroupID INTEGER NOT NULL,
                        AssignedDate TEXT,
                        PRIMARY KEY (StudentID, SubGroupID),
                        FOREIGN KEY (StudentID) REFERENCES Students(StudentID),
                        FOREIGN KEY (SubGroupID) REFERENCES SubGroups(SubGroupID)
                    );

                    CREATE TABLE IF NOT EXISTS GroupSubjects (
                        SubGroupID INTEGER NOT NULL,
                        SubjectID INTEGER NOT NULL,
                        PRIMARY KEY (SubGroupID, SubjectID),
                        FOREIGN KEY (SubGroupID) REFERENCES SubGroups(SubGroupID),
                        FOREIGN KEY (SubjectID) REFERENCES Subjects(SubjectID)
                    );


                    CREATE TABLE IF NOT EXISTS Lecturers (
                        LecturerID INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        NIC TEXT NOT NULL UNIQUE,
                        Phone TEXT,
                        Address TEXT,
                        Email TEXT,
                        DepartmentID INTEGER,
                        HireDate TEXT,
                        UserID INTEGER UNIQUE NOT NULL,
                        CreatedAt TEXT NOT NULL DEFAULT CURRENT_TIMESTAMP, -- Renamed from RegisteredAt
                        UpdatedAt TEXT NOT NULL DEFAULT CURRENT_TIMESTAMP,
                        FOREIGN KEY (DepartmentID) REFERENCES Departments(DepartmentID),
                        FOREIGN KEY (UserID) REFERENCES Users(UserID),
                        FOREIGN KEY (NIC) REFERENCES NICDetails(NIC)
                    );

                    CREATE TABLE IF NOT EXISTS Staff (
                        StaffID INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        NIC TEXT NOT NULL UNIQUE,
                        DepartmentID INTEGER,
                        Role TEXT, -- This could describe their staff role (e.g., 'Admin Assistant', 'HR Manager')
                        ContactNo TEXT,
                        Email TEXT,
                        HireDate TEXT,
                        UserID INTEGER UNIQUE NOT NULL,
                        CreatedAt TEXT NOT NULL DEFAULT CURRENT_TIMESTAMP, -- Renamed from RegisteredAt
                        UpdatedAt TEXT NOT NULL DEFAULT CURRENT_TIMESTAMP,
                        FOREIGN KEY (DepartmentID) REFERENCES Departments(DepartmentID),
                        FOREIGN KEY (UserID) REFERENCES Users(UserID),
                        FOREIGN KEY (NIC) REFERENCES NICDetails(NIC)
                    );

                    CREATE TABLE IF NOT EXISTS EmployeeIDs (
                        EmployeeID TEXT PRIMARY KEY NOT NULL UNIQUE,
                        UserID INTEGER UNIQUE NOT NULL,
                        FOREIGN KEY (UserID) REFERENCES Users(UserID)
                    );

                    
                    CREATE TABLE IF NOT EXISTS LecturerStudents (
                        LecturerID INTEGER NOT NULL,
                        StudentID INTEGER NOT NULL,
                        AssignedDate TEXT NOT NULL DEFAULT CURRENT_TIMESTAMP, -- When this direct relationship was established
                        RelationshipType TEXT, -- e.g., 'Academic Advisor', 'Project Guide', 'General Support'
                        PRIMARY KEY (LecturerID, StudentID),
                        FOREIGN KEY (LecturerID) REFERENCES Lecturers(LecturerID),
                        FOREIGN KEY (StudentID) REFERENCES Students(StudentID)
                    );


                    CREATE TABLE IF NOT EXISTS LectureSubjects (
                        LecturerID INTEGER NOT NULL,
                        SubjectID INTEGER NOT NULL,
                        PRIMARY KEY (LecturerID, SubjectID),
                        FOREIGN KEY (LecturerID) REFERENCES Lecturers(LecturerID),
                        FOREIGN KEY (SubjectID) REFERENCES Subjects(SubjectID)
                    );

                    CREATE TABLE IF NOT EXISTS Exams (
                        ExamID INTEGER PRIMARY KEY AUTOINCREMENT,
                        ExamName TEXT NOT NULL,
                        SubjectID INTEGER NOT NULL,
                        ExamDate TEXT NOT NULL,
                        ExamType TEXT NOT NULL,
                        MaxMarks INTEGER NOT NULL,
                        FOREIGN KEY (SubjectID) REFERENCES Subjects(SubjectID)
                    );

                    CREATE TABLE IF NOT EXISTS Marks (
                        MarkID INTEGER PRIMARY KEY AUTOINCREMENT,
                        StudentID INTEGER NOT NULL,
                        SubjectID INTEGER NOT NULL,
                        ExamID INTEGER NOT NULL,
                        MarksObtained INTEGER NOT NULL,
                        GradedByLecturerID INTEGER,
                        Grade TEXT,
                        EntryDate TEXT NOT NULL DEFAULT CURRENT_TIMESTAMP,
                        FOREIGN KEY (StudentID) REFERENCES Students(StudentID),
                        FOREIGN KEY (SubjectID) REFERENCES Subjects(SubjectID),
                        FOREIGN KEY (ExamID) REFERENCES Exams(ExamID),
                        FOREIGN KEY (GradedByLecturerID) REFERENCES Lecturers(LecturerID)
                    );


                    CREATE TABLE IF NOT EXISTS Assignments (
                        AssignmentID INTEGER PRIMARY KEY AUTOINCREMENT,
                        SubjectID INTEGER NOT NULL,
                        LecturerID INTEGER NOT NULL,
                        Title TEXT NOT NULL,
                        Description TEXT,
                        DueDate TEXT NOT NULL,
                        MaxMarks INTEGER,
                        FOREIGN KEY (SubjectID) REFERENCES Subjects(SubjectID),
                        FOREIGN KEY (LecturerID) REFERENCES Lecturers(LecturerID)
                    );

                    CREATE TABLE IF NOT EXISTS Submissions (
                        SubmissionID INTEGER PRIMARY KEY AUTOINCREMENT,
                        AssignmentID INTEGER NOT NULL,
                        StudentID INTEGER NOT NULL,
                        SubmittedAt TEXT NOT NULL DEFAULT CURRENT_TIMESTAMP,
                        Grade INTEGER,
                        GradedByLecturerID INTEGER,
                        FOREIGN KEY (AssignmentID) REFERENCES Assignments(AssignmentID),
                        FOREIGN KEY (StudentID) REFERENCES Students(StudentID),
                        FOREIGN KEY (GradedByLecturerID) REFERENCES Lecturers(LecturerID)
                    );

                  
                    CREATE TABLE IF NOT EXISTS Rooms (
                        RoomID INTEGER PRIMARY KEY AUTOINCREMENT,
                        RoomNumber TEXT NOT NULL UNIQUE,
                        Capacity INTEGER,
                        RoomType TEXT NOT NULL
                    );

                   
                    CREATE TABLE IF NOT EXISTS Timetables (
                        TimetableID INTEGER PRIMARY KEY AUTOINCREMENT,
                        CourseID INTEGER NOT NULL,
                        SubjectID INTEGER NOT NULL,
                        LecturerID INTEGER NOT NULL,
                        RoomID INTEGER NOT NULL,
                        DayOfWeek TEXT NOT NULL,
                        TimeSlotID INTEGER NOT NULL,
                        AcademicYear TEXT NOT NULL,
                        GroupId INTEGER NOT NULL,
                        ActivityType TEXT NOT NULL,
                        FOREIGN KEY (CourseID) REFERENCES Courses(CourseID),
                        FOREIGN KEY (SubjectID) REFERENCES Subjects(SubjectID),
                        FOREIGN KEY (TimeSlotID) REFERENCES TimeSlots(TimeSlotID)
                    );

                    CREATE TABLE IF NOT EXISTS Attendance (
                        AttendanceID INTEGER PRIMARY KEY AUTOINCREMENT,
                        StudentID INTEGER NOT NULL,
                        SubjectID INTEGER NOT NULL,
                        Date TEXT NOT NULL,
                        TimeSlot TEXT,
                        Status TEXT NOT NULL CHECK (Status IN ('Present', 'Absent', 'Late', 'Excused')),
                        FOREIGN KEY (StudentID) REFERENCES Students(StudentID),
                        FOREIGN KEY (SubjectID) REFERENCES Subjects(SubjectID),
                        UNIQUE (StudentID, SubjectID, Date, TimeSlot)
                    );

                  

                    CREATE TABLE IF NOT EXISTS Notifications (
                        NotificationID INTEGER PRIMARY KEY AUTOINCREMENT,
                        Title TEXT NOT NULL,
                        Message TEXT NOT NULL,
                        SentToUserID INTEGER,
                        SentToRole TEXT,
                        CreatedAt TEXT NOT NULL DEFAULT CURRENT_TIMESTAMP, -- Renamed from SentAt
                        IsRead INTEGER DEFAULT 0,
                        FOREIGN KEY (SentToUserID) REFERENCES Users(UserID)
                    );

                    CREATE TABLE IF NOT EXISTS BackupLogs (
                        BackupLogID INTEGER PRIMARY KEY AUTOINCREMENT,
                        CreatedAt TEXT NOT NULL DEFAULT CURRENT_TIMESTAMP, -- Renamed from BackupDate
                        BackupPath TEXT NOT NULL,
                        Status TEXT NOT NULL,
                        PerformedByUserID INTEGER,
                        FOREIGN KEY (PerformedByUserID) REFERENCES Users(UserID)
                    );

                    CREATE TABLE IF NOT EXISTS LeaveRequests (
                        LeaveID INTEGER PRIMARY KEY AUTOINCREMENT,
                        UserID INTEGER NOT NULL,
                        Reason TEXT,
                        StartDate TEXT NOT NULL,
                        EndDate TEXT NOT NULL,
                        CreatedAt TEXT NOT NULL DEFAULT CURRENT_TIMESTAMP, -- Renamed from RequestDate
                        Status TEXT NOT NULL,
                        ApprovedByUserID INTEGER,
                        FOREIGN KEY (UserID) REFERENCES Users(UserID),
                        FOREIGN KEY (ApprovedByUserID) REFERENCES Users(UserID)
                    );

                    CREATE TABLE IF NOT EXISTS StudentComplaints (
                        ComplaintID INTEGER PRIMARY KEY AUTOINCREMENT,
                        StudentID INTEGER NOT NULL,
                        ComplaintText TEXT NOT NULL,
                        CreatedAt TEXT NOT NULL DEFAULT CURRENT_TIMESTAMP, -- Renamed from SubmittedAt
                        Status TEXT NOT NULL,
                        ResolvedByUserID INTEGER,
                        ResolutionNotes TEXT,
                        FOREIGN KEY (StudentID) REFERENCES Students(StudentID),
                        FOREIGN KEY (ResolvedByUserID) REFERENCES Users(UserID)
                    );
                    ";



                cmd.ExecuteNonQuery();
            }
        }
    }
}