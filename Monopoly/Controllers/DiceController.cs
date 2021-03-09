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
        private int[] diceArray = new int[] {1, 2, 3, 4, 5, 6};
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
            // int[] randomDice = diceArray.OrderBy(x => random.Next()).ToArray();
            // for (int diceIndex = 0; diceIndex < diceImage.Length; diceIndex++)
            // {
            //     diceImage[diceIndex] = GetDicePath(randomDice[diceIndex]);
            // }
            
            return diceImage;
        }

        string GetDicePath(int diceValue)
        {
            return $"{dicePrefix}{diceValue}{diceSuffix}";
        }
    }
}
