using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG
{
    class NavigateActions : IActions
    {
        public bool Command(string command, Subject subject, Player player)
        {
            INavigateable navigateable = subject as INavigateable;

            //outcome = subject;

            switch (command)
            {
                case "MOVE NORTH":
                    navigateable.North(player);
                    return true;
                case "MOVE EAST":
                    navigateable.East(player);
                    return true;
                case "MOVE SOUTH":
                    navigateable.South(player);
                    return true;
                case "MOVE WEST":
                    navigateable.West(player);
                    return true;
            }

            return false;
        }

        public void PrintCommands(string subjectname)
        {
            Console.WriteLine("MOVE NORTH");
            Console.WriteLine("MOVE EAST");
            Console.WriteLine("MOVE SOUTH");
            Console.WriteLine("MOVE WEST");
        }
    }
}
