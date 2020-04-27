using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG
{
    class Equipment : Subject, IInspectable
    {
        public Shield shield;
        public Weapon weapon;

        public Equipment(string name) : base(name)
        {
            costsActionPoint = false;
            AddAction(Actions.inspect);

            shield = new Shield("Primitive Shield", 1);
            weapon = new Weapon("Primitive Sword", 1);
        }

        public void Inspect(Player player)
        {
            Console.WriteLine("Currently equipped:");
            Console.WriteLine(shield.username + shield.ConstructSuffixes());
            Console.WriteLine(weapon.username + weapon.ConstructSuffixes());
        }

        public void Equip(Item equippable, Player player)
        {
            Type T = equippable.GetType();
            if (T == typeof(Shield))
            {
                player.inventory.Add(shield);
                shield = equippable as Shield;
            }
            if (T == typeof(Weapon))
            {
                player.inventory.Add(weapon);
                weapon = equippable as Weapon;
            }
        }
    }
}
