using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Monopoly.Models;
using Monopoly.Utilities;

namespace Monopoly.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private DiceOperations _diceOperations = new DiceOperations();
        private RoomDatabaseOperations _gameRoomOperations = new RoomDatabaseOperations();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Game()
        {
            int room = Int32.Parse(Request.Cookies["Room"]);
            return View(_gameRoomOperations.GetPlayersForGame(room));
        }

        public IActionResult WaitingRoom()
        {
            return View();
        }

        public IActionResult RollDice()
        {
            return Json(_diceOperations.GetRolledDice());
        }

        public IActionResult RemovePlayer(string player)
        {
            _gameRoomOperations.removeConnection(player);
            return Ok();
        }

        public IActionResult GetPublicRoomsList()
        {
            return Json(_gameRoomOperations.GetPublicRooms());
        }

        public IActionResult GetPrivateRoomsList()
        {
            return Json(_gameRoomOperations.GetPrivateRooms());
        }

        public IActionResult GetPlayersFromRoom(string player)
        {
            return Json(_gameRoomOperations.GetPlayersFromRoom(player));
        }

        public IActionResult Chat()
        {
            return View("ChatView");
        }

        public IActionResult CreateRoom(string roomName, string password, string player, int playersNumber)
        {
            Room room = new Room{
                RoomName = roomName,
                Password = password,
                Player1 = player,
                PlayersNumber = playersNumber
            };
            _gameRoomOperations.AddRoom(room);
            return RedirectToAction("Index");
        }

        public IActionResult JoinPublicRoom(int roomId, string player)
        {
            _gameRoomOperations.AddPlayerToRoom(roomId, player);
            return RedirectToAction("Index");
        }

        public IActionResult JoinPrivateRoom(int roomId, string player, string password)
        {
            if(password == _gameRoomOperations.GetRoomPassword(roomId))
            {
                _gameRoomOperations.AddPlayerToRoom(roomId, player);
                return Json("OK");
            }
            return Json("Wrong password");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
