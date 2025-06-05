using System;
using Cysharp.Threading.Tasks;
using Joyixir.GameManager.Utils;
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
        GameProgressManager.SetZoneStarted(currentDifficulty, currentLocation);
        currentLevelConfig = levelConfig;
        gameIndex = cardIndex;
        SceneManager.sceneLoaded += OnSceneLoaded;
        LoadScene(cardIndex + 1);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentGameHandler = FindObjectsByType<GameHandler>(FindObjectsSortMode.None)[0];
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

    }

    public async UniTaskVoid OnFinishGameAsync(Common.LevelFinishData finishData = null)
    {
        GameProgressManager.MarkGamePlayed(gameIndex, currentLocation, currentDifficulty, finishData);
        await UniTask.Delay(1000);
        UIManager.Instance.ShowYouWon(finishData);
    }

    public void OnWinGame(Common.LevelFinishData finishData)
    {
        Timer.Instance.enabled = false;
        GameProgressManager.MarkGamePlayed(gameIndex, currentLocation, currentDifficulty, finishData);
        if (GameProgressManager.AreAllGamesCompleted(currentLocation, currentDifficulty))
        {
            GameProgressManager.DeleteZoneStarted(currentDifficulty);
            UIManager.Instance.ShowStatisticsView(currentDifficulty, currentLocation);
        }
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

    public void PlayNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        var sceneCount = SceneManager.sceneCountInBuildSettings;
        gameIndex++;
        if (currentSceneIndex + 1 < sceneCount)
        {
            LoadScene(currentSceneIndex + 1);
        }

    }
    
    public void DisableController()
    {
        if (MaleCharacter.Instance != null)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            MaleCharacter.Instance.GetComponent<movement>().enabled = false;
            MaleCharacter.Instance.GetComponent<Animator>().SetBool("MoveFWD", false);
        }
    }
    
    public void EnableController()
    {
        if (MaleCharacter.Instance != null)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            MaleCharacter.Instance.GetComponent<movement>().enabled = true;
            MaleCharacter.Instance.GetComponent<Animator>().SetBool("MoveFWD", true);
        }

    }
}