using System;

namespace TextBasedRPG
{
    class Door : Subject, IInspectable, IDisarmable, IOpenable
    {
        public Room room;
        private Trap trap;
        public int destinationX;
        public int destinationY;

        public Door(string name, Room room, Random random, int x, int y) : base(name)
        {
            this.room = room;

            AddSuffix(x + ", " + y);

            destinationX = x;
            destinationY = y;

            if (random.Next(0, 100) < 50)
            {
                trap = new Trap(room, random);
            }

            AddAction(Actions.inspect);
            AddAction(Actions.open);
        }

        public void Open(Player player)
        {
            if (trap == null || trap.Health <= 0)
            {
                player.ChangeRoom(destinationX, destinationY);
            }
            else
            {
                trap.Spring(player);
                AddSuffix("Trapped!");
                AddAction(Actions.disarm);
            }
        }

        public void Inspect(Player player)
        {
            if(trap != null)
            {
                if(player.stats.Perception > trap.Stealth)
                {
                    Printer.Print("You find a trap!");
                    AddSuffix("Trapped!");
                    AddAction(Actions.disarm);
                    return;
                }
            }
            Printer.Print("It looks safe.");
        }

        public void Disarm(Player player)
        {
            if (trap.Disarm(player))
            {
                RemoveSuffix("Trapped!");
                RemoveAction(Actions.disarm);
            }
        }
    }
}
