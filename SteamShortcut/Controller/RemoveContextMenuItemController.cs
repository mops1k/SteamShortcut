using SteamShortcut.Service;

namespace SteamShortcut.Controller;

public class RemoveContextMenuItemController(ShortcutContextMenu shortcutContextMenu) : IController
{
    public void Invoke(params object[]? args)
    {
        DialogResult result = MessageBox.Show(
            Localization.ContextMenu_RemoveQuestion,
            Localization.MessageBox_Confirm,
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
        );
        if (result != DialogResult.Yes)
        {
            return;
        }

        shortcutContextMenu.Remove();
    }
}
