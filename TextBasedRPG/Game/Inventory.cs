using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG
{
    class Inventory : Subject, IOpenable
    {
        public List<Item> Items { get; private set; }
        private Item selected;

        public Inventory(string name) : base(name)
        {
            Items = new List<Item>();
            AddAction(Actions.open);
            costsActionPoint = false;

            Add(new Bandage("Bandage", 2));
            Add(new Bandage("Bandage", 2));
        }

        public void Remove(Item item)
        {
            foreach (Item i in Items)
            {
                if (i.username == item.username)
                {
                    i.quantity--;
                    if(i.quantity <= 0)
                    {
                        Items.Remove(i);
                    }
                    return;
                }
            }
        }

        public void Add(Item item)
        {
            foreach (Item i in Items)
            {
                if(i.username == item.username)
                {
                    i.quantity++;
                    return;
                }
            }

            Items.Add(item);
            item.quantity = 1;
        }

        public void Open(Player player)
        {
            selected = null;
            bool usingInventory = true;

            while (usingInventory)
            {
                DECISIONS expectedDecision = DECISIONS.SUBJECT;
                while (expectedDecision == DECISIONS.SUBJECT)
                {
                    Console.WriteLine("You have;");
                    foreach (var item in Items)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write(item.username);
                        Console.ForegroundColor = ConsoleColor.Gray;

                        Console.Write(" | Quantity: " + item.quantity + " ");
                        Console.WriteLine(item.ConstructSuffixes());
                    }

                    string input = Console.ReadLine().ToUpper();

                    if (SelectItem(input))
                    {
                        Console.Write("Current Item: " + selected.username.ToUpper());
                        Console.WriteLine(selected.ConstructSuffixes());
                        Console.WriteLine();

                        Console.WriteLine("Actions;");
                        selected.PrintCommands();

                        expectedDecision = DECISIONS.ACTION;
                        break;
                    }
                    else if (input == "BACK")
                    {
                        usingInventory = false;
                        break;
                    }
                }

                while (expectedDecision == DECISIONS.ACTION)
                {
                    string input = Console.ReadLine().ToUpper();

                    if (SelectAction(input, player))
                    {
                        if (selected.costsActionPoint) player.remainingActions--;

                        expectedDecision = DECISIONS.SUBJECT;
                        usingInventory = false;
                        break;
                    }
                    else if (input == "BACK")
                    {
                        expectedDecision = DECISIONS.SUBJECT;
                        break;
                    }
                }
            }
        }

        private bool SelectItem(string input)
        {
            foreach (var item in Items)
            {
                if(item.username.ToUpper() == input)
                {
                    selected = item;
                    return true;
                }
            }
            return false;
        }

        private bool SelectAction(string input, Player player)
        {
            return selected.Act(input, player);
        }
    }
}
