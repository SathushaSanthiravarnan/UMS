using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models.DTOs.Grouping_MentoringDTOs;

namespace Unicom_Tic_Management_System.Services.Interfaces
{
    internal interface ISubGroupService
    {
        SubGroupDto GetSubGroupById(int subGroupId);
        List<SubGroupDto> GetSubGroupsByMainGroup(int mainGroupId);
        List<SubGroupDto> GetAllSubGroups();
        void AddSubGroup(SubGroupDto subGroupDto);
        void UpdateSubGroup(SubGroupDto subGroupDto);
        void DeleteSubGroup(int subGroupId);
    }
}
