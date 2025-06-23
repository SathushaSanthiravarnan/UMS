using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models.DTOs.Grouping_MentoringDTOs;

namespace Unicom_Tic_Management_System.Services.Interfaces
{
    internal interface IMainGroupService
    {

        MainGroupDto GetMainGroupById(int mainGroupId);
        MainGroupDto GetMainGroupByCode(string groupCode);
        List<MainGroupDto> GetAllMainGroups();
        void AddMainGroup(MainGroupDto mainGroupDto);
        void UpdateMainGroup(MainGroupDto mainGroupDto);
        void DeleteMainGroup(int mainGroupId);
    }
}
