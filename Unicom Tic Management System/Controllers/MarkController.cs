using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unicom_Tic_Management_System.Models.DTOs.AcademicOperationsDTOs;
using Unicom_Tic_Management_System.Services.Interfaces;

namespace Unicom_Tic_Management_System.Controllers
{
    internal class MarkController
    {
        private readonly IMarkService _markService;

       
        public MarkController(IMarkService markService)
        {
            _markService = markService ?? throw new ArgumentNullException(nameof(markService));
        }

       

        public void AddMark(MarkDto markDto)
        {
            try
            {
                _markService.AddMark(markDto);
                MessageBox.Show("Mark added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (ArgumentException ex) 
            {
                MessageBox.Show($"Validation Error: {ex.Message}", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding mark: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
               
            }
        }

        public void UpdateMark(MarkDto markDto)
        {
            try
            {
                _markService.UpdateMark(markDto);
                MessageBox.Show("Mark updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show($"Validation Error: {ex.Message}", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating mark: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void DeleteMark(int markId)
        {
            try
            {
                
                DialogResult confirmResult = MessageBox.Show(
                    "Are you sure you want to delete this mark?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmResult == DialogResult.Yes)
                {
                    _markService.DeleteMark(markId);
                    MessageBox.Show("Mark deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Deletion cancelled.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show($"Invalid ID: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting mark: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        public MarkDisplayDto GetMarkDisplayById(int markId)
        {
            try
            {
                return _markService.GetMarkDisplayById(markId);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show($"Invalid ID: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching mark: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public List<MarkDisplayDto> GetAllMarkDetails()
        {
            try
            {
                return _markService.GetAllMarkDetails();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching all mark details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<MarkDisplayDto>(); 
            }
        }

        public List<MarkDisplayDto> GetMarkDetailsByStudentId(int studentId)
        {
            try
            {
                return _markService.GetMarkDetailsByStudentId(studentId);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show($"Invalid Student ID: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return new List<MarkDisplayDto>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching mark details by Student ID: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<MarkDisplayDto>();
            }
        }

        public List<MarkDisplayDto> GetMarkDetailsBySubjectId(int subjectId)
        {
            try
            {
                return _markService.GetMarkDetailsBySubjectId(subjectId);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show($"Invalid Subject ID: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return new List<MarkDisplayDto>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching mark details by Subject ID: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<MarkDisplayDto>();
            }
        }

        public List<MarkDisplayDto> GetMarkDetailsByExamId(int examId)
        {
            try
            {
                return _markService.GetMarkDetailsByExamId(examId);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show($"Invalid Exam ID: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return new List<MarkDisplayDto>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching mark details by Exam ID: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<MarkDisplayDto>();
            }
        }

        public List<MarkDisplayDto> GetMarkDetailsByLecturerId(int lecturerId)
        {
            try
            {
                return _markService.GetMarkDetailsByLecturerId(lecturerId);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show($"Invalid Lecturer ID: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return new List<MarkDisplayDto>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching mark details by Lecturer ID: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<MarkDisplayDto>();
            }
        }

        // --- Business-specific report ---

        public List<TopPerformerDto> GetTopPerformers(int count)
        {
            try
            {
                return _markService.GetTopPerformers(count);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show($"Invalid count for top performers: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return new List<TopPerformerDto>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching top performers: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<TopPerformerDto>();
            }
        }
    }
}
