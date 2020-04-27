using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Type commands into the console and press 'Enter' to confirm them.");
            Console.WriteLine("Blue words are commands you can use.");
            Console.Write("Are you ready to ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("play");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("?");

            while (true)
            {
                string input = Console.ReadLine().ToUpper();
                if (input == "PLAY") break;
            }

            GameManager gameManager = new GameManager();

            while (true)
            {
                gameManager.NewGame();

                Console.Clear();
                Printer.speed = 18;

                gameManager.player.ChangeRoom(0, 0);

                while (gameManager.player.Health > 0)
                {
                    //player turn
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("--PLAYER TURN--");
                    Printer.Wait(0.5f);

                    while (gameManager.player.remainingActions > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("BACK");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(" to go back, unless an action was used.");
                        Console.ForegroundColor = ConsoleColor.Gray;

                        while (gameManager.ExpectedDecisionType == DECISIONS.SUBJECT)
                        {
                            if (gameManager.ChooseSubject())
                            {
                                gameManager.ExpectedDecisionType = DECISIONS.ACTION;
                            }
                        }

                        gameManager.Commands();

                        while (gameManager.ExpectedDecisionType == DECISIONS.ACTION)
                        {
                            string input = Console.ReadLine().ToUpper();

                            if (input == "BACK")
                            {
                                gameManager.ExpectedDecisionType = DECISIONS.SUBJECT;
                            }
                            else if (gameManager.PlayerAction(input))
                            {
                                Console.WriteLine();
                                if (gameManager.current.costsActionPoint) gameManager.player.remainingActions--;
                                gameManager.ExpectedDecisionType = DECISIONS.SUBJECT;
                            }
                        }
                    }

                    //enemy turn
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("--ENEMY TURN--");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Printer.Wait(0.5f);
                    gameManager.EnemyAction();

                    gameManager.player.remainingActions = 1;

                    if (gameManager.player.Health <= 0)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Printer.Print("YOU HAVE DIED");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Printer.Wait(1);
                        Console.WriteLine();
                        break;
                    }
                }
            }
        }
    }
}
