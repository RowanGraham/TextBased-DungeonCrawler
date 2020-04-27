using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG
{
    class DestroyActions : IActions
    {
        public bool Command(string command, Subject subject, Player player)
        {
            IDestructable destructable = subject as IDestructable;

            switch (command)
            {
                case "DESTROY":
                    destructable.Destroy(player);
                    return true;
            }

            return false;
        }

        public void PrintCommands(string subjectname)
        {
            Console.WriteLine("DESTROY");
        }
    }
}
