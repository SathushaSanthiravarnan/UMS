using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;

namespace Unicom_Tic_Management_System.Repositories.Interfaces
{
    internal interface IGroupMentorRepository
    {
        void AddGroupMentor(GroupMentor groupMentor);
        void DeleteGroupMentor(int subGroupId, int mentorId);
        void UpdateGroupMentorAssignedDate(int subGroupId, int mentorId, DateTime? newAssignedDate); // Specific update for nullable date
        GroupMentor GetGroupMentor(int subGroupId, int mentorId);
        List<GroupMentor> GetGroupMentorsBySubGroupId(int subGroupId);
        List<GroupMentor> GetGroupMentorsByMentorId(int mentorId);
        List<GroupMentor> GetAllGroupMentors();

    }
}
