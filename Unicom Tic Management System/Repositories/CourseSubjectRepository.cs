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
    internal class CourseSubjectRepository : ICourseSubjectRepository
    {
        public void AddCourseSubject(CourseSubject courseSubject)
        {
            try
            {
                if (courseSubject == null)
                    throw new ArgumentNullException(nameof(courseSubject));

                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        INSERT INTO CourseSubjects (CourseId, SubjectId)
                        VALUES (@CourseId, @SubjectId)";
                    cmd.Parameters.AddWithValue("@CourseId", courseSubject.CourseId);
                    cmd.Parameters.AddWithValue("@SubjectId", courseSubject.SubjectId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while adding course-subject relationship: " + ex.Message, ex);
            }
        }

        public CourseSubject GetCourseSubject(int courseId, int subjectId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT CourseId, SubjectId FROM CourseSubjects WHERE CourseId = @CourseId AND SubjectId = @SubjectId";
                    cmd.Parameters.AddWithValue("@CourseId", courseId);
                    cmd.Parameters.AddWithValue("@SubjectId", subjectId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new CourseSubject
                            {
                                CourseId = reader.GetInt32(0),
                                SubjectId = reader.GetInt32(1)
                            };
                        }
                        return null;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving course-subject relationship: " + ex.Message, ex);
            }
        }

        public List<CourseSubject> GetCourseSubjectsByCourseId(int courseId)
        {
            var courseSubjects = new List<CourseSubject>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT CourseId, SubjectId FROM CourseSubjects WHERE CourseId = @CourseId";
                    cmd.Parameters.AddWithValue("@CourseId", courseId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            courseSubjects.Add(new CourseSubject()
                            {
                                CourseId = reader.GetInt32(0),
                                SubjectId = reader.GetInt32(1)
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving course-subject relationships by CourseId: " + ex.Message, ex);
            }
            return courseSubjects;
        }

        public List<CourseSubject> GetCourseSubjectsBySubjectId(int subjectId)
        {
            var courseSubjects = new List<CourseSubject>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT CourseId, SubjectId FROM CourseSubjects WHERE SubjectId = @SubjectId";
                    cmd.Parameters.AddWithValue("@SubjectId", subjectId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            courseSubjects.Add(new CourseSubject()
                            {
                                CourseId = reader.GetInt32(0),
                                SubjectId = reader.GetInt32(1)
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving course-subject relationships by SubjectId: " + ex.Message, ex);
            }
            return courseSubjects;
        }

        public void DeleteCourseSubject(int courseId, int subjectId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "DELETE FROM CourseSubjects WHERE CourseId = @CourseId AND SubjectId = @SubjectId";
                    cmd.Parameters.AddWithValue("@CourseId", courseId);
                    cmd.Parameters.AddWithValue("@SubjectId", subjectId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while deleting course-subject relationship: " + ex.Message, ex);
            }
        }
    }
}
