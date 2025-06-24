using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Unicom_Tic_Management_System.ViewForms
{
    public partial class MainForm : Form
    {
        private string _userRole;
        public MainForm(string userRole)
        {
            InitializeComponent();
            _userRole = userRole;
            lblWelcome.Text = $"Welcome, {userRole}!";
            lblUserRole.Text = $"Your Role: {userRole}";
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Close();
        }
    }
}
