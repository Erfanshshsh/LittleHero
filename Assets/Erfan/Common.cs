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
        public bool IsWon;

        public LevelFinishData(int rightCount, int wrongCount, int timeCount, bool isWon)
        {
            RightCount = rightCount;
            WrongCount = wrongCount;
            TimeCount = timeCount;
            IsWon = isWon;
        }
    }
    

}
