using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;

namespace Unicom_Tic_Management_System.Repositories.Interfaces
{
    internal interface ISubGroupRepository
    {
        void AddSubGroup(SubGroup subGroup);
        void UpdateSubGroup(SubGroup subGroup);
        void DeleteSubGroup(int subGroupId);
        SubGroup GetSubGroupById(int subGroupId);
        SubGroup GetSubGroupByNameAndMainGroup(string subGroupName, int mainGroupId);
        List<SubGroup> GetAllSubGroups();
    }
}
