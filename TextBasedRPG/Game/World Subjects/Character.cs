using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG
{
    abstract class Character : Subject
    {
        public int Health;
        public int Stealth;
        public int Perception;
        public int Strength;
        public int Knowledge;
        public int Speed;

        public Room room;

        public Character(string name, Room room) : base(name)
        {
            this.room = room;
        }

        protected void SetStats(int health, int stealth, int perception, int strength, int knowledge, int speed)
        {
            Health = health;
            Stealth = stealth;
            Perception = perception;
            Strength = strength;
            Knowledge = knowledge;
            Speed = speed;
        }

        protected void Killed(Player player)
        {
            player.stats.GainExperience(35);
            Printer.Print("You gained 35 experience.");
            player.RemoveSubject(this);
        }
    }
}
