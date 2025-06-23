using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Datas;
using Unicom_Tic_Management_System.Models;
using Unicom_Tic_Management_System.Models.Enums;
using Unicom_Tic_Management_System.Repositories.Interfaces;

namespace Unicom_Tic_Management_System.Repositories
{
    internal class UserRepository : IUserRepository
    {
        public void AddUser(User user)
        {
            try
            {
                if (user == null)
                    throw new ArgumentNullException(nameof(user));

                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        INSERT INTO Users (Username, Password, NIC, Role)
                        VALUES (@Username, @Password, @NIC, @Role)";
                    cmd.Parameters.AddWithValue("@Username", user.Username);
                    cmd.Parameters.AddWithValue("@Password", user.PasswordHash);
                    cmd.Parameters.AddWithValue("@NIC", user.Nic);
                    cmd.Parameters.AddWithValue("@Role", user.Role.ToString());
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while adding user: " + ex.Message, ex);
            }
        }

        public void UpdateUser(User user)
        {
            try
            {
                if (user == null)
                    throw new ArgumentNullException(nameof(user));

                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        UPDATE Users
                        SET Username = @Username, Password = @Password, NIC = @NIC, UpdatedAt = CURRENT_TIMESTAMP, Role = @Role
                        WHERE UserID = @UserID";
                    cmd.Parameters.AddWithValue("@UserID", user.UserId);
                    cmd.Parameters.AddWithValue("@Username", user.Username);
                    cmd.Parameters.AddWithValue("@Password", user.PasswordHash);
                    cmd.Parameters.AddWithValue("@NIC", user.Nic);
                    cmd.Parameters.AddWithValue("@Role", user.Role.ToString());
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while updating user: " + ex.Message, ex);
            }
        }

        public void DeleteUser(int userId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "DELETE FROM Users WHERE UserID = @UserID";
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while deleting user: " + ex.Message, ex);
            }
        }

        public User GetUserById(int userId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT UserID, Username, Password, NIC, CreatedAt, UpdatedAt, Role FROM Users WHERE UserID = @UserID";
                    cmd.Parameters.AddWithValue("@UserID", userId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User
                            {
                                UserId = reader.GetInt32(0),
                                Username = reader.GetString(1),
                                PasswordHash = reader.GetString(2),
                                Nic = reader.GetString(3),
                                CreatedAt = DateTime.Parse(reader.GetString(4)),
                                UpdatedAt = DateTime.Parse(reader.GetString(5)),
                                Role = (UserRole)Enum.Parse(typeof(UserRole), reader.GetString(6))
                            };
                        }
                        return null;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving user by ID: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing user dates or role: " + ex.Message, ex);
            }
        }

        public User GetUserByUsername(string username)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT UserID, Username, Password, NIC, CreatedAt, UpdatedAt, Role FROM Users WHERE Username = @Username";
                    cmd.Parameters.AddWithValue("@Username", username);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User
                            {
                                UserId = reader.GetInt32(0),
                                Username = reader.GetString(1),
                                PasswordHash = reader.GetString(2),
                                Nic = reader.GetString(3),
                                CreatedAt = DateTime.Parse(reader.GetString(4)),
                                UpdatedAt = DateTime.Parse(reader.GetString(5)),
                                Role = (UserRole)Enum.Parse(typeof(UserRole), reader.GetString(6))
                            };
                        }
                        return null;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving user by username: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing user dates or role: " + ex.Message, ex);
            }
        }

        public User GetUserByNIC(string nic)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT UserID, Username, Password, NIC, CreatedAt, UpdatedAt, Role FROM Users WHERE NIC = @NIC";
                    cmd.Parameters.AddWithValue("@NIC", nic);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User
                            {
                                UserId = reader.GetInt32(0),
                                Username = reader.GetString(1),
                                PasswordHash = reader.GetString(2),
                                Nic = reader.GetString(3),
                                CreatedAt = DateTime.Parse(reader.GetString(4)),
                                UpdatedAt = DateTime.Parse(reader.GetString(5)),
                                Role = (UserRole)Enum.Parse(typeof(UserRole), reader.GetString(6))
                            };
                        }
                        return null;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving user by NIC: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing user dates or role: " + ex.Message, ex);
            }
        }

        public List<User> GetAllUsers()
        {
            var users = new List<User>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT UserID, Username, Password, NIC, CreatedAt, UpdatedAt, Role FROM Users";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(new User
                            {
                                UserId = reader.GetInt32(0),
                                Username = reader.GetString(1),
                                PasswordHash = reader.GetString(2),
                                Nic = reader.GetString(3),
                                CreatedAt = DateTime.Parse(reader.GetString(4)),
                                UpdatedAt = DateTime.Parse(reader.GetString(5)),
                                Role = (UserRole)Enum.Parse(typeof(UserRole), reader.GetString(6))
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving users: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing user dates or role: " + ex.Message, ex);
            }
            return users;
        }

        public User GetUserByNic(string nic)
        {
            throw new NotImplementedException();
        }

        public static string HashPassword(string password)
        {
            
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLowerInvariant();
            }
        }
    }

}
