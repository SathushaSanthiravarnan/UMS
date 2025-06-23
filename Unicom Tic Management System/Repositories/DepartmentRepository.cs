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
    internal class DepartmentRepository : IDepartmentRepository
    {
        public void AddDepartment(Department department)
        {
            try
            {
                if (department == null)
                    throw new ArgumentNullException(nameof(department));

                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        INSERT INTO Departments (DepartmentName)
                        VALUES (@DepartmentName)";
                    cmd.Parameters.AddWithValue("@DepartmentName", department.DepartmentName);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while adding department: " + ex.Message, ex);
            }
        }

        public void UpdateDepartment(Department department)
        {
            try
            {
                if (department == null)
                    throw new ArgumentNullException(nameof(department));

                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        UPDATE Departments
                        SET DepartmentName = @DepartmentName
                        WHERE DepartmentId = @DepartmentId";
                    cmd.Parameters.AddWithValue("@DepartmentId", department.DepartmentId);
                    cmd.Parameters.AddWithValue("@DepartmentName", department.DepartmentName);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while updating department: " + ex.Message, ex);
            }
        }

        public void DeleteDepartment(int departmentId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "DELETE FROM Departments WHERE DepartmentId = @DepartmentId";
                    cmd.Parameters.AddWithValue("@DepartmentId", departmentId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while deleting department: " + ex.Message, ex);
            }
        }

        public Department GetDepartmentById(int departmentId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT DepartmentId, DepartmentName FROM Departments WHERE DepartmentId = @DepartmentId";
                    cmd.Parameters.AddWithValue("@DepartmentId", departmentId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Department
                            {
                                DepartmentId = reader.GetInt32(0),
                                DepartmentName = reader.GetString(1)
                            };
                        }
                        return null;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving department by ID: " + ex.Message, ex);
            }
        }

        public Department GetDepartmentByName(string departmentName)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT DepartmentId, DepartmentName FROM Departments WHERE DepartmentName = @DepartmentName";
                    cmd.Parameters.AddWithValue("@DepartmentName", departmentName);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Department
                            {
                                DepartmentId = reader.GetInt32(0),
                                DepartmentName = reader.GetString(1)
                            };
                        }
                        return null;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving department by name: " + ex.Message, ex);
            }
        }

        public List<Department> GetAllDepartments()
        {
            var departments = new List<Department>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT DepartmentId, DepartmentName FROM Departments ORDER BY DepartmentName ASC";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            departments.Add(new Department
                            {
                                DepartmentId = reader.GetInt32(0),
                                DepartmentName = reader.GetString(1)
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving all departments: " + ex.Message, ex);
            }
            return departments;
        }
    }
}
