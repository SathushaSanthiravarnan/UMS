using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models.DTOs.Scheduling_AttendanceDTOs;
using Unicom_Tic_Management_System.Repositories;
using Unicom_Tic_Management_System.Services;
using Unicom_Tic_Management_System.Services.Interfaces;

namespace Unicom_Tic_Management_System.Controllers
{
    internal class RoomController
    {
        private readonly IRoomService _service;

        public RoomController(IRoomService service = null)
        {
            _service = service ?? new RoomService(new RoomRepository());
        }

        public void AddRoom(RoomDto dto) => _service.AddRoom(dto);
        public void UpdateRoom(RoomDto dto) => _service.UpdateRoom(dto);
        public void DeleteRoom(int id) => _service.DeleteRoom(id);
        public RoomDto GetRoomById(int id) => _service.GetRoomById(id);
        public RoomDto GetRoomByNumber(string number) => _service.GetRoomByNumber(number);
        public List<RoomDto> GetAllRooms() => _service.GetAllRooms();
        public List<RoomDto> GetRoomsByType(string type) => _service.GetRoomsByType(type);
        public List<RoomDto> GetRoomsByCapacity(int capacity) => _service.GetRoomsByCapacity(capacity);
    }
}

