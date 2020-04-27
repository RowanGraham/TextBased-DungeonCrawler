using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG
{
    class Weapon : Item, IEquippable
    {
        public int Strength { get; private set; }

        public Weapon(string name, int strength) : base(name)
        {
            Strength = strength;
            AddSuffix("strength: " + strength);
            AddAction(Actions.Equip);
        }

        public void Equip(Player player)
        {
            player.equipment.Equip(this, player);
            player.inventory.Remove(this);
        }
    }
}
