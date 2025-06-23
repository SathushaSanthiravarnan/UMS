using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unicom_Tic_Management_System.Models.DTOs.UserDtos;
using Unicom_Tic_Management_System.Repositories.Interfaces;
using Unicom_Tic_Management_System.Services.Interfaces;
using Unicom_Tic_Management_System.Utilities.Mappers;
using Unicom_Tic_Management_System.Utilities.Mappers.UnicomTIC_Management.Utilities;

namespace Unicom_Tic_Management_System.Services
{
    internal class NicDetailsService : INicDetailsService
    {
        private readonly INicDetailsRepository _repository;

        public NicDetailsService(INicDetailsRepository repository)
        {
            _repository = repository;
        }

        public void AddNicDetail(NicDetailDTO nicDetailDTO)
        {
            if (nicDetailDTO == null)
                throw new ArgumentNullException(nameof(nicDetailDTO));
            if (string.IsNullOrWhiteSpace(nicDetailDTO.Nic))
                throw new ArgumentException("NIC is required.");

            
            var exists = _repository.GetNICDetailByNIC(nicDetailDTO.Nic);
            if (exists != null)
                throw new InvalidOperationException($"NIC '{nicDetailDTO.Nic}' already exists.");

            
            var entity = NicDetailMapper.ToEntity(nicDetailDTO);

            
            try
            {
                _repository.AddNICDetail(entity);  // ✅ entity version
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Service error adding NIC detail.", ex);
            }
        }

        public void UpdateNicDetail(NicDetailDTO nicDetailDTO)
        {
            if (nicDetailDTO == null)
                throw new ArgumentNullException(nameof(nicDetailDTO));
            if (string.IsNullOrWhiteSpace(nicDetailDTO.Nic))
                throw new ArgumentException("NIC cannot be empty.", nameof(nicDetailDTO.Nic));

            
            var existingNic = _repository.GetNICDetailByNIC(nicDetailDTO.Nic);
            if (existingNic == null)
            {
                throw new KeyNotFoundException($"NIC '{nicDetailDTO.Nic}' not found for update.");
            }

            var nicDetail = NicDetailMapper.ToEntity(nicDetailDTO);
            try
            {
                _repository.UpdateNICDetail(nicDetail);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Service error updating NIC detail.", ex);
            }
        }

        public void DeleteNicDetail(string nic)
        {
            if (string.IsNullOrWhiteSpace(nic))
                throw new ArgumentException("NIC cannot be empty.", nameof(nic));

            
            if (_repository.GetNICDetailByNIC(nic) == null)
            {
                throw new KeyNotFoundException($"NIC '{nic}' not found for deletion.");
            }

            try
            {
                _repository.DeleteNICDetail(nic);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Service error deleting NIC detail.", ex);
            }
        }

        public NicDetailDTO GetNicDetailByNic(string nic)
        {
            if (string.IsNullOrWhiteSpace(nic))
                throw new ArgumentException("NIC cannot be empty.", nameof(nic));

            try
            {
                var nicDetail = _repository.GetNICDetailByNIC(nic);
                return NicDetailMapper.ToDTO(nicDetail);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Service error retrieving NIC detail.", ex);
            }
        }

        public List<NicDetailDTO> GetAllNicDetails()
        {
            try
            {
                var nicDetails = _repository.GetAllNICDetails();
                return NicDetailMapper.ToDTOList(nicDetails);
            }
            catch (Exception ex)
            {
                MessageBox.Show("🔥 Actual error:\n" + ex.ToString(), "Debug Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                throw new ApplicationException("Service error retrieving all NIC details.", ex);
            }
        }

        public bool IsNicAvailableForRegistration(string nic)
        {
            if (string.IsNullOrWhiteSpace(nic))
                throw new ArgumentException("NIC cannot be empty.", nameof(nic));

            var nicDetail = GetNicDetailByNic(nic); 
            return nicDetail != null && !nicDetail.IsUsed;
        }
    }
}
