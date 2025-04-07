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
                    var result = MessageBox.Show("Add Windows context menu item to executable files?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes) {
                        Console.WriteLine("Install");

                        return;
                    }
                    Console.WriteLine("Nope");
                }
                return;
            }

            Console.WriteLine("hi");
        }
    }
}
