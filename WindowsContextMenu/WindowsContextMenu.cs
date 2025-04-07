using System;
using Logger;
using Microsoft.Win32;

namespace WindowsContextMenu
{
    public class WindowsContextMenu(ILogger? logger = null)
    {
        private ILogger _logger => logger ?? new Logger.Logger("WindowsContextMenu");

        /// <summary>
        /// Checks if a context menu item with the specified name exists for the given file extension.
        /// </summary>
        /// <param name="fileExtension">The file extension (e.g., ".exe").</param>
        /// <param name="menuName">The name of the context menu item.</param>
        /// <returns>True if the context menu item exists, otherwise False.</returns>
        public bool IsContextMenuExists(string fileExtension, string menuName)
        {
            if (string.IsNullOrEmpty(fileExtension) || string.IsNullOrEmpty(menuName))
            {
                throw new ArgumentException("File extension and menu name cannot be empty.");
            }

            try
            {
                // Open the key for the specified file extension
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey($"Software\\Classes\\{fileExtension}\\shell", true))
                {
                    if (key != null)
                    {
                        // Check if a subkey with the specified name exists
                        using (RegistryKey subKey = key.OpenSubKey(menuName))
                        {
                            return subKey != null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Fatal("Error checking context menu existence.", ex);
            }

            return false;
        }

        /// <summary>
        /// Adds a context menu item for files with the specified extension.
        /// </summary>
        /// <param name="fileExtension">The file extension (e.g., ".exe").</param>
        /// <param name="menuName">The name of the context menu item.</param>
        /// <param name="menuCommand">The command to execute.</param>
        /// <param name="iconPath"></param>
        public void AddContextMenu(string fileExtension, string menuName, string menuCommand, string iconPath)
        {
            if (String.IsNullOrEmpty(fileExtension) || String.IsNullOrEmpty(menuName) || String.IsNullOrEmpty(menuCommand))
            {
                _logger.Error("File extension, menu name, and command cannot be empty.");
            
                throw new ArgumentException("File extension, menu name, and command cannot be empty.");
            }

            if (IsContextMenuExists(fileExtension, menuName))
            {
                _logger.Info($"Context menu item '{menuName}' for extension '{fileExtension}' already exists.");
                return;
            }

            try
            {
                // Open the key for the specified file extension
                var key = Registry.CurrentUser.OpenSubKey($"Software\\Classes\\{fileExtension}\\shell", true);
                if (key == null)
                {
                    try
                    {
                        key = Registry.CurrentUser.CreateSubKey($"Software\\Classes\\{fileExtension}\\shell");
                    }
                    catch (Exception e)
                    {
                        _logger.Fatal(e.Message, e);
                        return;
                    }
                }

                // Create a new subkey for the context menu item
                using (var newKey = key.CreateSubKey(menuName))
                {
                    newKey.SetValue("Icon", iconPath);
                    // Create the "command" subkey and set the command
                    using (RegistryKey commandKey = newKey.CreateSubKey("command"))
                    {
                        commandKey.SetValue("", menuCommand);
                        _logger.Info($"Context menu item '{menuName}' for extension '{fileExtension}' added successfully.");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Fatal("Error adding context menu item.", ex);
            }
        }

        /// <summary>
        /// Removes a context menu item for files with the specified extension.
        /// </summary>
        /// <param name="fileExtension">The file extension (e.g., ".exe").</param>
        /// <param name="menuName">The name of the context menu item to remove.</param>
        public void RemoveContextMenu(string fileExtension, string menuName)
        {
            if (string.IsNullOrEmpty(fileExtension) || string.IsNullOrEmpty(menuName))
            {
                throw new ArgumentException("File extension and menu name cannot be empty.");
            }

            if (!IsContextMenuExists(fileExtension, menuName))
            {
                _logger.Info($"Context menu item '{menuName}' for extension '{fileExtension}' does not exist.");
                return;
            }

            try
            {
                // Open the key for the specified file extension
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey($"Software\\Classes\\{fileExtension}\\shell", true))
                {
                    if (key != null)
                    {
                        // Delete the subkey for the context menu item
                        key.DeleteSubKeyTree(menuName);
                        _logger.Info($"Context menu item '{menuName}' for extension '{fileExtension}' removed successfully.");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Fatal("Error removing context menu item.", ex);
            }
        }
    }
}
