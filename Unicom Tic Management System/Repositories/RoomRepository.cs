using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Datas;
using Unicom_Tic_Management_System.Models;
using Unicom_Tic_Management_System.Repositories.Interfaces;

namespace Unicom_Tic_Management_System.Repositories
{
    internal class RoomRepository : IRoomRepository
    {
        public void AddRoom(Room room)
        {
            if (room == null) throw new ArgumentNullException(nameof(room));

            using (var connection = DatabaseManager.GetConnection())
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"INSERT INTO Rooms (RoomNumber, RoomType, Capacity) VALUES (@RoomNumber, @RoomType, @Capacity)";
                cmd.Parameters.AddWithValue("@RoomNumber", room.RoomNumber);
                cmd.Parameters.AddWithValue("@RoomType", room.RoomType);
                cmd.Parameters.AddWithValue("@Capacity", room.Capacity);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateRoom(Room room)
        {
            if (room == null) throw new ArgumentNullException(nameof(room));

            using (var connection = DatabaseManager.GetConnection())
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"UPDATE Rooms SET RoomNumber = @RoomNumber, RoomType = @RoomType, Capacity = @Capacity WHERE RoomId = @RoomId";
                cmd.Parameters.AddWithValue("@RoomId", room.RoomId);
                cmd.Parameters.AddWithValue("@RoomNumber", room.RoomNumber);
                cmd.Parameters.AddWithValue("@RoomType", room.RoomType);
                cmd.Parameters.AddWithValue("@Capacity", room.Capacity);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteRoom(int roomId)
        {
            using (var connection = DatabaseManager.GetConnection())
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = "DELETE FROM Rooms WHERE RoomId = @RoomId";
                cmd.Parameters.AddWithValue("@RoomId", roomId);
                cmd.ExecuteNonQuery();
            }
        }

        public Room GetRoomById(int roomId)
        {
            using (var connection = DatabaseManager.GetConnection())
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM Rooms WHERE RoomId = @RoomId";
                cmd.Parameters.AddWithValue("@RoomId", roomId);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Room
                        {
                            RoomId = reader.GetInt32(0),
                            RoomNumber = reader.GetString(1),
                            RoomType = reader.GetString(2),
                            Capacity = reader.GetInt32(3)
                        };
                    }
                }
            }
            return null;
        }

        public Room GetRoomByRoomNumber(string roomNumber)
        {
            using (var connection = DatabaseManager.GetConnection())
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM Rooms WHERE RoomNumber = @RoomNumber";
                cmd.Parameters.AddWithValue("@RoomNumber", roomNumber);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Room
                        {
                            RoomId = reader.GetInt32(0),
                            RoomNumber = reader.GetString(1),
                            RoomType = reader.GetString(2),
                            Capacity = reader.GetInt32(3)
                        };
                    }
                }
            }
            return null;
        }

        public List<Room> GetRoomsByType(string roomType)
        {
            var rooms = new List<Room>();
            using (var connection = DatabaseManager.GetConnection())
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM Rooms WHERE RoomType = @RoomType";
                cmd.Parameters.AddWithValue("@RoomType", roomType);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rooms.Add(new Room
                        {
                            RoomId = reader.GetInt32(0),
                            RoomNumber = reader.GetString(1),
                            RoomType = reader.GetString(2),
                            Capacity = reader.GetInt32(3)
                        });
                    }
                }
            }
            return rooms;
        }

        public List<Room> GetRoomsByMinimumCapacity(int minCapacity)
        {
            var rooms = new List<Room>();
            using (var connection = DatabaseManager.GetConnection())
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM Rooms WHERE Capacity >= @MinCapacity";
                cmd.Parameters.AddWithValue("@MinCapacity", minCapacity);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rooms.Add(new Room
                        {
                            RoomId = reader.GetInt32(0),
                            RoomNumber = reader.GetString(1),
                            RoomType = reader.GetString(2),
                            Capacity = reader.GetInt32(3)
                        });
                    }
                }
            }
            return rooms;
        }

        public List<Room> GetAllRooms()
        {
            var rooms = new List<Room>();
            using (var connection = DatabaseManager.GetConnection())
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM Rooms ORDER BY RoomNumber ASC";

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rooms.Add(new Room
                        {
                            RoomId = reader.GetInt32(0),
                            RoomNumber = reader.GetString(1),
                            RoomType = reader.GetString(2),
                            Capacity = reader.GetInt32(3)
                        });
                    }
                }
            }
            return rooms;
        }
    }
}
