using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ChooseSimilarGameHandler : GameHandler
{
    public Transform sampleSpawnTf;
    public List<Transform> spawnPoints = new List<Transform>();
    public Camera mainCamera;
    private ChooseSimilarItem sampleItem;

    private int rights;
    private int wrongs;

    private FindSimilarConfig.ZoneDifficultyConfig _zoneDConfig;
    private FindSimilarConfig currentConfig;
    private void Start()
    {
        currentConfig = GameManager.Instance.currentLevelConfig as FindSimilarConfig;
        _zoneDConfig = currentConfig.GetConfig(GameManager.Instance.currentLocation,
            GameManager.Instance.currentDifficulty);

        sampleItem = Instantiate(_zoneDConfig.sampleItem, sampleSpawnTf);
        var items = _zoneDConfig.GetShuffledCombinedList();

        for (var i = 0; i < items.Count; i++)
        {
            Instantiate(items[i], spawnPoints[i]);
        }

        UIManager.Instance.HowToPlayAndInGameProcedure(currentConfig.howToPlayText);
    }


    private async void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                var rayCastItem = hit.collider.GetComponent<ChooseSimilarItem>();
                if (rayCastItem == null) return;
                if (rayCastItem.itemType == sampleItem.itemType)
                {
                    rights++;
                    StaticTweeners.AnimateDown(rayCastItem.transform);
                    UIManager.Instance.inGameViewInstance.AddToRights(rights);
                    rayCastItem.EnableRightOutlinable();

                    rayCastItem.enabled = false;
                    // if (rights >= _zoneDConfig.bottomCorrectItems.Count)
                    // {
                    //     DelayFinishGameBehaviour();
                    // }
                }
                else
                {
                    wrongs++;
                    rayCastItem.gameObject.GetComponent<BoxCollider>().enabled = false;
                    StaticTweeners.DoYoyoScale(rayCastItem.transform);
                    UIManager.Instance.inGameViewInstance.AddToWrongs(wrongs);
                    rayCastItem.EnableWrongOutlinable();
                }
            }
        }
    }


    private async UniTaskVoid DelayFinishGameBehaviour()
    {
        await UniTask.DelayFrame(30);
        var gameState = Common.GameWinState.Neutral;
        gameState = rights >= wrongs ? Common.GameWinState.Win : Common.GameWinState.Loose;
        
        var finishData = new Common.LevelFinishData(rights, wrongs,
            (int)Timer.Instance.timeRemaining, gameState, checkBtnCount, currentConfig.gameName);
        GameManager.Instance.OnFinishGameAsync(finishData);
    }
    
    public override void CheckForFinish()
    {
        base.CheckForFinish();
        var gameState = Common.GameWinState.Neutral;
        
        if (rights >= _zoneDConfig.bottomCorrectItems.Count)
        {
            gameState = Common.GameWinState.Win;

        }

        var finishData = new Common.LevelFinishData(rights, wrongs,
            (int)Timer.Instance.timeRemaining, gameState, checkBtnCount, currentConfig.gameName);
        UIManager.Instance.ShowYouWon(finishData);
        if (gameState == Common.GameWinState.Win)
        {
            GameManager.Instance.OnWinGame(finishData);
        }
    }
    
    #region Singleton

    public bool isDontDestroyOnLoad = false;
    private static ChooseSimilarGameHandler _instance;

    public static ChooseSimilarGameHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<ChooseSimilarGameHandler>();

                if (_instance == null)
                {
                    Debug.LogError($"No instance of {typeof(ChooseSimilarGameHandler)} found in the scene.");
                }
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as ChooseSimilarGameHandler;
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