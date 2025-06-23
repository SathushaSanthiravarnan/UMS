using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;
using Unicom_Tic_Management_System.Models.DTOs.UserDtos;

namespace Unicom_Tic_Management_System.Services.Interfaces
{
    internal interface INicDetailsService
    {
        void AddNicDetail(NicDetailDTO dto);
        void UpdateNicDetail(NicDetailDTO dto);
        void DeleteNicDetail(string nic);
        NicDetailDTO GetNicDetailByNic(string nic);
        List<NicDetailDTO> GetAllNicDetails();
        bool IsNicAvailableForRegistration(string nic);
    }
}
