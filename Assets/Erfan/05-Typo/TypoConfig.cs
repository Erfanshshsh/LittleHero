using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "TypoConfig", menuName = "Erfan/Scriptable Objects/TypoConfig")]
public class TypoConfig : LevelConfig
{
    public List<ZoneDifficultyConfig> configurations = new List<ZoneDifficultyConfig>();
    
    [System.Serializable]
    public class ZoneDifficultyConfig
    {
        public Common.Location location;
        public Common.Difficulty difficulty;
        public TypoString typoString;
    }
    public ZoneDifficultyConfig GetConfig(Common.Location zone, Common.Difficulty difficulty)
    {
        return configurations.FirstOrDefault(cfg => cfg.location == zone && cfg.difficulty == difficulty);
    }
}



