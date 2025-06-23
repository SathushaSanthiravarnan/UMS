using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;
using Unicom_Tic_Management_System.Models.DTOs.Scheduling_AttendanceDTOs;

namespace Unicom_Tic_Management_System.Utilities.Mappers
{
    internal class RoomMapper
    {
        public static RoomDto ToDTO(Room room)
        {
            if (room == null) return null;
            return new RoomDto
            {
                RoomId = room.RoomId,
                RoomNumber = room.RoomNumber,
                Capacity = room.Capacity,
                RoomType = room.RoomType
            };
        }

        public static Room ToEntity(RoomDto roomDto)
        {
            if (roomDto == null) return null;
            return new Room
            {
                RoomId = roomDto.RoomId,
                RoomNumber = roomDto.RoomNumber,
                Capacity = roomDto.Capacity,
                RoomType = roomDto.RoomType
            };
        }

        public static List<RoomDto> ToDTOList(IEnumerable<Room> rooms)
        {
            return rooms?.Select(ToDTO).ToList() ?? new List<RoomDto>();
        }

        public static List<Room> ToEntityList(IEnumerable<RoomDto> roomDtos)
        {
            return roomDtos?.Select(ToEntity).ToList() ?? new List<Room>();
        }
    }
}
