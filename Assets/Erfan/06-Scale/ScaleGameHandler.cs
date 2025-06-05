using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ScaleGameHandler : GameHandler
{
    public Transform spawnPoint;
    public Transform referenceItemSpawnPoint;
    public Camera mainCamera;

    private int rights;
    private int wrongs;
    private int _totalCount;
    private ScaleConfig.ZoneDifficultyConfig _zoneDConfig;
    private ScalePrefab _scalePrefab;
    private ScaleItem _sampleItem;
    private ScaleConfig currentConfig;

    private void Start()
    {
        currentConfig = GameManager.Instance.currentLevelConfig as ScaleConfig;
        _zoneDConfig = currentConfig.GetConfig(GameManager.Instance.currentLocation,
            GameManager.Instance.currentDifficulty);


        _scalePrefab = Instantiate(_zoneDConfig.prefab, spawnPoint);
        _totalCount = _scalePrefab.rightScaleItems.Count + _scalePrefab.wrongScaleItems.Count;
        _sampleItem = Instantiate(_zoneDConfig.sampleScaleItem, referenceItemSpawnPoint);
        _sampleItem.EnableRightOutlinable();
        UIManager.Instance.HowToPlayAndInGameProcedure(currentConfig.howToPlayText);
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                var rayCastItem = hit.collider.GetComponent<ScaleItem>();
                if (rayCastItem == null) return;
                if (rayCastItem.isWrongScale)
                {
                    rights++;
                    UIManager.Instance.inGameViewInstance.AddToRights(rights);
                    rayCastItem.EnableRightOutlinable();
                    rayCastItem.enabled = false;
                    hit.collider.enabled = false;
                    // if (rights >= _scalePrefab.wrongScaleItems.Count)
                    //     DelayFinishGameBehaviour();
                }
                else
                {
                    wrongs++;
                    hit.collider.enabled = false;
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
        
        if (rights >= wrongs && rights >= _scalePrefab.wrongScaleItems.Count)
            gameState = Common.GameWinState.Win;
        else if (wrongs >= rights && rights+wrongs >= _totalCount)
            gameState = Common.GameWinState.Loose;


        var finishData = new Common.LevelFinishData(rights, wrongs,
            (int)Timer.Instance.timeRemaining, gameState, checkBtnCount,currentConfig.gameName);
        UIManager.Instance.ShowYouWon(finishData);
        if (gameState == Common.GameWinState.Win)
            GameManager.Instance.OnWinGame(finishData);
    }

    #region Singleton

    public bool isDontDestroyOnLoad = false;
    private static ScaleGameHandler _instance;

    public static ScaleGameHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<ScaleGameHandler>();

                if (_instance == null)
                {
                    Debug.LogError($"No instance of {typeof(ScaleGameHandler)} found in the scene.");
                }
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as ScaleGameHandler;
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