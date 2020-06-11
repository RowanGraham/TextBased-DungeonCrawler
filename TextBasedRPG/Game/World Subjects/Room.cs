using System;
using System.Collections.Generic;

namespace TextBasedRPG
{
    class Room : Subject
    {
        public int Difficulty { get; private set; }
        public bool IsExplored { get; private set; }

        public int X { get; private set; }
        public int Y { get; private set; }

        private List<Enemy> enemies = new List<Enemy>();
        public List<Door> doors = new List<Door>();
        private List<Treasure> treasures = new List<Treasure>();

        public Room(string name, int x, int y, Random random) : base(name)
        {
            X = x;
            Y = y;

            AddSuffix(x + ", " + y);

            Difficulty = (int)Math.Floor((x + y) * 0.5f);
            IsExplored = false;

            //make enemies and treasure
            int amountOfSubjects = random.Next(1, 4);
            for (int i = 0; i < amountOfSubjects; i++)
            {
                if (random.Next(0, 100) < 80)
                {
                    enemies.Add(new Enemy("Enemy", this, random));
                }
                else
                {
                    treasures.Add(new Treasure("Treasure", this, random));
                }
            }
        }

        public void CreateDoor(Random random, int x, int y)
        {
            doors.Add(new Door("Door", this, random, x, y));
        }

        public void EnemyTurn(Player player)
        {
            int livingEnemies = 0;

            foreach (var enemy in enemies)
            {
                if (enemy.Health > 0)
                {
                    enemy.EnemyTurn(player);
                    livingEnemies++;
                }
            }

            if (livingEnemies == 0)
            {
                Printer.Print("You don't notice anything.");
                Console.WriteLine();
            }
        }

        public void Explore(Player player)
        {
            if (IsExplored)
            {
                Printer.Print("You enter the room; you've been here before.");
            }
            else
            {
                Printer.Print("You enter a strange room...");
                IsExplored = true;
            }
            Console.WriteLine();

            int seenEnemies = 0;
            int alertedEnemies = 0;
            foreach (Enemy e in enemies)
            {
                if (e.Health <= 0) continue;
                if(player.stats.Perception > e.Stealth)
                {
                    seenEnemies++;
                    player.AddSubject(e);

                    if (e.IsAlerted) alertedEnemies++;
                    else e.AddAction(Actions.sneakAttack);
                }
            }

            if (seenEnemies > 1)
            {
                Printer.Print("There are " + seenEnemies + " enemies!");
                if (alertedEnemies == 1)
                {
                    if (alertedEnemies == seenEnemies) Printer.Print("It has seen you!");
                    else Printer.Print("One has seen you!");
                }
                if (alertedEnemies > 1)
                {
                    if (alertedEnemies == seenEnemies) Printer.Print("They have seen you!");
                    else Printer.Print("Some have seen you!");
                }
            }
            else if (seenEnemies == 1)
            {
                Printer.Print("There is " + seenEnemies + " enemy!");
            }

            int treasuresDetected = 0;
            foreach (var t in treasures)
            {
                if (t.Health <= 0) continue;
                if (player.stats.Perception > t.Stealth)
                {
                    treasuresDetected++;
                    player.AddSubject(t);
                }
            }
            if (treasuresDetected > 0) Printer.Print("You see treasure!");

            Printer.Print("You see " + doors.Count + " doors.");
            foreach (var d in doors)
            {
                player.AddSubject(d);
            }
        }
    }
}
