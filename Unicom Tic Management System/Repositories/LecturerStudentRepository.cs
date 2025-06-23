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
    internal class LecturerStudentRepository : ILecturerStudentRepository
    {
        public void AddLecturerStudent(LecturerStudent lecturerStudent)
        {
            try
            {
                if (lecturerStudent == null)
                    throw new ArgumentNullException(nameof(lecturerStudent));

                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        INSERT INTO LecturerStudents (LecturerId, StudentId, AssignedDate, RelationshipType)
                        VALUES (@LecturerId, @StudentId, @AssignedDate, @RelationshipType)";
                    cmd.Parameters.AddWithValue("@LecturerId", lecturerStudent.LecturerId);
                    cmd.Parameters.AddWithValue("@StudentId", lecturerStudent.StudentId);
                    cmd.Parameters.AddWithValue("@AssignedDate", lecturerStudent.AssignedDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@RelationshipType", lecturerStudent.RelationshipType);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while adding lecturer-student relationship: " + ex.Message, ex);
            }
        }

        public void UpdateLecturerStudent(LecturerStudent lecturerStudent)
        {
            try
            {
                if (lecturerStudent == null)
                    throw new ArgumentNullException(nameof(lecturerStudent));

                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        UPDATE LecturerStudents
                        SET AssignedDate = @AssignedDate,
                            RelationshipType = @RelationshipType
                        WHERE LecturerId = @LecturerId AND StudentId = @StudentId";
                    cmd.Parameters.AddWithValue("@LecturerId", lecturerStudent.LecturerId);
                    cmd.Parameters.AddWithValue("@StudentId", lecturerStudent.StudentId);
                    cmd.Parameters.AddWithValue("@AssignedDate", lecturerStudent.AssignedDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@RelationshipType", lecturerStudent.RelationshipType);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while updating lecturer-student relationship: " + ex.Message, ex);
            }
        }

        public void DeleteLecturerStudent(int lecturerId, int studentId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "DELETE FROM LecturerStudents WHERE LecturerId = @LecturerId AND StudentId = @StudentId";
                    cmd.Parameters.AddWithValue("@LecturerId", lecturerId);
                    cmd.Parameters.AddWithValue("@StudentId", studentId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while deleting lecturer-student relationship: " + ex.Message, ex);
            }
        }

        public LecturerStudent GetLecturerStudent(int lecturerId, int studentId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT LecturerId, StudentId, AssignedDate, RelationshipType FROM LecturerStudents WHERE LecturerId = @LecturerId AND StudentId = @StudentId";
                    cmd.Parameters.AddWithValue("@LecturerId", lecturerId);
                    cmd.Parameters.AddWithValue("@StudentId", studentId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new LecturerStudent
                            {
                                LecturerId = reader.GetInt32(0),
                                StudentId = reader.GetInt32(1),
                                AssignedDate = DateTime.Parse(reader.GetString(2)),
                                RelationshipType = reader.GetString(3)
                            };
                        }
                        return null;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving lecturer-student relationship: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing assigned date: " + ex.Message, ex);
            }
        }

        public List<LecturerStudent> GetStudentsByLecturerId(int lecturerId)
        {
            var lecturerStudents = new List<LecturerStudent>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT LecturerId, StudentId, AssignedDate, RelationshipType FROM LecturerStudents WHERE LecturerId = @LecturerId ORDER BY StudentId ASC";
                    cmd.Parameters.AddWithValue("@LecturerId", lecturerId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lecturerStudents.Add(new LecturerStudent
                            {
                                LecturerId = reader.GetInt32(0),
                                StudentId = reader.GetInt32(1),
                                AssignedDate = DateTime.Parse(reader.GetString(2)),
                                RelationshipType = reader.GetString(3)
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving students by lecturer ID: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing assigned dates: " + ex.Message, ex);
            }
            return lecturerStudents;
        }

        public List<LecturerStudent> GetLecturersByStudentId(int studentId)
        {
            var lecturerStudents = new List<LecturerStudent>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT LecturerId, StudentId, AssignedDate, RelationshipType FROM LecturerStudents WHERE StudentId = @StudentId ORDER BY LecturerId ASC";
                    cmd.Parameters.AddWithValue("@StudentId", studentId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lecturerStudents.Add(new LecturerStudent
                            {
                                LecturerId = reader.GetInt32(0),
                                StudentId = reader.GetInt32(1),
                                AssignedDate = DateTime.Parse(reader.GetString(2)),
                                RelationshipType = reader.GetString(3)
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving lecturers by student ID: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing assigned dates: " + ex.Message, ex);
            }
            return lecturerStudents;
        }

        public List<LecturerStudent> GetAllLecturerStudentRelationships()
        {
            var lecturerStudents = new List<LecturerStudent>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT LecturerId, StudentId, AssignedDate, RelationshipType FROM LecturerStudents ORDER BY LecturerId ASC, StudentId ASC";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lecturerStudents.Add(new LecturerStudent
                            {
                                LecturerId = reader.GetInt32(0),
                                StudentId = reader.GetInt32(1),
                                AssignedDate = DateTime.Parse(reader.GetString(2)),
                                RelationshipType = reader.GetString(3)
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving all lecturer-student relationships: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing assigned dates: " + ex.Message, ex);
            }
            return lecturerStudents;
        }
    }
}
