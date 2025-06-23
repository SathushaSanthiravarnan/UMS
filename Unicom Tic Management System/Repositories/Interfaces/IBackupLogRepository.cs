using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;

namespace Unicom_Tic_Management_System.Repositories.Interfaces
{
    internal interface IBackupLogRepository
    {
        void AddBackupLog(BackupLog backupLog);
        BackupLog GetBackupLogById(int backupLogId);
        List<BackupLog> GetAllBackupLogs();
        List<BackupLog> GetBackupLogsByUserId(int userId);
        List<BackupLog> GetBackupLogsByStatus(string status);
        List<BackupLog> GetBackupLogsByDateRange(DateTime startDate, DateTime endDate);
    }
}
