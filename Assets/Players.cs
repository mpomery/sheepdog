using System.Linq;
using System.Collections.Generic;

public static class Players
{
    private static int maxPlayers = 4;
    private static List<PlayerSettings> playerSettings = new List<PlayerSettings>(maxPlayers);

    public static bool AddPlayer(string controls)
    {
        var newPlayer = new PlayerSettings();
        newPlayer.Controller = controls;

        var firstEmpty = playerSettings.IndexOf(null);

        if (firstEmpty != -1)
        {
            playerSettings[firstEmpty] = newPlayer;
            return true;
        }
        return false;
    }

    public static bool RemovePlayer(string controls)
    {
        var player = playerSettings.Where(p => p != null && p.Controller == controls).FirstOrDefault();
        if (player != null)
        {
            var playerIndex = playerSettings.IndexOf(player);
            playerSettings[playerIndex] = null;
            return true;
        }
        return false;
    }

    public static bool IsPlayer(string controls)
    {
        return playerSettings.Exists(player => player != null && player.Controller == controls);
    }

    public static List<PlayerSettings> GetPlayers()
    {
        return playerSettings.Where(p => p != null).ToList();
    }

    public static List<PlayerSettings> GetPlayersList()
    {
        return playerSettings;
    }

    public static int PlayerCount()
    {
        return playerSettings.Where(p => p != null).Count();
    }

    public static string[] players { get; set;  }
}

public class PlayerSettings
{
    public string Controller { get; set; }
    public string Character { get; set; }
    public bool Ready { get; set; }
}