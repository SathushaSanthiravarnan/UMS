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
using Unicom_Tic_Management_System.Repositories;
using Unicom_Tic_Management_System.Repositories.Interfaces;

namespace Unicom_Tic_Management_System.ViewForms
{
    public partial class StaffForm : Form
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IUserRepository _userRepository;
        private readonly INicDetailsRepository _nicDetailRepository;

        private Staff _currentStaff = null;
       
        public StaffForm()
        {
            InitializeComponent();
            _staffRepository = new StaffRepository();
            _userRepository = new UserRepository();
            _nicDetailRepository = new NICDetailsRepository();
            InitializeForm();
        }

        private void InitializeForm()
        {
            ClearForm();
        }

        private void ClearForm()
        {
            _currentStaff = null;
            


            txtUsername.Clear();
            txtPassword.Clear();
            txtNic.Clear();
            txtName.Clear();
            txtContact.Clear();
            txtEmail.Clear();
            dtpHireDate.Value = DateTime.Now;
            cmbDepartment.SelectedIndex = -1;
            lblEmployeeId.Text = "AUTO";

            btnDelete.Enabled = false;
            btnAdd.Text = "Save";

            txtUsername.Enabled = true;
            txtPassword.Enabled = true;
            txtNic.Enabled = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) ||
               string.IsNullOrWhiteSpace(txtPassword.Text) ||
               string.IsNullOrWhiteSpace(txtNic.Text) ||
               string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Fill all required fields.", "Validation Error");
                return;
            }

            if (!_nicDetailRepository.NicExists(txtNic.Text.Trim()))
            {
                MessageBox.Show("NIC not found in authorized list.", "Validation Error");
                return;
            }

            User user = new User
            {
                Username = txtUsername.Text.Trim(),
                PasswordHash = UserRepository.HashPassword(txtPassword.Text),
                Nic = txtNic.Text.Trim(),
                Role = Models.Enums.UserRole.Staff,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            _userRepository.AddUser(user);

            Staff staff = new Staff
            {
                Name = txtName.Text.Trim(),
                Nic = txtNic.Text.Trim(),
                ContactNo = txtContact.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                HireDate = dtpHireDate.Value.Date,
                DepartmentId = cmbDepartment.SelectedValue as int?,
                UserId = user.UserId,
                EmployeeId = GenerateEmployeeId(),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            _staffRepository.AddStaff(staff);

            MessageBox.Show("Staff registered successfully with ID: " + staff.EmployeeId);
            ClearForm();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            {
                ClearForm();
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_currentStaff == null)
            {
                MessageBox.Show("No staff selected.");
                return;
            }

            var confirm = MessageBox.Show("Delete staff?", "Confirm", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                _staffRepository.DeleteStaff(_currentStaff.StaffId);
                MessageBox.Show("Staff deleted.");
                ClearForm();
            }

        }

        private string GenerateEmployeeId()
        {
            return "STF-" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + new Random().Next(100, 999);
        }
    }
}
