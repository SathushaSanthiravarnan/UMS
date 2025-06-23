using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models.DTOs.ActivityLogDto;
using Unicom_Tic_Management_System.Services.Interfaces;

namespace Unicom_Tic_Management_System.Controllers
{
    internal class ActivityLogController
    {
        private readonly IActivityLogService _service;

        public ActivityLogController(IActivityLogService service)
        {
            _service = service;
        }

        public void AddLog(ActivityLogDto logDto)
        {
            if (logDto == null)
                throw new ArgumentNullException(nameof(logDto));

            _service.AddLog(logDto);
        }

        public ActivityLogDto GetLogById(int logId)
        {
            return _service.GetLogById(logId);
        }

        public List<ActivityLogDto> GetAllLogs()
        {
            return _service.GetAllLogs();
        }

        public List<ActivityLogDto> GetLogsByUserId(int userId)
        {
            return _service.GetLogsByUserId(userId);
        }

        public List<ActivityLogDto> GetLogsByAction(string action)
        {
            return _service.GetLogsByAction(action);
        }

        public List<ActivityLogDto> GetLogsByDateRange(DateTime start, DateTime end)
        {
            return _service.GetLogsByDateRange(start, end);
        }
    }
}

