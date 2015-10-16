using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace ProcessGoblin
{
    class Program
    {
        private static bool go = true;

        static void Main(string[] args)
        {
            Console.WriteLine("Specify the name of the process to murder (the executable name without '.exe'):");
            string name = null;
            while (name == null)
            {
                name = Console.ReadLine();
            }

            Console.CancelKeyPress += Console_CancelKeyPress;

            do
            {
                foreach (var p in Process.GetProcesses().Where(p => p.ProcessName == name))
                {
                    var id = p.Id;
                    try
                    {
                        p.Kill();
                        Console.WriteLine("Killed process {0}", id);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Couldn't kill process {0} ({1})", id, e.GetType().FullName);
                    }
                }

                Thread.Sleep(500);
            } while (go);
        }

        static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            go = false;
        }
    }
}
