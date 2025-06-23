using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Datas;
using Unicom_Tic_Management_System.Models;
using Unicom_Tic_Management_System.Models.Enums;
using Unicom_Tic_Management_System.Repositories.Interfaces;

namespace Unicom_Tic_Management_System.Repositories
{
    internal class AttendanceRepository : IAttendanceRepository
    {
        public void AddAttendance(Attendance attendance)
        {
            try
            {
                if (attendance == null)
                    throw new ArgumentNullException(nameof(attendance));

                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        INSERT INTO Attendance (StudentId, SubjectId, Date, TimeSlotId, Status)
                        VALUES (@StudentId, @SubjectId, @Date, @TimeSlotId, @Status)";
                    cmd.Parameters.AddWithValue("@StudentId", attendance.StudentId);
                    cmd.Parameters.AddWithValue("@SubjectId", attendance.SubjectId);
                    cmd.Parameters.AddWithValue("@Date", attendance.Date.ToString("yyyy-MM-dd HH:mm:ss")); // Store DateTime as string
                    cmd.Parameters.AddWithValue("@TimeSlotId", attendance.TimeSlotId);
                    cmd.Parameters.AddWithValue("@Status", attendance.Status.ToString()); // Store enum as string
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while adding attendance record: " + ex.Message, ex);
            }
        }

        public void UpdateAttendance(Attendance attendance)
        {
            try
            {
                if (attendance == null)
                    throw new ArgumentNullException(nameof(attendance));

                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        UPDATE Attendance
                        SET StudentId = @StudentId, SubjectId = @SubjectId, Date = @Date,
                            TimeSlotId = @TimeSlotId, Status = @Status
                        WHERE AttendanceId = @AttendanceId";
                    cmd.Parameters.AddWithValue("@AttendanceId", attendance.AttendanceId);
                    cmd.Parameters.AddWithValue("@StudentId", attendance.StudentId);
                    cmd.Parameters.AddWithValue("@SubjectId", attendance.SubjectId);
                    cmd.Parameters.AddWithValue("@Date", attendance.Date.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@TimeSlotId", attendance.TimeSlotId);
                    cmd.Parameters.AddWithValue("@Status", attendance.Status.ToString());
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while updating attendance record: " + ex.Message, ex);
            }
        }

        public void DeleteAttendance(int attendanceId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "DELETE FROM Attendance WHERE AttendanceId = @AttendanceId";
                    cmd.Parameters.AddWithValue("@AttendanceId", attendanceId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while deleting attendance record: " + ex.Message, ex);
            }
        }

        public Attendance GetAttendanceById(int attendanceId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT AttendanceId, StudentId, SubjectId, Date, TimeSlotId, Status FROM Attendance WHERE AttendanceId = @AttendanceId";
                    cmd.Parameters.AddWithValue("@AttendanceId", attendanceId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Attendance
                            {
                                AttendanceId = reader.GetInt32(0),
                                StudentId = reader.GetInt32(1),
                                SubjectId = reader.GetInt32(2),
                                Date = DateTime.Parse(reader.GetString(3)), // Parse string to DateTime
                                TimeSlotId = reader.GetInt32(4),
                                Status = (AttendanceStatus)Enum.Parse(typeof(AttendanceStatus), reader.GetString(5)) 
                            };
                        }
                        return null;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving attendance by ID: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing attendance date or status: " + ex.Message, ex);
            }
        }

