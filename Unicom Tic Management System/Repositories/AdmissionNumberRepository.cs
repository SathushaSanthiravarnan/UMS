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
    internal class AdmissionNumberRepository : IAdmissionNumberRepository
    {
        public void AddAdmissionNumber(AdmissionNumber admissionNumber)
        {
            try
            {
                if (admissionNumber == null)
                    throw new ArgumentNullException(nameof(admissionNumber));

                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        INSERT INTO AdmissionNumbers (AdmissionNumberText, StudentId)
                        VALUES (@AdmissionNumberText, @StudentId)";
                    cmd.Parameters.AddWithValue("@AdmissionNumberText", admissionNumber.AdmissionNumberText);
                    cmd.Parameters.AddWithValue("@StudentId", admissionNumber.StudentId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while adding admission number: " + ex.Message, ex);
            }
        }

        public void UpdateAdmissionNumber(AdmissionNumber admissionNumber)
        {
            try
            {
                if (admissionNumber == null)
                    throw new ArgumentNullException(nameof(admissionNumber));

                // Assuming AdmissionNumberText is unique and used for updates
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        UPDATE AdmissionNumbers
                        SET StudentId = @StudentId
                        WHERE AdmissionNumberText = @AdmissionNumberText";
                    cmd.Parameters.AddWithValue("@AdmissionNumberText", admissionNumber.AdmissionNumberText);
                    cmd.Parameters.AddWithValue("@StudentId", admissionNumber.StudentId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while updating admission number: " + ex.Message, ex);
            }
        }

        public void DeleteAdmissionNumber(string admissionNumberText)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "DELETE FROM AdmissionNumbers WHERE AdmissionNumberText = @AdmissionNumberText";
                    cmd.Parameters.AddWithValue("@AdmissionNumberText", admissionNumberText);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while deleting admission number: " + ex.Message, ex);
            }
        }

        public AdmissionNumber GetAdmissionNumberByText(string admissionNumberText)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT AdmissionNumberText, StudentId FROM AdmissionNumbers WHERE AdmissionNumberText = @AdmissionNumberText";
                    cmd.Parameters.AddWithValue("@AdmissionNumberText", admissionNumberText);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new AdmissionNumber
                            {
                                AdmissionNumberText = reader.GetString(0),
                                StudentId = reader.GetInt32(1)
                            };
                        }
                        return null;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving admission number by text: " + ex.Message, ex);
            }
        }

        public AdmissionNumber GetAdmissionNumberByStudentId(int studentId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT AdmissionNumberText, StudentId FROM AdmissionNumbers WHERE StudentId = @StudentId";
                    cmd.Parameters.AddWithValue("@StudentId", studentId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new AdmissionNumber
                            {
                                AdmissionNumberText = reader.GetString(0),
                                StudentId = reader.GetInt32(1)
                            };
                        }
                        return null;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving admission number by student ID: " + ex.Message, ex);
            }
        }

        public List<AdmissionNumber> GetAllAdmissionNumbers()
        {
            var admissionNumbers = new List<AdmissionNumber>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT AdmissionNumberText, StudentId FROM AdmissionNumbers";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            admissionNumbers.Add(new AdmissionNumber
                            {
                                AdmissionNumberText = reader.GetString(0),
                                StudentId = reader.GetInt32(1)
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving all admission numbers: " + ex.Message, ex);
            }
            return admissionNumbers;
        }
    }
}
