using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models.DTOs.Scheduling_AttendanceDTOs;

namespace Unicom_Tic_Management_System.Services.Interfaces
{
    internal interface IRoomService
    {
        RoomDto GetRoomById(int roomId);
        RoomDto GetRoomByNumber(string roomNumber);
        List<RoomDto> GetAllRooms();
        List<RoomDto> GetRoomsByType(string roomType);
        List<RoomDto> GetRoomsByCapacity(int minCapacity);
        void AddRoom(RoomDto roomDto);
        void UpdateRoom(RoomDto roomDto);
        void DeleteRoom(int roomId);
    }
}
