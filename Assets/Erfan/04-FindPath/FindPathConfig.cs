using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "FindPathConfig", menuName = "Erfan/Scriptable Objects/FindPathConfig")]
public class FindPathConfig : LevelConfig
{
    public List<ZoneDifficultyConfig> configurations = new List<ZoneDifficultyConfig>();

    [System.Serializable]
    public class ZoneDifficultyConfig
    {
        public Common.Location location;
        public Common.Difficulty difficulty;
        public Butterfly butterflyPrefab;
        public FindPathLevel FindPathLevel;
    }

    public ZoneDifficultyConfig GetConfig(Common.Location zone, Common.Difficulty difficulty)
    {
        return configurations.FirstOrDefault(cfg => cfg.location == zone && cfg.difficulty == difficulty);
    }
}