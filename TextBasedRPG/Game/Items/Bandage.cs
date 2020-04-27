using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG
{
    class Bandage : Item, IUseable
    {
        public int HealthRestore;

        public Bandage(string name, int healthRestore) : base(name)
        {
            HealthRestore = healthRestore;
            AddAction(Actions.use);
            AddSuffix("Heals: " + healthRestore);
        }

        public void Use(Player player)
        {
            if (player.Health == player.stats.MaxHealth)
            {
                costsActionPoint = false;
                Printer.Print("You're already at full health.");
            }
            else
            {
                costsActionPoint = true;
                Printer.Print("You wrap the bandage around your wounds.");
                player.ChangeHealth(2);
                quantity--;
            }
        }
    }
}
