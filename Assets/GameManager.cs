using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public LevelConfig currentLevelConfig;
    public Common.Location currentLocation;
    public string currentLocationName;
    public Common.Difficulty currentDifficulty;
    public int gameIndex;
    public GameHandler currentGameHandler;

    protected override void Awake()
    {
        base.Awake();
        try
        {
            currentGameHandler = FindObjectsByType<GameHandler>(FindObjectsSortMode.None)[0];
        }
        catch (IndexOutOfRangeException)
        {
            // Handle the case where no GameHandler is found
            Debug.LogWarning("No GameHandler found in the scene.");
            currentGameHandler = null; // Or handle it differently based on your needs
        }
    }

    public void OnGameCardClicked(int cardIndex, LevelConfig levelConfig)
    {
        currentLevelConfig = levelConfig;
        gameIndex = cardIndex;
        SceneManager.sceneLoaded += OnSceneLoaded;
        LoadScene(cardIndex + 1);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentGameHandler = FindObjectsByType<GameHandler>(FindObjectsSortMode.None)[0];
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public async UniTaskVoid OnFinishGameAsync(Common.LevelFinishData finishData = null)
    {
        GameProgressManager.MarkGamePlayed(gameIndex, currentLocation, currentDifficulty);
        await UniTask.Delay(1000);
        UIManager.Instance.ShowYouWon(finishData);
    }

    public void OnWinGame()
    {
        Timer.Instance.enabled = false;
        GameProgressManager.MarkGamePlayed(gameIndex, currentLocation, currentDifficulty);
    }


    public void LoadScene(int sceneIndex)
    {
        UIManager.Instance.CloseAllWindows();
        SceneManager.LoadScene(sceneIndex);
    }

    public void RestartCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}