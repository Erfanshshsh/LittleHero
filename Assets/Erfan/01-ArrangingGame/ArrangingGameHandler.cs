using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using RTLTMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArrangingGameHandler : Singleton<ArrangingGameHandler>
{
    public List<Transform> spawnPoints = new List<Transform>();


    public int inBoxCount = 0;
    private List<DragObject> items = new List<DragObject>();
    private ArrangingGameConfig.ZoneDifficultyConfig _zoneDConfig;

    private void Start()
    {
        var currentConfig = GameManager.Instance.currentLevelConfig as ArrangingGameConfig;
        _zoneDConfig = currentConfig.GetConfig(GameManager.Instance.currentLocation,
            GameManager.Instance.currentDifficulty);
        items = _zoneDConfig.items;

        for (var i = 0; i < items.Count; i++)
        {
            Instantiate(items[i], spawnPoints[i]);
        }

        UIManager.Instance.HowToPlayAndInGameProcedure(currentConfig.howToPlayText,
            () => { });
        UpdateScore();
    }




    public void UpdateScore()
    {
        UIManager.Instance.inGameViewInstance.AddToRights(inBoxCount);
        // inBoxText.text = "تعداد صحیح: " + inBoxCount;
        if (inBoxCount >= items.Count)
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