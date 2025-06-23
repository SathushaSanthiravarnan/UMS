using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;
using Unicom_Tic_Management_System.Models.DTOs.Notifications_LogsDTOs;

namespace Unicom_Tic_Management_System.Utilities.Mappers
{
    internal class NotificationMapper
    {
        public static NotificationDto ToDTO(Notification notification)
        {
            if (notification == null) return null;
            return new NotificationDto
            {
                NotificationId = notification.NotificationId,
                Title = notification.Title,
                Message = notification.Message,
                SentToUserId = notification.SentToUserId,
                SentToRole = notification.SentToRole,
                CreatedAt = notification.CreatedAt,
                IsRead = notification.IsRead
            };
        }

        public static Notification ToEntity(NotificationDto notificationDto)
        {
            if (notificationDto == null) return null;
            return new Notification
            {
                NotificationId = notificationDto.NotificationId,
                Title = notificationDto.Title,
                Message = notificationDto.Message,
                SentToUserId = notificationDto.SentToUserId,
                SentToRole = notificationDto.SentToRole,
                CreatedAt = notificationDto.CreatedAt,
                IsRead = notificationDto.IsRead
            };
        }

        public static List<NotificationDto> ToDTOList(IEnumerable<Notification> notifications)
        {
            return notifications?.Select(ToDTO).ToList() ?? new List<NotificationDto>();
        }

        public static List<Notification> ToEntityList(IEnumerable<NotificationDto> notificationDtos)
        {
            return notificationDtos?.Select(ToEntity).ToList() ?? new List<Notification>();
        }
    }
}
