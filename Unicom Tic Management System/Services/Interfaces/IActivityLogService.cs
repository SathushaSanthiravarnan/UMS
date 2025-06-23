using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models.DTOs.ActivityLogDto;

namespace Unicom_Tic_Management_System.Services.Interfaces
{
    internal interface IActivityLogService
    {
        ActivityLogDto GetLogById(int logId);
        List<ActivityLogDto> GetAllLogs();
        List<ActivityLogDto> GetLogsByUserId(int userId);
        List<ActivityLogDto> GetLogsByAction(string action);
        List<ActivityLogDto> GetLogsByDateRange(DateTime startDate, DateTime endDate);
        void AddLog(ActivityLogDto logDto);
    }
}
