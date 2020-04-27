using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG
{
    class AttackActions : IActions
    {
        public bool Command(string command, Subject subject, Player player)
        {
            IAttackable attackable = subject as IAttackable;

            switch (command)
            {
                case "ATTACK":
                    attackable.Attack(player);
                    return true;
            }

            return false;
        }

        public void PrintCommands(string subjectname)
        {
            Console.WriteLine("ATTACK");
        }
    }
}
