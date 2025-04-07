using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace VDFMapper.ShortcutConfig
{
    public static class SteamShortcutPath
    {
        public static string? GetUserDataPath()
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                throw new PlatformNotSupportedException();
            }

            var key = Registry.LocalMachine.OpenSubKey(@"Software\Wow6432Node\Valve\Steam");
            if (key == null)
            {
                return null;
            }

            object? path = key.GetValue("InstallPath", null);

            return path == null ? null : Path.Combine((string)path, "userdata");
        }

        public static int? GetUserId()
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                throw new PlatformNotSupportedException();
            }

            var key = Registry.CurrentUser.OpenSubKey(@"Software\Valve\Steam\ActiveProcess");
            if (key == null)
            {
                return null;
            }

            int? activeUserId = (int?)key.GetValue("ActiveUser", null);

            return activeUserId == 0 ? null : activeUserId;
        }

        public static string GetShortcutsPath(int userId)
        {
            return Path.Combine(GetUserDataPath() ?? "", userId.ToString(), "config", "shortcuts.vdf");
        }

        public static string GetGridPath(int userId)
        {
            string path = Path.Combine(GetUserDataPath() ?? "", userId.ToString(), "config", "grid");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }
    }
}
