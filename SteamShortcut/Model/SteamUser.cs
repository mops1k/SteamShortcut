namespace SteamShortcut.Model;

public class SteamUser(int id, string? username)
{
    public int Id => id;
    private string? Username => username;

    public override string ToString() => $"{Username} (ID: {Id})";
}
