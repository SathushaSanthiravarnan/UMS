using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Datas;
using Unicom_Tic_Management_System.Models;
using Unicom_Tic_Management_System.Models.DTOs.UserDtos;
using Unicom_Tic_Management_System.Repositories.Interfaces;

namespace Unicom_Tic_Management_System.Repositories
{
    internal class NICDetailsRepository : INicDetailsRepository
    {
        public void AddNICDetail(NicDetail nicDetail)
        {
            try
            {
                if (nicDetail == null)
                    throw new ArgumentNullException(nameof(nicDetail));

                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "INSERT INTO NICDetails (NIC, IsUsed) VALUES (@NIC, @IsUsed)";
                    cmd.Parameters.AddWithValue("@NIC", nicDetail.Nic);
                    cmd.Parameters.AddWithValue("@IsUsed", nicDetail.IsUsed ? 1 : 0);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while adding NIC detail: " + ex.Message, ex);
            }
        }

        public void UpdateNICDetail(NicDetail nicDetail)
        {
            try
            {
                if (nicDetail == null)
                    throw new ArgumentNullException(nameof(nicDetail));

                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "UPDATE NICDetails SET IsUsed = @IsUsed WHERE NIC = @NIC";
                    cmd.Parameters.AddWithValue("@NIC", nicDetail.Nic);
                    cmd.Parameters.AddWithValue("@IsUsed", nicDetail.IsUsed ? 1 : 0);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while updating NIC detail: " + ex.Message, ex);
            }
        }

        public void DeleteNICDetail(string nic)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "DELETE FROM NICDetails WHERE NIC = @NIC";
                    cmd.Parameters.AddWithValue("@NIC", nic);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while deleting NIC detail: " + ex.Message, ex);
            }
        }

        public NicDetail GetNICDetailByNIC(string nic)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT NIC, IsUsed FROM NICDetails WHERE NIC = @NIC";
                    cmd.Parameters.AddWithValue("@NIC", nic);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new NicDetail
                            {
                                Nic = reader.GetString(0),
                                IsUsed = reader.GetBoolean(1)
                            };
                        }
                        return null;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving NIC detail: " + ex.Message, ex);
            }
        }

        public List<NicDetail> GetAllNICDetails()
        {
            var nicDetails = new List<NicDetail>();
            try
            {
                var connection = DatabaseManager.GetConnection();

                using (var cmd = connection.CreateCommand()) // ✅ Correct usage
                {
                    cmd.CommandText = "SELECT NIC, IsUsed FROM NICDetails";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            nicDetails.Add(new NicDetail
                            {
                                Nic = reader.GetString(0),
                                IsUsed = reader.GetBoolean(1)
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving NIC details: " + ex.Message, ex);
            }

            return nicDetails;
        }

        public void AddNICDetail(NicDetailDTO nicDetailDTO)
        {
            throw new NotImplementedException();
        }

        public void MarkAsUsed(string nic)
        {
            using (var connection = DatabaseManager.GetConnection())
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = "UPDATE NICDetails SET IsUsed = 1 WHERE NIC = @NIC";
                cmd.Parameters.AddWithValue("@NIC", nic);
                cmd.ExecuteNonQuery();
            }
        }

        public bool NicExists(string nic)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                   
                    cmd.CommandText = "SELECT COUNT(*) FROM NICDetails WHERE NIC = @NIC";
                    cmd.Parameters.AddWithValue("@NIC", nic);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
            catch (SQLiteException ex)
            {
                
                throw new Exception("Database error while checking NIC existence: " + ex.Message, ex);
            }
        }
    }
}


