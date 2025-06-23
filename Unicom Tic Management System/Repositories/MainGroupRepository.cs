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
    internal class MainGroupRepository : IMainGroupRepository
    {
        public void AddMainGroup(MainGroup mainGroup)
        {
            try
            {
                if (mainGroup == null)
                    throw new ArgumentNullException(nameof(mainGroup));

                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        INSERT INTO MainGroups (GroupCode, Description)
                        VALUES (@GroupCode, @Description)";
                    cmd.Parameters.AddWithValue("@GroupCode", mainGroup.GroupCode);
                    cmd.Parameters.AddWithValue("@Description", mainGroup.Description);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while adding main group: " + ex.Message, ex);
            }
        }

        public void UpdateMainGroup(MainGroup mainGroup)
        {
            try
            {
                if (mainGroup == null)
                    throw new ArgumentNullException(nameof(mainGroup));

                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        UPDATE MainGroups
                        SET GroupCode = @GroupCode, Description = @Description
                        WHERE MainGroupId = @MainGroupId";
                    cmd.Parameters.AddWithValue("@MainGroupId", mainGroup.MainGroupId);
                    cmd.Parameters.AddWithValue("@GroupCode", mainGroup.GroupCode);
                    cmd.Parameters.AddWithValue("@Description", mainGroup.Description);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while updating main group: " + ex.Message, ex);
            }
        }

        public void DeleteMainGroup(int mainGroupId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "DELETE FROM MainGroups WHERE MainGroupId = @MainGroupId";
                    cmd.Parameters.AddWithValue("@MainGroupId", mainGroupId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while deleting main group: " + ex.Message, ex);
            }
        }

        public MainGroup GetMainGroupById(int mainGroupId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT MainGroupId, GroupCode, Description FROM MainGroups WHERE MainGroupId = @MainGroupId";
                    cmd.Parameters.AddWithValue("@MainGroupId", mainGroupId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new MainGroup
                            {
                                MainGroupId = reader.GetInt32(0),
                                GroupCode = reader.GetString(1),
                                Description = reader.GetString(2)
                            };
                        }
                        return null;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving main group by ID: " + ex.Message, ex);
            }
        }

        public MainGroup GetMainGroupByGroupCode(string groupCode)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT MainGroupId, GroupCode, Description FROM MainGroups WHERE GroupCode = @GroupCode";
                    cmd.Parameters.AddWithValue("@GroupCode", groupCode);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new MainGroup
                            {
                                MainGroupId = reader.GetInt32(0),
                                GroupCode = reader.GetString(1),
                                Description = reader.GetString(2)
                            };
                        }
                        return null;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving main group by group code: " + ex.Message, ex);
            }
        }

        public List<MainGroup> GetAllMainGroups()
        {
            var mainGroups = new List<MainGroup>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT MainGroupId, GroupCode, Description FROM MainGroups ORDER BY GroupCode ASC";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            mainGroups.Add(new MainGroup
                            {
                                MainGroupId = reader.GetInt32(0),
                                GroupCode = reader.GetString(1),
                                Description = reader.GetString(2)
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving all main groups: " + ex.Message, ex);
            }
            return mainGroups;
        }
    }
}
