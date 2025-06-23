using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;

namespace Unicom_Tic_Management_System.Repositories.Interfaces
{
    internal interface IActivityLogRepository
    {
        void AddLog(ActivityLog log);
        ActivityLog GetLogById(int logId);
        List<ActivityLog> GetLogsByUserId(int userId);
        List<ActivityLog> GetAllLogs();
    }
}
