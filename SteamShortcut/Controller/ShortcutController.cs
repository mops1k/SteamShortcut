namespace SteamShortcut.Controller
{
    public class ShortcutController(Service.SteamShortcut steamShortcut) : IController
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

            // @todo restart steam
        }
    }
}
