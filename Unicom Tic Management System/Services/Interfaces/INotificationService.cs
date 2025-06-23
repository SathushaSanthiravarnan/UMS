using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models.DTOs.Notifications_LogsDTOs;
using Unicom_Tic_Management_System.Models.Enums;

namespace Unicom_Tic_Management_System.Services.Interfaces
{
    internal interface INotificationService
    {
        NotificationDto GetNotificationById(int notificationId);
        List<NotificationDto> GetAllNotifications();
        List<NotificationDto> GetNotificationsForUser(int userId);
        List<NotificationDto> GetNotificationsForRole(UserRole role); 
        List<NotificationDto> GetUnreadNotificationsForUser(int userId);
        void AddNotification(NotificationDto notificationDto);
        void UpdateNotification(NotificationDto notificationDto); 
        void DeleteNotification(int notificationId);
        void MarkNotificationAsRead(int notificationId);
    }
}
