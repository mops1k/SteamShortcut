using Logger;
using SteamShortcut.Service;

namespace SteamShortcut.Controller;

public class ShortcutController(Service.SteamShortcut steamShortcut, SteamProcess steamProcess, ILogger logger)
    : IController
{
    public void Invoke(params object[]? args)
    {
        if (args?.FirstOrDefault() is not string exePath)
        {
            return;
        }

        if (!Path.Exists(exePath))
        {
            logger.Error($"Cannot find executable path: {exePath}");
            return;
        }

        if (!steamShortcut.InitialisePaths())
        {
            return;
        }

        if (!steamShortcut.Add(exePath))
        {
            return;
        }

        if (!steamProcess.IsRunning)
        {
            return;
        }

        DialogResult result = MessageBox.Show(
            Localization.SteamShortcut_RestartSteamQuestion,
            Localization.SteamShortcut_RestartSteamCaption,
            MessageBoxButtons.OKCancel,
            MessageBoxIcon.Information
        );

        if (result == DialogResult.Cancel)
        {
            return;
        }

        if (!steamProcess.Restart())
        {
            logger.Error($"Failed to restart Steam process: {exePath}");
            MessageBox.Show(Localization.ShortcutController_Invoke_Failed_to_restart_Steam_process,
                Localization.SteamShortcut_RestartSteamCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
