using System.Collections.Generic;
using System.IO;

namespace TextBasedRPG
{
    class Log
    {
        string path;
        List<string> lines = new List<string>();

        public Log(string path)
        {
            this.path = path;
        }

        public void AddLine(string line)
        {
            lines.Add(line);
        }

        public void Write()
        {
            File.WriteAllLines(path, lines);
        }
    }
}
