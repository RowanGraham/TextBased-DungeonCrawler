using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG
{
    interface IActions
    {
        bool Command(string command, Subject subject, Player player);
        void PrintCommands(string subjectname);
    }
}
