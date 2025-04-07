using Logger;
using Microsoft.Extensions.DependencyInjection;
using SteamShortcut.Controller;
using SteamShortcut.Form;
using SteamShortcut.Service;

namespace SteamShortcut
{
    internal static class Program
    {
        private static ServiceProvider Container => Services();

        internal static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            SuggestAction(ref args);

            IController controller = args[0] switch
            {
                "--install" or "-i" => Container.GetRequiredService<AddContextMenuItemController>(),
                "--uninstall" or "-u" => Container.GetRequiredService<RemoveContextMenuItemController>(),
                _ => Container.GetRequiredService<ShortcutController>()
            };

            controller.Invoke(args[0]);
        }

        private static ServiceProvider Services()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<ILogger>(
                static _ => new Logger.Logger("SteamShortcut", "SteamShortcut")
            );
            services.AddSingleton<ShortcutContextMenu>();
            services.AddTransient<SteamUserDialog>();
            services.AddTransient<Service.SteamShortcut>();
            services.AddSingleton<AddContextMenuItemController>();
            services.AddSingleton<RemoveContextMenuItemController>();
            services.AddSingleton<ShortcutController>();
            services.AddTransient<SteamProcess>();

            return services.BuildServiceProvider();
        }

        private static void SuggestAction(ref string[] args)
        {
            var shortcutContextMenu = Container.GetRequiredService<ShortcutContextMenu>();

            if (args.Length == 0 && !shortcutContextMenu.IsExists())
            {
                Array.Resize(ref args, 1);
                args[0] = "--install";

                return;
            }

            if (args.Length != 0 || !shortcutContextMenu.IsExists())
            {
                return;
            }

            Array.Resize(ref args, 1);
            args[0] = "--uninstall";
        }
    }
}
