using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unicom_Tic_Management_System.Datas;
using Unicom_Tic_Management_System.Models;
using Unicom_Tic_Management_System.Repositories;
using Unicom_Tic_Management_System.Repositories.Interfaces;

namespace Unicom_Tic_Management_System.Repositories
{
    internal class TimetableRepository : ITimetableRepository
    {
        // Helper method to read a Timetable object with all related names from a SQLiteDataReader
        private Timetable ReadTimetableFromReader(SQLiteDataReader reader)
        {
            return new Timetable
            {
                TimetableEntryId = reader.GetInt32(reader.GetOrdinal("TimetableEntryId")),
                CourseId = reader.GetInt32(reader.GetOrdinal("CourseId")),
                CourseName = reader.GetString(reader.GetOrdinal("CourseName")), // Fetched via JOIN
                LecturerId = reader.GetInt32(reader.GetOrdinal("LecturerId")),
                LecturerName = reader.GetString(reader.GetOrdinal("LecturerName")), // Fetched via JOIN
                RoomId = reader.GetInt32(reader.GetOrdinal("RoomId")),
                RoomNumber = reader.GetString(reader.GetOrdinal("RoomNumber")), // Fetched via JOIN
                MainGroupId = reader.GetInt32(reader.GetOrdinal("MainGroupId")),
                MainGroupName = reader.GetString(reader.GetOrdinal("MainGroupName")), // Fetched via JOIN
                SubGroupId = reader.GetInt32(reader.GetOrdinal("SubGroupId")),
                SubGroupName = reader.GetString(reader.GetOrdinal("SubGroupName")), // Fetched via JOIN
                SlotId = reader.GetInt32(reader.GetOrdinal("SlotId")),
                SlotTimeRange = reader.GetString(reader.GetOrdinal("SlotTimeRange")), // Fetched via JOIN
                Date = reader.GetDateTime(reader.GetOrdinal("Date"))
            };
        }

        public void AddTimetableEntry(Timetable timetable)
        {
            try
            {
                if (timetable == null)
                    throw new ArgumentNullException(nameof(timetable));

                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        INSERT INTO Timetables (CourseId, LecturerId, RoomId, MainGroupId, SubGroupId, SlotId, Date)
                        VALUES (@CourseId, @LecturerId, @RoomId, @MainGroupId, @SubGroupId, @SlotId, @Date)";

                    cmd.Parameters.AddWithValue("@CourseId", timetable.CourseId);
                    cmd.Parameters.AddWithValue("@LecturerId", timetable.LecturerId);
                    cmd.Parameters.AddWithValue("@RoomId", timetable.RoomId);
                    cmd.Parameters.AddWithValue("@MainGroupId", timetable.MainGroupId);
                    cmd.Parameters.AddWithValue("@SubGroupId", timetable.SubGroupId);
                    cmd.Parameters.AddWithValue("@SlotId", timetable.SlotId);
                    cmd.Parameters.AddWithValue("@Date", timetable.Date.ToString("yyyy-MM-dd")); // Store date as string

                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                // Consider more specific error handling for UNIQUE constraints if applicable
                throw new Exception("Database error while adding timetable entry: " + ex.Message, ex);
            }
        }

        public void UpdateTimetableEntry(Timetable timetable)
        {
            try
            {
                if (timetable == null)
                    throw new ArgumentNullException(nameof(timetable));

                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        UPDATE Timetables
                        SET CourseId = @CourseId, LecturerId = @LecturerId, RoomId = @RoomId,
                            MainGroupId = @MainGroupId, SubGroupId = @SubGroupId, SlotId = @SlotId, Date = @Date
                        WHERE TimetableEntryId = @TimetableEntryId";

                    cmd.Parameters.AddWithValue("@TimetableEntryId", timetable.TimetableEntryId);
                    cmd.Parameters.AddWithValue("@CourseId", timetable.CourseId);
                    cmd.Parameters.AddWithValue("@LecturerId", timetable.LecturerId);
                    cmd.Parameters.AddWithValue("@RoomId", timetable.RoomId);
                    cmd.Parameters.AddWithValue("@MainGroupId", timetable.MainGroupId);
                    cmd.Parameters.AddWithValue("@SubGroupId", timetable.SubGroupId);
                    cmd.Parameters.AddWithValue("@SlotId", timetable.SlotId);
                    cmd.Parameters.AddWithValue("@Date", timetable.Date.ToString("yyyy-MM-dd"));

                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while updating timetable entry: " + ex.Message, ex);
            }
        }

