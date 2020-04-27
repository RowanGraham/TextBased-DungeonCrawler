using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG
{
    class SneakAttackActions : IActions
    {
        public bool Command(string command, Subject subject, Player player)
        {
            ISneakAttackable sneakAttackable = subject as ISneakAttackable;

            switch (command)
            {
                case "STEALTH ATTACK":
                    sneakAttackable.SneakAttack(player);
                    return true;
            }

            return false;
        }

        public void PrintCommands(string subjectname)
        {
            Console.WriteLine("STEALTH ATTACK");
        }
    }
}
