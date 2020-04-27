using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG
{
    class Player
    {
        public List<Subject> subjects { get; private set; }
        public List<Subject> constantSubjects { get; private set; }

        public Room room;
        public int remainingActions = 1;

        public Stats stats { get; private set; }
        public int Health { get; private set; }

        public Inventory inventory { get; private set; }
        public Equipment equipment { get; private set; }

        public Player(Room room)
        {
            subjects = new List<Subject>();
            constantSubjects = new List<Subject>();
            this.room = room;

            inventory = new Inventory("Inventory");
            constantSubjects.Add(inventory);

            equipment = new Equipment("Equipment");
            constantSubjects.Add(equipment);

            stats = new Stats("Stats");
            constantSubjects.Add(stats);

            Health = stats.MaxHealth;
        }

        public void ChangeHealth(int change)
        {
            if(change < 0)
            {
                if(equipment.shield.Defence > 0)
                {
                    change += equipment.shield.Defence;
                    change = Math.Min(-1, change);
                }

                Printer.Print("You take " + -change + " damage.");
            }

            if(change > 0)
            {
                int lostHealth = stats.MaxHealth - Health;
                change = Math.Min(change, lostHealth);
                Printer.Print("You healed " + change + " health.");
            }

            Health += change;
            Console.WriteLine("Current Health: " + Health + " (Max: " + stats.MaxHealth + ")");
        }

        public void AddSubject(Subject subject)
        {
            int similarTypes = 0;

            if (!subjects.Contains(subject))
            {
                foreach (var s in subjects)
                {
                    if (s.GetType() == subject.GetType()) similarTypes++;
                }

                if (similarTypes > 0) subject.username = subject.basename + (similarTypes + 1);
                else subject.username = subject.basename;

                subjects.Add(subject);
            }
        }

        public void RemoveSubject(Subject subject)
        {
            if (subjects.Contains(subject)) subjects.Remove(subject);
        }

        public void ChangeRoom(int x, int y)
        {
            subjects.Clear();

            room = World.rooms[x, y];

            room.username = "("+ room.X + "," + room.Y +")";
            room.Explore(this);
        }
    }
}
