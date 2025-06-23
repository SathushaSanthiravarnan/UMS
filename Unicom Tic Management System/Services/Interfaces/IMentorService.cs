using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models.DTOs.Grouping_MentoringDTOs;

namespace Unicom_Tic_Management_System.Services.Interfaces
{
    internal interface IMentorService
    {

        MentorDto GetMentorById(int mentorId);
        MentorDto GetMentorByNic(string nic);
        List<MentorDto> GetAllMentors();
        List<MentorDto> GetMentorsByDepartment(int departmentId);
        void AddMentor(MentorDto mentorDto);
        void UpdateMentor(MentorDto mentorDto);
        void DeleteMentor(int mentorId);

    }
}
