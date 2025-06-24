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
using Unicom_Tic_Management_System.Models.DTOs.AcademicOperationsDTOs;
using Unicom_Tic_Management_System.Models.Enums;
using Unicom_Tic_Management_System.Repositories;
using Unicom_Tic_Management_System.Services;

namespace Unicom_Tic_Management_System.ViewForms
{
    public partial class ExamForm : Form
    {
        private readonly ExamController _examController;
        private UserRole _currentRole = UserRole.Staff;
        
        public ExamForm()
        {
            InitializeComponent();

            var examRepo = new ExamRepository();
            var examService = new ExamService(examRepo);
            _examController = new ExamController(examService);

            LoadExams();

            dgvExams.SelectionChanged += dgvExams_SelectionChanged;
        }

        public void SetRole(UserRole role)
        {
            _currentRole = role;
            HandlePermissions();
        }

        private void HandlePermissions()
        {
            bool isAdmin = _currentRole == UserRole.Admin;

            btnAdd.Enabled = isAdmin;
            btnUpdate.Enabled = isAdmin;
            btnDelete.Enabled = isAdmin;
        }

        private void LoadExams()
        {
            var exams = _examController.GetAllExams();
            dgvExams.DataSource = exams;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var examDto = new ExamDto
                {
                    ExamName = txtExamName.Text.Trim(),
                    SubjectId = int.Parse(txtSubjectId.Text),
                    ExamDate = dtpExamDate.Value,
                    MaxMarks = int.Parse(txtMaxMarks.Text)
                };

                _examController.AddExam(examDto);
                LoadExams();
                ClearForm();
                MessageBox.Show("Exam added successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding exam: " + ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvExams.CurrentRow == null) return;

            try
            {
                var selectedExam = (ExamDto)dgvExams.CurrentRow.DataBoundItem;

                selectedExam.ExamName = txtExamName.Text.Trim();
                selectedExam.SubjectId = int.Parse(txtSubjectId.Text);
                selectedExam.ExamDate = dtpExamDate.Value;
                selectedExam.MaxMarks = int.Parse(txtMaxMarks.Text);

                _examController.UpdateExam(selectedExam);
                LoadExams();
                ClearForm();
                MessageBox.Show("Exam updated successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating exam: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvExams.CurrentRow == null) return;

            var selectedExam = (ExamDto)dgvExams.CurrentRow.DataBoundItem;

            var confirm = MessageBox.Show($"Are you sure you want to delete '{selectedExam.ExamName}'?",
                "Confirm Delete", MessageBoxButtons.YesNo);

            if (confirm == DialogResult.Yes)
            {
                _examController.DeleteExam(selectedExam.ExamId);
                LoadExams();
                ClearForm();
            }
        }

        private void dgvExams_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvExams.CurrentRow == null) return;

            var selected = (ExamDto)dgvExams.CurrentRow.DataBoundItem;

            txtExamName.Text = selected.ExamName;
            txtSubjectId.Text = selected.SubjectId.ToString();
            dtpExamDate.Value = selected.ExamDate;
            txtMaxMarks.Text = selected.MaxMarks.ToString();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void ClearForm()
        {
            txtExamName.Clear();
            txtSubjectId.Clear();
            txtMaxMarks.Clear();
            dtpExamDate.Value = DateTime.Now;
        }

        private void ExamForm_Load(object sender, EventArgs e)
        {

        }
    }
}
