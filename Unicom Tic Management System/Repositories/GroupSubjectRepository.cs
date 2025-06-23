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
    internal class GroupSubjectRepository : IGroupSubjectRepository
    {
        public void AddGroupSubject(GroupSubject groupSubject)
        {
            try
            {
                if (groupSubject == null)
                    throw new ArgumentNullException(nameof(groupSubject));

                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        INSERT INTO GroupSubjects (SubGroupId, SubjectId)
                        VALUES (@SubGroupId, @SubjectId)";
                    cmd.Parameters.AddWithValue("@SubGroupId", groupSubject.SubGroupId);
                    cmd.Parameters.AddWithValue("@SubjectId", groupSubject.SubjectId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while adding group-subject relationship: " + ex.Message, ex);
            }
        }

        public void DeleteGroupSubject(int subGroupId, int subjectId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "DELETE FROM GroupSubjects WHERE SubGroupId = @SubGroupId AND SubjectId = @SubjectId";
                    cmd.Parameters.AddWithValue("@SubGroupId", subGroupId);
                    cmd.Parameters.AddWithValue("@SubjectId", subjectId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while deleting group-subject relationship: " + ex.Message, ex);
            }
        }

        public GroupSubject GetGroupSubject(int subGroupId, int subjectId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT SubGroupId, SubjectId FROM GroupSubjects WHERE SubGroupId = @SubGroupId AND SubjectId = @SubjectId";
                    cmd.Parameters.AddWithValue("@SubGroupId", subGroupId);
                    cmd.Parameters.AddWithValue("@SubjectId", subjectId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new GroupSubject
                            {
                                SubGroupId = reader.GetInt32(0),
                                SubjectId = reader.GetInt32(1)
                            };
                        }
                        return null;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving group-subject relationship: " + ex.Message, ex);
            }
        }

        public List<GroupSubject> GetGroupSubjectsBySubGroupId(int subGroupId)
        {
            var groupSubjects = new List<GroupSubject>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT SubGroupId, SubjectId FROM GroupSubjects WHERE SubGroupId = @SubGroupId";
                    cmd.Parameters.AddWithValue("@SubGroupId", subGroupId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            groupSubjects.Add(new GroupSubject
                            {
                                SubGroupId = reader.GetInt32(0),
                                SubjectId = reader.GetInt32(1)
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving group-subjects by SubGroupId: " + ex.Message, ex);
            }
            return groupSubjects;
        }

        public List<GroupSubject> GetGroupSubjectsBySubjectId(int subjectId)
        {
            var groupSubjects = new List<GroupSubject>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT SubGroupId, SubjectId FROM GroupSubjects WHERE SubjectId = @SubjectId";
                    cmd.Parameters.AddWithValue("@SubjectId", subjectId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            groupSubjects.Add(new GroupSubject
                            {
                                SubGroupId = reader.GetInt32(0),
                                SubjectId = reader.GetInt32(1)
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving group-subjects by SubjectId: " + ex.Message, ex);
            }
            return groupSubjects;
        }

        public List<GroupSubject> GetAllGroupSubjects()
        {
            var groupSubjects = new List<GroupSubject>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT SubGroupId, SubjectId FROM GroupSubjects ORDER BY SubGroupId ASC, SubjectId ASC";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            groupSubjects.Add(new GroupSubject
                            {
                                SubGroupId = reader.GetInt32(0),
                                SubjectId = reader.GetInt32(1)
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving all group-subject relationships: " + ex.Message, ex);
            }
            return groupSubjects;
        }
    }
}
