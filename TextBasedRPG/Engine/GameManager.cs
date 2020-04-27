using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG
{
    class GameManager
    {
        public DECISIONS ExpectedDecisionType;
        public Player player { get; private set; }
        public Subject current { get; private set; }

        public void NewGame()
        {
            int n;
            while (true)
            {
                Console.WriteLine("Enter a world size between 10 and 100");
                string input = Console.ReadLine().ToUpper();
                if (int.TryParse(input, out n))
                {
                    n = Math.Abs(Math.Min(100, Math.Max(10, n)));
                    Console.WriteLine("World size: " + n);
                    Printer.Wait(1);
                    break;
                }
            }

            World.Generate(n, n);
            ExpectedDecisionType = DECISIONS.SUBJECT;
            player = new Player(World.rooms[0, 0]);
        }

        public bool ChooseSubject()
        {
            current = null;
            Console.WriteLine("Current Room: " + player.room.username);
            Console.WriteLine("Choose a subject;");
            var subjects = player.subjects.Concat(player.constantSubjects);

            foreach (var s in subjects)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(s.username.ToUpper());
                Console.ForegroundColor = ConsoleColor.Gray;

                Console.Write(s.ConstructSuffixes());
                Console.Write(", ");
            }
            Console.WriteLine();

            while (current == null)
            {
                string input = Console.ReadLine().ToUpper();

                foreach (var s in subjects)
                {
                    if(s.username.ToUpper() == input)
                    {
                        current = s;
                        break;
                    }
                }
            }

            Console.WriteLine();
            return current != null;
        }

        public void Commands()
        {
            Console.Write("Current Subject: " + current.username.ToUpper());
            Console.Write(current.ConstructSuffixes());
            Console.WriteLine();

            Console.WriteLine("actions;");
            current.PrintCommands();
        }

        public bool PlayerAction(string input)
        {
            Console.WriteLine();
            return (current.Act(input, player));
        }

        public void EnemyAction()
        {
            player.room.EnemyTurn(player);
        }
    }
}
