namespace SteamShortcut.Controller;

public interface IController
{
    public void Invoke(params object[]? args);
}
