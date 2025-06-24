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
using Unicom_Tic_Management_System.Models.DTOs.StaffDTOs;
using Unicom_Tic_Management_System.Models.Enums;
using Unicom_Tic_Management_System.Repositories;
using Unicom_Tic_Management_System.Repositories.Interfaces;
using Unicom_Tic_Management_System.Services;
using Unicom_Tic_Management_System.Services.Interfaces;

namespace Unicom_Tic_Management_System.ViewForms
{
    public partial class MarkForm : Form
    {

        private readonly MarkController _markController;
        private readonly UserRole _currentRole;
        private readonly StudentController _studentController;
        private readonly SubjectController _subjectController;
        private readonly ExamController _examController;
        private readonly LecturerController _lecturerController;
        private ILecturerService lecturerService;

        public MarkForm()
        {
            InitializeComponent();
            dgvTopPerformers.CellFormatting += dgvTopPerformers_CellFormatting;

            var markRepo = new MarkRepository();
            var studentRepo = new StudentRepository();
            var subjectRepo = new SubjectRepository();
            var examRepo = new ExamRepository();
            var lecturerRepo = new LecturerRepository();

            // Initialize services
            var studentService = new StudentService(studentRepo);
            var subjectService = new SubjectService(subjectRepo);
            var examService = new ExamService(examRepo);
            _lecturerController = new LecturerController(lecturerService);


            // Initialize mark service with all required repositories
            var markService = new MarkService(markRepo, studentRepo, subjectRepo, examRepo, lecturerRepo);


            _markController = new MarkController(markService);
            _studentController = new StudentController(new StudentService(new StudentRepository()));
            _subjectController = new SubjectController(new SubjectService(new SubjectRepository()));
            _examController = new ExamController(new ExamService(new ExamRepository()));
            _lecturerController = new LecturerController(new LecturerService());

            _currentRole = UserRole.Admin; 

            LoadComboBoxes();
            LoadAllMarks();
            LoadTopPerformers();
            HandlePermissions();
        }

    

       private void HandlePermissions()
        {
            bool isAdmin = _currentRole == UserRole.Admin;

            btnAdd.Enabled = isAdmin;
            btnUpdate.Enabled = isAdmin;
            btnDelete.Enabled = isAdmin;
            
        }

        // 2. Load marks
        private void LoadAllMarks()
        {
            var marks = _markController.GetAllMarkDetails();
            dgvMarks.DataSource = marks;
        }

        // 3. Load top 10 performers
        private void LoadTopPerformers()
        {

            var topPerformers = _markController.GetTopPerformers(10); 
            dgvTopPerformers.DataSource = topPerformers;
        }

        private void LoadComboBoxes()
        {
            var students = _studentController.GetAllStudents();
            cmbStudent.DataSource = students;
            cmbStudent.DisplayMember = "FullName";
            cmbStudent.ValueMember = "StudentId";

            var subjects = _subjectController.GetAllSubjects();
            cmbSubject.DataSource = subjects;
            cmbSubject.DisplayMember = "SubjectName";
            cmbSubject.ValueMember = "SubjectId";

            var exams = _examController.GetAllExams();
            cmbExam.DataSource = exams;
            cmbExam.DisplayMember = "ExamType";
            cmbExam.ValueMember = "ExamId";

            var lecturers = _lecturerController.GetAllLecturers();
            lecturers.Insert(0, new LecturerDto { LecturerId = 0, LecturerName = "-- Select --" });
            cmbLecturer.DataSource = lecturers;
            cmbLecturer.DisplayMember = "LecturerName";
            cmbLecturer.ValueMember = "LecturerId";

            cmbGrade.Items.Clear();
            cmbGrade.Items.AddRange(new string[] { "A", "B", "C", "D", "F" });
            cmbGrade.SelectedIndex = 0;
        }


