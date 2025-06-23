using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Datas;
using Unicom_Tic_Management_System.Models;
using Unicom_Tic_Management_System.Models.Enums;
using Unicom_Tic_Management_System.Repositories.Interfaces;

namespace Unicom_Tic_Management_System.Repositories
{
    internal class StudentRepository : IStudentRepository
    {
        public void AddStudent(Student student)
        {
            try
            {
                if (student == null)
                    throw new ArgumentNullException(nameof(student));

                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        INSERT INTO Students (Name, Nic, Address, ContactNo, Email, DateOfBirth, Gender, EnrollmentDate, CourseId, UserId, MainGroupId, SubGroupId, CreatedAt, UpdatedAt, AdmissionNumber)
                        VALUES (@Name, @Nic, @Address, @ContactNo, @Email, @DateOfBirth, @Gender, @EnrollmentDate, @CourseId, @UserId, @MainGroupId, @SubGroupId, @CreatedAt, @UpdatedAt, @AdmissionNumber)";

                    cmd.Parameters.AddWithValue("@Name", student.Name);
                    cmd.Parameters.AddWithValue("@Nic", student.Nic);
                    cmd.Parameters.AddWithValue("@Address", student.Address);
                    cmd.Parameters.AddWithValue("@ContactNo", student.ContactNo);
                    cmd.Parameters.AddWithValue("@Email", student.Email);
                    cmd.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth.HasValue ? (object)student.DateOfBirth.Value.ToString("yyyy-MM-dd HH:mm:ss") : DBNull.Value); // Nullable DateTime
                    cmd.Parameters.AddWithValue("@Gender", (int)student.Gender); // Store enum as integer
                    cmd.Parameters.AddWithValue("@EnrollmentDate", student.EnrollmentDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@CourseId", student.CourseId);
                    cmd.Parameters.AddWithValue("@UserId", student.UserId);
                    cmd.Parameters.AddWithValue("@MainGroupId", student.MainGroupId);
                    cmd.Parameters.AddWithValue("@SubGroupId", student.SubGroupId);
                    cmd.Parameters.AddWithValue("@CreatedAt", student.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@UpdatedAt", student.UpdatedAt.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@AdmissionNumber", student.AdmissionNumber);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                if (ex.Message.Contains("UNIQUE constraint failed"))
                {
                    throw new Exception("A student with the same NIC, Admission Number, Email, or User ID already exists.", ex);
                }
                throw new Exception("Database error while adding student: " + ex.Message, ex);
            }
        }

        public void UpdateStudent(Student student)
        {
            try
            {
                if (student == null)
                    throw new ArgumentNullException(nameof(student));

                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                        UPDATE Students
                        SET Name = @Name, Nic = @Nic, Address = @Address, ContactNo = @ContactNo,
                            Email = @Email, DateOfBirth = @DateOfBirth, Gender = @Gender,
                            EnrollmentDate = @EnrollmentDate, CourseId = @CourseId, UserId = @UserId,
                            MainGroupId = @MainGroupId, SubGroupId = @SubGroupId, UpdatedAt = @UpdatedAt,
                            AdmissionNumber = @AdmissionNumber
                        WHERE StudentId = @StudentId";

                    cmd.Parameters.AddWithValue("@StudentId", student.StudentId);
                    cmd.Parameters.AddWithValue("@Name", student.Name);
                    cmd.Parameters.AddWithValue("@Nic", student.Nic);
                    cmd.Parameters.AddWithValue("@Address", student.Address);
                    cmd.Parameters.AddWithValue("@ContactNo", student.ContactNo);
                    cmd.Parameters.AddWithValue("@Email", student.Email);
                    cmd.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth.HasValue ? (object)student.DateOfBirth.Value.ToString("yyyy-MM-dd HH:mm:ss") : DBNull.Value);
                    cmd.Parameters.AddWithValue("@Gender", (int)student.Gender);
                    cmd.Parameters.AddWithValue("@EnrollmentDate", student.EnrollmentDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@CourseId", student.CourseId);
                    cmd.Parameters.AddWithValue("@UserId", student.UserId);
                    cmd.Parameters.AddWithValue("@MainGroupId", student.MainGroupId);
                    cmd.Parameters.AddWithValue("@SubGroupId", student.SubGroupId);
                    cmd.Parameters.AddWithValue("@UpdatedAt", student.UpdatedAt.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@AdmissionNumber", student.AdmissionNumber);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                if (ex.Message.Contains("UNIQUE constraint failed"))
                {
                    throw new Exception("A student with the same NIC, Admission Number, Email, or User ID already exists.", ex);
                }
                throw new Exception("Database error while updating student: " + ex.Message, ex);
            }
        }

        public void DeleteStudent(int studentId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "DELETE FROM Students WHERE StudentId = @StudentId";
                    cmd.Parameters.AddWithValue("@StudentId", studentId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while deleting student: " + ex.Message, ex);
            }
        }

        public Student GetStudentById(int studentId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT StudentId, Name, Nic, Address, ContactNo, Email, DateOfBirth, Gender, EnrollmentDate, CourseId, UserId, MainGroupId, SubGroupId, CreatedAt, UpdatedAt, AdmissionNumber FROM Students WHERE StudentId = @StudentId";
                    cmd.Parameters.AddWithValue("@StudentId", studentId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Student
                            {
                                StudentId = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Nic = reader.GetString(2),
                                Address = reader.GetString(3),
                                ContactNo = reader.GetString(4),
                                Email = reader.GetString(5),
                                DateOfBirth = reader.IsDBNull(6) ? (DateTime?)null : DateTime.Parse(reader.GetString(6)),
                                Gender = (GenderType)reader.GetInt32(7),
                                EnrollmentDate = DateTime.Parse(reader.GetString(8)),
                                CourseId = reader.GetInt32(9),
                                UserId = reader.GetInt32(10),
                                MainGroupId = reader.GetInt32(11),
                                SubGroupId = reader.GetInt32(12),
                                CreatedAt = DateTime.Parse(reader.GetString(13)),
                                UpdatedAt = DateTime.Parse(reader.GetString(14)),
                                AdmissionNumber = reader.GetString(15)
                            };
                        }
                        return null;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving student by ID: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing student dates: " + ex.Message, ex);
            }
        }

