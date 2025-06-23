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
    internal class GroupMentorRepository : IGroupMentorRepository
    {
        public void AddGroupMentor(GroupMentor groupMentor)
        {
            try
            {
                if (groupMentor == null)
                    throw new ArgumentNullException(nameof(groupMentor));

                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        INSERT INTO GroupMentors (SubGroupId, MentorId, AssignedDate)
                        VALUES (@SubGroupId, @MentorId, @AssignedDate)";
                    cmd.Parameters.AddWithValue("@SubGroupId", groupMentor.SubGroupId);
                    cmd.Parameters.AddWithValue("@MentorId", groupMentor.MentorId);
                    // Handle nullable DateTime: store null if no value, otherwise format
                    cmd.Parameters.AddWithValue("@AssignedDate", groupMentor.AssignedDate.HasValue ?
                                                (object)groupMentor.AssignedDate.Value.ToString("yyyy-MM-dd HH:mm:ss") :
                                                DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while adding group-mentor relationship: " + ex.Message, ex);
            }
        }

        public void DeleteGroupMentor(int subGroupId, int mentorId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "DELETE FROM GroupMentors WHERE SubGroupId = @SubGroupId AND MentorId = @MentorId";
                    cmd.Parameters.AddWithValue("@SubGroupId", subGroupId);
                    cmd.Parameters.AddWithValue("@MentorId", mentorId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while deleting group-mentor relationship: " + ex.Message, ex);
            }
        }

        public void UpdateGroupMentorAssignedDate(int subGroupId, int mentorId, DateTime? newAssignedDate)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        UPDATE GroupMentors
                        SET AssignedDate = @NewAssignedDate
                        WHERE SubGroupId = @SubGroupId AND MentorId = @MentorId";
                    cmd.Parameters.AddWithValue("@SubGroupId", subGroupId);
                    cmd.Parameters.AddWithValue("@MentorId", mentorId);
                    cmd.Parameters.AddWithValue("@NewAssignedDate", newAssignedDate.HasValue ?
                                               (object)newAssignedDate.Value.ToString("yyyy-MM-dd HH:mm:ss") :
                                               DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while updating group-mentor assigned date: " + ex.Message, ex);
            }
        }


        public GroupMentor GetGroupMentor(int subGroupId, int mentorId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT SubGroupId, MentorId, AssignedDate FROM GroupMentors WHERE SubGroupId = @SubGroupId AND MentorId = @MentorId";
                    cmd.Parameters.AddWithValue("@SubGroupId", subGroupId);
                    cmd.Parameters.AddWithValue("@MentorId", mentorId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new GroupMentor
                            {
                                SubGroupId = reader.GetInt32(0),
                                MentorId = reader.GetInt32(1),
                                // Handle nullable DateTime read
                                AssignedDate = reader.IsDBNull(2) ? (DateTime?)null : DateTime.Parse(reader.GetString(2))
                            };
                        }
                        return null;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving group-mentor relationship: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing assigned date: " + ex.Message, ex);
            }
        }

        public List<GroupMentor> GetGroupMentorsBySubGroupId(int subGroupId)
        {
            var groupMentors = new List<GroupMentor>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT SubGroupId, MentorId, AssignedDate FROM GroupMentors WHERE SubGroupId = @SubGroupId ORDER BY MentorId ASC";
                    cmd.Parameters.AddWithValue("@SubGroupId", subGroupId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            groupMentors.Add(new GroupMentor
                            {
                                SubGroupId = reader.GetInt32(0),
                                MentorId = reader.GetInt32(1),
                                AssignedDate = reader.IsDBNull(2) ? (DateTime?)null : DateTime.Parse(reader.GetString(2))
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving group-mentors by SubGroupId: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing assigned dates: " + ex.Message, ex);
            }
            return groupMentors;
        }

        public List<GroupMentor> GetGroupMentorsByMentorId(int mentorId)
        {
            var groupMentors = new List<GroupMentor>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT SubGroupId, MentorId, AssignedDate FROM GroupMentors WHERE MentorId = @MentorId ORDER BY SubGroupId ASC";
                    cmd.Parameters.AddWithValue("@MentorId", mentorId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            groupMentors.Add(new GroupMentor
                            {
                                SubGroupId = reader.GetInt32(0),
                                MentorId = reader.GetInt32(1),
                                AssignedDate = reader.IsDBNull(2) ? (DateTime?)null : DateTime.Parse(reader.GetString(2))
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving group-mentors by MentorId: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing assigned dates: " + ex.Message, ex);
            }
            return groupMentors;
        }

        public List<GroupMentor> GetAllGroupMentors()
        {
            var groupMentors = new List<GroupMentor>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT SubGroupId, MentorId, AssignedDate FROM GroupMentors ORDER BY SubGroupId ASC, MentorId ASC";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            groupMentors.Add(new GroupMentor
                            {
                                SubGroupId = reader.GetInt32(0),
                                MentorId = reader.GetInt32(1),
                                AssignedDate = reader.IsDBNull(2) ? (DateTime?)null : DateTime.Parse(reader.GetString(2))
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving all group-mentor relationships: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing assigned dates: " + ex.Message, ex);
            }
            return groupMentors;
        }
    }
}
