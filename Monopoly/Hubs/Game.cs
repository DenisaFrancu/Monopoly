using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Primitives;
using Monopoly.Areas.Identity.Data;
using Monopoly.Models;
using Monopoly.Utilities;

namespace Monopoly.Hubs
{
    public class Game : Hub
    {
        UserManager<MonopolyUser> _userManager;
        private ConnectionsDatabaseOperations databaseOperations = new ConnectionsDatabaseOperations();
        private RoomDatabaseOperations roomDatabaseOperations = new RoomDatabaseOperations();

        public Game(UserManager<MonopolyUser> userManager)
        {
            _userManager = userManager;
        }
        
        public async Task AddToGroup()
        {
            string group = GetCurrentGroup();
            await Groups.AddToGroupAsync(Context.ConnectionId, group);
            int roomId = Int32.Parse(group);
            await databaseOperations.AddRoom(new ConnectionIds()
            {
                RoomId = roomId,
                connectionId = Context.ConnectionId
            });
        }

        public async Task SendMessageToGroup(string message)
        {
            string group = GetCurrentGroup();
            MonopolyUser user = GetCurrentUser();
            string name = user.FirstName + " " + user.LastName;
            await Clients.Group(group).SendAsync("ReceiveGroupMessage", name ?? "anonymous", message);
        }

        public async Task StartGame(int playersTurn)
        {
            string connection = databaseOperations.GetRoomConnections(databaseOperations.GetRoomId(Context.ConnectionId)).ElementAt(playersTurn);
            await Clients.Client(connection).SendAsync("PlayGame", playersTurn);
        }

        public async Task RollDices(string dice1, string dice2)
        {
            MonopolyUser currentPlayer = GetCurrentUser();
            string player = currentPlayer.FirstName + " " + currentPlayer.LastName;
            int roomId = Int32.Parse(GetCurrentGroup());
            string pawn = roomDatabaseOperations.GetPlayersForGame(roomId).Where(x => x.Name == player).FirstOrDefault().Pawn;
            await Clients.Group(GetCurrentGroup()).SendAsync("DisplayRollDices", dice1, dice2, pawn);
        }

        public MonopolyUser GetCurrentUser()
        {
            return _userManager.FindByEmailAsync(Context.User.Identity.Name).Result;
        }

        public string GetCurrentGroup()
        {
            MonopolyUser user = _userManager.FindByEmailAsync(Context.User.Identity.Name).Result;
            string player = user.FirstName + " " + user.LastName;
            var gameRoomContext = new GameRoomContext();
            string group = gameRoomContext.Rooms.Where(x => x.Player1 == player || x.Player2 == player || x.Player3 == player || x.Player4 == player).First().RoomId.ToString();
            return group;
        } 
    }
}