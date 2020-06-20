using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGR_Game_Engine
{
    class Program
    {
        private static GameManager Game { get; set; }
        static void Main(string[] args)
        {
            Console.WriteLine("Start Game? (Y/N)");
            var response = Console.ReadLine();

            if(response == "Y")
            {
                Console.WriteLine("StartingGame");
                Game = new GameManager();
                int winner = Game.PlayGame();

                Console.WriteLine("Game Over: Player " + winner + " Wins!");
            }

        }
    }
}
