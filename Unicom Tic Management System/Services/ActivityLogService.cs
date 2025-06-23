using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models.DTOs.ActivityLogDto;
using Unicom_Tic_Management_System.Repositories.Interfaces;
using Unicom_Tic_Management_System.Services.Interfaces;
using Unicom_Tic_Management_System.Utilities.Mappers;

namespace Unicom_Tic_Management_System.Services
{
    internal class ActivityLogService : IActivityLogService
    {
        private readonly IActivityLogRepository _repository;

        public ActivityLogService(IActivityLogRepository repository)
        {
            _repository = repository;
        }

        public void AddLog(ActivityLogDto logDto)
        {
            if (logDto == null)
                throw new ArgumentNullException(nameof(logDto));

            var entity = ActivityLogMapper.ToEntity(logDto);
            _repository.AddLog(entity);
        }

        public ActivityLogDto GetLogById(int logId)
        {
            var log = _repository.GetLogById(logId);
            return ActivityLogMapper.ToDTO(log);
        }

        public List<ActivityLogDto> GetAllLogs()
        {
            var logs = _repository.GetAllLogs();
            return ActivityLogMapper.ToDTOList(logs);
        }

        public List<ActivityLogDto> GetLogsByUserId(int userId)
        {
            var logs = _repository.GetLogsByUserId(userId);
            return ActivityLogMapper.ToDTOList(logs);
        }

        public List<ActivityLogDto> GetLogsByAction(string action)
        {
            if (string.IsNullOrWhiteSpace(action))
                return new List<ActivityLogDto>();

            var logs = _repository.GetAllLogs()
                .Where(l => !string.IsNullOrWhiteSpace(l.Action) &&
                            l.Action.IndexOf(action, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();

            return ActivityLogMapper.ToDTOList(logs);
        }

        public List<ActivityLogDto> GetLogsByDateRange(DateTime startDate, DateTime endDate)
        {
            var logs = _repository.GetAllLogs()
                .Where(l => l.CreatedAt >= startDate && l.CreatedAt <= endDate)
                .ToList();

            return ActivityLogMapper.ToDTOList(logs);
        }
    }
}
