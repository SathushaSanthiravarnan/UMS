using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unicom_Tic_Management_System.Models;
using Unicom_Tic_Management_System.Models.Enums;
using Unicom_Tic_Management_System.Repositories;
using Unicom_Tic_Management_System.Repositories.Interfaces;

namespace Unicom_Tic_Management_System.ViewForms
{
    public partial class StudentRegistrationForm : Form
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IUserRepository _userRepository;
        private readonly INicDetailsRepository _nicDetailsrepository;
        private readonly ICourseRepository _courseRepository;
        

        private Student _currentStudent = null;
        private User _currentUser = null;
        public StudentRegistrationForm()
        {
            InitializeComponent();

            _studentRepository = new StudentRepository();
            _userRepository = new UserRepository();
            _nicDetailsrepository = new NICDetailsRepository();
            _courseRepository = new CourseRepository();

            InitializeForm();
        }

        public StudentRegistrationForm(int studentId) : this()
        {
            LoadStudentForEdit(studentId);
        }

        private void InitializeForm()
        {
            LoadComboBoxes();
            LoadStudentsIntoGrid();
            ClearForm();
        }

        private void LoadComboBoxes()
        {
            try
            {
                comboGender.DataSource = Enum.GetValues(typeof(GenderType));
                comboGender.SelectedIndex = -1;

                List<Course> courses = _courseRepository.GetAllCourses();
                comboCourse.DisplayMember = "CourseName";
                comboCourse.ValueMember = "CourseId";
                comboCourse.DataSource = courses;
                comboCourse.SelectedIndex = -1;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading ComboBox data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            _currentStudent = null; 
            _currentUser = null;     
            textUserName.Clear();
            txtPassword.Clear();
            txtNic.Clear(); 

            txtName.Clear();
            textAddress.Clear();
            textContactNo.Clear();
            txtEmail.Clear();
            dtpDateOfBirth.Value = DateTime.Now; 
            dtpEnrollmentDate.Value = DateTime.Now;
            comboGender.SelectedIndex = -1;
            comboCourse.SelectedIndex = -1;
            txtAdmissionNumber.Text = "" ; 

            btnDelete.Enabled = false; 
            btnSignUp.Text = "(Sign Up)"; 

            
            textUserName.Enabled = true;
            txtPassword.Enabled = true;
            txtNic.Enabled = true; 
        }

        private void LoadStudentForEdit(int studentId)
        {
            try
            {
                _currentStudent = _studentRepository.GetStudentById(studentId);
                if (_currentStudent != null)
                {
                    _currentUser = _userRepository.GetUserById(_currentStudent.UserId);

                    if (_currentUser == null)
                    {
                        MessageBox.Show("Linked user details not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ClearForm();
                        return;
                    }


                    textUserName.Text = _currentUser.Username;
                    txtPassword.Text = "********";
                    txtNic.Text = _currentUser.Nic;


                    txtName.Text = _currentStudent.Name;
                    txtAddress.Text = _currentStudent.Address;
                    txtContactNo.Text = _currentStudent.ContactNo;
                    txtEmail.Text = _currentStudent.Email;
                    txtAdmissionNumber.Text = _currentStudent.AdmissionNumber;

                    if (_currentStudent.DateOfBirth.HasValue)
                        dtpDateOfBirth.Value = _currentStudent.DateOfBirth.Value;
                    else
                        dtpDateOfBirth.Value = DateTime.Now;

                    dtpEnrollmentDate.Value = _currentStudent.EnrollmentDate;

                    comboGender.SelectedItem = _currentStudent.Gender;
                    comboCourse.SelectedValue = _currentStudent.CourseId;
                    btnDelete.Enabled = true;
                    btnSignUp.Text = " (Update)";


                    textUserName.Enabled = false;
                    txtNic.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Student not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading student for update: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ClearForm();
            }
        }

        private void StudentRegistrationForm_Load(object sender, EventArgs e)
        {


        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtContactNo_Click(object sender, EventArgs e)
        {

        }

        private void comboMainGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void LoadStudentsIntoGrid()
        {
           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textUserName.Text) ||
                string.IsNullOrWhiteSpace(txtNic.Text) ||
                string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Please fill in all required fields (Username, NIC, Name, Email).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_currentStudent == null && string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Password is required to register a new student.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboCourse.SelectedValue == null || comboGender.SelectedValue == null)
                
            {
                MessageBox.Show("Please select Course, Main Group, Sub Group, and Gender.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (!_nicDetailsrepository.NicExists(txtNic.Text.Trim()))
                {
                    MessageBox.Show("This NIC is invalid or does not exist in the NicDetail table. Please add NIC details first.", "NIC Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                
                User user;
                if (_currentUser == null)
                {
                    
                    user = new User
                    {
                        Username = textUserName.Text.Trim(),
                        PasswordHash = UserRepository.HashPassword(txtPassword.Text), 
                        Nic = txtNic.Text.Trim(),
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };
                    _userRepository.AddUser(user); 
                }
                else
                {
                    
                    user = _currentUser;
                    if (txtPassword.Text != "********" && !string.IsNullOrWhiteSpace(txtPassword.Text))
                    {
                        user.PasswordHash = UserRepository.HashPassword(txtPassword.Text);
                    }

                    user.UpdatedAt = DateTime.Now;
                    _userRepository.UpdateUser(user); 
                }

                
                Student student = _currentStudent ?? new Student(); 

                student.Name = txtName.Text.Trim();
                student.Address = txtAddress.Text.Trim();
                student.ContactNo = txtContactNo.Text.Trim();
                student.Email = txtEmail.Text.Trim();
                student.DateOfBirth = dtpDateOfBirth.Value;
                student.EnrollmentDate = dtpEnrollmentDate.Value;
                student.Gender = (GenderType)comboGender.SelectedValue;
                student.CourseId = (int)comboCourse.SelectedValue;
                student.UserId = user.UserId; 

                if (_currentStudent == null)
                {
                    
                    student.AdmissionNumber = GenerateAdmissionNumber();
                    student.CreatedAt = DateTime.Now;
                    student.UpdatedAt = DateTime.Now;
                    _studentRepository.AddStudent(student);
                    MessageBox.Show("Student added successfully! Admission Number: " + student.AdmissionNumber, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    
                    student.UpdatedAt = DateTime.Now;
                    _studentRepository.UpdateStudent(student);
                    MessageBox.Show("Student updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                ClearForm(); 
                LoadStudentsIntoGrid(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            {
                ClearForm();
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private string GenerateAdmissionNumber()
        {
            
            return "UTI-" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + new Random().Next(100, 999);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_currentStudent == null)
            {
                MessageBox.Show("No student selected for deletion.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult result = MessageBox.Show($"Are you sure you want to delete student '{_currentStudent.Name}'?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    _studentRepository.DeleteStudent(_currentStudent.StudentId);
    
                    MessageBox.Show("Student deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadStudentsIntoGrid(); // Refresh DataGridView
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting student: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
    
}

