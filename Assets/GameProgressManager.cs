using UnityEngine;

public static class GameProgressManager 
{
    public static void MarkGamePlayed(int gameIndex, Common.Location location, Common.Difficulty difficulty)
    {
        string key = GetKey(gameIndex, location, difficulty);
        PlayerPrefs.SetInt(key, 1); // 1 = played
        PlayerPrefs.Save();
    }

    public static bool HasGameBeenPlayed(int gameIndex, Common.Location location, Common.Difficulty difficulty)
    {
        string key = GetKey(gameIndex, location, difficulty);
        return PlayerPrefs.GetInt(key, 0) == 1;
    }

    private static string GetKey(int gameIndex, Common.Location location, Common.Difficulty difficulty)
    {
        return $"{location}_Game{gameIndex}_{difficulty}";
    }
}