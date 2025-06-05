using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ArrangingGameHandler : GameHandler
{
    public List<Transform> spawnPoints = new List<Transform>();
    public int inBoxCount = 0;
    public int wrongInBoxCount = 0;
    private List<DragObject> items = new List<DragObject>();
    private ArrangingGameConfig.ZoneDifficultyConfig _zoneDConfig;
    private ArrangingGameConfig currentConfig;
    private void Start()
    {
        currentConfig = GameManager.Instance.currentLevelConfig as ArrangingGameConfig;
        _zoneDConfig = currentConfig.GetConfig(GameManager.Instance.currentLocation,
            GameManager.Instance.currentDifficulty);
        items = _zoneDConfig.items;

        for (var i = 0; i < items.Count; i++)
        {
            Instantiate(items[i], spawnPoints[i]);
        }

        UIManager.Instance.HowToPlayAndInGameProcedure(currentConfig.howToPlayText,
            () => { UpdateScore(); });
    }


    public void UpdateScore()
    {
        UIManager.Instance.inGameViewInstance.AddToRights(inBoxCount);
        UIManager.Instance.inGameViewInstance.AddToWrongs(wrongInBoxCount);
        
        // inBoxText.text = "تعداد صحیح: " + inBoxCount;
        // if (inBoxCount >= items.Count)
        // {
        //     DelayFinishGameBehaviour();
        // }
    }

    private async UniTaskVoid DelayFinishGameBehaviour()
    {
        await UniTask.DelayFrame(30);
        var finishData = new Common.LevelFinishData(inBoxCount, 0,
            (int)Timer.Instance.timeRemaining, Common.GameWinState.Win, checkBtnCount, currentConfig.gameName);
        GameManager.Instance.OnFinishGameAsync(finishData);
    }

    public override void CheckForFinish()
    {
        base.CheckForFinish();
        var gameState = Common.GameWinState.Neutral;
        if (inBoxCount >= items.Count  && wrongInBoxCount <= 0)
        {
            gameState = Common.GameWinState.Win;    
        }

        var finishData = new Common.LevelFinishData(inBoxCount, wrongInBoxCount,
            (int)Timer.Instance.timeRemaining, gameState,checkBtnCount, currentConfig.gameName);
        UIManager.Instance.ShowYouWon(finishData);
        if (gameState == Common.GameWinState.Win)
        {
            GameManager.Instance.OnWinGame(finishData);
        }
    }

    #region Singleton

    public bool isDontDestroyOnLoad = false;
    private static ArrangingGameHandler _instance;

    public static ArrangingGameHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<ArrangingGameHandler>();

                if (_instance == null)
                {
                    Debug.LogError($"No instance of {typeof(ArrangingGameHandler)} found in the scene.");
                }
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as ArrangingGameHandler;
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