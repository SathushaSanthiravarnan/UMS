using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models.DTOs.Scheduling_AttendanceDTOs;
using Unicom_Tic_Management_System.Repositories.Interfaces;
using Unicom_Tic_Management_System.Services.Interfaces;
using Unicom_Tic_Management_System.Utilities.Mappers;

namespace Unicom_Tic_Management_System.Services
{
    internal class RoomService : IRoomService
    {
        private readonly IRoomRepository _repository;

        public RoomService(IRoomRepository repository)
        {
            _repository = repository;
        }

        public void AddRoom(RoomDto roomDto)
        {
            if (roomDto == null) throw new ArgumentNullException(nameof(roomDto));
            var room = RoomMapper.ToEntity(roomDto);
            _repository.AddRoom(room);
        }

        public void UpdateRoom(RoomDto roomDto)
        {
            if (roomDto == null) throw new ArgumentNullException(nameof(roomDto));
            var room = RoomMapper.ToEntity(roomDto);
            _repository.UpdateRoom(room);
        }

        public void DeleteRoom(int roomId)
        {
            _repository.DeleteRoom(roomId);
        }

        public RoomDto GetRoomById(int roomId)
        {
            return RoomMapper.ToDTO(_repository.GetRoomById(roomId));
        }

        public RoomDto GetRoomByNumber(string roomNumber)
        {
            return RoomMapper.ToDTO(_repository.GetRoomByRoomNumber(roomNumber));
        }

        public List<RoomDto> GetAllRooms()
        {
            return RoomMapper.ToDTOList(_repository.GetAllRooms());
        }

        public List<RoomDto> GetRoomsByType(string roomType)
        {
            return RoomMapper.ToDTOList(_repository.GetRoomsByType(roomType));
        }

        public List<RoomDto> GetRoomsByCapacity(int minCapacity)
        {
            return RoomMapper.ToDTOList(_repository.GetRoomsByMinimumCapacity(minCapacity));
        }
    }
}
