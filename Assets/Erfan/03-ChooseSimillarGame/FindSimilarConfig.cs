using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "FindSimilarConfig", menuName = "Erfan/Scriptable Objects/FindSimilarConfig")]
public class FindSimilarConfig : LevelConfig
{
    public List<ZoneDifficultyConfig> configurations = new List<ZoneDifficultyConfig>();

    [System.Serializable]
    public class ZoneDifficultyConfig
    {
        public Common.Location location;
        public Common.Difficulty difficulty;
        public ChooseSimilarItem sampleItem;

        public List<ChooseSimilarItem> bottomWrongItems = new List<ChooseSimilarItem>();
        public List<ChooseSimilarItem> bottomCorrectItems = new List<ChooseSimilarItem>();


        public List<ChooseSimilarItem> GetShuffledCombinedList()
        {
            var combined = bottomWrongItems.Concat(bottomCorrectItems).ToList();
            return combined.OrderBy(_ => Random.value).ToList();
        }
    }

    public ZoneDifficultyConfig GetConfig(Common.Location zone, Common.Difficulty difficulty)
    {
        return configurations.FirstOrDefault(cfg => cfg.location == zone && cfg.difficulty == difficulty);
    }
}