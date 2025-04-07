using Logger;
using Microsoft.Win32;

namespace WindowsContextMenu
{
    public class WindowsContextMenu(ILogger? logger = null)
    {
        private ILogger Logger => logger ?? new Logger.Logger("WindowsContextMenu", "WindowsContextMenu");

        public bool IsContextMenuExists(string fileExtension, string menuName)
        {
            if (string.IsNullOrEmpty(fileExtension) || string.IsNullOrEmpty(menuName))
            {
                throw new ArgumentException("File extension and menu name cannot be empty.");
            }

            try
            {
                var key = Registry.CurrentUser.OpenSubKey($@"Software\Classes\{fileExtension}\shell", true);
                if (key != null)
                {
                    var subKey = key.OpenSubKey(menuName);
                    return subKey != null;
                }
            }
            catch (Exception ex)
            {
                Logger.Fatal("Error checking context menu existence.", ex);
            }

            return false;
        }

        public void AddContextMenu(string fileExtension, string menuName, string menuCommand, string iconPath)
        {
            if (string.IsNullOrEmpty(fileExtension) || string.IsNullOrEmpty(menuName) || string.IsNullOrEmpty(menuCommand))
            {
                Logger.Error("File extension, menu name, and command cannot be empty.");
            
                throw new ArgumentException("File extension, menu name, and command cannot be empty.");
            }

            if (IsContextMenuExists(fileExtension, menuName))
            {
                Logger.Info($"Context menu item '{menuName}' for extension '{fileExtension}' already exists.");
                return;
            }

            try
            {
                var key = Registry.CurrentUser.OpenSubKey($@"Software\Classes\{fileExtension}\shell", true);
                if (key == null)
                {
                    try
                    {
                        key = Registry.CurrentUser.CreateSubKey($@"Software\Classes\{fileExtension}\shell");
                    }
                    catch (Exception e)
                    {
                        Logger.Fatal(e.Message, e);
                        return;
                    }
                }

                var newKey = key.CreateSubKey(menuName);
                newKey.SetValue("Icon", iconPath);

                var commandKey = newKey.CreateSubKey("command");
                commandKey.SetValue("", menuCommand);

                Logger.Info($"Context menu item '{menuName}' for extension '{fileExtension}' added successfully.");
            }
            catch (Exception ex)
            {
                Logger.Fatal("Error adding context menu item.", ex);
            }
        }

        public void RemoveContextMenu(string fileExtension, string menuName)
        {
            if (string.IsNullOrEmpty(fileExtension) || string.IsNullOrEmpty(menuName))
            {
                throw new ArgumentException("File extension and menu name cannot be empty.");
            }

            if (!IsContextMenuExists(fileExtension, menuName))
            {
                Logger.Info($"Context menu item '{menuName}' for extension '{fileExtension}' does not exist.");
                return;
            }

            try
            {
                var key = Registry.CurrentUser.OpenSubKey($@"Software\Classes\{fileExtension}\shell", true);
                if (key == null)
                {
                    return;
                }
                key.DeleteSubKeyTree(menuName);
                Logger.Info($"Context menu item '{menuName}' for extension '{fileExtension}' removed successfully.");
            }
            catch (Exception ex)
            {
                Logger.Fatal("Error removing context menu item.", ex);
            }
        }
    }
}
