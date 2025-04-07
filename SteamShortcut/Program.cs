using Logger;

namespace SteamShortcut
{
    internal class Program
    {
        
        private static ILogger _logger => new Logger.Logger("SteamShortcut");
        private static ShortcutContextMenu ShortcutContextMenu => new ShortcutContextMenu(_logger);
        internal static void Main(string[] args)
        {

            if (args.Length < 1)
            {
                if (!ShortcutContextMenu.IsExists()) {
                    // @todo ask question to add
                    Console.WriteLine("Does not exists");
                }
                return;
            }

            Console.WriteLine("hi");
        }
    }
}
