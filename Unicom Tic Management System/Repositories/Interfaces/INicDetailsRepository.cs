using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;
using Unicom_Tic_Management_System.Models.DTOs.UserDtos;

namespace Unicom_Tic_Management_System.Repositories.Interfaces
{
    internal interface INicDetailsRepository
    {
        NicDetail GetNICDetailByNIC(string nic);
        void AddNICDetail(NicDetail nicDetail);
        void UpdateNICDetail(NicDetail nicDetail);
        void DeleteNICDetail(string nic);
        List<NicDetail> GetAllNICDetails();        
        void MarkAsUsed(string nic);
        bool NicExists(string nic);
    }
}
