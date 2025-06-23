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
    internal class LeaveRequestRepository : ILeaveRequestRepository
    {
        public void AddLeaveRequest(LeaveRequest leaveRequest)
        {
            try
            {
                if (leaveRequest == null)
                    throw new ArgumentNullException(nameof(leaveRequest));

                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        INSERT INTO LeaveRequests (UserId, Reason, StartDate, EndDate, CreatedAt, Status, ApprovedByUserId)
                        VALUES (@UserId, @Reason, @StartDate, @EndDate, @CreatedAt, @Status, @ApprovedByUserId)";
                    cmd.Parameters.AddWithValue("@UserId", leaveRequest.UserId);
                    cmd.Parameters.AddWithValue("@Reason", leaveRequest.Reason);
                    cmd.Parameters.AddWithValue("@StartDate", leaveRequest.StartDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@EndDate", leaveRequest.EndDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@CreatedAt", leaveRequest.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@Status", leaveRequest.Status.ToString()); // Store enum as string
                    cmd.Parameters.AddWithValue("@ApprovedByUserId", leaveRequest.ApprovedByUserId.HasValue ?
                                                (object)leaveRequest.ApprovedByUserId.Value :
                                                DBNull.Value); // Handle nullable int
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while adding leave request: " + ex.Message, ex);
            }
        }

        public void UpdateLeaveRequest(LeaveRequest leaveRequest)
        {
            try
            {
                if (leaveRequest == null)
                    throw new ArgumentNullException(nameof(leaveRequest));

                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        UPDATE LeaveRequests
                        SET UserId = @UserId, Reason = @Reason, StartDate = @StartDate,
                            EndDate = @EndDate, CreatedAt = @CreatedAt, Status = @Status,
                            ApprovedByUserId = @ApprovedByUserId
                        WHERE LeaveId = @LeaveId";
                    cmd.Parameters.AddWithValue("@LeaveId", leaveRequest.LeaveId);
                    cmd.Parameters.AddWithValue("@UserId", leaveRequest.UserId);
                    cmd.Parameters.AddWithValue("@Reason", leaveRequest.Reason);
                    cmd.Parameters.AddWithValue("@StartDate", leaveRequest.StartDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@EndDate", leaveRequest.EndDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@CreatedAt", leaveRequest.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@Status", leaveRequest.Status.ToString());
                    cmd.Parameters.AddWithValue("@ApprovedByUserId", leaveRequest.ApprovedByUserId.HasValue ?
                                                (object)leaveRequest.ApprovedByUserId.Value :
                                                DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while updating leave request: " + ex.Message, ex);
            }
        }

        public void DeleteLeaveRequest(int leaveId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "DELETE FROM LeaveRequests WHERE LeaveId = @LeaveId";
                    cmd.Parameters.AddWithValue("@LeaveId", leaveId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while deleting leave request: " + ex.Message, ex);
            }
        }

        public LeaveRequest GetLeaveRequestById(int leaveId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT LeaveId, UserId, Reason, StartDate, EndDate, CreatedAt, Status, ApprovedByUserId FROM LeaveRequests WHERE LeaveId = @LeaveId";
                    cmd.Parameters.AddWithValue("@LeaveId", leaveId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new LeaveRequest
                            {
                                LeaveId = reader.GetInt32(0),
                                UserId = reader.GetInt32(1),
                                Reason = reader.GetString(2),
                                StartDate = DateTime.Parse(reader.GetString(3)),
                                EndDate = DateTime.Parse(reader.GetString(4)),
                                CreatedAt = DateTime.Parse(reader.GetString(5)),
                                Status = (LeaveStatus)Enum.Parse(typeof(LeaveStatus), reader.GetString(6)),
                                ApprovedByUserId = reader.IsDBNull(7) ? (int?)null : reader.GetInt32(7)
                            };
                        }
                        return null;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving leave request by ID: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing leave request date or status: " + ex.Message, ex);
            }
            catch (ArgumentException ex)
            {
                throw new Exception("Invalid enum value encountered for LeaveStatus: " + ex.Message, ex);
            }
        }

        public List<LeaveRequest> GetLeaveRequestsByUserId(int userId)
        {
            var leaveRequests = new List<LeaveRequest>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT LeaveId, UserId, Reason, StartDate, EndDate, CreatedAt, Status, ApprovedByUserId FROM LeaveRequests WHERE UserId = @UserId ORDER BY CreatedAt DESC";
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            leaveRequests.Add(new LeaveRequest
                            {
                                LeaveId = reader.GetInt32(0),
                                UserId = reader.GetInt32(1),
                                Reason = reader.GetString(2),
                                StartDate = DateTime.Parse(reader.GetString(3)),
                                EndDate = DateTime.Parse(reader.GetString(4)),
                                CreatedAt = DateTime.Parse(reader.GetString(5)),
                                Status = (LeaveStatus)Enum.Parse(typeof(LeaveStatus), reader.GetString(6)),
                                ApprovedByUserId = reader.IsDBNull(7) ? (int?)null : reader.GetInt32(7)
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving leave requests by User ID: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing leave request dates or status: " + ex.Message, ex);
            }
            catch (ArgumentException ex)
            {
                throw new Exception("Invalid enum value encountered for LeaveStatus: " + ex.Message, ex);
            }
            return leaveRequests;
        }

        public List<LeaveRequest> GetLeaveRequestsByStatus(LeaveStatus status)
        {
            var leaveRequests = new List<LeaveRequest>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT LeaveId, UserId, Reason, StartDate, EndDate, CreatedAt, Status, ApprovedByUserId FROM LeaveRequests WHERE Status = @Status ORDER BY CreatedAt DESC";
                    cmd.Parameters.AddWithValue("@Status", status.ToString());

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            leaveRequests.Add(new LeaveRequest
                            {
                                LeaveId = reader.GetInt32(0),
                                UserId = reader.GetInt32(1),
                                Reason = reader.GetString(2),
                                StartDate = DateTime.Parse(reader.GetString(3)),
                                EndDate = DateTime.Parse(reader.GetString(4)),
                                CreatedAt = DateTime.Parse(reader.GetString(5)),
                                Status = (LeaveStatus)Enum.Parse(typeof(LeaveStatus), reader.GetString(6)),
                                ApprovedByUserId = reader.IsDBNull(7) ? (int?)null : reader.GetInt32(7)
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving leave requests by status: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing leave request dates or status: " + ex.Message, ex);
            }
            catch (ArgumentException ex)
            {
                throw new Exception("Invalid enum value encountered for LeaveStatus: " + ex.Message, ex);
            }
            return leaveRequests;
        }

        public List<LeaveRequest> GetLeaveRequestsByDateRange(DateTime startDate, DateTime endDate)
        {
            var leaveRequests = new List<LeaveRequest>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        SELECT LeaveId, UserId, Reason, StartDate, EndDate, CreatedAt, Status, ApprovedByUserId
                        FROM LeaveRequests
                        WHERE (StartDate <= @EndDate AND EndDate >= @StartDate) -- Checks for overlap
                        ORDER BY StartDate ASC";
                    cmd.Parameters.AddWithValue("@StartDate", startDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@EndDate", endDate.ToString("yyyy-MM-dd HH:mm:ss"));

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            leaveRequests.Add(new LeaveRequest
                            {
                                LeaveId = reader.GetInt32(0),
                                UserId = reader.GetInt32(1),
                                Reason = reader.GetString(2),
                                StartDate = DateTime.Parse(reader.GetString(3)),
                                EndDate = DateTime.Parse(reader.GetString(4)),
                                CreatedAt = DateTime.Parse(reader.GetString(5)),
                                Status = (LeaveStatus)Enum.Parse(typeof(LeaveStatus), reader.GetString(6)),
                                ApprovedByUserId = reader.IsDBNull(7) ? (int?)null : reader.GetInt32(7)
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving leave requests by date range: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing leave request dates or status: " + ex.Message, ex);
            }
            catch (ArgumentException ex)
            {
                throw new Exception("Invalid enum value encountered for LeaveStatus: " + ex.Message, ex);
            }
            return leaveRequests;
        }

        public List<LeaveRequest> GetAllLeaveRequests()
        {
            var leaveRequests = new List<LeaveRequest>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT LeaveId, UserId, Reason, StartDate, EndDate, CreatedAt, Status, ApprovedByUserId FROM LeaveRequests ORDER BY CreatedAt DESC";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            leaveRequests.Add(new LeaveRequest
                            {
                                LeaveId = reader.GetInt32(0),
                                UserId = reader.GetInt32(1),
                                Reason = reader.GetString(2),
                                StartDate = DateTime.Parse(reader.GetString(3)),
                                EndDate = DateTime.Parse(reader.GetString(4)),
                                CreatedAt = DateTime.Parse(reader.GetString(5)),
                                Status = (LeaveStatus)Enum.Parse(typeof(LeaveStatus), reader.GetString(6)),
                                ApprovedByUserId = reader.IsDBNull(7) ? (int?)null : reader.GetInt32(7)
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving all leave requests: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing leave request dates or status: " + ex.Message, ex);
            }
            catch (ArgumentException ex)
            {
                throw new Exception("Invalid enum value encountered for LeaveStatus: " + ex.Message, ex);
            }
            return leaveRequests;
        }
    }
}