        public List<Attendance> GetAttendanceByStudentId(int studentId)
        {
            var attendanceRecords = new List<Attendance>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT AttendanceId, StudentId, SubjectId, Date, TimeSlotId, Status FROM Attendance WHERE StudentId = @StudentId ORDER BY Date DESC, TimeSlotId DESC";
                    cmd.Parameters.AddWithValue("@StudentId", studentId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            attendanceRecords.Add(new Attendance
                            {
                                AttendanceId = reader.GetInt32(0),
                                StudentId = reader.GetInt32(1),
                                SubjectId = reader.GetInt32(2),
                                Date = DateTime.Parse(reader.GetString(3)),
                                TimeSlotId = reader.GetInt32(4),
                                Status = (AttendanceStatus)Enum.Parse(typeof(AttendanceStatus), reader.GetString(5))
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving attendance by student ID: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing attendance dates or statuses: " + ex.Message, ex);
            }
            return attendanceRecords;
        }

        public List<Attendance> GetAttendanceBySubjectId(int subjectId)
        {
            var attendanceRecords = new List<Attendance>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT AttendanceId, StudentId, SubjectId, Date, TimeSlotId, Status FROM Attendance WHERE SubjectId = @SubjectId ORDER BY Date DESC, TimeSlotId DESC";
                    cmd.Parameters.AddWithValue("@SubjectId", subjectId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            attendanceRecords.Add(new Attendance
                            {
                                AttendanceId = reader.GetInt32(0),
                                StudentId = reader.GetInt32(1),
                                SubjectId = reader.GetInt32(2),
                                Date = DateTime.Parse(reader.GetString(3)),
                                TimeSlotId = reader.GetInt32(4),
                                Status = (AttendanceStatus)Enum.Parse(typeof(AttendanceStatus), reader.GetString(5))
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving attendance by subject ID: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing attendance dates or statuses: " + ex.Message, ex);
            }
            return attendanceRecords;
        }

        public List<Attendance> GetAttendanceByDate(DateTime date)
        {
            var attendanceRecords = new List<Attendance>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();                   
                    cmd.CommandText = "SELECT AttendanceId, StudentId, SubjectId, Date, TimeSlotId, Status FROM Attendance WHERE strftime('%Y-%m-%d', Date) = @DatePart ORDER BY StudentId ASC, TimeSlotId ASC";
                    cmd.Parameters.AddWithValue("@DatePart", date.ToString("yyyy-MM-dd")); 

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            attendanceRecords.Add(new Attendance
                            {
                                AttendanceId = reader.GetInt32(0),
                                StudentId = reader.GetInt32(1),
                                SubjectId = reader.GetInt32(2),
                                Date = DateTime.Parse(reader.GetString(3)),
                                TimeSlotId = reader.GetInt32(4),
                                Status = (AttendanceStatus)Enum.Parse(typeof(AttendanceStatus), reader.GetString(5))
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving attendance by date: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing attendance dates or statuses: " + ex.Message, ex);
            }
            return attendanceRecords;
        }

        public List<Attendance> GetAttendanceByStudentAndSubject(int studentId, int subjectId)
        {
            var attendanceRecords = new List<Attendance>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT AttendanceId, StudentId, SubjectId, Date, TimeSlotId, Status FROM Attendance WHERE StudentId = @StudentId AND SubjectId = @SubjectId ORDER BY Date DESC, TimeSlotId DESC";
                    cmd.Parameters.AddWithValue("@StudentId", studentId);
                    cmd.Parameters.AddWithValue("@SubjectId", subjectId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            attendanceRecords.Add(new Attendance
                            {
                                AttendanceId = reader.GetInt32(0),
                                StudentId = reader.GetInt32(1),
                                SubjectId = reader.GetInt32(2),
                                Date = DateTime.Parse(reader.GetString(3)),
                                TimeSlotId = reader.GetInt32(4),
                                Status = (AttendanceStatus)Enum.Parse(typeof(AttendanceStatus), reader.GetString(5))
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving attendance by student and subject: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing attendance dates or statuses: " + ex.Message, ex);
            }
            return attendanceRecords;
        }

        public List<Attendance> GetAttendanceByStudentAndDate(int studentId, DateTime date)
        {
            var attendanceRecords = new List<Attendance>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT AttendanceId, StudentId, SubjectId, Date, TimeSlotId, Status FROM Attendance WHERE StudentId = @StudentId AND strftime('%Y-%m-%d', Date) = @DatePart ORDER BY TimeSlotId ASC";
                    cmd.Parameters.AddWithValue("@StudentId", studentId);
                    cmd.Parameters.AddWithValue("@DatePart", date.ToString("yyyy-MM-dd"));

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            attendanceRecords.Add(new Attendance
                            {
                                AttendanceId = reader.GetInt32(0),
                                StudentId = reader.GetInt32(1),
                                SubjectId = reader.GetInt32(2),
                                Date = DateTime.Parse(reader.GetString(3)),
                                TimeSlotId = reader.GetInt32(4),
                                Status = (AttendanceStatus)Enum.Parse(typeof(AttendanceStatus), reader.GetString(5))
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving attendance by student and date: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing attendance dates or statuses: " + ex.Message, ex);
            }
            return attendanceRecords;
        }


        public List<Attendance> GetAllAttendance()
        {
            var attendanceRecords = new List<Attendance>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT AttendanceId, StudentId, SubjectId, Date, TimeSlotId, Status FROM Attendance ORDER BY Date DESC, StudentId ASC, TimeSlotId DESC";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            attendanceRecords.Add(new Attendance
                            {
                                AttendanceId = reader.GetInt32(0),
                                StudentId = reader.GetInt32(1),
                                SubjectId = reader.GetInt32(2),
                                Date = DateTime.Parse(reader.GetString(3)),
                                TimeSlotId = reader.GetInt32(4),
                                Status = (AttendanceStatus)Enum.Parse(typeof(AttendanceStatus), reader.GetString(5))
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving all attendance records: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing attendance dates or statuses: " + ex.Message, ex);
            }
            return attendanceRecords;
        }
    }
}
