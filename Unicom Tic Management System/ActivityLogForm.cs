using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unicom_Tic_Management_System.Repositories;
using Unicom_Tic_Management_System.Services;
using Unicom_Tic_Management_System.Services.Interfaces;

namespace Unicom_Tic_Management_System
{
    public partial class ActivityLogForm : Form
    {
        private readonly IActivityLogService _service;

        public ActivityLogForm()
        {
            InitializeComponent();
            _service = new ActivityLogService(new ActivityLogRepository()); ;
        }

        private void ActivityLogForm_Load(object sender, EventArgs e)
        {
            LoadAllLogs();
        }

        private void LoadAllLogs()
        {
            try
            {
                var logs = _service.GetAllLogs();
                dgvLogs.DataSource = logs;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading logs: {ex.Message}");
            }
        }

        private void btnSearchByAction_Click(object sender, EventArgs e)
        {
            try
            {
                string actionKeyword = txtSearchAction.Text.Trim();
                var logs = _service.GetLogsByAction(actionKeyword);
                dgvLogs.DataSource = logs;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Search failed: {ex.Message}");
            }
        }

        private void btnFilterByDate_Click(object sender, EventArgs e)
        {
            try
            {
                var fromDate = dtpStart.Value.Date;
                var toDate = dtpEnd.Value.Date.AddDays(1).AddSeconds(-1); // include full end day
                var logs = _service.GetLogsByDateRange(fromDate, toDate);
                dgvLogs.DataSource = logs;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Date filter failed: {ex.Message}");
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadAllLogs();
        }
    }
}
