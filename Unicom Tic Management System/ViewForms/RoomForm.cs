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
using Unicom_Tic_Management_System.Models.DTOs.Scheduling_AttendanceDTOs;
using Unicom_Tic_Management_System.Models.Enums;

namespace Unicom_Tic_Management_System.ViewForms
{
    public partial class RoomForm : Form
    {
        private RoomController _controller;
        private int _selectedRoomId = -1;
        private UserRole userRole;
        private readonly UserRole _currentUserRole;

        public RoomForm()
        {
            InitializeComponent();
            _controller = new RoomController();
            _currentUserRole = userRole;
            SetupAccessControl();
            LoadRooms();
        }

        private void SetupAccessControl()
        {
            bool isAdmin = _currentUserRole == UserRole.Admin;

            btnAdd.Enabled = isAdmin;
            btnUpdate.Enabled = isAdmin;
            btnDelete.Enabled = isAdmin;
        }


        private void LoadRooms()
        {
            var rooms = _controller.GetAllRooms();
            dgvRooms.DataSource = rooms;
            dgvRooms.Columns["RoomId"].Visible = false;
        }

        private RoomDto GetRoomFromForm()
        {
            return new RoomDto
            {
                RoomId = _selectedRoomId,
                RoomNumber = txtRoomNumber.Text.Trim(),
                RoomType = cmbRoomType.SelectedItem?.ToString(),
                Capacity = int.TryParse(txtCapacity.Text, out int capacity) ? capacity : 0
            };
        }

        private void ClearForm()
        {
            _selectedRoomId = -1;
            txtRoomNumber.Clear();
            txtCapacity.Clear();
            cmbRoomType.SelectedIndex = -1;

            if (_currentUserRole == UserRole.Admin)
            {
                btnAdd.Enabled = true;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var dto = GetRoomFromForm();
                _controller.AddRoom(dto);
                MessageBox.Show("Room added successfully.");
                LoadRooms();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding room: " + ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedRoomId <= 0)
            {
                MessageBox.Show("Please select a room to update.");
                return;
            }

            try
            {
                var dto = GetRoomFromForm();
                _controller.UpdateRoom(dto);
                MessageBox.Show("Room updated successfully.");
                LoadRooms();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating room: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedRoomId <= 0)
            {
                MessageBox.Show("Please select a room to delete.");
                return;
            }

            var confirm = MessageBox.Show("Are you sure you want to delete this room?", "Confirm", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    _controller.DeleteRoom(_selectedRoomId);
                    MessageBox.Show("Room deleted successfully.");
                    LoadRooms();
                    ClearForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting room: " + ex.Message);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void dgvRooms_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvRooms.Rows[e.RowIndex];
                _selectedRoomId = Convert.ToInt32(row.Cells["RoomId"].Value);
                txtRoomNumber.Text = row.Cells["RoomNumber"].Value?.ToString();
                cmbRoomType.SelectedItem = row.Cells["RoomType"].Value?.ToString();
                txtCapacity.Text = row.Cells["Capacity"].Value?.ToString();

                if (_currentUserRole == UserRole.Admin)
                {
                    btnAdd.Enabled = false;
                    btnUpdate.Enabled = true;
                    btnDelete.Enabled = true;
                }
            }
        }
    }
}
