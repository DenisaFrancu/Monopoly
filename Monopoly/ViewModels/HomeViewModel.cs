using System.Collections.Generic;
using Monopoly.Models;

namespace Monopoly.ViewModels
{
    public class HomeViewModel
    {
        public List<Room> PublicRooms { get; set; }
        public List<Room> PrivateRooms { get; set; }
    }
}