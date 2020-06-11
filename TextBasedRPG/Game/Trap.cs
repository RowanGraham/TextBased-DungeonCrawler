using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG
{
    class Trap
    {
        public int Health { get; private set; }
        public int Stealth { get; private set; }
        public int Strength { get; private set; }
        public int Complexity { get; private set; }
        public int Speed { get; private set; }

        public Room room { get; private set; }

        public Trap(Room room, Random random)
        {
            this.room = room;

            SetStats(random.Next(1, 4), random.Next(1, 4), random.Next(1, 4), random.Next(1, 4), random.Next(1, 8));
            for (int i = 0; i < room.Difficulty; i++)
            {
                switch (random.Next(0, 6))
                {
                    case 0:
                        Health++;
                        break;
                    case 1:
                        Stealth++;
                        break;
                    case 2:
                        Strength++;
                        break;
                    case 3:
                        Complexity++;
                        break;
                    case 4:
                        Speed++;
                        break;
                }
            }
        }

        public void SetStats(int stealth, int strength, int health, int complexity, int speed)
        {
            Stealth = stealth;
            Strength = strength;
            Health = health;
            Complexity = complexity;
            Speed = (int)Math.Floor(speed * 1.5f);
        }

        public void Spring(Player player)
        {
            Printer.Print("You spring the trap!");

            if (Speed > player.stats.Speed)
            {
                player.ChangeHealth(-Strength);
            }
            else
            {
                Printer.Print("Speed success!");
                Printer.Print("You dodge the trap!");
            }

            //trap is removed after being sprung
            Health = 0;
        }

        public bool Destroy(Player player)
        {
            Health -= player.stats.Strength + player.equipment.weapon.Strength;
            if(Health > 0)
            {
                Printer.Print("You damage the trap, but it's not destroyed.");
                Strength = Math.Max(1, Strength--);
                return false;
            }
            else
            {
                Printer.Print("You hit the trap and break the mechanism.");
                return true;
            }
        }

        public bool Disarm(Player player)
        {
            if(player.stats.Knowledge > Complexity)
            {
                Printer.Print("You safely remove the mechanism.");
                Health = 0;
                return true;
            }
            else
            {
                Printer.Print("Knowledge failure!");
                Spring(player);
                return false;
            }
        }
    }
}
