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
    internal class StaffRepository : IStaffRepository
    {
        public void AddStaff(Staff staff)
        {
            try
            {
                if (staff == null)
                    throw new ArgumentNullException(nameof(staff));

                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        INSERT INTO Staff (Name, Nic, DepartmentId, ContactNo, Email, HireDate, UserId, CreatedAt, UpdatedAt)
                        VALUES (@Name, @Nic, @DepartmentId, @ContactNo, @Email, @HireDate, @UserId, @CreatedAt, @UpdatedAt)";
                    cmd.Parameters.AddWithValue("@Name", staff.Name);
                    cmd.Parameters.AddWithValue("@Nic", staff.Nic);
                    cmd.Parameters.AddWithValue("@DepartmentId", staff.DepartmentId.HasValue ? (object)staff.DepartmentId.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@ContactNo", staff.ContactNo);
                    cmd.Parameters.AddWithValue("@Email", staff.Email);
                    cmd.Parameters.AddWithValue("@HireDate", staff.HireDate.HasValue ? (object)staff.HireDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : DBNull.Value);
                    cmd.Parameters.AddWithValue("@UserId", staff.UserId);
                    cmd.Parameters.AddWithValue("@CreatedAt", staff.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@UpdatedAt", staff.UpdatedAt.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                if (ex.Message.Contains("UNIQUE constraint failed"))
                {
                    throw new Exception("A staff member with the same NIC, Email, or linked User ID already exists.", ex);
                }
                throw new Exception("Database error while adding staff member: " + ex.Message, ex);
            }
        }

        public void UpdateStaff(Staff staff)
        {
            try
            {
                if (staff == null)
                    throw new ArgumentNullException(nameof(staff));

                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        UPDATE Staff
                        SET Name = @Name, Nic = @Nic, DepartmentId = @DepartmentId,
                            ContactNo = @ContactNo, Email = @Email,
                            HireDate = @HireDate, UserId = @UserId, UpdatedAt = @UpdatedAt
                        WHERE StaffId = @StaffId";
                    cmd.Parameters.AddWithValue("@StaffId", staff.StaffId);
                    cmd.Parameters.AddWithValue("@Name", staff.Name);
                    cmd.Parameters.AddWithValue("@Nic", staff.Nic);
                    cmd.Parameters.AddWithValue("@DepartmentId", staff.DepartmentId.HasValue ? (object)staff.DepartmentId.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@ContactNo", staff.ContactNo);
                    cmd.Parameters.AddWithValue("@Email", staff.Email);
                    cmd.Parameters.AddWithValue("@HireDate", staff.HireDate.HasValue ? (object)staff.HireDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : DBNull.Value);
                    cmd.Parameters.AddWithValue("@UserId", staff.UserId);
                    cmd.Parameters.AddWithValue("@UpdatedAt", staff.UpdatedAt.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                if (ex.Message.Contains("UNIQUE constraint failed"))
                {
                    throw new Exception("A staff member with the same NIC, Email, or linked User ID already exists.", ex);
                }
                throw new Exception("Database error while updating staff member: " + ex.Message, ex);
            }
        }

        public void DeleteStaff(int staffId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "DELETE FROM Staff WHERE StaffId = @StaffId";
                    cmd.Parameters.AddWithValue("@StaffId", staffId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while deleting staff member: " + ex.Message, ex);
            }
        }

        public Staff GetStaffById(int staffId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT StaffId, Name, Nic, DepartmentId, ContactNo, Email, HireDate, UserId, CreatedAt, UpdatedAt FROM Staff WHERE StaffId = @StaffId";
                    cmd.Parameters.AddWithValue("@StaffId", staffId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return ReadStaff(reader);
                        }
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving staff by ID: " + ex.Message, ex);
            }
        }

        public Staff GetStaffByNic(string nic)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT StaffId, Name, Nic, DepartmentId, ContactNo, Email, HireDate, UserId, CreatedAt, UpdatedAt FROM Staff WHERE Nic = @Nic";
                    cmd.Parameters.AddWithValue("@Nic", nic);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return ReadStaff(reader);
                        }
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving staff by NIC: " + ex.Message, ex);
            }
        }

        public Staff GetStaffByEmail(string email)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT StaffId, Name, Nic, DepartmentId, ContactNo, Email, HireDate, UserId, CreatedAt, UpdatedAt FROM Staff WHERE Email = @Email";
                    cmd.Parameters.AddWithValue("@Email", email);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return ReadStaff(reader);
                        }
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving staff by Email: " + ex.Message, ex);
            }
        }

        public Staff GetStaffByUserId(int userId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT StaffId, Name, Nic, DepartmentId, ContactNo, Email, HireDate, UserId, CreatedAt, UpdatedAt FROM Staff WHERE UserId = @UserId";
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return ReadStaff(reader);
                        }
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving staff by User ID: " + ex.Message, ex);
            }
        }

        public List<Staff> GetStaffByDepartmentId(int departmentId)
        {
            var staffList = new List<Staff>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT StaffId, Name, Nic, DepartmentId, ContactNo, Email, HireDate, UserId, CreatedAt, UpdatedAt FROM Staff WHERE DepartmentId = @DepartmentId ORDER BY Name ASC";
                    cmd.Parameters.AddWithValue("@DepartmentId", departmentId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            staffList.Add(ReadStaff(reader));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving staff by Department ID: " + ex.Message, ex);
            }
            return staffList;
        }

        public List<Staff> GetAllStaff()
        {
            var staffList = new List<Staff>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT StaffId, Name, Nic, DepartmentId, ContactNo, Email, HireDate, UserId, CreatedAt, UpdatedAt FROM Staff ORDER BY Name ASC";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            staffList.Add(ReadStaff(reader));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving all staff: " + ex.Message, ex);
            }
            return staffList;
        }

        private Staff ReadStaff(SQLiteDataReader reader)
        {
            return new Staff
            {
                StaffId = reader.GetInt32(0),
                Name = reader.GetString(1),
                Nic = reader.GetString(2),
                DepartmentId = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3),
                ContactNo = reader.GetString(4),
                Email = reader.GetString(5),
                HireDate = reader.IsDBNull(6) ? (DateTime?)null : DateTime.Parse(reader.GetString(6)),
                UserId = reader.GetInt32(7),
                CreatedAt = DateTime.Parse(reader.GetString(8)),
                UpdatedAt = DateTime.Parse(reader.GetString(9))
            };
        }
    }
}