using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG
{
    class Enemy : Character, IAttackable, IInspectable, ISneakAttackable
    {
        private bool isAlerted = false;
        public bool IsAlerted
        {
            get
            {
                return isAlerted;
            }
            set
            {
                isAlerted = value;
                if (value)
                {
                    RemoveAction(Actions.sneakAttack);
                    AddSuffix("Alerted!");
                }
                else
                {
                    AddAction(Actions.sneakAttack);
                    RemoveSuffix("Alerted!");
                }
            }
        }

        public Enemy(string name, Room room, Random random) : base(name, room)
        {
            AddAction(Actions.inspect);
            AddAction(Actions.attack);

            SetStats(random.Next(1, 4), random.Next(1, 3), random.Next(1, 3), random.Next(1, 3), random.Next(1, 3), random.Next(1, 3));
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
                        Perception++;
                        break;
                    case 3:
                        Strength++;
                        break;
                    case 4:
                        Knowledge++;
                        break;
                    case 5:
                        Speed++;
                        break;
                }
            }
        }

        public void EnemyTurn(Player player)
        {
            if (IsAlerted)
            {
                player.AddSubject(this);
                Printer.Print(username + " attacks you with " + Strength + " strength.");
                player.ChangeHealth(-Strength);
                Console.WriteLine();
            }
            else if (Perception > player.stats.Stealth)
            {
                if (player.subjects.Contains(this))
                {
                    Printer.Print(username + " sees you!");
                }
                else
                {
                    Printer.Print("An enemy emerges from the darkness!");
                    player.AddSubject(this);
                }
                Console.WriteLine();
                IsAlerted = true;
            }
            else
            {
                if (player.subjects.Contains(this))
                {
                    Printer.Print("You remain hidden from " + username);
                    Console.WriteLine();
                }
            }
        }

        public void Attack(Player player)
        {
            Health -= player.stats.Strength + player.equipment.weapon.Strength;
            IsAlerted = true;

            Printer.Print("You attack; dealing " + (player.stats.Strength + player.equipment.weapon.Strength) + " damage!");

            if (Health <= 0)
            {
                Printer.Print("You killed it.");
                Killed(player);
            }
        }

        public void Inspect(Player player)
        {
            if(player.stats.Knowledge > room.Difficulty)
            {
                Printer.Print("You determine " + username + "'s stats;");
                Console.WriteLine("HEALTH: " + Health);
                Console.WriteLine("STEALTH: " + Stealth);
                Console.WriteLine("STRENGTH: " + Strength);
                Console.WriteLine("PERCEPTION: " + Perception);
                Console.WriteLine("KNOWLEDGE: " + Knowledge);
                Console.WriteLine("SPEED: " + Speed);
            }
            else
            {
                Printer.Print("Your knowledge is too low to learn anything.");
            }
        }

        public void SneakAttack(Player player)
        {
            Printer.Print("You attack; dealing " + (player.stats.Strength + player.equipment.weapon.Strength) + " damage!");
            Health -= player.stats.Strength + player.equipment.weapon.Strength;
            IsAlerted = true;

            if (player.stats.Stealth + player.stats.Speed > Perception + Speed)
            {
                int bonus = (int)Math.Round(player.stats.Strength * 0.5, MidpointRounding.AwayFromZero);
                Printer.Print("Sneak bonus; +" + bonus + " damage!");
                Health -= bonus;
            }
            else
            {
                Printer.Print("Stealth / Speed Failure!");
                Printer.Print("Sneak failed!");
            }
            
            if(Health <= 0)
            {
                Printer.Print("You killed it.");
                Killed(player);
            }
        }
    }
}
