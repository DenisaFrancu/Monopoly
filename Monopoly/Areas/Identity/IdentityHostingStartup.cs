using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Monopoly.Areas.Identity.Data;
using Monopoly.Data;

[assembly: HostingStartup(typeof(Monopoly.Areas.Identity.IdentityHostingStartup))]
namespace Monopoly.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<MonopolyDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("MonopolyDbContextConnection")));

                services.AddDefaultIdentity<MonopolyUser>(options => {
                    options.SignIn.RequireConfirmedAccount = false; //no email confirmation
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    }) 
                    .AddEntityFrameworkStores<MonopolyDbContext>();
            });
        }
    }
}