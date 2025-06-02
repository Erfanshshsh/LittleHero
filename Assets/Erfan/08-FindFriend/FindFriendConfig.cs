using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "FindFriendConfig", menuName = "Erfan/Scriptable Objects/FindFriendConfig")]
public class FindFriendConfig : LevelConfig
{
    public List<ZoneDifficultyConfig> configurations = new List<ZoneDifficultyConfig>();

    [System.Serializable]
    public class ZoneDifficultyConfig
    {
        public Common.Location location;
        public Common.Difficulty difficulty;
        public Sprite sampleFriendPic;
        public FriendType sampleFriendType;
        public List<Friend> Friends = new List<Friend>();
        public string howToPlayText;
    }

    public ZoneDifficultyConfig GetConfig(Common.Location zone, Common.Difficulty difficulty)
    {
        return configurations.FirstOrDefault(cfg => cfg.location == zone && cfg.difficulty == difficulty);
    }
    
    [Serializable]
    public class Friend
    {
        public string mName;
        public FriendType FriendType;
    }

    public enum FriendType
    {
        boy,
        girl
    }
}