using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG
{
    interface INavigateable
    {
        void North(Player player);
        void East(Player player);
        void South(Player player);
        void West(Player player);
    }
}
