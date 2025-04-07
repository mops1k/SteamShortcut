using Logger;
using System.Windows.Forms;

namespace SteamShortcut
{
    internal class Program
    {
        
        private static ILogger _logger => new Logger.Logger("SteamShortcut", "SteamShortcut");
        private static ShortcutContextMenu ShortcutContextMenu => new ShortcutContextMenu(_logger);
        internal static void Main(string[] args)
        {

            if (args.Length < 1)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

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
