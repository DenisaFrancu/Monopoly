using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Monopoly.Models;

namespace Monopoly.Controllers
{
    [Authorize]
    public class DiceController : Controller
    {
        public string[] diceImage = new string[2];
        private string dicePrefix = "/images/Dices/dice";
        private string diceSuffix = ".png";

        public DiceController()
        {
            diceImage[0] = diceImage[1] = GetDicePath(1);
        }
        public string[] GetRolledDice()
        {
            Random random = new Random();
            diceImage[0] = GetDicePath(random.Next(1,7));
            diceImage[1] = GetDicePath(random.Next(1,7));
            
            return diceImage;
        }

        string GetDicePath(int diceValue)
        {
            return $"{dicePrefix}{diceValue}{diceSuffix}";
        }
    }
}
