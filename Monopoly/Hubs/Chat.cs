using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Monopoly.Areas.Identity.Data;
using Monopoly.Data;

namespace Monopoly.Hubs
{
    public class Chat : Hub
    {
        UserManager<MonopolyUser> _userManager;

        public Chat(UserManager<MonopolyUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task SendMessage(string message)  
        {  
            MonopolyUser user = _userManager.FindByEmailAsync(Context.User.Identity.Name).Result;
            string name = user.FirstName + " " + user.LastName;
            string dateTime = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year + " - " + DateTime.Now.Hour + ":" + DateTime.Now.Minute; 
            await Clients.All.SendAsync("ReceiveMessage", name ?? "anonymous", message, dateTime);
        } 

        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task SendMessageToGroup(string message, string groupName)
        {
            MonopolyUser user = _userManager.FindByEmailAsync(Context.User.Identity.Name).Result;
            string name = user.FirstName + " " + user.LastName;
            await Clients.Group(groupName).SendAsync("ReceiveGroupMessage", name ?? "anonymous", message);
        }
    }
}