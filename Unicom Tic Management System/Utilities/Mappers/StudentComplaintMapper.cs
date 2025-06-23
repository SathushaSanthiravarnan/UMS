using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;
using Unicom_Tic_Management_System.Models.DTOs.Notifications_LogsDTOs;

namespace Unicom_Tic_Management_System.Utilities.Mappers
{
    internal class StudentComplaintMapper
    {
        public static StudentComplaintDto ToDTO(StudentComplaint studentComplaint)
        {
            if (studentComplaint == null) return null;
            return new StudentComplaintDto
            {
                ComplaintId = studentComplaint.ComplaintId,
                StudentId = studentComplaint.StudentId,
                ComplaintText = studentComplaint.ComplaintText,
                CreatedAt = studentComplaint.CreatedAt,
                Status = studentComplaint.Status,
                ResolvedByUserId = studentComplaint.ResolvedByUserId,
                ResolutionNotes = studentComplaint.ResolutionNotes
            };
        }

        public static StudentComplaint ToEntity(StudentComplaintDto studentComplaintDto)
        {
            if (studentComplaintDto == null) return null;
            return new StudentComplaint
            {
                ComplaintId = studentComplaintDto.ComplaintId,
                StudentId = studentComplaintDto.StudentId,
                ComplaintText = studentComplaintDto.ComplaintText,
                CreatedAt = studentComplaintDto.CreatedAt,
                Status = studentComplaintDto.Status,
                ResolvedByUserId = studentComplaintDto.ResolvedByUserId,
                ResolutionNotes = studentComplaintDto.ResolutionNotes
            };
        }

        public static List<StudentComplaintDto> ToDTOList(IEnumerable<StudentComplaint> studentComplaints)
        {
            return studentComplaints?.Select(ToDTO).ToList() ?? new List<StudentComplaintDto>();
        }

        public static List<StudentComplaint> ToEntityList(IEnumerable<StudentComplaintDto> studentComplaintDtos)
        {
            return studentComplaintDtos?.Select(ToEntity).ToList() ?? new List<StudentComplaint>();
        }
    }
}

