using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "NumbersGameConfig", menuName = "Erfan/Scriptable Objects/NumbersGameConfig")]
public class NumbersGameConfig : LevelConfig
{
    public List<ZoneDifficultyConfig> configurations = new List<ZoneDifficultyConfig>();

    [System.Serializable]
    public class ZoneDifficultyConfig
    {
        public Common.Location location;
        public Common.Difficulty difficulty;
        public List<NumbersGameDragObject> items = new List<NumbersGameDragObject>();
        public bool isOnlyOdds;
        public string howToPlayText;
        public int numToWin = 3;
    }

    public ZoneDifficultyConfig GetConfig(Common.Location zone, Common.Difficulty difficulty)
    {
        return configurations.FirstOrDefault(cfg => cfg.location == zone && cfg.difficulty == difficulty);
    }
}