using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unicom_Tic_Management_System.Datas;
using Unicom_Tic_Management_System.Models;
using Unicom_Tic_Management_System.Models.Enums;
using Unicom_Tic_Management_System.Repositories;
using Unicom_Tic_Management_System.Utilities;

namespace Unicom_Tic_Management_System.ViewForms
{
    public partial class SignUpOptionsForm : Form
    {
        private readonly UserRepository _userRepository;
        private Panel currentRegistrationPanel;
        public SignUpOptionsForm()
        {
            InitializeComponent();
            _userRepository = new UserRepository();
            this.Load += SignUpOptionsForm_Load;
        }

        private void pnlRegistrationForms_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SignUpOptionsForm_Load(object sender, EventArgs e)
        {
            pnlRegistrationForms.Controls.Clear();
            lblMessage.Text = "";
        }

        private void btnStudent_Click(object sender, EventArgs e)
        {
            ShowRegistrationPanel("Student");
        }

        private void btnLecturer_Click(object sender, EventArgs e)
        {
            ShowRegistrationPanel("Lecturer");
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            ShowRegistrationPanel("Staff");
        }

        private void ShowRegistrationPanel(string role)
        {
            if (currentRegistrationPanel != null)
            {
                pnlRegistrationForms.Controls.Remove(currentRegistrationPanel);
                currentRegistrationPanel.Dispose();
                currentRegistrationPanel = null;
            }

            currentRegistrationPanel = CreateRegistrationPanel(role);
            pnlRegistrationForms.Controls.Add(currentRegistrationPanel);
            currentRegistrationPanel.Dock = DockStyle.Fill;
            currentRegistrationPanel.BringToFront();
            lblMessage.Text = "";
        }

        private Panel CreateRegistrationPanel(string role)
        {
            Panel panel = new Panel();
            panel.Tag = role;
            panel.Dock = DockStyle.Fill;
            panel.Padding = new Padding(10);
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.BackColor = System.Drawing.Color.LightCyan;

            // Title
            Label lblTitle = new Label();
            lblTitle.Text = $"{role} Registration";
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            lblTitle.AutoSize = true;
            lblTitle.Location = new System.Drawing.Point(10, 10);
            panel.Controls.Add(lblTitle);

            // Email
            Label lblEmail = new Label();
            lblEmail.Text = "Email:";
            lblEmail.AutoSize = true;
            lblEmail.Location = new System.Drawing.Point(10, 60);
            panel.Controls.Add(lblEmail);

            TextBox txtEmail = new TextBox();
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new System.Drawing.Size(200, 20);
            txtEmail.Location = new System.Drawing.Point(100, 55);
            panel.Controls.Add(txtEmail);

            // Password
            Label lblPassword = new Label();
            lblPassword.Text = "Password:";
            lblPassword.AutoSize = true;
            lblPassword.Location = new System.Drawing.Point(10, 90);
            panel.Controls.Add(lblPassword);

            TextBox txtPassword = new TextBox();
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new System.Drawing.Size(200, 20);
            txtPassword.Location = new System.Drawing.Point(100, 85);
            txtPassword.UseSystemPasswordChar = true;
            panel.Controls.Add(txtPassword);

            // NIC
            Label lblNic = new Label();
            lblNic.Text = "NIC:";
            lblNic.AutoSize = true;
            lblNic.Location = new System.Drawing.Point(10, 120);
            panel.Controls.Add(lblNic);

            TextBox txtNic = new TextBox();
            txtNic.Name = "txtNic";
            txtNic.Size = new System.Drawing.Size(200, 20);
            txtNic.Location = new System.Drawing.Point(100, 115);
            panel.Controls.Add(txtNic);


            // Register button
            Button btnRegister = new Button();
            btnRegister.Text = "Register";
            btnRegister.Size = new System.Drawing.Size(100, 30);
            btnRegister.Location = new System.Drawing.Point(100, 160);
            // Pass email, password, NIC, and role to the handler
            btnRegister.Click += async (s, e) => await HandleRegister(txtEmail.Text, txtPassword.Text, txtNic.Text, role);
            panel.Controls.Add(btnRegister);

            // Back button
            Button btnBack = new Button();
            btnBack.Text = "Back";
            btnBack.Size = new System.Drawing.Size(80, 25);
            btnBack.Location = new System.Drawing.Point(210, 163);
            btnBack.Click += (s, e) =>
            {
                if (currentRegistrationPanel != null)
                {
                    pnlRegistrationForms.Controls.Remove(currentRegistrationPanel);
                    currentRegistrationPanel.Dispose();
                    currentRegistrationPanel = null;
                }
            };
            panel.Controls.Add(btnBack);

            return panel;
        }

        private async Task HandleRegister(string email, string password, string nic, string parsedrole)
        {
            lblMessage.Text = ""; // Clear message

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(nic))
            {
                lblMessage.Text = "Please enter email, password, and NIC.";
                return;
            }

            if (password.Length < 6)
            {
                lblMessage.Text = "Password must be at least 6 characters.";
                return;
            }

            if (nic.Length < 12) // 
            {
                lblMessage.Text = "Please enter a valid NIC (at least 9 characters).";
                return;
            }


            UserRole parsedRole; // Changed variable name from 'role' to 'parsedRole'
            if (!Enum.TryParse(parsedrole, out parsedRole)) // Use 'roleString' for input, 'parsedRole' for output
            {
                lblMessage.Text = "Invalid user role specified.";
                return;
            }

            string hashedPassword = PasswordHasher.Hash(password); 

            User newUser = new User
            {
                Username = email,
                PasswordHash = hashedPassword,
                Nic = nic,
                
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now


            };

            try
            {
                _userRepository.AddUser(newUser);
                lblMessage.Text = $"{parsedrole} registered successfully! You can now log in.";
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Registration error: " + ex.Message;
                Console.WriteLine("Registration error: " + ex.Message);
            }
        }

        private void btnBackToLogin_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Close();

        }
    }
}
