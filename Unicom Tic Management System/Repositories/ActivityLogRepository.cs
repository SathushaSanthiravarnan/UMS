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
    internal class ActivityLogRepository : IActivityLogRepository
    {
        public void AddLog(ActivityLog log)
        {
            try
            {
                if (log == null)
                    throw new ArgumentNullException(nameof(log));

                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        INSERT INTO ActivityLogs (UserId, Action, CreatedAt)
                        VALUES (@UserId, @Action, @CreatedAt)";
                    cmd.Parameters.AddWithValue("@UserId", log.UserId.HasValue ? (object)log.UserId.Value : DBNull.Value); // Handle nullable UserId
                    cmd.Parameters.AddWithValue("@Action", log.Action);
                    cmd.Parameters.AddWithValue("@CreatedAt", log.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss")); // Store DateTime as string
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while adding activity log: " + ex.Message, ex);
            }
        }

        public ActivityLog GetLogById(int logId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT LogId, UserId, Action, CreatedAt FROM ActivityLogs WHERE LogId = @LogId";
                    cmd.Parameters.AddWithValue("@LogId", logId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new ActivityLog
                            {
                                LogId = reader.GetInt32(0),
                                UserId = reader.IsDBNull(1) ? (int?)null : reader.GetInt32(1), // Handle nullable UserId
                                Action = reader.GetString(2),
                                CreatedAt = DateTime.Parse(reader.GetString(3)) // Parse string to DateTime
                            };
                        }
                        return null;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving activity log by ID: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing activity log date: " + ex.Message, ex);
            }
        }

        public List<ActivityLog> GetLogsByUserId(int userId)
        {
            var logs = new List<ActivityLog>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT LogId, UserId, Action, CreatedAt FROM ActivityLogs WHERE UserId = @UserId ORDER BY CreatedAt DESC";
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            logs.Add(new ActivityLog
                            {
                                LogId = reader.GetInt32(0),
                                UserId = reader.IsDBNull(1) ? (int?)null : reader.GetInt32(1),
                                Action = reader.GetString(2),
                                CreatedAt = DateTime.Parse(reader.GetString(3))
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving activity logs by user ID: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing activity log dates: " + ex.Message, ex);
            }
            return logs;
        }

        public List<ActivityLog> GetAllLogs()
        {
            var logs = new List<ActivityLog>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT LogId, UserId, Action, CreatedAt FROM ActivityLogs ORDER BY CreatedAt DESC";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            logs.Add(new ActivityLog
                            {
                                LogId = reader.GetInt32(0),
                                UserId = reader.IsDBNull(1) ? (int?)null : reader.GetInt32(1),
                                Action = reader.GetString(2),
                                CreatedAt = DateTime.Parse(reader.GetString(3))
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving all activity logs: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing activity log dates: " + ex.Message, ex);
            }
            return logs;
        }
    }
}
