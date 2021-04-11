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
            // var roomDb = new GameRoomContext();
            // List<Room> rooms = roomDb.Rooms.ToList();
            // foreach(Room r in rooms)
            // {
            //    roomDb.Remove(r);
            //    roomDb.SaveChanges();
            // }
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
