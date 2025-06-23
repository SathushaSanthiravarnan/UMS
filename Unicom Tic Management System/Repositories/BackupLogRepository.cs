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
    internal class BackupLogRepository : IBackupLogRepository
    {
        public void AddBackupLog(BackupLog backupLog)
        {
            try
            {
                if (backupLog == null)
                    throw new ArgumentNullException(nameof(backupLog));

                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        INSERT INTO BackupLogs (CreatedAt, BackupPath, Status, PerformedByUserId)
                        VALUES (@CreatedAt, @BackupPath, @Status, @PerformedByUserId)";
                    cmd.Parameters.AddWithValue("@CreatedAt", backupLog.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss")); // Store DateTime as string
                    cmd.Parameters.AddWithValue("@BackupPath", backupLog.BackupPath);
                    cmd.Parameters.AddWithValue("@Status", backupLog.Status);
                    cmd.Parameters.AddWithValue("@PerformedByUserId", backupLog.PerformedByUserId.HasValue ? (object)backupLog.PerformedByUserId.Value : DBNull.Value); // Handle nullable UserId
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while adding backup log: " + ex.Message, ex);
            }
        }

        public BackupLog GetBackupLogById(int backupLogId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT BackupLogId, CreatedAt, BackupPath, Status, PerformedByUserId FROM BackupLogs WHERE BackupLogId = @BackupLogId";
                    cmd.Parameters.AddWithValue("@BackupLogId", backupLogId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new BackupLog
                            {
                                BackupLogId = reader.GetInt32(0),
                                CreatedAt = DateTime.Parse(reader.GetString(1)), 
                                BackupPath = reader.GetString(2),
                                Status = reader.GetString(3),
                                PerformedByUserId = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4) 
                            };
                        }
                        return null;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving backup log by ID: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing backup log date: " + ex.Message, ex);
            }
        }

        public List<BackupLog> GetAllBackupLogs()
        {
            var logs = new List<BackupLog>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT BackupLogId, CreatedAt, BackupPath, Status, PerformedByUserId FROM BackupLogs ORDER BY CreatedAt DESC";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            logs.Add(new BackupLog
                            {
                                BackupLogId = reader.GetInt32(0),
                                CreatedAt = DateTime.Parse(reader.GetString(1)),
                                BackupPath = reader.GetString(2),
                                Status = reader.GetString(3),
                                PerformedByUserId = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4)
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving all backup logs: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing backup log dates: " + ex.Message, ex);
            }
            return logs;
        }

        public List<BackupLog> GetBackupLogsByUserId(int userId)
        {
            var logs = new List<BackupLog>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT BackupLogId, CreatedAt, BackupPath, Status, PerformedByUserId FROM BackupLogs WHERE PerformedByUserId = @UserId ORDER BY CreatedAt DESC";
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            logs.Add(new BackupLog
                            {
                                BackupLogId = reader.GetInt32(0),
                                CreatedAt = DateTime.Parse(reader.GetString(1)),
                                BackupPath = reader.GetString(2),
                                Status = reader.GetString(3),
                                PerformedByUserId = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4)
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving backup logs by user ID: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing backup log dates: " + ex.Message, ex);
            }
            return logs;
        }

        public List<BackupLog> GetBackupLogsByStatus(string status)
        {
            var logs = new List<BackupLog>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT BackupLogId, CreatedAt, BackupPath, Status, PerformedByUserId FROM BackupLogs WHERE Status = @Status ORDER BY CreatedAt DESC";
                    cmd.Parameters.AddWithValue("@Status", status);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            logs.Add(new BackupLog
                            {
                                BackupLogId = reader.GetInt32(0),
                                CreatedAt = DateTime.Parse(reader.GetString(1)),
                                BackupPath = reader.GetString(2),
                                Status = reader.GetString(3),
                                PerformedByUserId = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4)
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving backup logs by status: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing backup log dates: " + ex.Message, ex);
            }
            return logs;
        }

        public List<BackupLog> GetBackupLogsByDateRange(DateTime startDate, DateTime endDate)
        {
            var logs = new List<BackupLog>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    // Using ISO 8601 format for date comparison
                    cmd.CommandText = "SELECT BackupLogId, CreatedAt, BackupPath, Status, PerformedByUserId FROM BackupLogs WHERE CreatedAt BETWEEN @StartDate AND @EndDate ORDER BY CreatedAt DESC";
                    cmd.Parameters.AddWithValue("@StartDate", startDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@EndDate", endDate.ToString("yyyy-MM-dd HH:mm:ss"));

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            logs.Add(new BackupLog
                            {
                                BackupLogId = reader.GetInt32(0),
                                CreatedAt = DateTime.Parse(reader.GetString(1)),
                                BackupPath = reader.GetString(2),
                                Status = reader.GetString(3),
                                PerformedByUserId = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4)
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving backup logs by date range: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing backup log dates: " + ex.Message, ex);
            }
            return logs;
        }
    }
}
