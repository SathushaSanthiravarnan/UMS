using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models.DTOs.Notifications_LogsDTOs;
using Unicom_Tic_Management_System.Models.Enums;

namespace Unicom_Tic_Management_System.Services.Interfaces
{
    internal interface ILeaveRequestService
    {
        LeaveRequestDto GetLeaveRequestById(int leaveId);
        List<LeaveRequestDto> GetAllLeaveRequests();
        List<LeaveRequestDto> GetLeaveRequestsByUser(int userId);
        List<LeaveRequestDto> GetLeaveRequestsByStatus(LeaveStatus status);
        List<LeaveRequestDto> GetLeaveRequestsForApproval();
        void AddLeaveRequest(LeaveRequestDto leaveRequestDto);
        void UpdateLeaveRequest(LeaveRequestDto leaveRequestDto); 
        void ApproveLeaveRequest(int leaveId, int approvedByUserId);
        void RejectLeaveRequest(int leaveId, int approvedByUserId);
        void DeleteLeaveRequest(int leaveId);
    }
}
