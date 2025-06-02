using System;
using RTLTMPro;
using UnityEditor;
using UnityEngine;

public class Common
{
    public enum ArrangingGameItemType
    {
        Sport,
        Math,
        Art
    }
    
    
    public enum ChooseSimilarGameItemType
    {
        School,
        Hospital,
        AmusementPark,
        Sports,
        Nature,
        Animals
        
    }
    
    
    public enum GameLocation
    {
        School,
        Hospital,
        AmusementPark
    }

    public enum Difficulty
    {
        Easy, Medium, Hard
    }
    
    public enum Location { School, Hospital, AmusementPark }

    public class LevelFinishData
    {
        public int RightCount;
        public int WrongCount;
        public int TimeCount;
        public GameWinState gameWinState;

        public LevelFinishData(int rightCount, int wrongCount, int timeCount, GameWinState mGameWinState)
        {
            RightCount = rightCount;
            WrongCount = wrongCount;
            TimeCount = timeCount;
            gameWinState = mGameWinState;
        }
    }
    
    public enum NumbersGameItemType
    {
        even,
        odd,
    }
    
    public enum GameWinState
    {
        Neutral,
        Win,
        Loose,
    }

    

}
