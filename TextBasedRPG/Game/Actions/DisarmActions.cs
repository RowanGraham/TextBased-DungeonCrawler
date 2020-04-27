using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG
{
    class DisarmActions : IActions
    {
        public bool Command(string command, Subject subject, Player player)
        {
            IDisarmable disarmable = subject as IDisarmable;

            switch (command)
            {
                case "DISARM":
                    disarmable.Disarm(player);
                    return true;
            }

            return false;
        }

        public void PrintCommands(string subjectname)
        {
            Console.WriteLine("DISARM");
        }
    }
}
