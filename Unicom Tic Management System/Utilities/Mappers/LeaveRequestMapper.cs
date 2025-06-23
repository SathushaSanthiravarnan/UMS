using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;
using Unicom_Tic_Management_System.Models.DTOs.Notifications_LogsDTOs;
using Unicom_Tic_Management_System.Models.Enums;

namespace Unicom_Tic_Management_System.Utilities.Mappers
{
    internal class LeaveRequestMapper
    {
        public static LeaveRequestDto ToDTO(LeaveRequest leaveRequest)
        {
            if (leaveRequest == null) return null;
            return new LeaveRequestDto
            {
                LeaveId = leaveRequest.LeaveId,
                UserId = leaveRequest.UserId,
                Reason = leaveRequest.Reason,
                StartDate = leaveRequest.StartDate,
                EndDate = leaveRequest.EndDate,
                CreatedAt = leaveRequest.CreatedAt,
                Status = (LeaveStatus)leaveRequest.Status, // Cast entity enum to DTO enum
                ApprovedByUserId = leaveRequest.ApprovedByUserId
            };
        }

        public static LeaveRequest ToEntity(LeaveRequestDto leaveRequestDto)
        {
            if (leaveRequestDto == null) return null;
            return new LeaveRequest
            {
                LeaveId = leaveRequestDto.LeaveId,
                UserId = leaveRequestDto.UserId,
                Reason = leaveRequestDto.Reason,
                StartDate = leaveRequestDto.StartDate,
                EndDate = leaveRequestDto.EndDate,
                CreatedAt = leaveRequestDto.CreatedAt,
                Status = (LeaveStatus)leaveRequestDto.Status, // Cast DTO enum to entity enum
                ApprovedByUserId = leaveRequestDto.ApprovedByUserId
            };
        }

        public static List<LeaveRequestDto> ToDTOList(IEnumerable<LeaveRequest> leaveRequests)
        {
            return leaveRequests?.Select(ToDTO).ToList() ?? new List<LeaveRequestDto>();
        }

        public static List<LeaveRequest> ToEntityList(IEnumerable<LeaveRequestDto> leaveRequestDtos)
        {
            return leaveRequestDtos?.Select(ToEntity).ToList() ?? new List<LeaveRequest>();
        }
    }
}
