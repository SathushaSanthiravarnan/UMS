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
    internal class SubmissionRepository : ISubmissionRepository
    {
        public void AddSubmission(Submission submission)
        {
            try
            {
                if (submission == null)
                    throw new ArgumentNullException(nameof(submission));

                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        INSERT INTO Submissions (AssignmentId, StudentId, SubmittedAt, Grade, GradedByLecturerId)
                        VALUES (@AssignmentId, @StudentId, @SubmittedAt, @Grade, @GradedByLecturerId)";
                    cmd.Parameters.AddWithValue("@AssignmentId", submission.AssignmentId);
                    cmd.Parameters.AddWithValue("@StudentId", submission.StudentId);
                    cmd.Parameters.AddWithValue("@SubmittedAt", submission.SubmittedAt.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@Grade", submission.Grade.HasValue ? (object)submission.Grade.Value : DBNull.Value); // Nullable int
                    cmd.Parameters.AddWithValue("@GradedByLecturerId", submission.GradedByLecturerId.HasValue ? (object)submission.GradedByLecturerId.Value : DBNull.Value); // Nullable int
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                if (ex.Message.Contains("UNIQUE constraint failed"))
                {
                    throw new Exception($"A submission for Assignment ID {submission.AssignmentId} by Student ID {submission.StudentId} already exists.", ex);
                }
                throw new Exception("Database error while adding submission: " + ex.Message, ex);
            }
        }

        public void UpdateSubmission(Submission submission)
        {
            try
            {
                if (submission == null)
                    throw new ArgumentNullException(nameof(submission));

                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        UPDATE Submissions
                        SET SubmittedAt = @SubmittedAt, Grade = @Grade, GradedByLecturerId = @GradedByLecturerId
                        WHERE SubmissionId = @SubmissionId";
                    cmd.Parameters.AddWithValue("@SubmissionId", submission.SubmissionId);
                    cmd.Parameters.AddWithValue("@SubmittedAt", submission.SubmittedAt.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@Grade", submission.Grade.HasValue ? (object)submission.Grade.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@GradedByLecturerId", submission.GradedByLecturerId.HasValue ? (object)submission.GradedByLecturerId.Value : DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while updating submission: " + ex.Message, ex);
            }
        }

        public void DeleteSubmission(int submissionId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "DELETE FROM Submissions WHERE SubmissionId = @SubmissionId";
                    cmd.Parameters.AddWithValue("@SubmissionId", submissionId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while deleting submission: " + ex.Message, ex);
            }
        }

        public Submission GetSubmissionById(int submissionId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT SubmissionId, AssignmentId, StudentId, SubmittedAt, Grade, GradedByLecturerId FROM Submissions WHERE SubmissionId = @SubmissionId";
                    cmd.Parameters.AddWithValue("@SubmissionId", submissionId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Submission
                            {
                                SubmissionId = reader.GetInt32(0),
                                AssignmentId = reader.GetInt32(1),
                                StudentId = reader.GetInt32(2),
                                SubmittedAt = DateTime.Parse(reader.GetString(3)),
                                Grade = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4),
                                GradedByLecturerId = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5)
                            };
                        }
                        return null;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving submission by ID: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing submission dates/grades: " + ex.Message, ex);
            }
        }

        public Submission GetSubmissionByAssignmentAndStudent(int assignmentId, int studentId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT SubmissionId, AssignmentId, StudentId, SubmittedAt, Grade, GradedByLecturerId FROM Submissions WHERE AssignmentId = @AssignmentId AND StudentId = @StudentId";
                    cmd.Parameters.AddWithValue("@AssignmentId", assignmentId);
                    cmd.Parameters.AddWithValue("@StudentId", studentId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Submission
                            {
                                SubmissionId = reader.GetInt32(0),
                                AssignmentId = reader.GetInt32(1),
                                StudentId = reader.GetInt32(2),
                                SubmittedAt = DateTime.Parse(reader.GetString(3)),
                                Grade = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4),
                                GradedByLecturerId = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5)
                            };
                        }
                        return null;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving submission by assignment and student: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing submission dates/grades: " + ex.Message, ex);
            }
        }

        public List<Submission> GetSubmissionsByAssignmentId(int assignmentId)
        {
            var submissions = new List<Submission>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT SubmissionId, AssignmentId, StudentId, SubmittedAt, Grade, GradedByLecturerId FROM Submissions WHERE AssignmentId = @AssignmentId ORDER BY SubmittedAt DESC";
                    cmd.Parameters.AddWithValue("@AssignmentId", assignmentId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            submissions.Add(new Submission
                            {
                                SubmissionId = reader.GetInt32(0),
                                AssignmentId = reader.GetInt32(1),
                                StudentId = reader.GetInt32(2),
                                SubmittedAt = DateTime.Parse(reader.GetString(3)),
                                Grade = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4),
                                GradedByLecturerId = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5)
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving submissions by assignment ID: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing submission dates/grades: " + ex.Message, ex);
            }
            return submissions;
        }

        public List<Submission> GetSubmissionsByStudentId(int studentId)
        {
            var submissions = new List<Submission>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT SubmissionId, AssignmentId, StudentId, SubmittedAt, Grade, GradedByLecturerId FROM Submissions WHERE StudentId = @StudentId ORDER BY SubmittedAt DESC";
                    cmd.Parameters.AddWithValue("@StudentId", studentId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            submissions.Add(new Submission
                            {
                                SubmissionId = reader.GetInt32(0),
                                AssignmentId = reader.GetInt32(1),
                                StudentId = reader.GetInt32(2),
                                SubmittedAt = DateTime.Parse(reader.GetString(3)),
                                Grade = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4),
                                GradedByLecturerId = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5)
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving submissions by student ID: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing submission dates/grades: " + ex.Message, ex);
            }
            return submissions;
        }

        public List<Submission> GetSubmissionsByLecturerId(int lecturerId)
        {
            var submissions = new List<Submission>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT SubmissionId, AssignmentId, StudentId, SubmittedAt, Grade, GradedByLecturerId FROM Submissions WHERE GradedByLecturerId = @LecturerId ORDER BY SubmittedAt DESC";
                    cmd.Parameters.AddWithValue("@LecturerId", lecturerId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            submissions.Add(new Submission
                            {
                                SubmissionId = reader.GetInt32(0),
                                AssignmentId = reader.GetInt32(1),
                                StudentId = reader.GetInt32(2),
                                SubmittedAt = DateTime.Parse(reader.GetString(3)),
                                Grade = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4),
                                GradedByLecturerId = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5)
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving submissions by lecturer ID: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing submission dates/grades: " + ex.Message, ex);
            }
            return submissions;
        }

        public List<Submission> GetAllSubmissions()
        {
            var submissions = new List<Submission>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT SubmissionId, AssignmentId, StudentId, SubmittedAt, Grade, GradedByLecturerId FROM Submissions ORDER BY SubmittedAt DESC";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            submissions.Add(new Submission
                            {
                                SubmissionId = reader.GetInt32(0),
                                AssignmentId = reader.GetInt32(1),
                                StudentId = reader.GetInt32(2),
                                SubmittedAt = DateTime.Parse(reader.GetString(3)),
                                Grade = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4),
                                GradedByLecturerId = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5)
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving all submissions: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing submission dates/grades: " + ex.Message, ex);
            }
            return submissions;
        }
    }
}

