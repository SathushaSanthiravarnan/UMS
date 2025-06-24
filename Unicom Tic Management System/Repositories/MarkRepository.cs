using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Datas;
using Unicom_Tic_Management_System.Models;
using Unicom_Tic_Management_System.Models.DTOs.AcademicOperationsDTOs;
using Unicom_Tic_Management_System.Repositories.Interfaces;

namespace Unicom_Tic_Management_System.Repositories
{
    internal class MarkRepository : IMarkRepository
    {
        // Helper method to read Mark from SQLiteDataReader
        private Mark ReadMarkFromReader(SQLiteDataReader reader)
        {
            return new Mark
            {
                MarkId = reader.GetInt32(reader.GetOrdinal("MarkId")),
                StudentId = reader.GetInt32(reader.GetOrdinal("StudentId")),
                SubjectId = reader.GetInt32(reader.GetOrdinal("SubjectId")),
                ExamId = reader.GetInt32(reader.GetOrdinal("ExamId")),
                MarksObtained = reader.GetInt32(reader.GetOrdinal("MarksObtained")),
                GradedByLecturerId = reader.IsDBNull(reader.GetOrdinal("GradedByLecturerId")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("GradedByLecturerId")),
                Grade = reader.GetString(reader.GetOrdinal("Grade")),
                EntryDate = DateTime.Parse(reader.GetString(reader.GetOrdinal("EntryDate")))
            };
        }

