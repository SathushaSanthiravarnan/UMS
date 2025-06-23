using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unicom_Tic_Management_System.Models.DTOs.Grouping_MentoringDTOs;
using Unicom_Tic_Management_System.Models.Enums;
using Unicom_Tic_Management_System.Services.Interfaces;

namespace Unicom_Tic_Management_System.Controllers
{
    internal class StudentController
    {
        private readonly IStudentService _service;

        public StudentController(IStudentService service)
        {
            _service = service;
        }

        public void AddStudent(StudentDto dto)
        {
            try
            {
                _service.AddStudent(dto);
                MessageBox.Show("Student added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding student: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void UpdateStudent(StudentDto dto)
        {
            try
            {
                _service.UpdateStudent(dto);
                MessageBox.Show("Student updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating student: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void DeleteStudent(int id)
        {
            try
            {
                _service.DeleteStudent(id);
                MessageBox.Show("Student deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting student: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public StudentDto GetStudentById(int id)
        {
            try
            {
                return _service.GetStudentById(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching student: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public StudentDto GetStudentByNic(string nic)
        {
            try
            {
                return _service.GetStudentByNic(nic);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching student by NIC: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public StudentDto GetStudentByAdmissionNumber(string admissionNo)
        {
            try
            {
                return _service.GetStudentByAdmissionNumber(admissionNo);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching student by Admission Number: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public StudentDto GetStudentByEmail(string email)
        {
            try
            {
                return _service.GetStudentByEmail(email);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching student by Email: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public StudentDto GetStudentByUserId(int userId)
        {
            try
            {
                return _service.GetStudentByUserId(userId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching student by User ID: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public List<StudentDto> GetAllStudents()
        {
            try
            {
                return _service.GetAllStudents();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching students: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<StudentDto>();
            }
        }

        public List<StudentDto> GetStudentsByCourseId(int courseId)
        {
            try
            {
                return _service.GetStudentsByCourseId(courseId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching students by course: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<StudentDto>();
            }
        }

        public List<StudentDto> GetStudentsByMainGroupId(int mainGroupId)
        {
            try
            {
                return _service.GetStudentsByMainGroupId(mainGroupId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching students by main group: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<StudentDto>();
            }
        }

        public List<StudentDto> GetStudentsBySubGroupId(int subGroupId)
        {
            try
            {
                return _service.GetStudentsBySubGroupId(subGroupId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching students by sub group: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<StudentDto>();
            }
        }

        public List<StudentDto> GetStudentsByGender(GenderType gender)
        {
            try
            {
                return _service.GetStudentsByGender(gender);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching students by gender: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<StudentDto>();
            }
        }
    }
}

