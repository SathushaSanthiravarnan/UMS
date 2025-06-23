using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;

namespace Unicom_Tic_Management_System.Repositories.Interfaces
{
    internal interface IRoomRepository
    {
        void AddRoom(Room room);
        void UpdateRoom(Room room);
        void DeleteRoom(int roomId);
        Room GetRoomById(int roomId);
        Room GetRoomByRoomNumber(string roomNumber);
        List<Room> GetRoomsByType(string roomType);
        List<Room> GetRoomsByMinimumCapacity(int minCapacity);
        List<Room> GetAllRooms();

    }
}