        public void AddMark(Mark mark)
        {
            try
            {
                if (mark == null)
                    throw new ArgumentNullException(nameof(mark));

                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        INSERT INTO Marks (StudentId, SubjectId, ExamId, MarksObtained, GradedByLecturerId, Grade, EntryDate)
                        VALUES (@StudentId, @SubjectId, @ExamId, @MarksObtained, @GradedByLecturerId, @Grade, @EntryDate)";
                    cmd.Parameters.AddWithValue("@StudentId", mark.StudentId);
                    cmd.Parameters.AddWithValue("@SubjectId", mark.SubjectId);
                    cmd.Parameters.AddWithValue("@ExamId", mark.ExamId);
                    cmd.Parameters.AddWithValue("@MarksObtained", mark.MarksObtained);
                    cmd.Parameters.AddWithValue("@GradedByLecturerId", mark.GradedByLecturerId.HasValue ? (object)mark.GradedByLecturerId.Value : DBNull.Value); // Handle nullable int
                    cmd.Parameters.AddWithValue("@Grade", mark.Grade);
                    cmd.Parameters.AddWithValue("@EntryDate", mark.EntryDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while adding mark: " + ex.Message, ex);
            }
        }

        public void UpdateMark(Mark mark)
        {
            try
            {
                if (mark == null)
                    throw new ArgumentNullException(nameof(mark));

                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        UPDATE Marks
                        SET StudentId = @StudentId, SubjectId = @SubjectId, ExamId = @ExamId,
                            MarksObtained = @MarksObtained, GradedByLecturerId = @GradedByLecturerId,
                            Grade = @Grade, EntryDate = @EntryDate
                        WHERE MarkId = @MarkId";
                    cmd.Parameters.AddWithValue("@MarkId", mark.MarkId);
                    cmd.Parameters.AddWithValue("@StudentId", mark.StudentId);
                    cmd.Parameters.AddWithValue("@SubjectId", mark.SubjectId);
                    cmd.Parameters.AddWithValue("@ExamId", mark.ExamId);
                    cmd.Parameters.AddWithValue("@MarksObtained", mark.MarksObtained);
                    cmd.Parameters.AddWithValue("@GradedByLecturerId", mark.GradedByLecturerId.HasValue ? (object)mark.GradedByLecturerId.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@Grade", mark.Grade);
                    cmd.Parameters.AddWithValue("@EntryDate", mark.EntryDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while updating mark: " + ex.Message, ex);
            }
        }

        public void DeleteMark(int markId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "DELETE FROM Marks WHERE MarkId = @MarkId";
                    cmd.Parameters.AddWithValue("@MarkId", markId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while deleting mark: " + ex.Message, ex);
            }
        }

        public Mark GetMarkById(int markId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT MarkId, StudentId, SubjectId, ExamId, MarksObtained, GradedByLecturerId, Grade, EntryDate FROM Marks WHERE MarkId = @MarkId";
                    cmd.Parameters.AddWithValue("@MarkId", markId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return ReadMarkFromReader(reader);
                        }
                        return null;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving mark by ID: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing mark date: " + ex.Message, ex);
            }
        }

        public Mark GetMarkForStudentSubjectExam(int studentId, int subjectId, int examId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT MarkId, StudentId, SubjectId, ExamId, MarksObtained, GradedByLecturerId, Grade, EntryDate FROM Marks WHERE StudentId = @StudentId AND SubjectId = @SubjectId AND ExamId = @ExamId";
                    cmd.Parameters.AddWithValue("@StudentId", studentId);
                    cmd.Parameters.AddWithValue("@SubjectId", subjectId);
                    cmd.Parameters.AddWithValue("@ExamId", examId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return ReadMarkFromReader(reader);
                        }
                        return null;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving mark for student, subject, exam: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing mark date: " + ex.Message, ex);
            }
        }

        public List<Mark> GetMarksByStudentId(int studentId)
        {
            var marks = new List<Mark>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT MarkId, StudentId, SubjectId, ExamId, MarksObtained, GradedByLecturerId, Grade, EntryDate FROM Marks WHERE StudentId = @StudentId ORDER BY EntryDate DESC, SubjectId ASC";
                    cmd.Parameters.AddWithValue("@StudentId", studentId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            marks.Add(ReadMarkFromReader(reader));
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving marks by student ID: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing mark dates: " + ex.Message, ex);
            }
            return marks;
        }

        public List<Mark> GetMarksBySubjectId(int subjectId)
        {
            var marks = new List<Mark>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT MarkId, StudentId, SubjectId, ExamId, MarksObtained, GradedByLecturerId, Grade, EntryDate FROM Marks WHERE SubjectId = @SubjectId ORDER BY EntryDate DESC, StudentId ASC";
                    cmd.Parameters.AddWithValue("@SubjectId", subjectId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            marks.Add(ReadMarkFromReader(reader));
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving marks by subject ID: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing mark dates: " + ex.Message, ex);
            }
            return marks;
        }

        public List<Mark> GetMarksByExamId(int examId)
        {
            var marks = new List<Mark>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT MarkId, StudentId, SubjectId, ExamId, MarksObtained, GradedByLecturerId, Grade, EntryDate FROM Marks WHERE ExamId = @ExamId ORDER BY EntryDate DESC, StudentId ASC";
                    cmd.Parameters.AddWithValue("@ExamId", examId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            marks.Add(ReadMarkFromReader(reader));
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving marks by exam ID: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing mark dates: " + ex.Message, ex);
            }
            return marks;
        }

        public List<Mark> GetMarksByLecturerId(int lecturerId)
        {
            var marks = new List<Mark>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT MarkId, StudentId, SubjectId, ExamId, MarksObtained, GradedByLecturerId, Grade, EntryDate FROM Marks WHERE GradedByLecturerId = @LecturerId ORDER BY EntryDate DESC, StudentId ASC";
                    cmd.Parameters.AddWithValue("@LecturerId", lecturerId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            marks.Add(ReadMarkFromReader(reader));
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving marks by lecturer ID: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing mark dates: " + ex.Message, ex);
            }
            return marks;
        }

        public List<Mark> GetAllMarks()
        {
            var marks = new List<Mark>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT MarkId, StudentId, SubjectId, ExamId, MarksObtained, GradedByLecturerId, Grade, EntryDate FROM Marks ORDER BY EntryDate DESC, StudentId ASC, SubjectId ASC";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            marks.Add(ReadMarkFromReader(reader));
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving all marks: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing mark dates: " + ex.Message, ex);
            }
            return marks;
        }

      
         public List<MarkDisplayDto> GetAllMarksWithDetails()
        {
            List<MarkDisplayDto> marksWithDetails = new List<MarkDisplayDto>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        SELECT
                            M.MarkId,
                            M.StudentId,
                            M.SubjectId, Sub.SubjectName,
                            M.ExamId, E.ExamType,
                            M.MarksObtained,
                            M.GradedByLecturerId, L.LecturerName,
                            M.Grade,
                            M.EntryDate
                        FROM Marks M
                        JOIN Students S ON M.StudentId = S.StudentId
                        JOIN Subjects Sub ON M.SubjectId = Sub.SubjectId
                        JOIN Exams E ON M.ExamId = E.ExamId
                        LEFT JOIN Lecturers L ON M.GradedByLecturerId = L.LecturerId
                        ORDER BY M.EntryDate DESC, Sub.SubjectName;";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            marksWithDetails.Add(new MarkDisplayDto
                            {
                                MarkId = reader.GetInt32(reader.GetOrdinal("MarkId")),
                                StudentId = reader.GetInt32(reader.GetOrdinal("StudentId")),
                                StudentAdmissionNumber = reader.GetString(reader.GetOrdinal("AdmissionNumber")),
                                StudentName = $"{reader.GetString(reader.GetOrdinal("FirstName"))} {reader.GetString(reader.GetOrdinal("LastName"))}",
                                SubjectId = reader.GetInt32(reader.GetOrdinal("SubjectId")),
                                SubjectName = reader.GetString(reader.GetOrdinal("SubjectName")),
                                ExamId = reader.GetInt32(reader.GetOrdinal("ExamId")),
                                ExamType = reader.GetString(reader.GetOrdinal("ExamType")),
                                MarksObtained = reader.GetInt32(reader.GetOrdinal("MarksObtained")),
                                GradedByLecturerId = reader.IsDBNull(reader.GetOrdinal("GradedByLecturerId")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("GradedByLecturerId")),
                               
                                Grade = reader.GetString(reader.GetOrdinal("Grade")),
                                EntryDate = DateTime.Parse(reader.GetString(reader.GetOrdinal("EntryDate")))
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving all marks with details: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing mark details: " + ex.Message, ex);
            }
            return marksWithDetails;
        }

         public List<TopPerformerDto> GetTopNPerformers(int count)
        {
            List<TopPerformerDto> topPerformers = new List<TopPerformerDto>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = $@"
                        SELECT
                            S.AdmissionNumber
                           
                        FROM Marks M
                        JOIN Students S ON M.StudentId = S.StudentId
                        GROUP BY S.StudentId
                        
                        LIMIT {count};"; // Use LIMIT for Top N

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            topPerformers.Add(new TopPerformerDto
                            {
                                AdmissionNumber = reader.GetString(reader.GetOrdinal("AdmissionNumber")),
                                StudentName = reader.GetString(reader.GetOrdinal("Name")),
                               
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving top performers: " + ex.Message, ex);
            }
            return topPerformers;
        }
    }
}
