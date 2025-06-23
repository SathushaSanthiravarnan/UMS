using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unicom_Tic_Management_System.Models.DTOs.UserDtos;
using Unicom_Tic_Management_System.Services.Interfaces;

namespace Unicom_Tic_Management_System.Controllers
{
    internal class NicDetailsController
    {
        private readonly INicDetailsService _service;

        public NicDetailsController(INicDetailsService service)
        {
            _service = service;
        }

        public List<NicDetailDTO> GetAll()
        {
            try
            {
                return _service.GetAllNicDetails();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving NIC list: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<NicDetailDTO>();
            }
        }

        public NicDetailDTO GetByNic(string nic)
        {
            try
            {
                return _service.GetNicDetailByNic(nic);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving NIC detail: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public void Add(NicDetailDTO dto)
        {
            try
            {
                _service.AddNicDetail(dto);
                MessageBox.Show("NIC added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding NIC: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Update(NicDetailDTO dto)
        {
            try
            {
                _service.UpdateNicDetail(dto);
                MessageBox.Show("NIC updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating NIC: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Delete(string nic)
        {
            try
            {
                _service.DeleteNicDetail(nic);
                MessageBox.Show("NIC deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting NIC: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void RegisterNic(string nic)
        {
            if (_service.IsNicAvailableForRegistration(nic))
            {
                MessageBox.Show("NIC is available and valid.");
            }
            else
            {
                MessageBox.Show("NIC is already used or not valid.");
            }
        }

        public void MarkAsUsed(string nic)
        {
            var nicDto = _service.GetNicDetailByNic(nic);
            if (nicDto == null)
                throw new InvalidOperationException("NIC not found.");

            nicDto.IsUsed = true;
            _service.UpdateNicDetail(nicDto);
        }
        public bool IsNicAvailableForRegistration(string nic)
        {
            try
            {
                return _service.IsNicAvailableForRegistration(nic);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking NIC availability: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }

}

