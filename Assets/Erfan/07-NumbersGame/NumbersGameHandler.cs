using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using RTLTMPro;
using UnityEngine;
using UnityEngine.UI;

public class NumbersGameHandler : GameHandler
{
    public List<Transform> spawnPoints = new List<Transform>();
    public int rightInBoxCount = 0;
    public int wrongInBoxCount = 0;
    private List<NumbersGameDragObject> items = new List<NumbersGameDragObject>();
    private NumbersGameConfig.ZoneDifficultyConfig _zoneDConfig;
    public NumbersContainer oddsContainer;
    public NumbersContainer evensContainer;
    NumbersGameConfig currentConfig;
    private void Start()
    {
        currentConfig = GameManager.Instance.currentLevelConfig as NumbersGameConfig;
        _zoneDConfig = currentConfig.GetConfig(GameManager.Instance.currentLocation,
            GameManager.Instance.currentDifficulty);
        items = _zoneDConfig.items;
        StaticUtils.ShuffleList(items);
        if (_zoneDConfig.isOnlyOdds)
        {
            evensContainer.gameObject.SetActive(false);
        }

        for (var i = 0; i < items.Count; i++)
        {
            Instantiate(items[i], spawnPoints[i]);
        }

        UIManager.Instance.HowToPlayAndInGameProcedure(_zoneDConfig.howToPlayText,
            () => { UpdateScore(); });
    }


    public void UpdateScore()
    {
        UIManager.Instance.inGameViewInstance.AddToRights(rightInBoxCount);
        UIManager.Instance.inGameViewInstance.AddToWrongs(wrongInBoxCount);
        // if (rightInBoxCount >= _zoneDConfig.numToWin)
        // {
        //     DelayFinishGameBehaviour();
        // }
    }

    public async UniTaskVoid DelayFinishGameBehaviour()
    {
        await UniTask.DelayFrame(30);
        var finishData = new Common.LevelFinishData(rightInBoxCount, 0,
            (int)Timer.Instance.timeRemaining, Common.GameWinState.Win, checkBtnCount, currentConfig.gameName);
        GameManager.Instance.OnFinishGameAsync(finishData);
    }

    public override void CheckForFinish()
    {
        base.CheckForFinish();
        var gameState = Common.GameWinState.Neutral;
        if (rightInBoxCount >= _zoneDConfig.numToWin && wrongInBoxCount <= 0)
        {
            gameState = Common.GameWinState.Win;
        }

        var finishData = new Common.LevelFinishData(rightInBoxCount, wrongInBoxCount,
            (int)Timer.Instance.timeRemaining, gameState, checkBtnCount, currentConfig.gameName);
        UIManager.Instance.ShowYouWon(finishData);
        if (gameState == Common.GameWinState.Win)
        {
            GameManager.Instance.OnWinGame(finishData);
        }
    }

    #region Singleton

    public bool isDontDestroyOnLoad = false;
    private static NumbersGameHandler _instance;

    public static NumbersGameHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<NumbersGameHandler>();

                if (_instance == null)
                {
                    Debug.LogError($"No instance of {typeof(NumbersGameHandler)} found in the scene.");
                }
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as NumbersGameHandler;
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