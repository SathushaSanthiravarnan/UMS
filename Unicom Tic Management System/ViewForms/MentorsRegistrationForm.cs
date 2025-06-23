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
    public partial class MentorsRegistrationForm : Form
    {
        private readonly IMentorRepository _mentorRepository; 
        private readonly IUserRepository _userRepository;
        private readonly INicDetailsRepository _nicDetailsRepository;


        private Mentor _currentMentor = null;
        private User _currentUser = null;

        public MentorsRegistrationForm()
        {
            InitializeComponent();

            _mentorRepository = new MentorRepository(); 
            _userRepository = new UserRepository();
            _nicDetailsRepository = new NICDetailsRepository();


            InitializeForm();
        }

      

        private void InitializeForm()
        {


            ClearForm();
        }


        private void ClearForm()
        {
            _currentMentor = null;
            _currentUser = null;

            txtUsername.Clear();
            txtPassword.Clear();
            txtNic.Clear();

            txtName.Clear();

            txtContactNo.Clear();
            txtEmail.Clear();





            btnDelete.Enabled = false;
            btnSignUp.Text = "Save";

            txtUsername.Enabled = true;
            txtPassword.Enabled = true;
            txtNic.Enabled = true;
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

            if (_currentMentor == null && string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Password is required to register a new mentor.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (!IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }



            try
            {

                if (!_nicDetailsRepository.NicExists(txtNic.Text.Trim())) 
                {
                    MessageBox.Show("This NIC is not valid or does not exist in the NicDetails table. Please add NIC details first.", "NIC Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                User user;
                if (_currentUser == null)
                {

                    user = new User
                    {
                        Username = txtUsername.Text.Trim(),
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
                    if (txtPassword.Text != "********" && !string.IsNullOrWhiteSpace(txtPassword.Text)) // Hash only if password changes
                    {
                        user.PasswordHash = UserRepository.HashPassword(txtPassword.Text);
                    }
                    user.UpdatedAt = DateTime.Now;
                    _userRepository.UpdateUser(user);
                }


                Mentor mentor = _currentMentor ?? new Mentor();

                mentor.Name = txtName.Text.Trim();
                mentor.Nic = txtNic.Text.Trim();

                mentor.Phone = txtContactNo.Text.Trim();
                mentor.Email = txtEmail.Text.Trim();



                mentor.UserId = user.UserId;
                if (_currentMentor == null)
                {


                    mentor.CreatedAt = DateTime.Now;
                    mentor.UpdatedAt = DateTime.Now;
                    _mentorRepository.AddMentor(mentor);
                    MessageBox.Show("Mentor successfully added! Mentor ID: " + mentor.MentorId, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {


                    mentor.UpdatedAt = DateTime.Now;
                    _mentorRepository.UpdateMentor(mentor);
                    MessageBox.Show("Mentor successfully updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                ClearForm();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_currentMentor == null)
            {
                MessageBox.Show("No mentor selected for deletion.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult result = MessageBox.Show($"Are you sure you want to delete mentor '{_currentMentor.Name}'?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    _mentorRepository.DeleteMentor(_currentMentor.MentorId);

                    MessageBox.Show("Mentor successfully deleted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting mentor: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

    }
}
