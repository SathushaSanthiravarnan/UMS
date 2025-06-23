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
using Unicom_Tic_Management_System.Models.DTOs.UserDtos;
using Unicom_Tic_Management_System.Repositories;
using Unicom_Tic_Management_System.Repositories.Interfaces;
using Unicom_Tic_Management_System.Services;


namespace Unicom_Tic_Management_System.ViewForms
{
    public partial class NicDetailForm : Form
    {
        private readonly NicDetailsController _controller;
        private string _selectedNic = null;

        public NicDetailForm()
        {
            InitializeComponent();
            _controller = new NicDetailsController(new NicDetailsService(new NICDetailsRepository()));
            LoadNicDetails();
        }

        private void LoadNicDetails()
        {
            try
            {
                dgvNIC.DataSource = _controller.GetAll();
                dgvNIC.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading NIC details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string nic = txtNic.Text.Trim();
                bool isUsed = chkIsUsed.Checked;

                if (_controller.GetByNic(nic) != null)
                {
                    MessageBox.Show("NIC already exists!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _controller.Add(new NicDetailDTO { Nic = nic, IsUsed = isUsed });
                LoadNicDetails();
                ClearForm();
                MessageBox.Show("NIC added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Add Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            _selectedNic = null;
            txtNic.Clear();
            chkIsUsed.Checked = false;
            txtNic.Enabled = true;
            dgvNIC.ClearSelection();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedNic == null)
            {
                MessageBox.Show("Select NIC to update", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                string nic = txtNic.Text.Trim();
                bool isUsed = chkIsUsed.Checked;

                _controller.Update(new NicDetailDTO { Nic = nic, IsUsed = isUsed });
                LoadNicDetails();
                ClearForm();
                MessageBox.Show("NIC updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Update Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedNic == null)
            {
                MessageBox.Show("Select NIC to delete", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("Delete selected NIC?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    _controller.Delete(_selectedNic);
                    LoadNicDetails();
                    ClearForm();
                    MessageBox.Show("NIC deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Delete Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvNIC_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void NicDetailForm_Load(object sender, EventArgs e)
        {

        }

        private void dgvNIC_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvNIC.SelectedRows.Count > 0 && dgvNIC.SelectedRows[0].DataBoundItem is NicDetailDTO dto)
            {
                _selectedNic = dto.Nic;
                txtNic.Text = dto.Nic;
                chkIsUsed.Checked = dto.IsUsed;
                txtNic.Enabled = false;
            }
        }
    }
}
