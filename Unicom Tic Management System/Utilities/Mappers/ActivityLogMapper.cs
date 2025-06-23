using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;
using Unicom_Tic_Management_System.Models.DTOs.ActivityLogDto;

namespace Unicom_Tic_Management_System.Utilities.Mappers
{
    internal static class ActivityLogMapper
    {
        public static ActivityLogDto ToDTO(ActivityLog log)
        {
            if (log == null) return null;
            return new ActivityLogDto
            {
                LogId = log.LogId,
                UserId = log.UserId,
                Action = log.Action,
                CreatedAt = log.CreatedAt
            };
        }

        public static ActivityLog ToEntity(ActivityLogDto logDto)
        {
            if (logDto == null) return null;
            return new ActivityLog
            {
                LogId = logDto.LogId,
                UserId = logDto.UserId,
                Action = logDto.Action,
                CreatedAt = logDto.CreatedAt
            };
        }

        public static List<ActivityLogDto> ToDTOList(IEnumerable<ActivityLog> logs)
        {
            return logs?.Select(l => ToDTO(l)).ToList() ?? new List<ActivityLogDto>();
        }

        public static List<ActivityLog> ToEntityList(IEnumerable<ActivityLogDto> logDtos)
        {
            return logDtos?.Select(l => ToEntity(l)).ToList() ?? new List<ActivityLog>();
        }
    }
}