        public Student GetStudentByNic(string nic)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT StudentId, Name, Nic, Address, ContactNo, Email, DateOfBirth, Gender, EnrollmentDate, CourseId, UserId, MainGroupId, SubGroupId, CreatedAt, UpdatedAt, AdmissionNumber FROM Students WHERE Nic = @Nic";
                    cmd.Parameters.AddWithValue("@Nic", nic);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Student
                            {
                                StudentId = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Nic = reader.GetString(2),
                                Address = reader.GetString(3),
                                ContactNo = reader.GetString(4),
                                Email = reader.GetString(5),
                                DateOfBirth = reader.IsDBNull(6) ? (DateTime?)null : DateTime.Parse(reader.GetString(6)),
                                Gender = (GenderType)reader.GetInt32(7),
                                EnrollmentDate = DateTime.Parse(reader.GetString(8)),
                                CourseId = reader.GetInt32(9),
                                UserId = reader.GetInt32(10),
                                MainGroupId = reader.GetInt32(11),
                                SubGroupId = reader.GetInt32(12),
                                CreatedAt = DateTime.Parse(reader.GetString(13)),
                                UpdatedAt = DateTime.Parse(reader.GetString(14)),
                                AdmissionNumber = reader.GetString(15)
                            };
                        }
                        return null;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving student by NIC: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing student dates: " + ex.Message, ex);
            }
        }

        public Student GetStudentByAdmissionNumber(string admissionNumber)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT StudentId, Name, Nic, Address, ContactNo, Email, DateOfBirth, Gender, EnrollmentDate, CourseId, UserId, MainGroupId, SubGroupId, CreatedAt, UpdatedAt, AdmissionNumber FROM Students WHERE AdmissionNumber = @AdmissionNumber";
                    cmd.Parameters.AddWithValue("@AdmissionNumber", admissionNumber);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Student
                            {
                                StudentId = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Nic = reader.GetString(2),
                                Address = reader.GetString(3),
                                ContactNo = reader.GetString(4),
                                Email = reader.GetString(5),
                                DateOfBirth = reader.IsDBNull(6) ? (DateTime?)null : DateTime.Parse(reader.GetString(6)),
                                Gender = (GenderType)reader.GetInt32(7),
                                EnrollmentDate = DateTime.Parse(reader.GetString(8)),
                                CourseId = reader.GetInt32(9),
                                UserId = reader.GetInt32(10),
                                MainGroupId = reader.GetInt32(11),
                                SubGroupId = reader.GetInt32(12),
                                CreatedAt = DateTime.Parse(reader.GetString(13)),
                                UpdatedAt = DateTime.Parse(reader.GetString(14)),
                                AdmissionNumber = reader.GetString(15)
                            };
                        }
                        return null;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving student by Admission Number: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing student dates: " + ex.Message, ex);
            }
        }

        public Student GetStudentByEmail(string email)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT StudentId, Name, Nic, Address, ContactNo, Email, DateOfBirth, Gender, EnrollmentDate, CourseId, UserId, MainGroupId, SubGroupId, CreatedAt, UpdatedAt, AdmissionNumber FROM Students WHERE Email = @Email";
                    cmd.Parameters.AddWithValue("@Email", email);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Student
                            {
                                StudentId = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Nic = reader.GetString(2),
                                Address = reader.GetString(3),
                                ContactNo = reader.GetString(4),
                                Email = reader.GetString(5),
                                DateOfBirth = reader.IsDBNull(6) ? (DateTime?)null : DateTime.Parse(reader.GetString(6)),
                                Gender = (GenderType)reader.GetInt32(7),
                                EnrollmentDate = DateTime.Parse(reader.GetString(8)),
                                CourseId = reader.GetInt32(9),
                                UserId = reader.GetInt32(10),
                                MainGroupId = reader.GetInt32(11),
                                SubGroupId = reader.GetInt32(12),
                                CreatedAt = DateTime.Parse(reader.GetString(13)),
                                UpdatedAt = DateTime.Parse(reader.GetString(14)),
                                AdmissionNumber = reader.GetString(15)
                            };
                        }
                        return null;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving student by email: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing student dates: " + ex.Message, ex);
            }
        }

        public Student GetStudentByUserId(int userId)
        {
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT StudentId, Name, Nic, Address, ContactNo, Email, DateOfBirth, Gender, EnrollmentDate, CourseId, UserId, MainGroupId, SubGroupId, CreatedAt, UpdatedAt, AdmissionNumber FROM Students WHERE UserId = @UserId";
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Student
                            {
                                StudentId = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Nic = reader.GetString(2),
                                Address = reader.GetString(3),
                                ContactNo = reader.GetString(4),
                                Email = reader.GetString(5),
                                DateOfBirth = reader.IsDBNull(6) ? (DateTime?)null : DateTime.Parse(reader.GetString(6)),
                                Gender = (GenderType)reader.GetInt32(7),
                                EnrollmentDate = DateTime.Parse(reader.GetString(8)),
                                CourseId = reader.GetInt32(9),
                                UserId = reader.GetInt32(10),
                                MainGroupId = reader.GetInt32(11),
                                SubGroupId = reader.GetInt32(12),
                                CreatedAt = DateTime.Parse(reader.GetString(13)),
                                UpdatedAt = DateTime.Parse(reader.GetString(14)),
                                AdmissionNumber = reader.GetString(15)
                            };
                        }
                        return null;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving student by User ID: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing student dates: " + ex.Message, ex);
            }
        }

        public List<Student> GetStudentsByCourseId(int courseId)
        {
            var students = new List<Student>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT StudentId, Name, Nic, Address, ContactNo, Email, DateOfBirth, Gender, EnrollmentDate, CourseId, UserId, MainGroupId, SubGroupId, CreatedAt, UpdatedAt, AdmissionNumber FROM Students WHERE CourseId = @CourseId ORDER BY Name ASC";
                    cmd.Parameters.AddWithValue("@CourseId", courseId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            students.Add(new Student
                            {
                                StudentId = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Nic = reader.GetString(2),
                                Address = reader.GetString(3),
                                ContactNo = reader.GetString(4),
                                Email = reader.GetString(5),
                                DateOfBirth = reader.IsDBNull(6) ? (DateTime?)null : DateTime.Parse(reader.GetString(6)),
                                Gender = (GenderType)reader.GetInt32(7),
                                EnrollmentDate = DateTime.Parse(reader.GetString(8)),
                                CourseId = reader.GetInt32(9),
                                UserId = reader.GetInt32(10),
                                MainGroupId = reader.GetInt32(11),
                                SubGroupId = reader.GetInt32(12),
                                CreatedAt = DateTime.Parse(reader.GetString(13)),
                                UpdatedAt = DateTime.Parse(reader.GetString(14)),
                                AdmissionNumber = reader.GetString(15)
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving students by Course ID: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing student dates: " + ex.Message, ex);
            }
            return students;
        }

        public List<Student> GetStudentsByMainGroupId(int mainGroupId)
        {
            var students = new List<Student>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT StudentId, Name, Nic, Address, ContactNo, Email, DateOfBirth, Gender, EnrollmentDate, CourseId, UserId, MainGroupId, SubGroupId, CreatedAt, UpdatedAt, AdmissionNumber FROM Students WHERE MainGroupId = @MainGroupId ORDER BY Name ASC";
                    cmd.Parameters.AddWithValue("@MainGroupId", mainGroupId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            students.Add(new Student
                            {
                                StudentId = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Nic = reader.GetString(2),
                                Address = reader.GetString(3),
                                ContactNo = reader.GetString(4),
                                Email = reader.GetString(5),
                                DateOfBirth = reader.IsDBNull(6) ? (DateTime?)null : DateTime.Parse(reader.GetString(6)),
                                Gender = (GenderType)reader.GetInt32(7),
                                EnrollmentDate = DateTime.Parse(reader.GetString(8)),
                                CourseId = reader.GetInt32(9),
                                UserId = reader.GetInt32(10),
                                MainGroupId = reader.GetInt32(11),
                                SubGroupId = reader.GetInt32(12),
                                CreatedAt = DateTime.Parse(reader.GetString(13)),
                                UpdatedAt = DateTime.Parse(reader.GetString(14)),
                                AdmissionNumber = reader.GetString(15)
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving students by Main Group ID: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing student dates: " + ex.Message, ex);
            }
            return students;
        }

        public List<Student> GetStudentsBySubGroupId(int subGroupId)
        {
            var students = new List<Student>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT StudentId, Name, Nic, Address, ContactNo, Email, DateOfBirth, Gender, EnrollmentDate, CourseId, UserId, MainGroupId, SubGroupId, CreatedAt, UpdatedAt, AdmissionNumber FROM Students WHERE SubGroupId = @SubGroupId ORDER BY Name ASC";
                    cmd.Parameters.AddWithValue("@SubGroupId", subGroupId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            students.Add(new Student
                            {
                                StudentId = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Nic = reader.GetString(2),
                                Address = reader.GetString(3),
                                ContactNo = reader.GetString(4),
                                Email = reader.GetString(5),
                                DateOfBirth = reader.IsDBNull(6) ? (DateTime?)null : DateTime.Parse(reader.GetString(6)),
                                Gender = (GenderType)reader.GetInt32(7),
                                EnrollmentDate = DateTime.Parse(reader.GetString(8)),
                                CourseId = reader.GetInt32(9),
                                UserId = reader.GetInt32(10),
                                MainGroupId = reader.GetInt32(11),
                                SubGroupId = reader.GetInt32(12),
                                CreatedAt = DateTime.Parse(reader.GetString(13)),
                                UpdatedAt = DateTime.Parse(reader.GetString(14)),
                                AdmissionNumber = reader.GetString(15)
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving students by Sub Group ID: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing student dates: " + ex.Message, ex);
            }
            return students;
        }

        public List<Student> GetStudentsByGender(GenderType gender)
        {
            var students = new List<Student>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT StudentId, Name, Nic, Address, ContactNo, Email, DateOfBirth, Gender, EnrollmentDate, CourseId, UserId, MainGroupId, SubGroupId, CreatedAt, UpdatedAt, AdmissionNumber FROM Students WHERE Gender = @Gender ORDER BY Name ASC";
                    cmd.Parameters.AddWithValue("@Gender", (int)gender);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            students.Add(new Student
                            {
                                StudentId = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Nic = reader.GetString(2),
                                Address = reader.GetString(3),
                                ContactNo = reader.GetString(4),
                                Email = reader.GetString(5),
                                DateOfBirth = reader.IsDBNull(6) ? (DateTime?)null : DateTime.Parse(reader.GetString(6)),
                                Gender = (GenderType)reader.GetInt32(7),
                                EnrollmentDate = DateTime.Parse(reader.GetString(8)),
                                CourseId = reader.GetInt32(9),
                                UserId = reader.GetInt32(10),
                                MainGroupId = reader.GetInt32(11),
                                SubGroupId = reader.GetInt32(12),
                                CreatedAt = DateTime.Parse(reader.GetString(13)),
                                UpdatedAt = DateTime.Parse(reader.GetString(14)),
                                AdmissionNumber = reader.GetString(15)
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving students by gender: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing student dates: " + ex.Message, ex);
            }
            return students;
        }

        public List<Student> GetAllStudents()
        {
            var students = new List<Student>();
            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT StudentId, Name, Nic, Address, ContactNo, Email, DateOfBirth, Gender, EnrollmentDate, CourseId, UserId, MainGroupId, SubGroupId, CreatedAt, UpdatedAt, AdmissionNumber FROM Students ORDER BY Name ASC";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            students.Add(new Student
                            {
                                StudentId = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Nic = reader.GetString(2),
                                Address = reader.GetString(3),
                                ContactNo = reader.GetString(4),
                                Email = reader.GetString(5),
                                DateOfBirth = reader.IsDBNull(6) ? (DateTime?)null : DateTime.Parse(reader.GetString(6)),
                                Gender = (GenderType)reader.GetInt32(7),
                                EnrollmentDate = DateTime.Parse(reader.GetString(8)),
                                CourseId = reader.GetInt32(9),
                                UserId = reader.GetInt32(10),
                                MainGroupId = reader.GetInt32(11),
                                SubGroupId = reader.GetInt32(12),
                                CreatedAt = DateTime.Parse(reader.GetString(13)),
                                UpdatedAt = DateTime.Parse(reader.GetString(14)),
                                AdmissionNumber = reader.GetString(15)
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Database error while retrieving all students: " + ex.Message, ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Data format error while parsing student dates: " + ex.Message, ex);
            }
            return students;
        }
    }
}
