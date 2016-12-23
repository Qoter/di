using TagCloud.Client.ConsoleClient;

namespace TagCloud.Client
{
    public class EntryPoint
    {
        public static void Main(string[] args)
        {
            new ConsoleUi(args).Run();
        }
    }
}
