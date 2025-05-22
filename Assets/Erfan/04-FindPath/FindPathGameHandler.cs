using System;
using System.Collections.Generic;
using UnityEngine;


public class FindPathGameHandler : Singleton<FindPathGameHandler>
{
    public Transform levelPrefabTf;

    public int rightScore;
    public int wrongScore;
    public int overallCounter;
    private FindPathConfig _currentConfig;
    private List<Butterfly> _butterflyList = new List<Butterfly>();
    private void Start()
    {
        _currentConfig = GameManager.Instance.findPathConfig;

        UIManager.Instance.HowToPlayAndInGameProcedure(_currentConfig.howToPlayText);
        var level = Instantiate(_currentConfig.FindPathLevel, levelPrefabTf);

        for (var i = 0; i < level.splines.Count; i++)
        {
            var spline = level.splines[i];
            var firstKnot = spline[^1];
            Vector3 localPos = firstKnot.Position;

            // Convert local spline point to world space
            Vector3 worldPos = level.m_Spline.transform.TransformPoint(localPos);
            var butterfly = Instantiate(_currentConfig.butterflyPrefab, worldPos, Quaternion.identity);
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

        if (overallCounter >= _currentConfig.FindPathLevel.splines.Count)
        {
            FinishGameBehaviour();
            return true; 
        }
        return false; 

    }
    private void FinishGameBehaviour()
    {
        var finishData = new Common.LevelFinishData(rightScore, wrongScore, 
            (int)Timer.Instance.timeRemaining, rightScore >= wrongScore);
        UIManager.Instance.ShowYouWon(finishData);
    }
}