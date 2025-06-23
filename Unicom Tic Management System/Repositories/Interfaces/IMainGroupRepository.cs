using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;

namespace Unicom_Tic_Management_System.Repositories.Interfaces
{
    internal interface IMainGroupRepository
    {
        void AddMainGroup(MainGroup mainGroup);
        void UpdateMainGroup(MainGroup mainGroup);
        void DeleteMainGroup(int mainGroupId);
        MainGroup GetMainGroupById(int mainGroupId);
        MainGroup GetMainGroupByGroupCode(string groupCode);
        List<MainGroup> GetAllMainGroups();
    }
}
