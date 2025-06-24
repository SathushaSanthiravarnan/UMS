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
    internal class SubjectRepository : ISubjectRepository
    {
        public void AddSubject(Subject subject)
        {
            if (subject == null)
                throw new ArgumentNullException(nameof(subject));

            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        INSERT INTO Subjects (SubjectName, CourseId)
                        VALUES (@SubjectName, @CourseId)";
                    cmd.Parameters.AddWithValue("@SubjectName", subject.SubjectName);
                    cmd.Parameters.AddWithValue("@CourseId", subject.CourseId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                if (ex.Message.Contains("UNIQUE constraint failed"))
                    throw new Exception($"Subject '{subject.SubjectName}' already exists for Course ID {subject.CourseId}.", ex);
                throw new Exception("Database error while adding subject: " + ex.Message, ex);
            }
        }

        public void UpdateSubject(Subject subject)
        {
            if (subject == null)
                throw new ArgumentNullException(nameof(subject));

            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        UPDATE Subjects
                        SET SubjectName = @SubjectName, CourseId = @CourseId
                        WHERE SubjectId = @SubjectId";
                    cmd.Parameters.AddWithValue("@SubjectId", subject.SubjectId);
                    cmd.Parameters.AddWithValue("@SubjectName", subject.SubjectName);
                    cmd.Parameters.AddWithValue("@CourseId", subject.CourseId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                if (ex.Message.Contains("UNIQUE constraint failed"))
                    throw new Exception($"Subject '{subject.SubjectName}' already exists for Course ID {subject.CourseId}.", ex);
                throw new Exception("Database error while updating subject: " + ex.Message, ex);
            }
        }

        public void DeleteSubject(int subjectId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "DELETE FROM Subjects WHERE SubjectId = @SubjectId";
                    cmd.Parameters.AddWithValue("@SubjectId", subjectId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while deleting subject: " + ex.Message, ex);
            }
        }

        public Subject GetSubjectById(int subjectId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT SubjectId, SubjectName, CourseId FROM Subjects WHERE SubjectId = @SubjectId";
                    cmd.Parameters.AddWithValue("@SubjectId", subjectId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Subject
                            {
                                SubjectId = reader.GetInt32(0),
                                SubjectName = reader.GetString(1),
                                CourseId = reader.GetInt32(2)
                            };
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving subject by ID: " + ex.Message, ex);
            }

            return null;
        }

        public Subject GetSubjectByNameAndCourse(string subjectName, int courseId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        SELECT SubjectId, SubjectName, CourseId 
                        FROM Subjects 
                        WHERE SubjectName = @SubjectName AND CourseId = @CourseId";
                    cmd.Parameters.AddWithValue("@SubjectName", subjectName);
                    cmd.Parameters.AddWithValue("@CourseId", courseId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Subject
                            {
                                SubjectId = reader.GetInt32(0),
                                SubjectName = reader.GetString(1),
                                CourseId = reader.GetInt32(2)
                            };
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving subject by name and course: " + ex.Message, ex);
            }

            return null;
        }

        public List<Subject> GetSubjectsByCourseId(int courseId)
        {
            var subjects = new List<Subject>();

            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT SubjectId, SubjectName, CourseId FROM Subjects WHERE CourseId = @CourseId ORDER BY SubjectName ASC";
                    cmd.Parameters.AddWithValue("@CourseId", courseId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            subjects.Add(new Subject
                            {
                                SubjectId = reader.GetInt32(0),
                                SubjectName = reader.GetString(1),
                                CourseId = reader.GetInt32(2)
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving subjects by course ID: " + ex.Message, ex);
            }

            return subjects;
        }

        public List<Subject> GetAllSubjects()
        {
            var subjects = new List<Subject>();

            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT SubjectId, SubjectName, CourseId FROM Subjects ORDER BY SubjectName ASC";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            subjects.Add(new Subject
                            {
                                SubjectId = reader.GetInt32(0),
                                SubjectName = reader.GetString(1),
                                CourseId = reader.GetInt32(2)
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving all subjects: " + ex.Message, ex);
            }

            return subjects;
        }

        public List<Subject> GetAllSubjectsWithCourseNames()
        {
            var subjects = new List<Subject>();

            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        SELECT s.SubjectId, s.SubjectName, s.CourseId, c.CourseName, c.Description
                        FROM Subjects s
                        INNER JOIN Courses c ON s.CourseId = c.CourseId";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            subjects.Add(new Subject
                            {
                                SubjectId = Convert.ToInt32(reader["SubjectId"]),
                                SubjectName = reader["SubjectName"].ToString(),
                                CourseId = Convert.ToInt32(reader["CourseId"]),
                                Course = new Course
                                {
                                    CourseId = Convert.ToInt32(reader["CourseId"]),
                                    CourseName = reader["CourseName"].ToString(),
                                    Description = reader["Description"]?.ToString()
                                }
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving subjects with course names: " + ex.Message, ex);
            }

            return subjects;
        }
    }
}
