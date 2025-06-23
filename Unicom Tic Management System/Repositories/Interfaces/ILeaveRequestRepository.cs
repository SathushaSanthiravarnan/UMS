using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;
using Unicom_Tic_Management_System.Models.Enums;

namespace Unicom_Tic_Management_System.Repositories.Interfaces
{
    internal interface ILeaveRequestRepository
    {
        void AddLeaveRequest(LeaveRequest leaveRequest);
        void UpdateLeaveRequest(LeaveRequest leaveRequest); // For updating all fields, including status/approver
        void DeleteLeaveRequest(int leaveId);
        LeaveRequest GetLeaveRequestById(int leaveId);
        List<LeaveRequest> GetLeaveRequestsByUserId(int userId);
        List<LeaveRequest> GetLeaveRequestsByStatus(LeaveStatus status);
        List<LeaveRequest> GetLeaveRequestsByDateRange(DateTime startDate, DateTime endDate);
        List<LeaveRequest> GetAllLeaveRequests();
    }
}
