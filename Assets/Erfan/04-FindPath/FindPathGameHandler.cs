using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;


public class FindPathGameHandler : GameHandler
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

        // if (overallCounter >= _zoneDConfig.FindPathLevel.splines.Count)
        // {
        //     DelayFinishGameBehaviour();
        //     return true;
        // }

        return false;
    }


    private async UniTaskVoid DelayFinishGameBehaviour()
    {
        await UniTask.DelayFrame(30);
        var gameState = Common.GameWinState.Neutral;
        gameState = rightScore >= wrongScore ? Common.GameWinState.Win : Common.GameWinState.Loose;
        var finishData = new Common.LevelFinishData(rightScore, wrongScore,
            (int)Timer.Instance.timeRemaining, gameState);
        GameManager.Instance.OnFinishGameAsync(finishData);
    }

    public override void CheckForFinish()
    {
        base.CheckForFinish();
        var gameState = Common.GameWinState.Neutral;

        if (rightScore >= wrongScore && overallCounter >= 5)
        {
            gameState = Common.GameWinState.Win;
        }
        else if(wrongScore >= rightScore && overallCounter >= 5)
        {
            gameState = Common.GameWinState.Loose;
        }


        var finishData = new Common.LevelFinishData(rightScore, wrongScore,
            (int)Timer.Instance.timeRemaining, gameState);
        UIManager.Instance.ShowYouWon(finishData);
        if (gameState == Common.GameWinState.Win)
        {
            GameManager.Instance.OnWinGame();
        }
    }

    #region Singleton

    public bool isDontDestroyOnLoad = false;
    private static FindPathGameHandler _instance;

    public static FindPathGameHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<FindPathGameHandler>();

                if (_instance == null)
                {
                    Debug.LogError($"No instance of {typeof(FindPathGameHandler)} found in the scene.");
                }
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as FindPathGameHandler;
            if (isDontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
        else if (_instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    #endregion
}