using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Monopoly.Models;

namespace Monopoly
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var roomDb = new GameRoomContext();
            //roomDb.Rooms.Add(new Room
            //{
            //    RoomName = "Room 1",
            //    Password = "",
            //    Player1 = "fr.denisa@yahoo.com",
            //    Player2 = "fr.adriana@yahoo.com",
            //    Player3 = "",
            //    Player4 = "",
            //    PlayersNumber = 2
            //});
            //roomDb.SaveChanges();
            //var room = roomDb.Rooms.OrderBy(b => b.RoomId).First();
            //roomDb.Remove(room);
            //roomDb.SaveChanges();
            //roomDb.Rooms.Add(new Room
            //{
            //    RoomName = "Room 1",
            //    Password = "",
            //    Player1 = "fr.denisa@yahoo.com",
            //    Player2 = "",
            //    Player3 = "",
            //    Player4 = "",
            //    PlayersNumber = 1
            //});
            //roomDb.SaveChanges();
            //var x = roomDb.Rooms.OrderBy(b => b.RoomId).First();
            //List<Room> rooms = roomDb.Rooms.ToList();
            //foreach(Room r in rooms)
            //{
            //    roomDb.Remove(r);
            //    roomDb.SaveChanges();
            //}
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
