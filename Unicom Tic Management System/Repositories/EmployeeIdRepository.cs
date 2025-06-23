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
    internal class EmployeeIdRepository : IEmployeeIdRepository
    {
        public void AddEmployeeId(EmployeeId employeeId)
        {
            try
            {
                if (employeeId == null)
                    throw new ArgumentNullException(nameof(employeeId));

                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        INSERT INTO EmployeeIds (EmployeeIdText, UserId)
                        VALUES (@EmployeeIdText, @UserId)";
                    cmd.Parameters.AddWithValue("@EmployeeIdText", employeeId.EmployeeIdText);
                    cmd.Parameters.AddWithValue("@UserId", employeeId.UserId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while adding employee ID: " + ex.Message, ex);
            }
        }

        public void UpdateEmployeeId(EmployeeId employeeId)
        {
            try
            {
                if (employeeId == null)
                    throw new ArgumentNullException(nameof(employeeId));

                // Assuming EmployeeIdText is the unique identifier for updating
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        UPDATE EmployeeIds
                        SET UserId = @UserId
                        WHERE EmployeeIdText = @EmployeeIdText";
                    cmd.Parameters.AddWithValue("@EmployeeIdText", employeeId.EmployeeIdText);
                    cmd.Parameters.AddWithValue("@UserId", employeeId.UserId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while updating employee ID: " + ex.Message, ex);
            }
        }

        public void DeleteEmployeeId(string employeeIdText)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "DELETE FROM EmployeeIds WHERE EmployeeIdText = @EmployeeIdText";
                    cmd.Parameters.AddWithValue("@EmployeeIdText", employeeIdText);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while deleting employee ID: " + ex.Message, ex);
            }
        }

        public EmployeeId GetEmployeeIdByText(string employeeIdText)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT EmployeeIdText, UserId FROM EmployeeIds WHERE EmployeeIdText = @EmployeeIdText";
                    cmd.Parameters.AddWithValue("@EmployeeIdText", employeeIdText);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new EmployeeId
                            {
                                EmployeeIdText = reader.GetString(0),
                                UserId = reader.GetInt32(1)
                            };
                        }
                        return null;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving employee ID by text: " + ex.Message, ex);
            }
        }

        public EmployeeId GetEmployeeIdByUserId(int userId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT EmployeeIdText, UserId FROM EmployeeIds WHERE UserId = @UserId";
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new EmployeeId
                            {
                                EmployeeIdText = reader.GetString(0),
                                UserId = reader.GetInt32(1)
                            };
                        }
                        return null;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving employee ID by user ID: " + ex.Message, ex);
            }
        }

        public List<EmployeeId> GetAllEmployeeIds()
        {
            var employeeIds = new List<EmployeeId>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT EmployeeIdText, UserId FROM EmployeeIds ORDER BY EmployeeIdText ASC";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employeeIds.Add(new EmployeeId
                            {
                                EmployeeIdText = reader.GetString(0),
                                UserId = reader.GetInt32(1)
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving all employee IDs: " + ex.Message, ex);
            }
            return employeeIds;
        }
    }
}
