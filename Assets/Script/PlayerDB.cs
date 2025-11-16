using System.Collections.Generic;
using UnityEngine;
using System.IO;


public static class PlayerDB
{
    private const string PLAYERPREFS_KEY = "player_current";
    private static string dbPath => Path.Combine(Application.persistentDataPath, "playerdb.json");


    public static List<PlayerData> LoadAll()
    {
        try
        {
            if (!File.Exists(dbPath)) return new List<PlayerData>();
            var json = File.ReadAllText(dbPath);
            var wrapper = JsonUtility.FromJson<PlayerListWrapper>(json);
            return wrapper?.players ?? new List<PlayerData>();
        }
        catch
        {
            return new List<PlayerData>();
        }
    }


    public static void SaveAll(List<PlayerData> players)
    {
        var wrapper = new PlayerListWrapper { players = players };
        var json = JsonUtility.ToJson(wrapper, true);
        File.WriteAllText(dbPath, json);
    }


    public static void SaveCurrentPlayerId(string playerId)
    {
        PlayerPrefs.SetString(PLAYERPREFS_KEY, playerId);
        PlayerPrefs.Save();
    }


    public static string GetCurrentPlayerId()
    {
        return PlayerPrefs.GetString(PLAYERPREFS_KEY, "");
    }


    public static void ClearCurrentPlayer()
    {
        PlayerPrefs.DeleteKey(PLAYERPREFS_KEY);
        PlayerPrefs.Save();
    }


    [System.Serializable]
    private class PlayerListWrapper { public List<PlayerData> players; }
}