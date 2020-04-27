using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TextBasedRPG
{
    static class Printer
    {
        static public float speed = 12f;

        static public void Print(string text)
        {
            float sleep = 1 / speed;

            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(TimeSpan.FromSeconds(sleep));
            }

            Console.WriteLine();
        }

        static public void Wait(float seconds)
        {
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
        }
    }
}