        public void DeleteTimetableEntry(int timetableId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "DELETE FROM Timetables WHERE TimetableEntryId = @TimetableEntryId";
                    cmd.Parameters.AddWithValue("@TimetableEntryId", timetableId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while deleting timetable entry: " + ex.Message, ex);
            }
        }

        public Timetable GetTimetableEntryById(int timetableId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        SELECT
                            t.TimetableEntryId,
                            t.CourseId, c.CourseName,
                            t.LecturerId, l.LecturerName,
                            t.RoomId, r.RoomNumber,
                            t.MainGroupId, mg.MainGroupName,
                            t.SubGroupId, sg.SubGroupName,
                            t.SlotId, ts.SlotName AS SlotTimeRange, -- Assuming SlotName is what you want for SlotTimeRange
                            t.Date
                        FROM Timetables AS t
                        LEFT JOIN Courses AS c ON t.CourseId = c.CourseId
                        LEFT JOIN Lecturers AS l ON t.LecturerId = l.LecturerId
                        LEFT JOIN Rooms AS r ON t.RoomId = r.RoomId
                        LEFT JOIN MainGroups AS mg ON t.MainGroupId = mg.MainGroupId
                        LEFT JOIN SubGroups AS sg ON t.SubGroupId = sg.SubGroupId
                        LEFT JOIN TimeSlots AS ts ON t.SlotId = ts.TimeSlotId
                        WHERE t.TimetableEntryId = @TimetableEntryId";
                    cmd.Parameters.AddWithValue("@TimetableEntryId", timetableId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return ReadTimetableFromReader(reader);
                        }
                        return null;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving timetable entry by ID: " + ex.Message, ex);
            }
        }

