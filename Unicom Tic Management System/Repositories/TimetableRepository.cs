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
    internal class TimetableRepository : ITimetableRepository
    {
        public void AddTimetableEntry(Timetable timetable)
        {
            try
            {
                if (timetable == null)
                    throw new ArgumentNullException(nameof(timetable));

                var existing = GetTimetableEntriesByDayAndTimeSlot(timetable.DayOfWeek, timetable.TimeSlotId, timetable.AcademicYear);

                if (existing.Exists(t => t.RoomId == timetable.RoomId))
                    throw new Exception($"Room {timetable.RoomId} is already occupied at {timetable.DayOfWeek} during slot {timetable.TimeSlotId}");

                if (existing.Exists(t => t.LecturerId == timetable.LecturerId))
                    throw new Exception($"Lecturer {timetable.LecturerId} is already scheduled at {timetable.DayOfWeek} during slot {timetable.TimeSlotId}");

                if (existing.Exists(t => t.CourseId == timetable.CourseId && t.SubjectId == timetable.SubjectId))
                    throw new Exception($"Course {timetable.CourseId} with Subject {timetable.SubjectId} is already scheduled at {timetable.DayOfWeek} during slot {timetable.TimeSlotId}");

                if (existing.Exists(t => t.GroupId == timetable.GroupId && t.RoomId == timetable.RoomId))
                    throw new Exception($"Group {timetable.GroupId} is already using Room {timetable.RoomId} during this time");

                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        INSERT INTO Timetables (CourseId, SubjectId, LecturerId, RoomId, DayOfWeek, TimeSlotId, AcademicYear, GroupId, ActivityType)
                        VALUES (@CourseId, @SubjectId, @LecturerId, @RoomId, @DayOfWeek, @TimeSlotId, @AcademicYear, @GroupId, @ActivityType)";
                    cmd.Parameters.AddWithValue("@CourseId", timetable.CourseId);
                    cmd.Parameters.AddWithValue("@SubjectId", timetable.SubjectId);
                    cmd.Parameters.AddWithValue("@LecturerId", timetable.LecturerId);
                    cmd.Parameters.AddWithValue("@RoomId", timetable.RoomId);
                    cmd.Parameters.AddWithValue("@DayOfWeek", timetable.DayOfWeek);
                    cmd.Parameters.AddWithValue("@TimeSlotId", timetable.TimeSlotId);
                    cmd.Parameters.AddWithValue("@AcademicYear", timetable.AcademicYear);
                    cmd.Parameters.AddWithValue("@GroupId", timetable.GroupId);
                    cmd.Parameters.AddWithValue("@ActivityType", timetable.ActivityType);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while adding timetable entry: " + ex.Message, ex);
            }
        }

        public void UpdateTimetableEntry(Timetable timetable)
        {
            try
            {
                if (timetable == null)
                    throw new ArgumentNullException(nameof(timetable));

                var existing = GetTimetableEntriesByDayAndTimeSlot(timetable.DayOfWeek, timetable.TimeSlotId, timetable.AcademicYear)
                    .FindAll(t => t.TimetableId != timetable.TimetableId);

                if (existing.Exists(t => t.RoomId == timetable.RoomId))
                    throw new Exception($"Room {timetable.RoomId} is already occupied at {timetable.DayOfWeek} during slot {timetable.TimeSlotId}");

                if (existing.Exists(t => t.LecturerId == timetable.LecturerId))
                    throw new Exception($"Lecturer {timetable.LecturerId} is already scheduled at {timetable.DayOfWeek} during slot {timetable.TimeSlotId}");

                if (existing.Exists(t => t.CourseId == timetable.CourseId && t.SubjectId == timetable.SubjectId))
                    throw new Exception($"Course {timetable.CourseId} with Subject {timetable.SubjectId} is already scheduled at {timetable.DayOfWeek} during slot {timetable.TimeSlotId}");

                if (existing.Exists(t => t.GroupId == timetable.GroupId && t.RoomId == timetable.RoomId))
                    throw new Exception($"Group {timetable.GroupId} is already using Room {timetable.RoomId} during this time");

                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        UPDATE Timetables
                        SET CourseId = @CourseId, SubjectId = @SubjectId, LecturerId = @LecturerId,
                            RoomId = @RoomId, DayOfWeek = @DayOfWeek, TimeSlotId = @TimeSlotId,
                            AcademicYear = @AcademicYear, GroupId = @GroupId, ActivityType = @ActivityType
                        WHERE TimetableId = @TimetableId";
                    cmd.Parameters.AddWithValue("@TimetableId", timetable.TimetableId);
                    cmd.Parameters.AddWithValue("@CourseId", timetable.CourseId);
                    cmd.Parameters.AddWithValue("@SubjectId", timetable.SubjectId);
                    cmd.Parameters.AddWithValue("@LecturerId", timetable.LecturerId);
                    cmd.Parameters.AddWithValue("@RoomId", timetable.RoomId);
                    cmd.Parameters.AddWithValue("@DayOfWeek", timetable.DayOfWeek);
                    cmd.Parameters.AddWithValue("@TimeSlotId", timetable.TimeSlotId);
                    cmd.Parameters.AddWithValue("@AcademicYear", timetable.AcademicYear);
                    cmd.Parameters.AddWithValue("@GroupId", timetable.GroupId);
                    cmd.Parameters.AddWithValue("@ActivityType", timetable.ActivityType);
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
                    cmd.CommandText = "DELETE FROM Timetables WHERE TimetableId = @TimetableId";
                    cmd.Parameters.AddWithValue("@TimetableId", timetableId);
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
            return GetOne("WHERE TimetableId = @Id", "@Id", timetableId);
        }

        public List<Timetable> GetTimetableEntriesByCourse(int courseId, string academicYear = null)
        {
            return GetMany("WHERE CourseId = @Id" + (academicYear != null ? " AND AcademicYear = @Year" : ""), "@Id", courseId, academicYear);
        }

        public List<Timetable> GetTimetableEntriesBySubject(int subjectId, string academicYear = null)
        {
            return GetMany("WHERE SubjectId = @Id" + (academicYear != null ? " AND AcademicYear = @Year" : ""), "@Id", subjectId, academicYear);
        }

        public List<Timetable> GetTimetableEntriesByLecturer(int lecturerId, string academicYear = null)
        {
            return GetMany("WHERE LecturerId = @Id" + (academicYear != null ? " AND AcademicYear = @Year" : ""), "@Id", lecturerId, academicYear);
        }

        public List<Timetable> GetTimetableEntriesByRoom(int roomId, string academicYear = null)
        {
            return GetMany("WHERE RoomId = @Id" + (academicYear != null ? " AND AcademicYear = @Year" : ""), "@Id", roomId, academicYear);
        }

        public List<Timetable> GetTimetableEntriesByDayAndTimeSlot(string dayOfWeek, int timeSlotId, string academicYear = null)
        {
            var entries = new List<Timetable>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    string sql = "SELECT * FROM Timetables WHERE DayOfWeek = @Day AND TimeSlotId = @Slot";
                    if (academicYear != null)
                    {
                        sql += " AND AcademicYear = @Year";
                        cmd.Parameters.AddWithValue("@Year", academicYear);
                    }
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@Day", dayOfWeek);
                    cmd.Parameters.AddWithValue("@Slot", timeSlotId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            entries.Add(ReadTimetable(reader));
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("DB error in GetTimetableEntriesByDayAndTimeSlot: " + ex.Message, ex);
            }
            return entries;
        }

        public List<Timetable> GetTimetableEntriesByAcademicYear(string academicYear)
        {
            return GetMany("WHERE AcademicYear = @Year", null, 0, academicYear);
        }

        public List<Timetable> GetAllTimetableEntries()
        {
            return GetMany("", null);
        }

        // 🔁 Shared Helpers
        private List<Timetable> GetMany(string whereClause, string paramName, int paramValue = 0, string year = null)
        {
            var entries = new List<Timetable>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    string query = $"SELECT * FROM Timetables {whereClause} ORDER BY AcademicYear ASC, DayOfWeek ASC, TimeSlotId ASC";
                    cmd.CommandText = query;

                    if (paramName != null)
                        cmd.Parameters.AddWithValue(paramName, paramValue);
                    if (year != null)
                        cmd.Parameters.AddWithValue("@Year", year);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                            entries.Add(ReadTimetable(reader));
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("DB error in GetMany: " + ex.Message, ex);
            }
            return entries;
        }

        private Timetable GetOne(string whereClause, string paramName, int paramValue)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = $"SELECT * FROM Timetables {whereClause}";
                    cmd.Parameters.AddWithValue(paramName, paramValue);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                            return ReadTimetable(reader);
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("DB error in GetOne: " + ex.Message, ex);
            }
            return null;
        }

        private Timetable ReadTimetable(SQLiteDataReader reader)
        {
            return new Timetable
            {
                TimetableId = reader.GetInt32(reader.GetOrdinal("TimetableId")),
                CourseId = reader.GetInt32(reader.GetOrdinal("CourseId")),
                SubjectId = reader.GetInt32(reader.GetOrdinal("SubjectId")),
                LecturerId = reader.GetInt32(reader.GetOrdinal("LecturerId")),
                RoomId = reader.GetInt32(reader.GetOrdinal("RoomId")),
                DayOfWeek = reader.GetString(reader.GetOrdinal("DayOfWeek")),
                TimeSlotId = reader.GetInt32(reader.GetOrdinal("TimeSlotId")),
                AcademicYear = reader.GetString(reader.GetOrdinal("AcademicYear")),
                GroupId = reader.GetInt32(reader.GetOrdinal("GroupId")),
                ActivityType = reader.GetString(reader.GetOrdinal("ActivityType"))
            };
        }
    }

}
