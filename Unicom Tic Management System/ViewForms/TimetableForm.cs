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
    public partial class TimetableForm : Form
    {
        private readonly TimetableController _timetableController = new TimetableController();
        private readonly SubjectController _subjectController = new SubjectController();
        private readonly RoomController _roomController = new RoomController();
        private readonly TimeSlotController _timeSlotController = new TimeSlotController();

        private int? editingId = null;
        
        private readonly string _userRole;
        public TimetableForm(string userRole)
        {
            InitializeComponent();
            _userRole = userRole;


            LoadFormData();
            ApplyRolePermissions();

        }

        private void LoadFormData()
        {
            LoadSubjects();
            LoadTimeSlots();
            LoadRooms();
            LoadTimetableData();
            
        }

        private void ApplyRolePermissions()
        {
            bool isAdmin = string.Equals(_userRole, "Admin", StringComparison.OrdinalIgnoreCase);



            btnAdd.Visible = isAdmin;
            btnUpdate.Visible = isAdmin;
            btnDelete.Visible = isAdmin;
            btnClear.Visible = isAdmin;
            dgvTimetables.ReadOnly = !isAdmin;
        }

        private void LoadSubjects()
        {
            var subjects = _subjectController.GetAllSubjects();
            cmbSubject.DataSource = subjects;
            cmbSubject.DisplayMember = "SubjectName";
            cmbSubject.ValueMember = "SubjectID";
        }

        private void LoadTimeSlots()
        {
            var slots = _timeSlotController.GetAllTimeSlots();
            cmbTimeSlot.DataSource = slots;
            cmbTimeSlot.DisplayMember = "SlotName";
            cmbTimeSlot.ValueMember = "SlotName";
        }

        private void LoadRooms(string typeFilter = "All")
        {
            var rooms = _roomController.GetAllRooms();
            if (typeFilter != "All")
                rooms = rooms.Where(r => r.RoomType.Equals(typeFilter, StringComparison.OrdinalIgnoreCase)).ToList();

            cmbRoom.DataSource = rooms;
            cmbRoom.DisplayMember = "RoomName";
            cmbRoom.ValueMember = "RoomID";
        }

        private void LoadTimetableData()
        {
            var entries = _timetableController.GetAllTimetableEntries();
            dgvTimetables.DataSource = entries;

            if (dgvTimetables.Columns["TimetableId"] != null)
                dgvTimetables.Columns["TimetableId"].Visible = false;
        }
        

        private TimetableEntryDto GetTimetableFromForm()
        {
            if (cmbSubject.SelectedItem == null || cmbTimeSlot.SelectedItem == null || cmbRoom.SelectedItem == null)
                return null;

            return new TimetableEntryDto
            {
                TimetableId = editingId ?? 0,
                SubjectId = (int)cmbSubject.SelectedValue,
                SlotId = Convert.ToInt32(cmbTimeSlot.SelectedValue),
                RoomId = Convert.ToInt32(cmbRoom.SelectedValue)
            };
        }
        

        private void btnAdd_Click(object sender, EventArgs e)
        {
           if (_userRole != "Admin")
            {
                MessageBox.Show("Only Admin can add timetable entries.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var entry = GetTimetableFromForm();
            if (entry == null)
            {
                MessageBox.Show("Please fill all fields.", "Missing Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _timetableController.AddTimetableEntry(entry);
            MessageBox.Show("Timetable entry added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ClearForm();
            LoadTimetableData();

        }

        private void dgvTimetables_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_userRole != "Admin")
                return;

            if (e.RowIndex >= 0)
            {
                var row = dgvTimetables.Rows[e.RowIndex];
                editingId = Convert.ToInt32(row.Cells["TimetableID"].Value);

                cmbSubject.SelectedValue = Convert.ToInt32(row.Cells["SubjectID"].Value);
                cmbTimeSlot.SelectedValue = Convert.ToInt32(row.Cells["TimeSlotID"].Value);
                cmbRoom.SelectedValue = Convert.ToInt32(row.Cells["RoomID"].Value);
            }
        }

        private void btnClear_TextChanged(object sender, EventArgs e)
        {
            {
                ClearForm();
            }
        }

        private void ClearForm()
        {
            cmbSubject.SelectedIndex = -1;
            cmbTimeSlot.SelectedIndex = -1;
            cmbRoom.SelectedIndex = -1;
            editingId = null;
        }

        private void cmbRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filter = cmbRoom.SelectedItem?.ToString() ?? "All";
            LoadRooms(filter);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_userRole != "Admin")
            {
                MessageBox.Show("Only Admin can delete timetable entries.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvTimetables.CurrentRow != null)
            {
                int id = Convert.ToInt32(dgvTimetables.CurrentRow.Cells["TimetableID"].Value);
                _timetableController.DeleteTimetableEntry(id);
                MessageBox.Show("Timetable entry deleted.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadTimetableData();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_userRole != "Admin")
            {
                MessageBox.Show("Only Admin can update timetable entries.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (editingId == null)
            {
                MessageBox.Show("Please select a timetable entry to update.", "Select Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var entry = GetTimetableFromForm();
            if (entry == null)
            {
                MessageBox.Show("Please fill all fields.", "Missing Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            entry.TimetableId = editingId.Value;
            _timetableController.UpdateTimetableEntry(entry);
            MessageBox.Show("Timetable entry updated successfully!", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ClearForm();
            LoadTimetableData();
        }

    }
}