        public List<Timetable> GetAllTimetableEntries()
        {
            var timetableEntries = new List<Timetable>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        SELECT
                            t.TimetableEntryId,
                            t.CourseId, c.CourseName,
                            t.LecturerId, l.LecturerName,
                            t.RoomId, r.RoomNumber,
                            t.MainGroupId, mg.MainGroupName,
                            t.SubGroupId, sg.SubGroupName,
                            t.SlotId, ts.SlotName AS SlotTimeRange, -- Assuming SlotName is what you want for SlotTimeRange
                            t.Date
                        FROM Timetables AS t
                        LEFT JOIN Courses AS c ON t.CourseId = c.CourseId
                        LEFT JOIN Lecturers AS l ON t.LecturerId = l.LecturerId
                        LEFT JOIN Rooms AS r ON t.RoomId = r.RoomId
                        LEFT JOIN MainGroups AS mg ON t.MainGroupId = mg.MainGroupId
                        LEFT JOIN SubGroups AS sg ON t.SubGroupId = sg.SubGroupId
                        LEFT JOIN TimeSlots AS ts ON t.SlotId = ts.TimeSlotId
                        ORDER BY t.Date, ts.StartTime ASC"; // Order by date and then time slot start time

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            timetableEntries.Add(ReadTimetableFromReader(reader));
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving all timetable entries: " + ex.Message, ex);
            }
            return timetableEntries;
        }

        // Implement other GetTimetableEntriesBy... methods similarly, adjusting WHERE clauses
        // For example:

        public List<Timetable> GetTimetableEntriesByCourse(int courseId, string academicYear = null)
        {
            var timetableEntries = new List<Timetable>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    string sql = @"
                        SELECT
                            t.TimetableEntryId,
                            t.CourseId, c.CourseName,
                            t.LecturerId, l.LecturerName,
                            t.RoomId, r.RoomNumber,
                            t.MainGroupId, mg.MainGroupName,
                            t.SubGroupId, sg.SubGroupName,
                            t.SlotId, ts.SlotName AS SlotTimeRange,
                            t.Date
                        FROM Timetables AS t
                        LEFT JOIN Courses AS c ON t.CourseId = c.CourseId
                        LEFT JOIN Lecturers AS l ON t.LecturerId = l.LecturerId
                        LEFT JOIN Rooms AS r ON t.RoomId = r.RoomId
                        LEFT JOIN MainGroups AS mg ON t.MainGroupId = mg.MainGroupId
                        LEFT JOIN SubGroups AS sg ON t.SubGroupId = sg.SubGroupId
                        LEFT JOIN TimeSlots AS ts ON t.SlotId = ts.TimeSlotId
                        WHERE t.CourseId = @CourseId";

                    if (!string.IsNullOrEmpty(academicYear))
                    {
                        // Assuming you have an AcademicYear column in your Timetables table
                        // Or a way to derive it from the Date column
                        sql += " AND strftime('%Y', t.Date) = @AcademicYear"; // Example for year
                        cmd.Parameters.AddWithValue("@AcademicYear", academicYear);
                    }
                    sql += " ORDER BY t.Date, ts.StartTime ASC";
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@CourseId", courseId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            timetableEntries.Add(ReadTimetableFromReader(reader));
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving timetable entries by course: " + ex.Message, ex);
            }
            return timetableEntries;
        }

        // ... You would implement similar logic for other methods:
        // GetTimetableEntriesBySubject (if subject is different from Course, ensure join is correct)
        // GetTimetableEntriesByLecturer
        // GetTimetableEntriesByRoom
        // GetTimetableEntriesByDayAndTimeSlot (requires adding DayOfWeek to Timetable table or deriving it)
        // GetTimetableEntriesByAcademicYear
        public List<Timetable> GetTimetableEntriesBySubject(int subjectId, string academicYear = null)
        {
            // Assuming 'Subject' is synonymous with 'Course' in your current model,
            // or you have a separate Subject table to join with.
            // For now, I'll treat it like Course for demonstration.
            return GetTimetableEntriesByCourse(subjectId, academicYear);
        }

        public List<Timetable> GetTimetableEntriesByLecturer(int lecturerId, string academicYear = null)
        {
            var timetableEntries = new List<Timetable>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    string sql = @"
                        SELECT
                            t.TimetableEntryId,
                            t.CourseId, c.CourseName,
                            t.LecturerId, l.LecturerName,
                            t.RoomId, r.RoomNumber,
                            t.MainGroupId, mg.MainGroupName,
                            t.SubGroupId, sg.SubGroupName,
                            t.SlotId, ts.SlotName AS SlotTimeRange,
                            t.Date
                        FROM Timetables AS t
                        LEFT JOIN Courses AS c ON t.CourseId = c.CourseId
                        LEFT JOIN Lecturers AS l ON t.LecturerId = l.LecturerId
                        LEFT JOIN Rooms AS r ON t.RoomId = r.RoomId
                        LEFT JOIN MainGroups AS mg ON t.MainGroupId = mg.MainGroupId
                        LEFT JOIN SubGroups AS sg ON t.SubGroupId = sg.SubGroupId
                        LEFT JOIN TimeSlots AS ts ON t.SlotId = ts.TimeSlotId
                        WHERE t.LecturerId = @LecturerId";

                    if (!string.IsNullOrEmpty(academicYear))
                    {
                        sql += " AND strftime('%Y', t.Date) = @AcademicYear";
                        cmd.Parameters.AddWithValue("@AcademicYear", academicYear);
                    }
                    sql += " ORDER BY t.Date, ts.StartTime ASC";
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@LecturerId", lecturerId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            timetableEntries.Add(ReadTimetableFromReader(reader));
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving timetable entries by lecturer: " + ex.Message, ex);
            }
            return timetableEntries;
        }

        public List<Timetable> GetTimetableEntriesByRoom(int roomId, string academicYear = null)
        {
            var timetableEntries = new List<Timetable>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    string sql = @"
                        SELECT
                            t.TimetableEntryId,
                            t.CourseId, c.CourseName,
                            t.LecturerId, l.LecturerName,
                            t.RoomId, r.RoomNumber,
                            t.MainGroupId, mg.MainGroupName,
                            t.SubGroupId, sg.SubGroupName,
                            t.SlotId, ts.SlotName AS SlotTimeRange,
                            t.Date
                        FROM Timetables AS t
                        LEFT JOIN Courses AS c ON t.CourseId = c.CourseId
                        LEFT JOIN Lecturers AS l ON t.LecturerId = l.LecturerId
                        LEFT JOIN Rooms AS r ON t.RoomId = r.RoomId
                        LEFT JOIN MainGroups AS mg ON t.MainGroupId = mg.MainGroupId
                        LEFT JOIN SubGroups AS sg ON t.SubGroupId = sg.SubGroupId
                        LEFT JOIN TimeSlots AS ts ON t.SlotId = ts.TimeSlotId
                        WHERE t.RoomId = @RoomId";

                    if (!string.IsNullOrEmpty(academicYear))
                    {
                        sql += " AND strftime('%Y', t.Date) = @AcademicYear";
                        cmd.Parameters.AddWithValue("@AcademicYear", academicYear);
                    }
                    sql += " ORDER BY t.Date, ts.StartTime ASC";
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@RoomId", roomId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            timetableEntries.Add(ReadTimetableFromReader(reader));
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving timetable entries by room: " + ex.Message, ex);
            }
            return timetableEntries;
        }

        public List<Timetable> GetTimetableEntriesByDayAndTimeSlot(string dayOfWeek, int timeSlotId, string academicYear = null)
        {
            var timetableEntries = new List<Timetable>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    string sql = @"
                        SELECT
                            t.TimetableEntryId,
                            t.CourseId, c.CourseName,
                            t.LecturerId, l.LecturerName,
                            t.RoomId, r.RoomNumber,
                            t.MainGroupId, mg.MainGroupName,
                            t.SubGroupId, sg.SubGroupName,
                            t.SlotId, ts.SlotName AS SlotTimeRange,
                            t.Date
                        FROM Timetables AS t
                        LEFT JOIN Courses AS c ON t.CourseId = c.CourseId
                        LEFT JOIN Lecturers AS l ON t.LecturerId = l.LecturerId
                        LEFT JOIN Rooms AS r ON t.RoomId = r.RoomId
                        LEFT JOIN MainGroups AS mg ON t.MainGroupId = mg.MainGroupId
                        LEFT JOIN SubGroups AS sg ON t.SubGroupId = sg.SubGroupId
                        LEFT JOIN TimeSlots AS ts ON t.SlotId = ts.TimeSlotId
                        WHERE t.SlotId = @TimeSlotId
                        AND strftime('%w', t.Date) = @DayOfWeekNumber"; // %w for day of week (0=Sunday, 6=Saturday)

                    // You'll need a mapping for dayOfWeek string to SQLite's %w (e.g., "Monday" -> "1")
                    // For simplicity, let's assume dayOfWeek is already the number or handle the conversion here.
                    // Example: if "Monday" is passed, convert to "1"
                    string dayOfWeekNumber;
                    switch (dayOfWeek.ToLower())
                    {
                        case "sunday": dayOfWeekNumber = "0"; break;
                        case "monday": dayOfWeekNumber = "1"; break;
                        case "tuesday": dayOfWeekNumber = "2"; break;
                        case "wednesday": dayOfWeekNumber = "3"; break;
                        case "thursday": dayOfWeekNumber = "4"; break;
                        case "friday": dayOfWeekNumber = "5"; break;
                        case "saturday": dayOfWeekNumber = "6"; break;
                        default: throw new ArgumentException("Invalid day of week string.");
                    }


                    if (!string.IsNullOrEmpty(academicYear))
                    {
                        sql += " AND strftime('%Y', t.Date) = @AcademicYear";
                        cmd.Parameters.AddWithValue("@AcademicYear", academicYear);
                    }
                    sql += " ORDER BY t.Date, ts.StartTime ASC";
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@TimeSlotId", timeSlotId);
                    cmd.Parameters.AddWithValue("@DayOfWeekNumber", dayOfWeekNumber);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            timetableEntries.Add(ReadTimetableFromReader(reader));
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving timetable entries by day and time slot: " + ex.Message, ex);
            }
            return timetableEntries;
        }

        public List<Timetable> GetTimetableEntriesByAcademicYear(string academicYear)
        {
            var timetableEntries = new List<Timetable>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    string sql = @"
                        SELECT
                            t.TimetableEntryId,
                            t.CourseId, c.CourseName,
                            t.LecturerId, l.LecturerName,
                            t.RoomId, r.RoomNumber,
                            t.MainGroupId, mg.MainGroupName,
                            t.SubGroupId, sg.SubGroupName,
                            t.SlotId, ts.SlotName AS SlotTimeRange,
                            t.Date
                        FROM Timetables AS t
                        LEFT JOIN Courses AS c ON t.CourseId = c.CourseId
                        LEFT JOIN Lecturers AS l ON t.LecturerId = l.LecturerId
                        LEFT JOIN Rooms AS r ON t.RoomId = r.RoomId
                        LEFT JOIN MainGroups AS mg ON t.MainGroupId = mg.MainGroupId
                        LEFT JOIN SubGroups AS sg ON t.SubGroupId = sg.SubGroupId
                        LEFT JOIN TimeSlots AS ts ON t.SlotId = ts.TimeSlotId
                        WHERE strftime('%Y', t.Date) = @AcademicYear
                        ORDER BY t.Date, ts.StartTime ASC";
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@AcademicYear", academicYear);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            timetableEntries.Add(ReadTimetableFromReader(reader));
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving timetable entries by academic year: " + ex.Message, ex);
            }
            return timetableEntries;
        }
    }
}
