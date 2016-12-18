using System;
using TagCloud.Client.ConsoleClient;

namespace TagCloud.Client
{
    public class EntryPoint
    {
        public static void Main(string[] args)
        {
            try
            {
                new ConsoleUi(args).Run();
            }
            catch (Exception e)
            {
                while (e != null)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine();
                    e = e.InnerException;
                }
            }
        }
    }
}
