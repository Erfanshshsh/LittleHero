using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "FindDifferenceGame", menuName = "Erfan/Scriptable Objects/FindDifferenceGame")]
public class FindDifferenceGame : LevelConfig
{
    public List<ZoneDifficultyConfig> configurations = new List<ZoneDifficultyConfig>();

    [System.Serializable]
    public class ZoneDifficultyConfig
    {
        public Common.Location location;
        public Common.Difficulty difficulty;
        public FindDifferenceImage prefab;
        public Color levelRightSpriteColor = Color.yellow;
    }

    public ZoneDifficultyConfig GetConfig(Common.Location zone, Common.Difficulty difficulty)
    {
        return configurations.FirstOrDefault(cfg => cfg.location == zone && cfg.difficulty == difficulty);
    }
}