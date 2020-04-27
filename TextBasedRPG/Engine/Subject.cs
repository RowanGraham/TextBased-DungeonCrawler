using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG
{
    abstract class Subject
    {
        public string basename { get; private set; }
        public string username;

        public bool costsActionPoint = true;

        private List<string> suffixes = new List<string>();
        private List<IActions> actions = new List<IActions>();

        public Subject(string name)
        {
            basename = name;
            username = name;
        }

        public bool Act(string command, Player player)
        {
            foreach (IActions IAction in actions)
            {
                if(IAction.Command(command, this, player))
                {
                    return true;
                }
            }
            return false;
        }

        public void PrintCommands()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            foreach (IActions IAction in actions)
            {
                IAction.PrintCommands(username);
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public string ConstructSuffixes()
        {
            string suffix = "";

            foreach (var suf in suffixes)
            {
                suffix += " ";
                suffix += suf;
            }

            return suffix;
        }

        public void AddSuffix(string suffix)
        {
            string s = "(" + suffix + ")";
            if (!suffixes.Contains(s))
            {
                suffixes.Add(s);
            }
        }

        public void RemoveSuffix(string suffix)
        {
            suffixes.Remove("(" + suffix + ")");
        }

        public void AddAction(IActions action)
        {
            if (!actions.Contains(action))
            {
                actions.Add(action);
            }
        }

        public void RemoveAction(IActions action)
        {
            actions.Remove(action);
        }
    }
}
