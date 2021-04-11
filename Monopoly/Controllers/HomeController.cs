using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Monopoly.Hubs;
using Monopoly.Models;
using Monopoly.ViewModels;

namespace Monopoly.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private DiceController _diceController = new DiceController();
        private DatabaseOperations _gameRoomOperations = new DatabaseOperations();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            HomeViewModel home = new HomeViewModel()
            {
                PublicRooms = _gameRoomOperations.GetPublicRooms(),
                PrivateRooms = _gameRoomOperations.GetPrivateRooms()
            };
            return View(home);
        }

        public IActionResult Game()
        {
            return View();
        }

        public IActionResult WaitingRoom()
        {
            return View();
        }

        public IActionResult RollDice()
        {
            return Json(_diceController.GetRolledDice());
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

        public IActionResult GetPlayersCount(string player)
        {
            return Json(_gameRoomOperations.GetPlayersCount(player));
        }

        public IActionResult Chat()
        {
            return View("ChatView");
        }

        public IActionResult CreateRoom(string roomName, string password, string player)
        {
            Room room = new Room{
                RoomName = roomName,
                Password = password,
                Player1 = player,
                PlayersNumber = 1
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
