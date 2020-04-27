using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG
{
    class OpenActions : IActions
    {
        public bool Command(string command, Subject subject, Player player)
        {
            IOpenable openable = subject as IOpenable;

            //outcome = subject;

            switch (command)
            {
                case "OPEN":
                    openable.Open(player);
                    return true;
            }

            return false;
        }

        public void PrintCommands(string subjectname)
        {
            Console.WriteLine("OPEN");
        }
    }
}
