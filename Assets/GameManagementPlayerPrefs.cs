using UnityEngine;

namespace Joyixir.GameManager.Utils
{
    internal static class GameManagementPlayerPrefs
    {
        public static string GameMode
        {
            get => PlayerPrefs.GetString("GM-GameMode", "");
            set => PlayerPrefs.SetString("GM-GameMode", value);
        }

        public static bool IsLevelRestarted
        {
            get => PlayerPrefs.GetInt("GM-IsLevelRestarted", 0) == 1; // default is false
            set => PlayerPrefs.SetInt("GM-IsLevelRestarted", value ? 1 : 0);
        }


        
    }
}