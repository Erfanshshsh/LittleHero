using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using RTLTMPro;
using UnityEngine;
using UnityEngine.UI;

public class NumbersGameHandler : Singleton<NumbersGameHandler>
{
    public List<Transform> spawnPoints = new List<Transform>();
    public int inBoxCount = 0;
    private List<NumbersGameDragObject> items = new List<NumbersGameDragObject>();
    private NumbersGameConfig.ZoneDifficultyConfig _zoneDConfig;
    public NumbersContainer oddsContainer;
    public NumbersContainer evensContainer;
    private void Start()
    {
        var currentConfig = GameManager.Instance.currentLevelConfig as NumbersGameConfig;
        _zoneDConfig = currentConfig.GetConfig(GameManager.Instance.currentLocation,
            GameManager.Instance.currentDifficulty);
        items = _zoneDConfig.items;
        if (_zoneDConfig.isOnlyOdds)
        {
            evensContainer.gameObject.SetActive(false);
        }

        for (var i = 0; i < items.Count; i++)
        {
            Instantiate(items[i], spawnPoints[i]);
        }

        UIManager.Instance.HowToPlayAndInGameProcedure(_zoneDConfig.howToPlayText,
            () => {UpdateScore(); });
        
    }




    public void UpdateScore()
    {
        UIManager.Instance.inGameViewInstance.AddToRights(inBoxCount);
        // inBoxText.text = "تعداد صحیح: " + inBoxCount;
        if (inBoxCount >= _zoneDConfig.numToWin)
        {
            DelayFinishGameBehaviour();
        }
    }
    
    private async UniTaskVoid DelayFinishGameBehaviour()
    {
        await UniTask.DelayFrame(30);
        var finishData = new Common.LevelFinishData(inBoxCount, 0,
            (int)Timer.Instance.timeRemaining, true);
        GameManager.Instance.OnFinishGameAsync(finishData);
    }
}