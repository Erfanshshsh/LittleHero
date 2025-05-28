using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;


public class FindPathGameHandler : Singleton<FindPathGameHandler>
{
    public Transform levelPrefabTf;

    public int rightScore;
    public int wrongScore;
    public int overallCounter;
    private List<Butterfly> _butterflyList = new List<Butterfly>();
    private FindPathConfig.ZoneDifficultyConfig _zoneDConfig;

    private void Start()
    {
        var currentConfig = GameManager.Instance.currentLevelConfig as FindPathConfig;
        _zoneDConfig = currentConfig.GetConfig(GameManager.Instance.currentLocation,
            GameManager.Instance.currentDifficulty);

        UIManager.Instance.HowToPlayAndInGameProcedure(currentConfig.howToPlayText);
        var level = Instantiate(_zoneDConfig.FindPathLevel, levelPrefabTf);

        for (var i = 0; i < level.splines.Count; i++)
        {
            var spline = level.splines[i];
            var firstKnot = spline[^1];
            Vector3 localPos = firstKnot.Position;

            // Convert local spline point to world space
            Vector3 worldPos = level.m_Spline.transform.TransformPoint(localPos);
            var butterfly = Instantiate(_zoneDConfig.butterflyPrefab, worldPos, Quaternion.identity);
            butterfly.Initialize(i);
            butterfly.onSelectRightFlower.AddListener(OnRight);
            butterfly.onSelectWrongFlower.AddListener(OnWrong);
            _butterflyList.Add(butterfly);
        }
    }


    private void OnDisable()
    {
        foreach (var butterfly in _butterflyList)
        {
            butterfly.onSelectRightFlower.RemoveAllListeners();
            butterfly.onSelectWrongFlower.RemoveAllListeners();
        }
    }


    private void OnRight()
    {
        rightScore++;
        if (HandleAnswer()) return;
        UIManager.Instance.inGameViewInstance.AddToRights(rightScore);
    }


    private void OnWrong()
    {
        wrongScore++;
        if (HandleAnswer()) return;
        UIManager.Instance.inGameViewInstance.AddToWrongs(wrongScore);
    }

    private bool HandleAnswer()
    {
        overallCounter++;

        if (overallCounter >= _zoneDConfig.FindPathLevel.splines.Count)
        {
            DelayFinishGameBehaviour();
            return true;
        }

        return false;
    }


    private async UniTaskVoid DelayFinishGameBehaviour()
    {
        await UniTask.DelayFrame(30);
        var finishData = new Common.LevelFinishData(rightScore, wrongScore,
            (int)Timer.Instance.timeRemaining, rightScore >= wrongScore);
        GameManager.Instance.OnFinishGameAsync(finishData);
    }
}