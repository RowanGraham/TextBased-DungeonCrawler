using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG
{
    class EquipActions : IActions
    {
        public bool Command(string command, Subject subject, Player player)
        {
            IEquippable equippable = subject as IEquippable;

            switch (command)
            {
                case "EQUIP":
                    equippable.Equip(player);
                    return true;
            }

            return false;
        }

        public void PrintCommands(string subjectname)
        {
            Console.WriteLine("EQUIP");
        }
    }
}
