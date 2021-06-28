using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Monopoly.Areas.Identity.Data;
using Monopoly.Data;
using Monopoly.Models;

namespace Monopoly.Utilities
{
    [Authorize]
    public class RoomDatabaseOperations
    {
        public RoomDatabaseOperations() { }
        
        public void AddRoom(Room room)
        {
            var gameRoomContext = new MonopolyDbContext();
            List<Room> rooms = gameRoomContext.Rooms.ToList();
            List<ConnectionIds> connections = gameRoomContext.ConnectionIds.ToList();
            foreach (Room r in rooms)
            {
                if (r.Player1 == room.Player1 || r.Player2 == room.Player1 || r.Player3 == room.Player1 || r.Player4 == room.Player1)
                {
                    gameRoomContext.Remove(r);
                    gameRoomContext.SaveChanges();
                }
            }

            foreach (ConnectionIds c in connections)
            {
                if (c.PlayerName == room.Player1)
                {
                    gameRoomContext.Remove(c);
                    gameRoomContext.SaveChanges();
                }
            }
            gameRoomContext.Rooms.Add(room);
            gameRoomContext.SaveChanges();
        }

        public void AddPlayerToRoom(int roomId, string player)
        {
            //remove all connections and rooms for this player
            var gameRoomContext = new MonopolyDbContext();

            List<Room> rooms = gameRoomContext.Rooms.ToList();
            List<ConnectionIds> connections = gameRoomContext.ConnectionIds.ToList();
            foreach(Room r in rooms)
            {
                if(r.Player1 == player || r.Player2 == player || r.Player3 == player || r.Player4 == player)
                {
                    gameRoomContext.Remove(r);
                    gameRoomContext.SaveChanges();
                }
            }

            foreach(ConnectionIds c in connections)
            {
                if(c.PlayerName == player)
                {
                    gameRoomContext.Remove(c);
                    gameRoomContext.SaveChanges();
                }
            }


            Room room = gameRoomContext.Rooms.Where(x => x.RoomId == roomId).FirstOrDefault();
            if(room != null)
            {
                if(room.Player1 == null)
                    room.Player1 = player;
                else if(room.Player2 == null)
                    room.Player2 = player;
                else if(room.Player3 == null)
                    room.Player3 = player;
                else if(room.Player4 == null)
                    room.Player4 = player;
                gameRoomContext.SaveChanges();
            }
        }

        public void RemovePlayerFromRoom(string player)
        {
            var gameRoomContext = new MonopolyDbContext();
            Room room = gameRoomContext.Rooms.Where(x => x.Player1 == player || x.Player2 == player || x.Player3 == player || x.Player4 == player).FirstOrDefault();
            if(room != null)
            {
                if(room.Player1 == player)
                    room.Player1 = null;
                else if(room.Player2 == player)
                    room.Player2 = null;
                else if(room.Player3 == player)
                    room.Player3 = null;
                else if(room.Player4 == player)
                    room.Player4 = null;
                gameRoomContext.SaveChanges();
            }
        }

        public List<Room> GetPublicRooms()
        {
            var gameRoomContext = new MonopolyDbContext();
            return gameRoomContext.Rooms.Where(x => x.Password == null).OrderBy(o => o.RoomName).ToList();
        }

        public List<Room> GetPrivateRooms()
        {
            var gameRoomContext = new MonopolyDbContext();
            return gameRoomContext.Rooms.Where(x => x.Password != null).OrderBy(o => o.RoomName).ToList();
        }

        public List<string> GetPlayersFromRoom(string player)
        {
            var gameRoomContext = new MonopolyDbContext();
            Room currentRoom = gameRoomContext.Rooms.Where(x => x.Player1 == player || x.Player2 == player || x.Player3 == player || x.Player4 == player).First();
            List<string> players = new List<string>();
            if(currentRoom.Player1 != null)
                players.Add(currentRoom.Player1);         
            if(currentRoom.Player2 != null)
                players.Add(currentRoom.Player2);
            if(currentRoom.Player3 != null)
                players.Add(currentRoom.Player3);
            if(currentRoom.Player4 != null)
                players.Add(currentRoom.Player4);   
            return players;
        }

