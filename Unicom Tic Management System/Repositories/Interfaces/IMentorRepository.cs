using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;

namespace Unicom_Tic_Management_System.Repositories.Interfaces
{
    internal interface IMentorRepository
    {
        void AddMentor(Mentor mentor);
        void UpdateMentor(Mentor mentor);
        void DeleteMentor(int mentorId);
        Mentor GetMentorById(int mentorId);
        Mentor GetMentorByNic(string nic);
        Mentor GetMentorByEmail(string email);
        Mentor GetMentorByUserId(int userId); 
        List<Mentor> GetMentorsByDepartmentId(int departmentId);
        List<Mentor> GetAllMentors();
    }
}
