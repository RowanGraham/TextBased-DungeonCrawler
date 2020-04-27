using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG
{
    class LevelUpActions : IActions
    {
        public bool Command(string command, Subject subject, Player player)
        {
            ILevelUp levelUp = subject as ILevelUp;

            switch (command)
            {
                case "LEVEL UP":
                    levelUp.LevelUp(player);
                    return true;
            }

            return false;
        }

        public void PrintCommands(string subjectname)
        {
            Console.WriteLine("LEVEL UP");
        }
    }
}