        public List<Player> GetPlayersForGame(int roomId)
        {
            var gameRoomContext = new MonopolyDbContext();
            Room currentRoom = gameRoomContext.Rooms.Where(x => x.RoomId == roomId).FirstOrDefault();
            List<Player> players = new List<Player>();
            if(currentRoom.Player1 != null)
                players.Add(new Player
                {
                    Name = currentRoom.Player1,
                    Money = 2000,
                    Pawn = "~/images/pawns/blue.png",
                    PawnPath = "https://i.imgur.com/GJNa5Pc.png"
                });
            if(currentRoom.Player2 != null)
                players.Add(new Player
                {
                    Name = currentRoom.Player2,
                    Money = 2000,
                    Pawn = "~/images/pawns/green.png",
                    PawnPath = "https://i.imgur.com/X0a0MNt.png"
                });
            if(currentRoom.Player3 != null)
                players.Add(new Player
                {
                    Name = currentRoom.Player3,
                    Money = 2000,
                    Pawn = "~/images/pawns/red.png",
                    PawnPath = "https://i.imgur.com/wEkGoqn.png"
                });
            if(currentRoom.Player4 != null)
                players.Add(new Player
                {
                    Name = currentRoom.Player4,
                    Money = 2000,
                    Pawn = "~/images/pawns/yellow.png",
                    PawnPath = "https://i.imgur.com/K398MH6.png"
                });
            
            return players;
        }

        public int GetPlayersCount(string player)
        {
            var gameRoomContext = new MonopolyDbContext();
            Room currentRoom = gameRoomContext.Rooms.Where(x => x.Player1 == player || x.Player2 == player || x.Player3 == player || x.Player4 == player).First();
            return currentRoom.PlayersNumber;
        }

        public string GetRoomPassword(int roomId)
        {
            var gameRoomContext = new MonopolyDbContext();
            Room room = gameRoomContext.Rooms.Where(x => x.RoomId == roomId).FirstOrDefault();
            return room.Password;
        }

        public void removeConnection(string currentPlayer)
        {
            int roomId = 0;
            var roomDb = new MonopolyDbContext();
            ConnectionIds connection = roomDb.ConnectionIds.Where(x => x.PlayerName == currentPlayer).FirstOrDefault();
            if(connection != null)
            {
                roomId = roomDb.ConnectionIds.Where(x => x.PlayerName == currentPlayer).FirstOrDefault().RoomId;
                roomDb.Remove(connection);
                roomDb.SaveChanges();
            }
            checkLastPlayer(roomId);
        }

        public void checkLastPlayer(int roomId)
        {
            var roomDb = new MonopolyDbContext();
            List<ConnectionIds> connections = roomDb.ConnectionIds.Where(x => x.RoomId == roomId).ToList();
            List<Room> rooms = roomDb.Rooms.ToList();    
            if(connections.Count() == 1)
            {
                foreach(ConnectionIds connection in connections)
                {
                    if(connection.RoomId == roomId)
                    {
                        roomDb.Remove(connection);
                        roomDb.SaveChanges();
                    }
                }

                foreach(Room room in rooms)
                {
                    if(room.RoomId == roomId)
                    {
                        roomDb.Remove(room);
                        roomDb.SaveChanges();
                    }
                }
            }
        }

        public void PlayerLeavesGame(string player)
        {
            int roomId = 0;
            var roomDb = new MonopolyDbContext();
            ConnectionIds connection = roomDb.ConnectionIds.Where(x => x.PlayerName == player).FirstOrDefault();
            if (connection != null)
            {
                roomId = roomDb.ConnectionIds.Where(x => x.PlayerName == player).FirstOrDefault().RoomId;
                roomDb.Remove(connection);
                roomDb.SaveChanges();
            }

            Room room = roomDb.Rooms.Where(x => x.Player1 == player || x.Player2 == player || x.Player3 == player || x.Player4 == player).FirstOrDefault();
            if (room != null)
            {
                if (room.Player1 == player)
                    room.Player1 = null;
                else if (room.Player2 == player)
                    room.Player2 = null;
                else if (room.Player3 == player)
                    room.Player3 = null;
                else if (room.Player4 == player)
                    room.Player4 = null;
                roomDb.SaveChanges();
            }
        }
    }
}
