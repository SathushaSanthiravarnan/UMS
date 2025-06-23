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
    internal class LectureSubjectRepository : ILectureSubjectRepository
    {
        public void AddLectureSubject(LectureSubject lectureSubject)
        {
            try
            {
                if (lectureSubject == null)
                    throw new ArgumentNullException(nameof(lectureSubject));

                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        INSERT INTO LectureSubjects (LecturerId, SubjectId)
                        VALUES (@LecturerId, @SubjectId)";
                    cmd.Parameters.AddWithValue("@LecturerId", lectureSubject.LecturerId);
                    cmd.Parameters.AddWithValue("@SubjectId", lectureSubject.SubjectId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while adding lecturer-subject relationship: " + ex.Message, ex);
            }
        }

        public void DeleteLectureSubject(int lecturerId, int subjectId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "DELETE FROM LectureSubjects WHERE LecturerId = @LecturerId AND SubjectId = @SubjectId";
                    cmd.Parameters.AddWithValue("@LecturerId", lecturerId);
                    cmd.Parameters.AddWithValue("@SubjectId", subjectId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while deleting lecturer-subject relationship: " + ex.Message, ex);
            }
        }

        public LectureSubject GetLectureSubject(int lecturerId, int subjectId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT LecturerId, SubjectId FROM LectureSubjects WHERE LecturerId = @LecturerId AND SubjectId = @SubjectId";
                    cmd.Parameters.AddWithValue("@LecturerId", lecturerId);
                    cmd.Parameters.AddWithValue("@SubjectId", subjectId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new LectureSubject
                            {
                                LecturerId = reader.GetInt32(0),
                                SubjectId = reader.GetInt32(1)
                            };
                        }
                        return null;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving lecturer-subject relationship: " + ex.Message, ex);
            }
        }

        public List<LectureSubject> GetSubjectsByLecturerId(int lecturerId)
        {
            var lectureSubjects = new List<LectureSubject>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT LecturerId, SubjectId FROM LectureSubjects WHERE LecturerId = @LecturerId ORDER BY SubjectId ASC";
                    cmd.Parameters.AddWithValue("@LecturerId", lecturerId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lectureSubjects.Add(new LectureSubject
                            {
                                LecturerId = reader.GetInt32(0),
                                SubjectId = reader.GetInt32(1)
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving subjects by lecturer ID: " + ex.Message, ex);
            }
            return lectureSubjects;
        }

        public List<LectureSubject> GetLecturersBySubjectId(int subjectId)
        {
            var lectureSubjects = new List<LectureSubject>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT LecturerId, SubjectId FROM LectureSubjects WHERE SubjectId = @SubjectId ORDER BY LecturerId ASC";
                    cmd.Parameters.AddWithValue("@SubjectId", subjectId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lectureSubjects.Add(new LectureSubject
                            {
                                LecturerId = reader.GetInt32(0),
                                SubjectId = reader.GetInt32(1)
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving lecturers by subject ID: " + ex.Message, ex);
            }
            return lectureSubjects;
        }

        public List<LectureSubject> GetAllLectureSubjects()
        {
            var lectureSubjects = new List<LectureSubject>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT LecturerId, SubjectId FROM LectureSubjects ORDER BY LecturerId ASC, SubjectId ASC";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lectureSubjects.Add(new LectureSubject
                            {
                                LecturerId = reader.GetInt32(0),
                                SubjectId = reader.GetInt32(1)
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving all lecturer-subject relationships: " + ex.Message, ex);
            }
            return lectureSubjects;
        }
    }
}
