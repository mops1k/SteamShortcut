using Logger;

namespace SteamShortcut.Service;

public class ShortcutContextMenu(ILogger? logger = null)
{
    private readonly string _menuName = Localization.ContextMenu_Name;
    private ILogger _logger => logger ?? new Logger.Logger("SteamShortcut", "SteamShortcut");
    private WindowsContextMenu.WindowsContextMenu WinContextMenu => new(_logger);

    private string? ExeFullPath
    {
        get
        {
            string exePath = AppContext.BaseDirectory;

            return Path.Combine(Path.GetDirectoryName(exePath) ?? ".", "SteamShortcut.exe");
        }
    }

    public bool IsExists() => WinContextMenu.IsContextMenuExists("exefile", _menuName);

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

        WinContextMenu.AddContextMenu("exefile", _menuName, $"{ExeFullPath} \"%1\"", $"\"{ExeFullPath}\",0");
    }

    public void Remove() => WinContextMenu.RemoveContextMenu("exefile", _menuName);
}
