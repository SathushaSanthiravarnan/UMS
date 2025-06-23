using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models.DTOs.Notifications_LogsDTOs;

namespace Unicom_Tic_Management_System.Services.Interfaces
{
    internal interface IStudentComplaintService
    {
        StudentComplaintDto GetComplaintById(int complaintId);
        List<StudentComplaintDto> GetAllComplaints();
        List<StudentComplaintDto> GetComplaintsByStudent(int studentId);
        List<StudentComplaintDto> GetComplaintsByStatus(string status); 
        void AddComplaint(StudentComplaintDto complaintDto);
        void UpdateComplaint(StudentComplaintDto complaintDto); 
        void ResolveComplaint(int complaintId, int resolvedByUserId, string resolutionNotes);
        void DeleteComplaint(int complaintId);
    }
}
