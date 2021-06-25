using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Monopoly.Areas.Identity.Data;
using Monopoly.Utilities;
using Monopoly.Models;
using System.Collections.Generic;
using Monopoly.Data;

namespace Monopoly.Hubs
{
    public class Chat : Hub
    {
        UserManager<MonopolyUser> _userManager;
        private RoomDatabaseOperations _gameRoomOperations = new RoomDatabaseOperations();

        public Chat(UserManager<MonopolyUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task SendMessage(string message)  
        {  
            MonopolyUser user = GetCurrentUser();
            string name = user.FirstName + " " + user.LastName;
            string dateTime = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year + " - " + DateTime.Now.Hour + ":" + DateTime.Now.Minute; 
            await Clients.All.SendAsync("ReceiveMessage", name ?? "anonymous", message, dateTime);
        } 

        public async Task AddToGroup()
        {
            string group = GetCurrentGroup();
            await Groups.AddToGroupAsync(Context.ConnectionId, group);
        }

        public async Task AddToLoby()
        {
            string group = GetCurrentGroup();
            await Groups.AddToGroupAsync(Context.ConnectionId, group);
            CheckRedirectToGame();
        }

        public void CheckRedirectToGame()
        {
            string group =  GetCurrentGroup();
            var roomDb = new MonopolyDbContext();
            Room currentRoom = roomDb.Rooms.Where(x => x.RoomId == Int32.Parse(group)).FirstOrDefault();
            int playersNumber = 0;
            if(currentRoom.Player1!=null)
                playersNumber++;
            if(currentRoom.Player2!=null)
                playersNumber++;
            if(currentRoom.Player3!=null)
                playersNumber++;
            if(currentRoom.Player4!=null)
                playersNumber++;
            int room = currentRoom.RoomId;
            if(playersNumber == currentRoom.PlayersNumber)
                Clients.Group(group).SendAsync("RedirectToGame", room);
        }

        public async Task RemoveFromGroup()
        {
            string group = GetCurrentGroup();
            MonopolyUser currentUser = GetCurrentUser();
            string player = currentUser.FirstName + " " + currentUser.LastName;
            _gameRoomOperations.RemovePlayerFromRoom(player);
            await SendLeftRoomMessage(group, player);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, group);
        }

        public async Task SendMessageToGroup(string message)
        {
            string group = GetCurrentGroup();
            MonopolyUser user = GetCurrentUser();
            string name = user.FirstName + " " + user.LastName;
            await Clients.Group(group).SendAsync("ReceiveGroupMessage", name ?? "anonymous", message);
        }


        public async Task SendJoinedRoomMessage()
        {
            string group = GetCurrentGroup();
            MonopolyUser user = GetCurrentUser();
            string name = user.FirstName + " " + user.LastName;
            await Clients.Group(group).SendAsync("JoinedRoomMessage", name ?? "anonymous", "has joined the room.");
        }

        public async Task SendLeftRoomMessage(string group, string name)
        {
            await Clients.Group(group).SendAsync("JoinedRoomMessage", name ?? "anonymous", "has left the room.");
        }  

        public MonopolyUser GetCurrentUser()
        {
            return _userManager.FindByEmailAsync(Context.User.Identity.Name).Result;
        }
        
        public string GetCurrentGroup()
        {
            MonopolyUser user = _userManager.FindByEmailAsync(Context.User.Identity.Name).Result;
            string player = user.FirstName + " " + user.LastName;
            var gameRoomContext = new MonopolyDbContext();
            List<Room> group = gameRoomContext.Rooms.Where(x => x.Player1 == player || x.Player2 == player || x.Player3 == player || x.Player4 == player).ToList();
            return group.Last().RoomId.ToString();
        }     
    }
}