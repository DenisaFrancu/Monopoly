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

        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task SendMessageToGroup(string message, string groupName)
        {
            await Clients.Group(groupName).SendAsync("ReceiveGroupMessage", Context.User.Identity.Name ?? "anonymous", message);
        }
    }
}