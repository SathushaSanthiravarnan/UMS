using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Datas;
using Unicom_Tic_Management_System.Models;
using Unicom_Tic_Management_System.Repositories.Interfaces;

namespace Unicom_Tic_Management_System.Repositories
{
    internal class ExamRepository : IExamRepository
    {
        public void AddExam(Exam exam)
        {
            try
            {
                if (exam == null)
                    throw new ArgumentNullException(nameof(exam));

                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        INSERT INTO Exams (ExamName, SubjectId, ExamDate, MaxMarks)
                        VALUES (@ExamName, @SubjectId, @ExamDate, @MaxMarks)";
                    cmd.Parameters.AddWithValue("@ExamName", exam.ExamName);
                    cmd.Parameters.AddWithValue("@SubjectId", exam.SubjectId);
                    cmd.Parameters.AddWithValue("@ExamDate", exam.ExamDate.ToString("yyyy-MM-dd HH:mm:ss")); // Store DateTime as string
                    cmd.Parameters.AddWithValue("@MaxMarks", exam.MaxMarks);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while adding exam: " + ex.Message, ex);
            }
        }

        public void UpdateExam(Exam exam)
        {
            try
            {
                if (exam == null)
                    throw new ArgumentNullException(nameof(exam));

                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        UPDATE Exams
                        SET ExamName = @ExamName, SubjectId = @SubjectId,
                            ExamDate = @ExamDate, MaxMarks = @MaxMarks
                        WHERE ExamId = @ExamId";
                    cmd.Parameters.AddWithValue("@ExamId", exam.ExamId);
                    cmd.Parameters.AddWithValue("@ExamName", exam.ExamName);
                    cmd.Parameters.AddWithValue("@SubjectId", exam.SubjectId);
                    cmd.Parameters.AddWithValue("@ExamDate", exam.ExamDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@MaxMarks", exam.MaxMarks);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while updating exam: " + ex.Message, ex);
            }
        }

        public void DeleteExam(int examId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "DELETE FROM Exams WHERE ExamId = @ExamId";
                    cmd.Parameters.AddWithValue("@ExamId", examId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while deleting exam: " + ex.Message, ex);
            }
        }

        public Exam GetExamById(int examId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT ExamId, ExamName, SubjectId, ExamDate, MaxMarks FROM Exams WHERE ExamId = @ExamId";
                    cmd.Parameters.AddWithValue("@ExamId", examId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Exam
                            {
                                ExamId = reader.GetInt32(0),
                                ExamName = reader.GetString(1),
                                SubjectId = reader.GetInt32(2),
                                ExamDate = DateTime.Parse(reader.GetString(3)), // Parse string to DateTime
                                MaxMarks = reader.GetInt32(4)
                            };
                        }
                        return null;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving exam by ID: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing exam date: " + ex.Message, ex);
            }
        }

        public List<Exam> GetExamsBySubjectId(int subjectId)
        {
            var exams = new List<Exam>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT ExamId, ExamName, SubjectId, ExamDate, MaxMarks FROM Exams WHERE SubjectId = @SubjectId ORDER BY ExamDate ASC";
                    cmd.Parameters.AddWithValue("@SubjectId", subjectId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            exams.Add(new Exam
                            {
                                ExamId = reader.GetInt32(0),
                                ExamName = reader.GetString(1),
                                SubjectId = reader.GetInt32(2),
                                ExamDate = DateTime.Parse(reader.GetString(3)),
                                MaxMarks = reader.GetInt32(4)
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving exams by subject ID: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing exam dates: " + ex.Message, ex);
            }
            return exams;
        }

        public List<Exam> GetExamsByDate(DateTime examDate)
        {
            var exams = new List<Exam>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    // Using strftime to compare only the date part
                    cmd.CommandText = "SELECT ExamId, ExamName, SubjectId, ExamDate, MaxMarks FROM Exams WHERE strftime('%Y-%m-%d', ExamDate) = @ExamDatePart ORDER BY ExamDate ASC, ExamName ASC";
                    cmd.Parameters.AddWithValue("@ExamDatePart", examDate.ToString("yyyy-MM-dd"));

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            exams.Add(new Exam
                            {
                                ExamId = reader.GetInt32(0),
                                ExamName = reader.GetString(1),
                                SubjectId = reader.GetInt32(2),
                                ExamDate = DateTime.Parse(reader.GetString(3)),
                                MaxMarks = reader.GetInt32(4)
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving exams by date: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing exam dates: " + ex.Message, ex);
            }
            return exams;
        }

        public List<Exam> GetAllExams()
        {
            var exams = new List<Exam>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT ExamId, ExamName, SubjectId, ExamDate, MaxMarks FROM Exams ORDER BY ExamDate ASC, ExamName ASC";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            exams.Add(new Exam
                            {
                                ExamId = reader.GetInt32(0),
                                ExamName = reader.GetString(1),
                                SubjectId = reader.GetInt32(2),
                                ExamDate = DateTime.Parse(reader.GetString(3)),
                                MaxMarks = reader.GetInt32(4)
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving all exams: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing exam dates: " + ex.Message, ex);
            }
            return exams;
        }
    }
}
