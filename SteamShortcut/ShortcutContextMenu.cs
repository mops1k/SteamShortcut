using System.Reflection;
using Logger;

namespace SteamShortcut;

public class ShortcutContextMenu(ILogger? logger = null)
{
    private ILogger _logger => logger ?? new Logger.Logger("SteamShortcut");
    private WindowsContextMenu.WindowsContextMenu WinContextMenu => new WindowsContextMenu.WindowsContextMenu(_logger);
    private string MenuName = "Add to Steam";
    private string? ExeFullPath
    {
        get
        {
            var exePath = Assembly.GetEntryAssembly()?.Location;
            if (exePath == null)
            {
                return null;
            }

            return Path.Combine(Path.GetDirectoryName(exePath), "SteamShortcut.exe");
        }
    }

    public bool IsExists()
    {
        return WinContextMenu.IsContextMenuExists("exefile", MenuName);
    }

    public void Add()
    {
        if (ExeFullPath == null)
        {
            throw new FileNotFoundException("Could not find the SteamShortcut executable.");
        }

        if (!File.Exists(ExeFullPath))
        {
            throw new FileNotFoundException($"SteamShortcut executable not found at: {ExeFullPath}");
        }

        WinContextMenu.AddContextMenu("exefile", MenuName, $"{ExeFullPath} \"%1\"", $"\"{ExeFullPath}\",0");
    }

    public void Remove()
    {
        WinContextMenu.RemoveContextMenu("exefile", MenuName);
    }
}