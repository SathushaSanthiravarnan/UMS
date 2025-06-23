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

namespace Unicom_Tic_Management_System.ViewForms
{
    public partial class CourseForm : Form
    {
        private readonly CourseController _controller;
        private int _selectedCourseId = -1;
        public CourseForm()
        {
            InitializeComponent();
            _controller = new CourseController();
            LoadCourses();
        }

        private void LoadCourses()
        {
            var courses = _controller.GetAllCourses();
            dgvCourses.DataSource = null;
            dgvCourses.DataSource = courses;
            dgvCourses.ClearSelection();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCourseName.Text))
            {
                MessageBox.Show("Please enter course name");
                return;
            }

            var dto = new CourseDto
            {
                CourseName = txtCourseName.Text
            };

            _controller.AddCourse(dto);
            LoadCourses();
            ClearForm();
        }

        private void ClearForm()
        {
            txtCourseName.Text = "";
            _selectedCourseId = -1;
        }

        private void dgvCourses_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCourses.SelectedRows.Count > 0)
            {
                var course = (CourseDto)dgvCourses.SelectedRows[0].DataBoundItem;
                _selectedCourseId = course.CourseId;
                txtCourseName.Text = course.CourseName;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedCourseId == -1)
            {
                MessageBox.Show("Please select a course to update");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCourseName.Text))
            {
                MessageBox.Show("Please enter course name");
                return;
            }

            var dto = new CourseDto
            {
                CourseId = _selectedCourseId,
                CourseName = txtCourseName.Text
            };

            _controller.UpdateCourse(dto);
            LoadCourses();
            ClearForm();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedCourseId == -1) 
            {
                MessageBox.Show("Please select a course to delete");
                return;
            }

            var confirmResult = MessageBox.Show("Are you sure to delete this course?", "Confirm Delete", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                _controller.DeleteCourse(_selectedCourseId);
                LoadCourses();
                ClearForm();
            }
        }

        private void CourseForm_Load(object sender, EventArgs e)
        {

        }
    }
}
