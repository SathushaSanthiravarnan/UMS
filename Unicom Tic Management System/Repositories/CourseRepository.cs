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
    internal class CourseRepository : ICourseRepository
    {
        public void AddCourse(Course course)
        {
            try
            {
                if (course == null)
                    throw new ArgumentNullException(nameof(course));

                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        INSERT INTO Courses (CourseName, Description)
                        VALUES (@CourseName, @Description)";
                    cmd.Parameters.AddWithValue("@CourseName", course.CourseName);
                    cmd.Parameters.AddWithValue("@Description", course.Description);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while adding course: " + ex.Message, ex);
            }
        }

        public void UpdateCourse(Course course)
        {
            try
            {
                if (course == null)
                    throw new ArgumentNullException(nameof(course));

                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        UPDATE Courses
                        SET CourseName = @CourseName, Description = @Description
                        WHERE CourseId = @CourseId";
                    cmd.Parameters.AddWithValue("@CourseId", course.CourseId);
                    cmd.Parameters.AddWithValue("@CourseName", course.CourseName);
                    cmd.Parameters.AddWithValue("@Description", course.Description);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while updating course: " + ex.Message, ex);
            }
        }

        public void DeleteCourse(int courseId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "DELETE FROM Courses WHERE CourseId = @CourseId";
                    cmd.Parameters.AddWithValue("@CourseId", courseId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while deleting course: " + ex.Message, ex);
            }
        }

        public Course GetCourseById(int courseId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT CourseId, CourseName, Description FROM Courses WHERE CourseId = @CourseId";
                    cmd.Parameters.AddWithValue("@CourseId", courseId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Course
                            {
                                CourseId = reader.GetInt32(0),
                                CourseName = reader.GetString(1),
                                Description = reader.GetString(2)
                            };
                        }
                        return null;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving course by ID: " + ex.Message, ex);
            }
        }

        public Course GetCourseByName(string courseName)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT CourseId, CourseName, Description FROM Courses WHERE CourseName = @CourseName";
                    cmd.Parameters.AddWithValue("@CourseName", courseName);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Course
                            {
                                CourseId = reader.GetInt32(0),
                                CourseName = reader.GetString(1),
                                Description = reader.GetString(2)
                            };
                        }
                        return null;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving course by name: " + ex.Message, ex);
            }
        }

        public List<Course> GetAllCourses()
        {
            var courses = new List<Course>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT CourseId, CourseName, Description FROM Courses";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            courses.Add(new Course
                            {
                                CourseId = Convert.ToInt32(reader["CourseId"]),
                                CourseName = reader["CourseName"]?.ToString() ?? "",
                                Description = reader["Description"]?.ToString() ?? ""

                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving all courses: " + ex.Message, ex);
            }
            return courses;
        }
    }
}
