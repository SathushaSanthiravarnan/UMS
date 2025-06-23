using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unicom_Tic_Management_System.Controllers;
using Unicom_Tic_Management_System.Models.DTOs.AcademicDTOs;
using Unicom_Tic_Management_System.Repositories;
using Unicom_Tic_Management_System.Services;

namespace Unicom_Tic_Management_System.ViewForms
{
    public partial class SubjectForm : Form
    {
        private readonly SubjectController _subjectController;
        private readonly CourseController _courseController;
        private int _selectedSubjectId = -1;
        public SubjectForm()
        {
            InitializeComponent();

            _subjectController = new SubjectController(new SubjectService(new SubjectRepository()));
            _courseController = new CourseController();

            LoadCourses();
            LoadSubjects();

            dgvSubjects.SelectionChanged += dgvSubjects_SelectionChanged;
        }

        private void LoadCourses()
        {
            var courses = _courseController.GetAllCourses(); 
            cmbCourses.DataSource = courses;
            cmbCourses.DisplayMember = "CourseName";
            cmbCourses.ValueMember = "CourseId";
        }

        private void LoadSubjects()
        {
            try
            {
                var subjects = _subjectController.GetAllSubjects();
                dgvSubjects.DataSource = _subjectController.GetAllSubjects();
                dgvSubjects.ClearSelection();

                if (dgvSubjects.Columns["CourseId"] != null)
                {
                    dgvSubjects.Columns["CourseId"].Visible = false;
                }

                
                if (dgvSubjects.Columns["CourseName"] != null)
                {
                    dgvSubjects.Columns["CourseName"].HeaderText = "Course";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading subjects: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void SubjectForm_Load(object sender, EventArgs e)
        {
            this.Load += SubjectForm_Load;

        }

        private void ClearFields()
        {
            _selectedSubjectId = -1;
            txtSubjectName.Text = "";
           
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (string.IsNullOrWhiteSpace(txtSubjectName.Text))
                {
                    MessageBox.Show("Please enter subject name", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cmbCourses.SelectedItem == null)
                {
                    MessageBox.Show("Please select a course", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                
                var selectedCourseId = Convert.ToInt32(cmbCourses.SelectedValue);

                
                var subjectDto = new SubjectDto
                {
                    SubjectName = txtSubjectName.Text.Trim(),
                    CourseId = selectedCourseId
                };

                
                _subjectController.AddSubject(subjectDto);

                
                LoadSubjects();
                ClearFields();

                MessageBox.Show("Subject added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding subject: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedSubjectId == -1)
            {
                MessageBox.Show("Please select a subject to update.");
                return;
            }
            try
            {
                var subjectDto = new SubjectDto
                {
                    SubjectId = _selectedSubjectId,
                    SubjectName = txtSubjectName.Text.Trim(),
                    
                };

                _subjectController.UpdateSubject(subjectDto);
                LoadSubjects();
                ClearFields();
            }
            
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating subject: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedSubjectId == -1)
            {
                MessageBox.Show("Please select a subject to delete.");
                return;
            }

            var confirm = MessageBox.Show("Are you sure you want to delete this subject?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    _subjectController.DeleteSubject(_selectedSubjectId);
                    LoadSubjects();
                    ClearFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting subject: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvSubjects_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSubjects.SelectedRows.Count > 0)
            {
                var selected = dgvSubjects.SelectedRows[0].DataBoundItem as SubjectDto;
                if (selected != null)
                {
                    _selectedSubjectId = selected.SubjectId;
                    txtSubjectName.Text = selected.SubjectName;
                    cmbCourses.SelectedValue = selected.CourseId;

                }
            }
        }
    }
}
