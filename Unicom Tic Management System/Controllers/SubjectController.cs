using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unicom_Tic_Management_System.Models.DTOs.AcademicDTOs;
using Unicom_Tic_Management_System.Services.Interfaces;

namespace Unicom_Tic_Management_System.Controllers
{
    internal class SubjectController
    {
        private readonly ISubjectService _service;

        public SubjectController(ISubjectService service)
        {
            _service = service;
        }

        public void AddSubject(SubjectDto subjectDto)
        {
            try
            {
                _service.AddSubject(subjectDto);
                MessageBox.Show("Subject added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding subject: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void UpdateSubject(SubjectDto subjectDto)
        {
            try
            {
                _service.UpdateSubject(subjectDto);
                MessageBox.Show("Subject updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating subject: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void DeleteSubject(int subjectId)
        {
            try
            {
                _service.DeleteSubject(subjectId);
                MessageBox.Show("Subject deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting subject: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public SubjectDto GetSubjectById(int subjectId)
        {
            try
            {
                return _service.GetSubjectById(subjectId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving subject by ID: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public SubjectDto GetSubjectByName(string subjectName)
        {
            try
            {
                return _service.GetSubjectByName(subjectName);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving subject by name: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public List<SubjectDto> GetAllSubjects()
        {
            try
            {
                return _service.GetAllSubjects();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving all subjects: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<SubjectDto>();
            }
        }

        public List<SubjectDto> GetSubjectsByCourse(int courseId)
        {
            try
            {
                return _service.GetSubjectsByCourse(courseId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving subjects by course: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<SubjectDto>();
            }
        }
    }
}
