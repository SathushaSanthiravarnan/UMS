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
    internal class LecturerRepository : ILecturerRepository
    {
        public void AddLecturer(Lecturer lecturer)
        {
            try
            {
                if (lecturer == null)
                    throw new ArgumentNullException(nameof(lecturer));

                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        INSERT INTO Lecturers (Name, Nic, Phone, Address, Email, DepartmentId, HireDate, UserId, CreatedAt, UpdatedAt)
                        VALUES (@Name, @Nic, @Phone, @Address, @Email, @DepartmentId, @HireDate, @UserId, @CreatedAt, @UpdatedAt)";
                    cmd.Parameters.AddWithValue("@Name", lecturer.Name);
                    cmd.Parameters.AddWithValue("@Nic", lecturer.Nic);
                    cmd.Parameters.AddWithValue("@Phone", lecturer.Phone);
                    cmd.Parameters.AddWithValue("@Address", lecturer.Address);
                    cmd.Parameters.AddWithValue("@Email", lecturer.Email);
                    cmd.Parameters.AddWithValue("@DepartmentId", lecturer.DepartmentId.HasValue ? (object)lecturer.DepartmentId.Value : DBNull.Value); // Nullable int
                    cmd.Parameters.AddWithValue("@HireDate", lecturer.HireDate.HasValue ? (object)lecturer.HireDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : DBNull.Value); // Nullable DateTime
                    cmd.Parameters.AddWithValue("@UserId", lecturer.UserId);
                    cmd.Parameters.AddWithValue("@CreatedAt", lecturer.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@UpdatedAt", lecturer.UpdatedAt.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while adding lecturer: " + ex.Message, ex);
            }
        }

        public void UpdateLecturer(Lecturer lecturer)
        {
            try
            {
                if (lecturer == null)
                    throw new ArgumentNullException(nameof(lecturer));

                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        UPDATE Lecturers
                        SET Name = @Name, Nic = @Nic, Phone = @Phone, Address = @Address,
                            Email = @Email, DepartmentId = @DepartmentId, HireDate = @HireDate,
                            UserId = @UserId, UpdatedAt = @UpdatedAt
                        WHERE LecturerId = @LecturerId";
                    cmd.Parameters.AddWithValue("@LecturerId", lecturer.LecturerId);
                    cmd.Parameters.AddWithValue("@Name", lecturer.Name);
                    cmd.Parameters.AddWithValue("@Nic", lecturer.Nic);
                    cmd.Parameters.AddWithValue("@Phone", lecturer.Phone);
                    cmd.Parameters.AddWithValue("@Address", lecturer.Address);
                    cmd.Parameters.AddWithValue("@Email", lecturer.Email);
                    cmd.Parameters.AddWithValue("@DepartmentId", lecturer.DepartmentId.HasValue ? (object)lecturer.DepartmentId.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@HireDate", lecturer.HireDate.HasValue ? (object)lecturer.HireDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : DBNull.Value);
                    cmd.Parameters.AddWithValue("@UserId", lecturer.UserId);
                    cmd.Parameters.AddWithValue("@UpdatedAt", lecturer.UpdatedAt.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while updating lecturer: " + ex.Message, ex);
            }
        }

        public void DeleteLecturer(int lecturerId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "DELETE FROM Lecturers WHERE LecturerId = @LecturerId";
                    cmd.Parameters.AddWithValue("@LecturerId", lecturerId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while deleting lecturer: " + ex.Message, ex);
            }
        }

        public Lecturer GetLecturerById(int lecturerId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT LecturerId, Name, Nic, Phone, Address, Email, DepartmentId, HireDate, UserId, CreatedAt, UpdatedAt FROM Lecturers WHERE LecturerId = @LecturerId";
                    cmd.Parameters.AddWithValue("@LecturerId", lecturerId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Lecturer
                            {
                                LecturerId = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Nic = reader.GetString(2),
                                Phone = reader.GetString(3),
                                Address = reader.GetString(4),
                                Email = reader.GetString(5),
                                DepartmentId = reader.IsDBNull(6) ? (int?)null : reader.GetInt32(6),
                                HireDate = reader.IsDBNull(7) ? (DateTime?)null : DateTime.Parse(reader.GetString(7)),
                                UserId = reader.GetInt32(8),
                                CreatedAt = DateTime.Parse(reader.GetString(9)),
                                UpdatedAt = DateTime.Parse(reader.GetString(10))
                            };
                        }
                        return null;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving lecturer by ID: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing lecturer dates: " + ex.Message, ex);
            }
        }

        public Lecturer GetLecturerByNic(string nic)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT LecturerId, Name, Nic, Phone, Address, Email, DepartmentId, HireDate, UserId, CreatedAt, UpdatedAt FROM Lecturers WHERE Nic = @Nic";
                    cmd.Parameters.AddWithValue("@Nic", nic);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Lecturer
                            {
                                LecturerId = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Nic = reader.GetString(2),
                                Phone = reader.GetString(3),
                                Address = reader.GetString(4),
                                Email = reader.GetString(5),
                                DepartmentId = reader.IsDBNull(6) ? (int?)null : reader.GetInt32(6),
                                HireDate = reader.IsDBNull(7) ? (DateTime?)null : DateTime.Parse(reader.GetString(7)),
                                UserId = reader.GetInt32(8),
                                CreatedAt = DateTime.Parse(reader.GetString(9)),
                                UpdatedAt = DateTime.Parse(reader.GetString(10))
                            };
                        }
                        return null;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving lecturer by NIC: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing lecturer dates: " + ex.Message, ex);
            }
        }

        public Lecturer GetLecturerByEmail(string email)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT LecturerId, Name, Nic, Phone, Address, Email, DepartmentId, HireDate, UserId, CreatedAt, UpdatedAt FROM Lecturers WHERE Email = @Email";
                    cmd.Parameters.AddWithValue("@Email", email);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Lecturer
                            {
                                LecturerId = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Nic = reader.GetString(2),
                                Phone = reader.GetString(3),
                                Address = reader.GetString(4),
                                Email = reader.GetString(5),
                                DepartmentId = reader.IsDBNull(6) ? (int?)null : reader.GetInt32(6),
                                HireDate = reader.IsDBNull(7) ? (DateTime?)null : DateTime.Parse(reader.GetString(7)),
                                UserId = reader.GetInt32(8),
                                CreatedAt = DateTime.Parse(reader.GetString(9)),
                                UpdatedAt = DateTime.Parse(reader.GetString(10))
                            };
                        }
                        return null;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving lecturer by email: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing lecturer dates: " + ex.Message, ex);
            }
        }

        public Lecturer GetLecturerByUserId(int userId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT LecturerId, Name, Nic, Phone, Address, Email, DepartmentId, HireDate, UserId, CreatedAt, UpdatedAt FROM Lecturers WHERE UserId = @UserId";
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Lecturer
                            {
                                LecturerId = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Nic = reader.GetString(2),
                                Phone = reader.GetString(3),
                                Address = reader.GetString(4),
                                Email = reader.GetString(5),
                                DepartmentId = reader.IsDBNull(6) ? (int?)null : reader.GetInt32(6),
                                HireDate = reader.IsDBNull(7) ? (DateTime?)null : DateTime.Parse(reader.GetString(7)),
                                UserId = reader.GetInt32(8),
                                CreatedAt = DateTime.Parse(reader.GetString(9)),
                                UpdatedAt = DateTime.Parse(reader.GetString(10))
                            };
                        }
                        return null;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving lecturer by user ID: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing lecturer dates: " + ex.Message, ex);
            }
        }

        public List<Lecturer> GetLecturersByDepartmentId(int departmentId)
        {
            var lecturers = new List<Lecturer>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT LecturerId, Name, Nic, Phone, Address, Email, DepartmentId, HireDate, UserId, CreatedAt, UpdatedAt FROM Lecturers WHERE DepartmentId = @DepartmentId ORDER BY Name ASC";
                    cmd.Parameters.AddWithValue("@DepartmentId", departmentId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lecturers.Add(new Lecturer
                            {
                                LecturerId = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Nic = reader.GetString(2),
                                Phone = reader.GetString(3),
                                Address = reader.GetString(4),
                                Email = reader.GetString(5),
                                DepartmentId = reader.IsDBNull(6) ? (int?)null : reader.GetInt32(6),
                                HireDate = reader.IsDBNull(7) ? (DateTime?)null : DateTime.Parse(reader.GetString(7)),
                                UserId = reader.GetInt32(8),
                                CreatedAt = DateTime.Parse(reader.GetString(9)),
                                UpdatedAt = DateTime.Parse(reader.GetString(10))
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving lecturers by department ID: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing lecturer dates: " + ex.Message, ex);
            }
            return lecturers;
        }

        public List<Lecturer> GetAllLecturers()
        {
            var lecturers = new List<Lecturer>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT LecturerId, Name, Nic, Phone, Address, Email, DepartmentId, HireDate, UserId, CreatedAt, UpdatedAt FROM Lecturers ORDER BY Name ASC";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lecturers.Add(new Lecturer
                            {
                                LecturerId = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Nic = reader.GetString(2),
                                Phone = reader.GetString(3),
                                Address = reader.GetString(4),
                                Email = reader.GetString(5),
                                DepartmentId = reader.IsDBNull(6) ? (int?)null : reader.GetInt32(6),
                                HireDate = reader.IsDBNull(7) ? (DateTime?)null : DateTime.Parse(reader.GetString(7)),
                                UserId = reader.GetInt32(8),
                                CreatedAt = DateTime.Parse(reader.GetString(9)),
                                UpdatedAt = DateTime.Parse(reader.GetString(10))
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving all lecturers: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing lecturer dates: " + ex.Message, ex);
            }
            return lecturers;
        }
    }
}
