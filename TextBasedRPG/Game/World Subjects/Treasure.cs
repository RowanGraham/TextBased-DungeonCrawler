using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG
{
    class Treasure : Subject, IInspectable, IOpenable, IDisarmable, IDestructable
    {
        private Room room;
        private Trap trap;
        private Item item;

        public int Stealth { get; private set; }
        public int Health { get; private set; }

        public Treasure(string name, Room room, Random random) : base(name)
        {
            this.room = room;

            Stealth = random.Next(1, 4) + room.Difficulty;
            Health = random.Next(1, 3) + room.Difficulty;

            trap = new Trap(room, random);

            int n = random.Next(0, 2 + room.Difficulty);

            switch (n)
            {
                case 0:
                    item = new Shield("Simple Shield", 2);
                    break;
                case 1:
                    item = new Weapon("Simple Sword", 2);
                    break;
                case 4:
                    item = new Shield("Superior Shield", 3);
                    break;
                case 5:
                    item = new Weapon("Superior Sword", 3);
                    break;
            }

            if(item == null)
            {
                if(room.Difficulty > 4)
                {
                    item = new Bandage("Superior Bandage", 3);
                }
                else
                {
                    item = new Bandage("Bandage", 2);
                }
            }

            AddAction(Actions.inspect);
            AddAction(Actions.open);
        }

        public void Inspect(Player player)
        {
            if(player.stats.Perception > trap.Stealth)
            {
                Printer.Print("You hear the mechanisms of a trap.");
                AddSuffix("Trapped!");
                AddAction(Actions.disarm);
                AddAction(Actions.destroy);
            }
            else
            {
                Printer.Print("It looks safe.");
            }
        }

        public void Open(Player player)
        {
            if (trap.Health > 0)
            {
                trap.Spring(player);

                AddSuffix("Trapped!");
                AddAction(Actions.disarm);
                AddAction(Actions.destroy);
            }
            else if (item != null)
            {
                Printer.Print("You find a " + item.username + "!");
                Printer.Wait(0.25f);
                Printer.Print("You add it to your inventory.");

                player.inventory.Add(item);
                player.RemoveSubject(this);
                item = null;
            }
            else
            {
                Printer.Print("The treasure's gone. Why is the treasure gone?");
                player.RemoveSubject(this);
            }
        }

        public void Disarm(Player player)
        {
            if(trap.Disarm(player))
            {
                RemoveSuffix("Trapped!");
                RemoveAction(Actions.disarm);
                RemoveAction(Actions.destroy);
            }
        }

        public void Destroy(Player player)
        {
            if (trap.Destroy(player))
            {
                RemoveSuffix("Trapped!");
                RemoveAction(Actions.disarm);
                RemoveAction(Actions.destroy);

                Health += trap.Health;
                if(Health <= 0)
                {
                    Printer.Print("You hit it too hard! The treasure is destroyed.");
                    player.RemoveSubject(this);
                }
            }
        }
    }
}
