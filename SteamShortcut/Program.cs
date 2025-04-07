using Logger;
using Microsoft.Extensions.DependencyInjection;
using SteamShortcut.Controller;
using SteamShortcut.Form;
using SteamShortcut.Service;

namespace SteamShortcut
{
    internal static class Program
    {
        internal static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var container = Services();
            var shortcutContextMenu = container.GetRequiredService<ShortcutContextMenu>();
            var logger = container.GetRequiredService<ILogger>();
            logger.Info("Starting Steam Shortcut application");

            if (args.Length == 0 && !shortcutContextMenu.IsExists())
            {
                Array.Resize(ref args, 1);
                args[0] = "--install";
            }

            if (args.Length == 0 && shortcutContextMenu.IsExists())
            {
                Array.Resize(ref args, 1);
                args[0] = "--uninstall";
            }

            IController? controller;
            switch (args[0])
            {
                case "--install":
                case "-i":
                    controller = container.GetRequiredService<AddContextMenuItemController>();
                    break;
                case "--uninstall":
                case "-u":
                    controller = container.GetRequiredService<RemoveContextMenuItemController>();
                    break;
                default:
                    controller = container.GetRequiredService<ShortcutController>();
                    break;
            }

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

            return services.BuildServiceProvider();
        }
    }
}
