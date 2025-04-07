using Logger;
using SteamShortcut.Service;

namespace SteamShortcut.Controller
{
    public class ShortcutController(Service.SteamShortcut steamShortcut, SteamProcess steamProcess, ILogger logger) : IController
    {
        public void Invoke(params object[]? args)
        {
            if (args?.FirstOrDefault() is not string exePath)
            {
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

            var result = MessageBox.Show(
                Localization.SteamShortcut_RestartSteamQuestion,
                Localization.SteamShortcut_RestartSteamCaption,
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Information
            );

            if (result == DialogResult.Cancel)
            {
                return;
            }

            steamProcess.Restart();
        }
    }
}
