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

namespace Monopoly.Controllers
{
    [Authorize]
    public class DatabaseOperations
    {
        public DatabaseOperations() { }
        
        public void AddRoom(Room room)
        {
            var gameRoomContext = new GameRoomContext();
            gameRoomContext.Rooms.Add(room);
            gameRoomContext.SaveChanges();
            List<Room> rooms = gameRoomContext.Rooms.ToList();
        }

        public void AddPlayerToRoom(int roomId, string player)
        {
            var gameRoomContext = new GameRoomContext();
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
            var gameRoomContext = new GameRoomContext();
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
            var gameRoomContext = new GameRoomContext();
            return gameRoomContext.Rooms.Where(x => x.Password == null).OrderBy(o => o.RoomName).ToList();
        }

        public List<Room> GetPrivateRooms()
        {
            var gameRoomContext = new GameRoomContext();
            return gameRoomContext.Rooms.Where(x => x.Password != null).OrderBy(o => o.RoomName).ToList();
        }

        public List<string> GetPlayersFromRoom(string player)
        {
            var gameRoomContext = new GameRoomContext();
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
            var gameRoomContext = new GameRoomContext();
            Room currentRoom = gameRoomContext.Rooms.Where(x => x.RoomId == roomId).FirstOrDefault();
            List<Player> players = new List<Player>();
            if(currentRoom.Player1 != null)
                players.Add(new Player
                {
                    Name = currentRoom.Player1,
                    Money = 2000,
                    Pawn = "~/images/pawns/blue.png"
                });
            if(currentRoom.Player2 != null)
                players.Add(new Player
                {
                    Name = currentRoom.Player2,
                    Money = 2000,
                    Pawn = "~/images/pawns/green.png"
                });
            if(currentRoom.Player3 != null)
                players.Add(new Player
                {
                    Name = currentRoom.Player3,
                    Money = 2000,
                    Pawn = "~/images/pawns/red.png"
                });
            if(currentRoom.Player4 != null)
                players.Add(new Player
                {
                    Name = currentRoom.Player4,
                    Money = 2000,
                    Pawn = "~/images/pawns/yellow.png"
                });
            
            return players.OrderBy(x => x.Name).ToList();
        }

        public int GetPlayersCount(string player)
        {
            var gameRoomContext = new GameRoomContext();
            Room currentRoom = gameRoomContext.Rooms.Where(x => x.Player1 == player || x.Player2 == player || x.Player3 == player || x.Player4 == player).First();
            return currentRoom.PlayersNumber;
        }

        public string GetRoomPassword(int roomId)
        {
            var gameRoomContext = new GameRoomContext();
            Room room = gameRoomContext.Rooms.Where(x => x.RoomId == roomId).FirstOrDefault();
            return room.Password;
        }
    }
}
