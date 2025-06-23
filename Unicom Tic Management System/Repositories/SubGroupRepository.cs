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
    internal class SubGroupRepository : ISubGroupRepository
    {
        public void AddSubGroup(SubGroup subGroup)
        {
            try
            {
                if (subGroup == null)
                    throw new ArgumentNullException(nameof(subGroup));

                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        INSERT INTO SubGroups (MainGroupId, SubGroupName, Description)
                        VALUES (@MainGroupId, @SubGroupName, @Description)";
                    cmd.Parameters.AddWithValue("@MainGroupId", subGroup.MainGroupId);
                    cmd.Parameters.AddWithValue("@SubGroupName", subGroup.SubGroupName);
                    cmd.Parameters.AddWithValue("@Description", subGroup.Description);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                if (ex.Message.Contains("UNIQUE constraint failed"))
                {
                    throw new Exception($"A sub-group with the name '{subGroup.SubGroupName}' already exists for Main Group ID {subGroup.MainGroupId}.", ex);
                }
                throw new Exception("Database error while adding sub-group: " + ex.Message, ex);
            }
        }

        public void UpdateSubGroup(SubGroup subGroup)
        {
            try
            {
                if (subGroup == null)
                    throw new ArgumentNullException(nameof(subGroup));

                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        UPDATE SubGroups
                        SET MainGroupId = @MainGroupId, SubGroupName = @SubGroupName, Description = @Description
                        WHERE SubGroupId = @SubGroupId";
                    cmd.Parameters.AddWithValue("@SubGroupId", subGroup.SubGroupId);
                    cmd.Parameters.AddWithValue("@MainGroupId", subGroup.MainGroupId);
                    cmd.Parameters.AddWithValue("@SubGroupName", subGroup.SubGroupName);
                    cmd.Parameters.AddWithValue("@Description", subGroup.Description);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                if (ex.Message.Contains("UNIQUE constraint failed"))
                {
                    throw new Exception($"A sub-group with the name '{subGroup.SubGroupName}' already exists for Main Group ID {subGroup.MainGroupId}.", ex);
                }
                throw new Exception("Database error while updating sub-group: " + ex.Message, ex);
            }
        }

        public void DeleteSubGroup(int subGroupId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "DELETE FROM SubGroups WHERE SubGroupId = @SubGroupId";
                    cmd.Parameters.AddWithValue("@SubGroupId", subGroupId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while deleting sub-group: " + ex.Message, ex);
            }
        }

        public SubGroup GetSubGroupById(int subGroupId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT SubGroupId, MainGroupId, SubGroupName, Description FROM SubGroups WHERE SubGroupId = @SubGroupId";
                    cmd.Parameters.AddWithValue("@SubGroupId", subGroupId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new SubGroup
                            {
                                SubGroupId = reader.GetInt32(0),
                                MainGroupId = reader.GetInt32(1),
                                SubGroupName = reader.GetString(2),
                                Description = reader.GetString(3)
                            };
                        }
                        return null;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving sub-group by ID: " + ex.Message, ex);
            }
        }

        public SubGroup GetSubGroupByNameAndMainGroup(string subGroupName, int mainGroupId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT SubGroupId, MainGroupId, SubGroupName, Description FROM SubGroups WHERE SubGroupName = @SubGroupName AND MainGroupId = @MainGroupId";
                    cmd.Parameters.AddWithValue("@SubGroupName", subGroupName);
                    cmd.Parameters.AddWithValue("@MainGroupId", mainGroupId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new SubGroup
                            {
                                SubGroupId = reader.GetInt32(0),
                                MainGroupId = reader.GetInt32(1),
                                SubGroupName = reader.GetString(2),
                                Description = reader.GetString(3)
                            };
                        }
                        return null;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving sub-group by name and main group: " + ex.Message, ex);
            }
        }

        public List<SubGroup> GetSubGroupsByMainGroupId(int mainGroupId)
        {
            var subGroups = new List<SubGroup>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT SubGroupId, MainGroupId, SubGroupName, Description FROM SubGroups WHERE MainGroupId = @MainGroupId ORDER BY SubGroupName ASC";
                    cmd.Parameters.AddWithValue("@MainGroupId", mainGroupId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            subGroups.Add(new SubGroup
                            {
                                SubGroupId = reader.GetInt32(0),
                                MainGroupId = reader.GetInt32(1),
                                SubGroupName = reader.GetString(2),
                                Description = reader.GetString(3)
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving sub-groups by main group ID: " + ex.Message, ex);
            }
            return subGroups;
        }

        public List<SubGroup> GetAllSubGroups()
        {
            var subGroups = new List<SubGroup>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT SubGroupId, MainGroupId, SubGroupName, Description FROM SubGroups ORDER BY MainGroupId ASC, SubGroupName ASC";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            subGroups.Add(new SubGroup
                            {
                                SubGroupId = reader.GetInt32(0),
                                MainGroupId = reader.GetInt32(1),
                                SubGroupName = reader.GetString(2),
                                Description = reader.GetString(3)
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving all sub-groups: " + ex.Message, ex);
            }
            return subGroups;
        }
    }
}

