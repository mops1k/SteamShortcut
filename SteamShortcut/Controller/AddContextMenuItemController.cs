using SteamShortcut.Service;

namespace SteamShortcut.Controller;

public class AddContextMenuItemController(ShortcutContextMenu shortcutContextMenu) : IController
{
    public void Invoke(params object[]? args)
    {
        DialogResult result = MessageBox.Show(
            Localization.ContextMenu_AddQuestion,
            Localization.MessageBox_Confirm,
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
        );
        if (result != DialogResult.Yes)
        {
            return;
        }

        shortcutContextMenu.Add();
    }
}
