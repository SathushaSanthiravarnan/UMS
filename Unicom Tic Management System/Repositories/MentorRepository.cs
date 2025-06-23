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
    internal class MentorRepository : IMentorRepository
    {
        public void AddMentor(Mentor mentor)
        {
            try
            {
                if (mentor == null)
                    throw new ArgumentNullException(nameof(mentor));

                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        INSERT INTO Mentors (Name, Nic, DepartmentId, UserId, Phone, Email, CreatedAt, UpdatedAt)
                        VALUES (@Name, @Nic, @DepartmentId, @UserId, @Phone, @Email, @CreatedAt, @UpdatedAt)";
                    cmd.Parameters.AddWithValue("@Name", mentor.Name);
                    cmd.Parameters.AddWithValue("@Nic", mentor.Nic);
                    cmd.Parameters.AddWithValue("@DepartmentId", mentor.DepartmentId.HasValue ? (object)mentor.DepartmentId.Value : DBNull.Value); // Nullable int
                    cmd.Parameters.AddWithValue("@UserId", mentor.UserId.HasValue ? (object)mentor.UserId.Value : DBNull.Value); // Nullable int
                    cmd.Parameters.AddWithValue("@Phone", mentor.Phone);
                    cmd.Parameters.AddWithValue("@Email", mentor.Email);
                    cmd.Parameters.AddWithValue("@CreatedAt", mentor.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@UpdatedAt", mentor.UpdatedAt.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                // Check for unique constraint violation (e.g., NIC or Email)
                if (ex.Message.Contains("UNIQUE constraint failed"))
                {
                    throw new Exception("A mentor with the same NIC or Email already exists.", ex);
                }
                throw new Exception("Database error while adding mentor: " + ex.Message, ex);
            }
        }

        public void UpdateMentor(Mentor mentor)
        {
            try
            {
                if (mentor == null)
                    throw new ArgumentNullException(nameof(mentor));

                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        UPDATE Mentors
                        SET Name = @Name, Nic = @Nic, DepartmentId = @DepartmentId,
                            UserId = @UserId, Phone = @Phone, Email = @Email,
                            UpdatedAt = @UpdatedAt
                        WHERE MentorId = @MentorId";
                    cmd.Parameters.AddWithValue("@MentorId", mentor.MentorId);
                    cmd.Parameters.AddWithValue("@Name", mentor.Name);
                    cmd.Parameters.AddWithValue("@Nic", mentor.Nic);
                    cmd.Parameters.AddWithValue("@DepartmentId", mentor.DepartmentId.HasValue ? (object)mentor.DepartmentId.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@UserId", mentor.UserId.HasValue ? (object)mentor.UserId.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@Phone", mentor.Phone);
                    cmd.Parameters.AddWithValue("@Email", mentor.Email);
                    cmd.Parameters.AddWithValue("@UpdatedAt", mentor.UpdatedAt.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                if (ex.Message.Contains("UNIQUE constraint failed"))
                {
                    throw new Exception("A mentor with the same NIC, Email, or linked User ID already exists.", ex);
                }
                throw new Exception("Database error while updating mentor: " + ex.Message, ex);
            }
        }

        public void DeleteMentor(int mentorId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "DELETE FROM Mentors WHERE MentorId = @MentorId";
                    cmd.Parameters.AddWithValue("@MentorId", mentorId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while deleting mentor: " + ex.Message, ex);
            }
        }

        public Mentor GetMentorById(int mentorId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT MentorId, Name, Nic, DepartmentId, UserId, Phone, Email, CreatedAt, UpdatedAt FROM Mentors WHERE MentorId = @MentorId";
                    cmd.Parameters.AddWithValue("@MentorId", mentorId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Mentor
                            {
                                MentorId = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Nic = reader.GetString(2),
                                DepartmentId = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3),
                                UserId = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4),
                                Phone = reader.GetString(5),
                                Email = reader.GetString(6),
                                CreatedAt = DateTime.Parse(reader.GetString(7)),
                                UpdatedAt = DateTime.Parse(reader.GetString(8))
                            };
                        }
                        return null;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving mentor by ID: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing mentor dates: " + ex.Message, ex);
            }
        }

        public Mentor GetMentorByNic(string nic)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT MentorId, Name, Nic, DepartmentId, UserId, Phone, Email, CreatedAt, UpdatedAt FROM Mentors WHERE Nic = @Nic";
                    cmd.Parameters.AddWithValue("@Nic", nic);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Mentor
                            {
                                MentorId = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Nic = reader.GetString(2),
                                DepartmentId = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3),
                                UserId = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4),
                                Phone = reader.GetString(5),
                                Email = reader.GetString(6),
                                CreatedAt = DateTime.Parse(reader.GetString(7)),
                                UpdatedAt = DateTime.Parse(reader.GetString(8))
                            };
                        }
                        return null;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving mentor by NIC: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing mentor dates: " + ex.Message, ex);
            }
        }

        public Mentor GetMentorByEmail(string email)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT MentorId, Name, Nic, DepartmentId, UserId, Phone, Email, CreatedAt, UpdatedAt FROM Mentors WHERE Email = @Email";
                    cmd.Parameters.AddWithValue("@Email", email);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Mentor
                            {
                                MentorId = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Nic = reader.GetString(2),
                                DepartmentId = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3),
                                UserId = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4),
                                Phone = reader.GetString(5),
                                Email = reader.GetString(6),
                                CreatedAt = DateTime.Parse(reader.GetString(7)),
                                UpdatedAt = DateTime.Parse(reader.GetString(8))
                            };
                        }
                        return null;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving mentor by email: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing mentor dates: " + ex.Message, ex);
            }
        }

        public Mentor GetMentorByUserId(int userId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT MentorId, Name, Nic, DepartmentId, UserId, Phone, Email, CreatedAt, UpdatedAt FROM Mentors WHERE UserId = @UserId";
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Mentor
                            {
                                MentorId = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Nic = reader.GetString(2),
                                DepartmentId = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3),
                                UserId = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4),
                                Phone = reader.GetString(5),
                                Email = reader.GetString(6),
                                CreatedAt = DateTime.Parse(reader.GetString(7)),
                                UpdatedAt = DateTime.Parse(reader.GetString(8))
                            };
                        }
                        return null;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving mentor by User ID: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing mentor dates: " + ex.Message, ex);
            }
        }

        public List<Mentor> GetMentorsByDepartmentId(int departmentId)
        {
            var mentors = new List<Mentor>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT MentorId, Name, Nic, DepartmentId, UserId, Phone, Email, CreatedAt, UpdatedAt FROM Mentors WHERE DepartmentId = @DepartmentId ORDER BY Name ASC";
                    cmd.Parameters.AddWithValue("@DepartmentId", departmentId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            mentors.Add(new Mentor
                            {
                                MentorId = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Nic = reader.GetString(2),
                                DepartmentId = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3),
                                UserId = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4),
                                Phone = reader.GetString(5),
                                Email = reader.GetString(6),
                                CreatedAt = DateTime.Parse(reader.GetString(7)),
                                UpdatedAt = DateTime.Parse(reader.GetString(8))
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving mentors by department ID: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing mentor dates: " + ex.Message, ex);
            }
            return mentors;
        }

        public List<Mentor> GetAllMentors()
        {
            var mentors = new List<Mentor>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT MentorId, Name, Nic, DepartmentId, UserId, Phone, Email, CreatedAt, UpdatedAt FROM Mentors ORDER BY Name ASC";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            mentors.Add(new Mentor
                            {
                                MentorId = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Nic = reader.GetString(2),
                                DepartmentId = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3),
                                UserId = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4),
                                Phone = reader.GetString(5),
                                Email = reader.GetString(6),
                                CreatedAt = DateTime.Parse(reader.GetString(7)),
                                UpdatedAt = DateTime.Parse(reader.GetString(8))
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving all mentors: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing mentor dates: " + ex.Message, ex);
            }
            return mentors;


        }
    }
}
