using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Monopoly.Areas.Identity.Data;

namespace Monopoly.Hubs
{
    public class Chat : Hub
    {
        public async Task SendMessage(string message)  
        {  
            string dateTime = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year + " - " + DateTime.Now.Hour + ":" + DateTime.Now.Minute; 
            await Clients.All.SendAsync("ReceiveMessage", Context.User.Identity.Name ?? "anonymous", message, dateTime);
        } 
    }
}