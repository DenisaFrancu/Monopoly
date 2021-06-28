using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Monopoly.Areas.Identity.Data;

namespace Monopoly.Data
{
    public class MonopolyDbContext : IdentityDbContext<MonopolyUser>
    {
        public MonopolyDbContext(DbContextOptions<MonopolyDbContext> options)
            : base(options)
        {
        }

        public MonopolyDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Room>();
            builder.Entity<ConnectionIds>();
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlServer(@"Data Source=tcp:monopolydbserver.database.windows.net,1433;Initial Catalog=Monopoly_db;User Id=denisafrancu@monopolydbserver;Password=Deni010203");

        public DbSet<Room> Rooms { get; set; }
        public DbSet<ConnectionIds> ConnectionIds { get; set; }
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
        public string PlayerName { get; set; }
    }
}
