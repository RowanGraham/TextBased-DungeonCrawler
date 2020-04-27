using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG
{
    class Stats : Subject, IInspectable, ILevelUp
    {
        public int MaxHealth { get; private set; }
        public int Strength { get; private set; }
        public int Stealth { get; private set; }
        public int Perception { get; private set; }
        public int Knowledge { get; private set; }
        public int Speed { get; private set; }

        public int Experience { get; private set; }
        public int Level { get; private set; }

        private int unspentLevels = 0;
        private bool hasLevelUpAction = false;

        public Stats(string name) : base(name)
        {
            costsActionPoint = false;

            AddAction(Actions.inspect);
            MaxHealth = 5;
            Strength = 1;
            Stealth = 1;
            Perception = 2;
            Knowledge = 2;
            Speed = 2;
        }

        public void GainExperience(int exp)
        {
            Experience += exp;
            while(Experience >= 100)
            {
                unspentLevels++;
                Experience -= 100;
            }

            if (unspentLevels > 0 && !hasLevelUpAction)
            {
                AddSuffix("Can Level Up!");
                AddAction(Actions.levelUp);
                hasLevelUpAction = true;
            }
        }

        public void Inspect(Player player)
        {
            Console.WriteLine("Level: " + Level);
            Console.WriteLine("Experience: " + Experience);
            Console.WriteLine("Current Health: " + player.Health);
            Console.WriteLine("Current Defence: " + player.equipment.shield.Defence);
            Console.WriteLine("--");

            Console.WriteLine("MaxHealth: " + MaxHealth);
            Console.WriteLine("Strength: " + Strength);
            Console.WriteLine("Stealth: " + Stealth);
            Console.WriteLine("Perception: " + Perception);
            Console.WriteLine("Knowledge: " + Knowledge);
            Console.WriteLine("Speed: " + Speed);
        }

        public void LevelUp(Player player)
        {
            int points = 0;
            while(unspentLevels > 0)
            {
                unspentLevels--;
                Level++;
                points++;
            }

            Console.WriteLine("Choose a stat to increase.");
            Console.WriteLine("Remaining points: " + points);

            while(points > 0)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("MAX HEALTH");
                Console.WriteLine("STRENGTH");
                Console.WriteLine("STEALTH");
                Console.WriteLine("PERCEPTION");
                Console.WriteLine("KNOWLEDGE");
                Console.WriteLine("SPEED");
                Console.ForegroundColor = ConsoleColor.Gray;

                while (true)
                {
                    string input = Console.ReadLine().ToUpper();
                    if (IncreaseSkill(input))
                    {
                        Printer.Print(input + " increased");
                        break;
                    }
                }

                points--;
                Console.WriteLine("Remaining points: " + points);
            }

            RemoveSuffix("Can Level Up!");
            RemoveAction(Actions.levelUp);
            hasLevelUpAction = false;
        }

        private bool IncreaseSkill(string skill)
        {
            switch (skill)
            {
                case "MAX HEALTH":
                    MaxHealth++;
                    return true;
                case "STRENGTH":
                    Strength++;
                    return true;
                case "STEALTH":
                    Stealth++;
                    return true;
                case "PERCEPTION":
                    Perception++;
                    return true;
                case "KNOWLEDGE":
                    Knowledge++;
                    return true;
                case "SPEED":
                    Speed++;
                    return true;
            }
            return false;
        }
    }
}
