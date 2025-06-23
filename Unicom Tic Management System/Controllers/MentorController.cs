using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models.DTOs.Grouping_MentoringDTOs;
using Unicom_Tic_Management_System.Services.Interfaces;

namespace Unicom_Tic_Management_System.Controllers
{
    internal class MentorController
    {
        private readonly IMentorService _mentorService;

        public MentorController(IMentorService mentorService)
        {
            _mentorService = mentorService;
        }

        public void AddMentor(MentorDto mentor)
        {
            _mentorService.AddMentor(mentor);
        }

        public void UpdateMentor(MentorDto mentor)
        {
            _mentorService.UpdateMentor(mentor);
        }

        public void DeleteMentor(int mentorId)
        {
            _mentorService.DeleteMentor(mentorId);
        }

        public MentorDto GetMentorById(int id)
        {
            return _mentorService.GetMentorById(id);
        }

        public MentorDto GetMentorByNic(string nic)
        {
            return _mentorService.GetMentorByNic(nic);
        }

        public List<MentorDto> GetAllMentors()
        {
            return _mentorService.GetAllMentors();
        }

        public List<MentorDto> GetMentorsByDepartment(int departmentId)
        {
            return _mentorService.GetMentorsByDepartment(departmentId);
        }
    }
}
