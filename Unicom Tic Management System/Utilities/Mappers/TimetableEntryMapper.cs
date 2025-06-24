using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;
using Unicom_Tic_Management_System.Models.DTOs.Scheduling_AttendanceDTOs;

namespace Unicom_Tic_Management_System.Utilities.Mappers
{
    public static class TimetableEntryMapper
    {
        /// <summary>
        /// Converts a TimetableEntryDto to a Timetable entity.
        /// </summary>
        /// <param name="dto">The DTO object to convert.</param>
        /// <returns>A Timetable entity.</returns>
        public static Timetable ToEntity(TimetableEntryDto dto)
        {
            if (dto == null)
            {
                return null;
            }

            return new Timetable
            {
                TimetableEntryId = dto.TimetableId,
                CourseId = dto.CourseId,
                LecturerId = dto.LecturerId,
                RoomId = dto.RoomId,
                MainGroupId = dto.MainGroupId,
                SubGroupId = dto.SubGroupId,
                SlotId = dto.SlotId,
                Date = dto.Date,
                // These fields are typically populated when reading from DB, not when converting DTO to entity for write operations
                // Ensure your database operations don't rely on these being set here during Add/Update.
                CourseName = dto.CourseName,
                LecturerName = dto.LecturerName,
                RoomNumber = dto.RoomNumber,
                MainGroupName = dto.MainGroupName,
                SubGroupName = dto.SubGroupName,
                SlotTimeRange = dto.SlotTimeRange
            };
        }

        /// <summary>
        /// Converts a Timetable entity to a TimetableEntryDto.
        /// </summary>
        /// <param name="entity">The Timetable entity to convert.</param>
        /// <returns>A TimetableEntryDto object.</returns>
        public static TimetableEntryDto ToDTO(Timetable entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new TimetableEntryDto
            {
                TimetableId = entity.TimetableEntryId,
                CourseId = entity.CourseId,
                CourseName = entity.CourseName,
                LecturerId = entity.LecturerId,
                LecturerName = entity.LecturerName,
                RoomId = entity.RoomId,
                RoomNumber = entity.RoomNumber,
                MainGroupId = entity.MainGroupId,
                MainGroupName = entity.MainGroupName,
                SubGroupId = entity.SubGroupId,
                SubGroupName = entity.SubGroupName,
                SlotId = entity.SlotId,
                SlotTimeRange = entity.SlotTimeRange,
                Date = entity.Date
            };
        }

        /// <summary>
        /// Converts a list of Timetable entities to a list of TimetableEntryDto objects.
        /// </summary>
        /// <param name="entities">The list of Timetable entities to convert.</param>
        /// <returns>A list of TimetableEntryDto objects.</returns>
        public static List<TimetableEntryDto> ToDTOList(List<Timetable> entities)
        {
            if (entities == null)
            {
                return new List<TimetableEntryDto>();
            }
            return entities.ConvertAll(ToDTO);
        }
    }
}
