using System;
using WindowsService;

namespace WindowsServiceConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger loger = new Logger();
            loger.Start();
            Console.WriteLine("Loger Start");
        }
    }
}