        private void SetupDataGridView()
        {
            dgvMarks.AutoGenerateColumns = false;


            dgvMarks.Columns.Clear();


            dgvMarks.Columns.Add(CreateColumn("MarkId", "Mark ID", "MarkId"));
            dgvMarks.Columns.Add(CreateColumn("StudentName", "Student Name", "StudentName"));
            dgvMarks.Columns.Add(CreateColumn("SubjectName", "Subject Name", "SubjectName"));
            dgvMarks.Columns.Add(CreateColumn("ExamType", "Exam Type", "ExamType"));
            dgvMarks.Columns.Add(CreateColumn("MarksObtained", "Marks", "MarksObtained"));
            dgvMarks.Columns.Add(CreateColumn("Grade", "Grade", "Grade"));
            dgvMarks.Columns.Add(CreateColumn("GradedByLecturerName", "Graded By", "GradedByLecturerName"));
            dgvMarks.Columns.Add(CreateColumn("EntryDate", "Entry Date", "EntryDate"));


            dgvMarks.Columns["MarkId"].ReadOnly = true;
            dgvMarks.Columns["MarkId"].Visible = false;
            dgvMarks.Columns["MarksObtained"].Width = 60;
            dgvMarks.Columns["Grade"].Width = 50;

            dgvMarks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMarks.ReadOnly = true;
        }

        private DataGridViewTextBoxColumn CreateColumn(string name, string headerText, string dataPropertyName)
        {
            return new DataGridViewTextBoxColumn
            {
                Name = name,
                HeaderText = headerText,
                DataPropertyName = dataPropertyName,
                ReadOnly = true
            };
        }

        private void ClearForm()
        {

            cmbStudent.SelectedIndex = -1;
            cmbSubject.SelectedIndex = -1;
            cmbExam.SelectedIndex = -1;
            txtMarksObtained.Text = string.Empty;
            cmbLecturer.SelectedIndex = -1;
            dtpEntryDate.Value = DateTime.Now;

            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var dto = new MarkDto
            {
                StudentId = (int)cmbStudent.SelectedValue,
                SubjectId = (int)cmbSubject.SelectedValue,
                ExamId = (int)cmbExam.SelectedValue,
                MarksObtained = int.Parse(txtMarksObtained.Text),
                GradedByLecturerId = (int?)cmbLecturer.SelectedValue,
                Grade = cmbGrade.SelectedItem.ToString(),
                EntryDate = dtpEntryDate.Value
            };

            _markController.AddMark(dto);
            LoadAllMarks();
            LoadTopPerformers();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvMarks.SelectedRows.Count == 0) return;

            int markId = (int)dgvMarks.SelectedRows[0].Cells["MarkId"].Value;

            var dto = new MarkDto
            {
                MarkId = markId,
                StudentId = (int)cmbStudent.SelectedValue,
                SubjectId = (int)cmbSubject.SelectedValue,
                ExamId = (int)cmbExam.SelectedValue,
                MarksObtained = int.Parse(txtMarksObtained.Text),
                GradedByLecturerId = (int?)cmbLecturer.SelectedValue,
                Grade = cmbGrade.SelectedItem.ToString(),
                EntryDate = dtpEntryDate.Value
            };

            _markController.UpdateMark(dto);
            LoadAllMarks();
            LoadTopPerformers();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvMarks.SelectedRows.Count == 0) return;

            int markId = (int)dgvMarks.SelectedRows[0].Cells["MarkId"].Value;

            _markController.DeleteMark(markId);
            LoadAllMarks();
            LoadTopPerformers();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void dgvMarks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dgvMarks_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvMarks.SelectedRows.Count == 0) return;

            var selected = (MarkDisplayDto)dgvMarks.SelectedRows[0].DataBoundItem;

            cmbStudent.SelectedValue = selected.StudentId;
            cmbSubject.SelectedValue = selected.SubjectId;
            cmbExam.SelectedValue = selected.ExamId;
            cmbLecturer.SelectedValue = selected.GradedByLecturerId;
            txtMarksObtained.Text = selected.MarksObtained.ToString();
            cmbGrade.SelectedItem = selected.Grade;
            dtpEntryDate.Value = selected.EntryDate;
        }

        private void dgvTopPerformers_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex == 0)
            {
                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                e.CellStyle.BackColor = Color.Gold;
                e.CellStyle.ForeColor = Color.Black;
            }
        }
    }





}



