using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;

namespace Unicom_Tic_Management_System.Repositories.Interfaces
{
    internal interface IGroupSubjectRepository
    {
        void AddGroupSubject(GroupSubject groupSubject);
        void DeleteGroupSubject(int subGroupId, int subjectId);
        GroupSubject GetGroupSubject(int subGroupId, int subjectId);
        List<GroupSubject> GetGroupSubjectsBySubGroupId(int subGroupId);
        List<GroupSubject> GetGroupSubjectsBySubjectId(int subjectId);
        List<GroupSubject> GetAllGroupSubjects();
    }
}
