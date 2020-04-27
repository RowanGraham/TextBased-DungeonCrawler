using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG
{
    abstract class Item : Subject
    {
        public int quantity = 1;

        public Item(string name) : base(name)
        {
            
        }
    }
}
