using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG
{
    class Shield : Item, IEquippable
    {
        public int Defence { get; private set; }

        public Shield(string name, int defence) : base(name)
        {
            Defence = defence;
            AddSuffix("defence: " + defence);
            AddAction(Actions.Equip);
        }

        public void Equip(Player player)
        {
            player.equipment.Equip(this, player);
            player.inventory.Remove(this);
        }
    }
}
