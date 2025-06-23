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
    internal class AssignmentRepository : IAssignmentRepository
    {
        public void AddAssignment(Assignment assignment)
        {
            try
            {
                if (assignment == null)
                    throw new ArgumentNullException(nameof(assignment));

                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        INSERT INTO Assignments (SubjectId, LecturerId, Title, Description, DueDate, MaxMarks)
                        VALUES (@SubjectId, @LecturerId, @Title, @Description, @DueDate, @MaxMarks)";
                    cmd.Parameters.AddWithValue("@SubjectId", assignment.SubjectId);
                    cmd.Parameters.AddWithValue("@LecturerId", assignment.LecturerId);
                    cmd.Parameters.AddWithValue("@Title", assignment.Title);
                    cmd.Parameters.AddWithValue("@Description", assignment.Description);
                    cmd.Parameters.AddWithValue("@DueDate", assignment.DueDate.ToString("yyyy-MM-dd HH:mm:ss")); // Store DateTime as string
                    cmd.Parameters.AddWithValue("@MaxMarks", assignment.MaxMarks.HasValue ? (object)assignment.MaxMarks.Value : DBNull.Value); // Handle nullable MaxMarks
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while adding assignment: " + ex.Message, ex);
            }
        }

        public void UpdateAssignment(Assignment assignment)
        {
            try
            {
                if (assignment == null)
                    throw new ArgumentNullException(nameof(assignment));

                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        UPDATE Assignments
                        SET SubjectId = @SubjectId, LecturerId = @LecturerId, Title = @Title,
                            Description = @Description, DueDate = @DueDate, MaxMarks = @MaxMarks
                        WHERE AssignmentId = @AssignmentId";
                    cmd.Parameters.AddWithValue("@AssignmentId", assignment.AssignmentId);
                    cmd.Parameters.AddWithValue("@SubjectId", assignment.SubjectId);
                    cmd.Parameters.AddWithValue("@LecturerId", assignment.LecturerId);
                    cmd.Parameters.AddWithValue("@Title", assignment.Title);
                    cmd.Parameters.AddWithValue("@Description", assignment.Description);
                    cmd.Parameters.AddWithValue("@DueDate", assignment.DueDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@MaxMarks", assignment.MaxMarks.HasValue ? (object)assignment.MaxMarks.Value : DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while updating assignment: " + ex.Message, ex);
            }
        }

        public void DeleteAssignment(int assignmentId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "DELETE FROM Assignments WHERE AssignmentId = @AssignmentId";
                    cmd.Parameters.AddWithValue("@AssignmentId", assignmentId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while deleting assignment: " + ex.Message, ex);
            }
        }

        public Assignment GetAssignmentById(int assignmentId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT AssignmentId, SubjectId, LecturerId, Title, Description, DueDate, MaxMarks FROM Assignments WHERE AssignmentId = @AssignmentId";
                    cmd.Parameters.AddWithValue("@AssignmentId", assignmentId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Assignment
                            {
                                AssignmentId = reader.GetInt32(0),
                                SubjectId = reader.GetInt32(1),
                                LecturerId = reader.GetInt32(2),
                                Title = reader.GetString(3),
                                Description = reader.GetString(4),
                                DueDate = DateTime.Parse(reader.GetString(5)), // Parse string to DateTime
                                MaxMarks = reader.IsDBNull(6) ? (int?)null : reader.GetInt32(6) // Handle nullable MaxMarks
                            };
                        }
                        return null;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving assignment by ID: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing assignment date or marks: " + ex.Message, ex);
            }
        }

        public List<Assignment> GetAssignmentsBySubjectId(int subjectId)
        {
            var assignments = new List<Assignment>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT AssignmentId, SubjectId, LecturerId, Title, Description, DueDate, MaxMarks FROM Assignments WHERE SubjectId = @SubjectId ORDER BY DueDate ASC";
                    cmd.Parameters.AddWithValue("@SubjectId", subjectId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            assignments.Add(new Assignment
                            {
                                AssignmentId = reader.GetInt32(0),
                                SubjectId = reader.GetInt32(1),
                                LecturerId = reader.GetInt32(2),
                                Title = reader.GetString(3),
                                Description = reader.GetString(4),
                                DueDate = DateTime.Parse(reader.GetString(5)),
                                MaxMarks = reader.IsDBNull(6) ? (int?)null : reader.GetInt32(6)
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving assignments by subject ID: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing assignment dates or marks: " + ex.Message, ex);
            }
            return assignments;
        }

        public List<Assignment> GetAssignmentsByLecturerId(int lecturerId)
        {
            var assignments = new List<Assignment>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT AssignmentId, SubjectId, LecturerId, Title, Description, DueDate, MaxMarks FROM Assignments WHERE LecturerId = @LecturerId ORDER BY DueDate ASC";
                    cmd.Parameters.AddWithValue("@LecturerId", lecturerId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            assignments.Add(new Assignment
                            {
                                AssignmentId = reader.GetInt32(0),
                                SubjectId = reader.GetInt32(1),
                                LecturerId = reader.GetInt32(2),
                                Title = reader.GetString(3),
                                Description = reader.GetString(4),
                                DueDate = DateTime.Parse(reader.GetString(5)),
                                MaxMarks = reader.IsDBNull(6) ? (int?)null : reader.GetInt32(6)
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving assignments by lecturer ID: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing assignment dates or marks: " + ex.Message, ex);
            }
            return assignments;
        }

        public List<Assignment> GetAllAssignments()
        {
            var assignments = new List<Assignment>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT AssignmentId, SubjectId, LecturerId, Title, Description, DueDate, MaxMarks FROM Assignments ORDER BY DueDate ASC";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            assignments.Add(new Assignment
                            {
                                AssignmentId = reader.GetInt32(0),
                                SubjectId = reader.GetInt32(1),
                                LecturerId = reader.GetInt32(2),
                                Title = reader.GetString(3),
                                Description = reader.GetString(4),
                                DueDate = DateTime.Parse(reader.GetString(5)),
                                MaxMarks = reader.IsDBNull(6) ? (int?)null : reader.GetInt32(6)
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving all assignments: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing assignment dates or marks: " + ex.Message, ex);
            }
            return assignments;
        }
    }
}
