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
using Unicom_Tic_Management_System.Models;
using Unicom_Tic_Management_System.Repositories;
using Unicom_Tic_Management_System.Services;

namespace Unicom_Tic_Management_System.ViewForms
{
    public partial class TimeSlotForm : Form
    {
        private readonly TimeSlotController _controller;
        private int _selectedTimeSlotId = -1;
        public TimeSlotForm()
        {
            InitializeComponent();
            _controller = new TimeSlotController(new TimeSlotService(new TimeSlotRepository()));
            LoadTimeSlots();
        }

        private void LoadTimeSlots()
        {
            try
            {
                var timeSlots = _controller.GetAllTimeSlots();
                dgvTimeSlots.DataSource = null;
                dgvTimeSlots.DataSource = timeSlots;

                cmbTimeSlots.DataSource = null;
                cmbTimeSlots.DataSource = timeSlots;
                cmbTimeSlots.DisplayMember = "SlotName";
                cmbTimeSlots.ValueMember = "TimeSlotId";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading time slots: " + ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string slotName = txtSlotName.Text.Trim();
                string startTime = dtpStartTime.Value.ToString("HH:mm");
                string endTime = dtpEndTime.Value.ToString("HH:mm");

                if (string.IsNullOrWhiteSpace(slotName))
                {
                    MessageBox.Show("Slot name is required.");
                    return;
                }

                if (!IsValidSlotNameFormat(slotName))
                {
                    MessageBox.Show("Slot name must be in format HH:mm - HH:mm (e.g., 09:00 - 10:00)");
                    return;
                }


                if (startTime == endTime || DateTime.Parse(startTime) >= DateTime.Parse(endTime))
                {
                    MessageBox.Show("Start time must be earlier than end time.");
                    return;
                }

                if (DateTime.Parse(startTime) > DateTime.Parse(endTime))
                {
                    MessageBox.Show("Start time must be before end time.");
                    return;
                }

                var timeSlot = new TimeSlot
                {
                    SlotName = slotName,
                    StartTime = startTime,
                    EndTime = endTime
                };

                _controller.AddTimeSlot(timeSlot);
                MessageBox.Show("Time slot added successfully.");
                LoadTimeSlots();
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedTimeSlotId < 0)
            {
                MessageBox.Show("Please select a time slot to update.");
                return;
            }

            try
            {
                string slotName = txtSlotName.Text.Trim();
                string startTime = dtpStartTime.Value.ToString("HH:mm");
                string endTime = dtpEndTime.Value.ToString("HH:mm");

                if (!IsValidSlotNameFormat(slotName))
                {
                    MessageBox.Show("Slot name must be in format HH:mm - HH:mm (e.g., 09:00 - 10:00)");
                    return;
                }

                if (string.IsNullOrWhiteSpace(slotName))
                {
                    MessageBox.Show("Slot name is required.");
                    return;
                }

                if (startTime == endTime)
                {
                    MessageBox.Show("Start and End time cannot be the same.");
                    return;
                }

                if (DateTime.Parse(startTime) > DateTime.Parse(endTime))
                {
                    MessageBox.Show("Start time must be before end time.");
                    return;
                }

                var timeSlot = new TimeSlot
                {
                    TimeSlotId = _selectedTimeSlotId,
                    SlotName = slotName,
                    StartTime = startTime,
                    EndTime = endTime
                };

                _controller.UpdateTimeSlot(timeSlot);
                MessageBox.Show("Time slot updated successfully.");
                LoadTimeSlots();
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedTimeSlotId < 0)
            {
                MessageBox.Show("Please select a time slot to delete.");
                return;
            }

            try
            {
                _controller.DeleteTimeSlot(_selectedTimeSlotId);
                MessageBox.Show("Time slot deleted successfully.");
                LoadTimeSlots();
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void dgvTimeSlots_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTimeSlots.CurrentRow == null)
                return;

            var selected = dgvTimeSlots.CurrentRow.DataBoundItem as TimeSlot;
            if (selected != null)
            {
                _selectedTimeSlotId = selected.TimeSlotId;
                txtSlotName.Text = selected.SlotName;
                dtpStartTime.Value = DateTime.Parse(selected.StartTime);
                dtpEndTime.Value = DateTime.Parse(selected.EndTime);
            }
        }

        private void ClearInputs()
        {
            txtSlotName.Text = "";
            dtpStartTime.Value = DateTime.Today.AddHours(9);
            dtpEndTime.Value = DateTime.Today.AddHours(10);
            _selectedTimeSlotId = -1;
        }

        private void cmbTimeSlots_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTimeSlots.SelectedItem is TimeSlot selectedSlot)
            {
                txtSlotName.Text = selectedSlot.SlotName;
                dtpStartTime.Value = DateTime.Today.Add(TimeSpan.Parse(selectedSlot.StartTime));
                dtpEndTime.Value = DateTime.Today.Add(TimeSpan.Parse(selectedSlot.EndTime));
            }
        }

        private void dtpStartTime_ValueChanged(object sender, EventArgs e)
        {
            UpdateSlotNameFromTimePickers();
        }

        private void dtpEndTime_ValueChanged(object sender, EventArgs e)
        {
            UpdateSlotNameFromTimePickers();
        }

        private void UpdateSlotNameFromTimePickers()
        {
            string start = dtpStartTime.Value.ToString("HH:mm");
            string end = dtpEndTime.Value.ToString("HH:mm");

            if (DateTime.Parse(start) < DateTime.Parse(end))
            {
                txtSlotName.Text = $"{start} - {end}";
            }
            else
            {
                txtSlotName.Text = ""; 
            }
        }

        private bool IsValidSlotNameFormat(string slotName)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(slotName, @"^\d{2}:\d{2} - \d{2}:\d{2}$");
        }


    }
}
