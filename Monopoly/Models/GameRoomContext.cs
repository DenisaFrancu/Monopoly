using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Monopoly.Models
{
    public class GameRoomContext : DbContext
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<ConnectionIds> ConnectionIds { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlServer(@"Server=DFRANCU-RO-DE;Database=MonopolyRooms;Trusted_Connection=True;MultipleActiveResultSets=true");
    }

    public class Room
    {
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public string Password { get; set; }
        public int PlayersNumber { get; set; }
        public string Player1 { get; set; }
        public string Player2 { get; set; }
        public string Player3 { get; set; }
        public string Player4 { get; set; }
    }

    public class ConnectionIds
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string connectionId { get; set; }
    }
}
