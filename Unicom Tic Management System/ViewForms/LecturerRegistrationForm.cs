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
    public partial class LecturerRegistrationForm : Form
    {
        private readonly ILecturerRepository _lecturerRepository;
        private readonly IUserRepository _userRepository;
        private readonly INicDetailsRepository _nicDetailRepository;

        
        private Lecturer _currentLecturer = null;
        private User _currentUser = null;

        public LecturerRegistrationForm()
        {
            InitializeComponent();

            _lecturerRepository = new LecturerRepository();
            _userRepository = new UserRepository();
            _nicDetailRepository = new NICDetailsRepository();
            InitializeForm();

        }

        public LecturerRegistrationForm(int lecturerId) : this()
        {
            LoadLecturerForEdit(lecturerId);
        }

        private void InitializeForm()
        {
            LoadComboBoxes();
            ClearForm();
        }

        private void LoadComboBoxes()
        {
            try
            {
               
                cmbGender.DataSource = Enum.GetValues(typeof(GenderType));
                cmbGender.SelectedIndex = -1; 
            }
            catch (Exception ex)
            {
                MessageBox.Show("ComboBox தரவுகளை ஏற்றும்போது பிழை: " + ex.Message, "பிழை", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            _currentLecturer = null; 
            _currentUser = null;     

            txtUsername.Clear();
            txtPassword.Clear();
            txtNic.Clear(); 

            txtName.Clear();
            txtAddress.Clear();
            txtContactNo.Clear();
            txtEmail.Clear();
            dtpDateOfBirth.Value = DateTime.Now; 
            dtpHireDate.Value = DateTime.Now;    
            cmbGender.SelectedIndex = -1;
            txtEmployeeId.Text = "Employee ID will be auto-generated for new lecturers"; 


            btnDelete.Enabled = false; 
            btnSignUp.Text = "Sign Up"; 

            
            txtPassword.Enabled = true;
            txtNic.Enabled = true; 
        }

        private void LoadLecturerForEdit(int lecturerId)
        {
            try
            {
                _currentLecturer = _lecturerRepository.GetLecturerById(lecturerId);
                if (_currentLecturer != null)
                {
                    _currentUser = _userRepository.GetUserById(_currentLecturer.UserId);

                    if (_currentUser == null)
                    {
                        MessageBox.Show("Associated user details not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ClearForm();
                        return;
                    }

                   
                    txtUsername.Text = _currentUser.Username;
                    txtPassword.Text = "********"; 
                    txtNic.Text = _currentUser.Nic;

                    
                    txtName.Text = _currentLecturer.Name;
                    txtAddress.Text = _currentLecturer.Address;
                    txtContactNo.Text = _currentLecturer.Phone;
                    txtEmail.Text = _currentLecturer.Email;
                    txtEmployeeId.Text = _currentLecturer.EmployeeId;

                    if (_currentLecturer.DateOfBirth.HasValue)
                        dtpDateOfBirth.Value = _currentLecturer.DateOfBirth.Value;
                    else
                        dtpDateOfBirth.Value = DateTime.Now;

                    if (_currentLecturer.HireDate.HasValue)
                    {
                        dtpHireDate.Value = _currentLecturer.HireDate.Value;
                    }
                    else

                        btnDelete.Enabled = true; 
                    btnSignUp.Text = "Update"; 

                 
                    txtUsername.Enabled = false;
                    txtNic.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Lecturer not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading lecturer for update: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ClearForm();
            }
        }
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) ||
               string.IsNullOrWhiteSpace(txtNic.Text) ||
               string.IsNullOrWhiteSpace(txtName.Text) ||
               string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Please fill in all required fields (Username, NIC, Name, Email).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_currentLecturer == null && string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Password is required to register a new lecturer.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbGender.SelectedValue == null)
            {
                MessageBox.Show("Please select a gender.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                
                if (!_nicDetailRepository.NicExists(txtNic.Text.Trim()))
                {
                    MessageBox.Show("This NIC is invalid or not found in the NicDetail table. Please add NIC details first.", "NIC Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                
                User user;
                if (_currentUser == null)
                {
                    
                    user = new User
                    {
                        Username = txtUsername.Text.Trim(),
                        PasswordHash = UserRepository.HashPassword(txtPassword.Text), // Hash the password
                        Nic = txtNic.Text.Trim(),
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };
                    _userRepository.AddUser(user); 
                    
                }
                else
                {
                    
                    user = _currentUser;
                    if (txtPassword.Text != "********" && !string.IsNullOrWhiteSpace(txtPassword.Text)) // Only hash if password changed
                    {
                        user.PasswordHash = UserRepository.HashPassword(txtPassword.Text);
                    }
                    user.UpdatedAt = DateTime.Now;
                    _userRepository.UpdateUser(user); 
                }

                
                Lecturer lecturer = _currentLecturer ?? new Lecturer(); 

                lecturer.Name = txtName.Text.Trim();
                lecturer.Nic = txtNic.Text.Trim(); 
                lecturer.Address = txtAddress.Text.Trim();
                lecturer.Phone = txtContactNo.Text.Trim();
                lecturer.Email = txtEmail.Text.Trim();
                lecturer.DateOfBirth = dtpDateOfBirth.Value;
                lecturer.Gender = (GenderType)cmbGender.SelectedValue;
                lecturer.HireDate = dtpHireDate.Value; 
                lecturer.UserId = user.UserId; 

                if (_currentLecturer == null)
                {
                    
                    lecturer.EmployeeId = GenerateEmployeeId();
                    lecturer.CreatedAt = DateTime.Now;
                    lecturer.UpdatedAt = DateTime.Now;
                    _lecturerRepository.AddLecturer(lecturer);
                    MessageBox.Show("Lecturer added successfully! Employee ID: " + lecturer.EmployeeId, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    
                    lecturer.EmployeeId = txtEmployeeId.Text; 
                    lecturer.UpdatedAt = DateTime.Now;
                    _lecturerRepository.UpdateLecturer(lecturer);
                    MessageBox.Show("Lecturer updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                ClearForm(); 
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (_currentLecturer == null)
            {
                MessageBox.Show("No lecturer selected for deletion.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult result = MessageBox.Show($"Are you sure you want to delete lecturer '{_currentLecturer.Name}'?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    _lecturerRepository.DeleteLecturer(_currentLecturer.LecturerId);
                    
                    MessageBox.Show("Lecturer deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting lecturer: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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

        private string GenerateEmployeeId()
        {
           

            return "EMP-" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + new Random().Next(100, 999);
        }


    }
}
