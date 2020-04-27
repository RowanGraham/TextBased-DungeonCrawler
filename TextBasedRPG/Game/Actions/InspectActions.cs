using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG
{
    class InspectActions : IActions
    {
        public bool Command(string command, Subject subject, Player player)
        {
            IInspectable inspectable = subject as IInspectable;

            switch (command)
            {
                case "INSPECT":
                    inspectable.Inspect(player);
                    return true;
            }

            return false;
        }

        public void PrintCommands(string subjectname)
        {
            Console.WriteLine("INSPECT");
        }
    }
}
