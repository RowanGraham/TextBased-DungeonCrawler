using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG
{
    class UseActions : IActions
    {
        public bool Command(string command, Subject subject, Player player)
        {
            IUseable useable = subject as IUseable;

            switch (command)
            {
                case "USE":
                    useable.Use(player);
                    return true;
            }

            return false;
        }

        public void PrintCommands(string subjectname)
        {
            Console.WriteLine("USE");
        }
    }
}
