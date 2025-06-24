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
using Unicom_Tic_Management_System.Repositories;
using Unicom_Tic_Management_System.Utilities;

namespace Unicom_Tic_Management_System.ViewForms
{
    public partial class LoginForm : Form
    {
        private readonly UserRepository _userRepository;

        public LoginForm()
        {
            InitializeComponent();
            _userRepository = new UserRepository();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            lblMessage.Text = ""; // Clear message

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                lblMessage.Text = "Please enter username and password.";
                return;
            }

            try
            {
                User user = _userRepository.GetUserByUsername(username); 

                if (user != null)
                {
                   
                    if (PasswordHasher.VerifyPassword(password, user.PasswordHash))
                    {
                        lblMessage.Text = $"Login successful! ({user.Role})";
                        ShowMainForm(user.Role.ToString());
                    }
                    else
                    {
                        lblMessage.Text = "Invalid username or password."; 
                    }
                }
                else
                {
                    lblMessage.Text = "Invalid username or password.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Login error: " + ex.Message;
                Console.WriteLine("Login error: " + ex.Message);
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            SignUpOptionsForm signUpOptionsForm = new SignUpOptionsForm();
            signUpOptionsForm.Show();
            this.Hide();
        }

        private void ShowMainForm(string role)
        {
            MainForm mainForm = new MainForm(role);
            mainForm.Show();
            this.Hide(); 
        }
    }
}
