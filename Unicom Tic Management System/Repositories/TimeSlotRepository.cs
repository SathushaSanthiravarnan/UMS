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
    internal class TimeSlotRepository : ITimeSlotRepository
    {
        public void AddTimeSlot(TimeSlot timeSlot)
        {
            try
            {
                if (timeSlot == null)
                    throw new ArgumentNullException(nameof(timeSlot));

                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        INSERT INTO TimeSlots (SlotName, StartTime, EndTime)
                        VALUES (@SlotName, @StartTime, @EndTime)";
                    cmd.Parameters.AddWithValue("@SlotName", timeSlot.SlotName);
                    cmd.Parameters.AddWithValue("@StartTime", timeSlot.StartTime);
                    cmd.Parameters.AddWithValue("@EndTime", timeSlot.EndTime);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                if (ex.Message.Contains("UNIQUE constraint failed"))
                {
                    throw new Exception($"A time slot with the name '{timeSlot.SlotName}' already exists.", ex);
                }
                throw new Exception("Database error while adding time slot: " + ex.Message, ex);
            }
        }

        public void UpdateTimeSlot(TimeSlot timeSlot)
        {
            try
            {
                if (timeSlot == null)
                    throw new ArgumentNullException(nameof(timeSlot));

                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        UPDATE TimeSlots
                        SET SlotName = @SlotName, StartTime = @StartTime, EndTime = @EndTime
                        WHERE TimeSlotId = @TimeSlotId";
                    cmd.Parameters.AddWithValue("@TimeSlotId", timeSlot.TimeSlotId);
                    cmd.Parameters.AddWithValue("@SlotName", timeSlot.SlotName);
                    cmd.Parameters.AddWithValue("@StartTime", timeSlot.StartTime);
                    cmd.Parameters.AddWithValue("@EndTime", timeSlot.EndTime);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                if (ex.Message.Contains("UNIQUE constraint failed"))
                {
                    throw new Exception($"A time slot with the name '{timeSlot.SlotName}' already exists.", ex);
                }
                throw new Exception("Database error while updating time slot: " + ex.Message, ex);
            }
        }

        public void DeleteTimeSlot(int timeSlotId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "DELETE FROM TimeSlots WHERE TimeSlotId = @TimeSlotId";
                    cmd.Parameters.AddWithValue("@TimeSlotId", timeSlotId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while deleting time slot: " + ex.Message, ex);
            }
        }

        public TimeSlot GetTimeSlotById(int timeSlotId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT TimeSlotId, SlotName, StartTime, EndTime FROM TimeSlots WHERE TimeSlotId = @TimeSlotId";
                    cmd.Parameters.AddWithValue("@TimeSlotId", timeSlotId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new TimeSlot
                            {
                                TimeSlotId = reader.GetInt32(0),
                                SlotName = reader.GetString(1),
                                StartTime = reader.GetString(2),
                                EndTime = reader.GetString(3)
                            };
                        }
                        return null;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving time slot by ID: " + ex.Message, ex);
            }
        }

        public TimeSlot GetTimeSlotBySlotName(string slotName)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT TimeSlotId, SlotName, StartTime, EndTime FROM TimeSlots WHERE SlotName = @SlotName";
                    cmd.Parameters.AddWithValue("@SlotName", slotName);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new TimeSlot
                            {
                                TimeSlotId = reader.GetInt32(0),
                                SlotName = reader.GetString(1),
                                StartTime = reader.GetString(2),
                                EndTime = reader.GetString(3)
                            };
                        }
                        return null;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving time slot by name: " + ex.Message, ex);
            }
        }

        public List<TimeSlot> GetAllTimeSlots()
        {
            var timeSlots = new List<TimeSlot>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT TimeSlotId, SlotName, StartTime, EndTime FROM TimeSlots ORDER BY StartTime ASC";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            timeSlots.Add(new TimeSlot
                            {
                                TimeSlotId = reader.GetInt32(0),
                                SlotName = reader.GetString(1),
                                StartTime = reader.GetString(2),
                                EndTime = reader.GetString(3)
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving all time slots: " + ex.Message, ex);
            }
            return timeSlots;
        }
    }
}
