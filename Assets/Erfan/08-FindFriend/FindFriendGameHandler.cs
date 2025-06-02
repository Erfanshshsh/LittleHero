using UnityEngine;
using Image = UnityEngine.UI.Image;

public class FindFriendGameHandler : GameHandler
{
    private FindFriendConfig.ZoneDifficultyConfig _zoneDConfig;
    private int _wrongCounter;
    private int _rightCounter;
    private FindFriendConfig.FriendType sampleFriendType;
    public FindFriendTextElement textElementPrefab;
    public Transform textParent;
    public Image friendImage;
    private void Start()
    {
        textElementPrefab.gameObject.SetActive(false);
        var currentConfig = GameManager.Instance.currentLevelConfig as FindFriendConfig;
        _zoneDConfig = currentConfig.GetConfig(GameManager.Instance.currentLocation,
            GameManager.Instance.currentDifficulty);
        friendImage.sprite = _zoneDConfig.sampleFriendPic;
        sampleFriendType = _zoneDConfig.sampleFriendType;
        foreach (var mFriend in _zoneDConfig.Friends)
        {
            var friend = Instantiate(textElementPrefab, textParent);
            friend.gameObject.SetActive(true);
            friend.SetText(mFriend.mName);
            friend.friendType = mFriend.FriendType;
            friend.onClick += OnClickFriend;

        }
        

        UIManager.Instance.HowToPlayAndInGameProcedure(_zoneDConfig.howToPlayText,
            () => {  });
    }
    
    
    
    private void OnClickFriend(FindFriendConfig.FriendType friendType)
    {

        if (friendType == sampleFriendType)
        {
            UIManager.Instance.inGameViewInstance.AddToRights(1);
            _rightCounter++;
        }
        else
        {
            _wrongCounter++;
            UIManager.Instance.inGameViewInstance.AddToWrongs(_wrongCounter);

            
        }
    }
    
    
    public override void CheckForFinish()
    {
        base.CheckForFinish();
        var gameState = Common.GameWinState.Neutral;
        if (_rightCounter>0 && _wrongCounter<=0)
        {
            gameState = Common.GameWinState.Win;
        }
        else
        {
            gameState = Common.GameWinState.Loose;

        }

        var finishData = new Common.LevelFinishData(_rightCounter, _wrongCounter,
            (int)Timer.Instance.timeRemaining, gameState);
        UIManager.Instance.ShowYouWon(finishData);
        if (gameState == Common.GameWinState.Win)
        {
            GameManager.Instance.OnWinGame();
        }
    }

    #region Singleton

    public bool isDontDestroyOnLoad = false;
    private static FindFriendGameHandler _instance;

    public static FindFriendGameHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<FindFriendGameHandler>();

                if (_instance == null)
                {
                    Debug.LogError($"No instance of {typeof(FindFriendGameHandler)} found in the scene.");
                }
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as FindFriendGameHandler;
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
