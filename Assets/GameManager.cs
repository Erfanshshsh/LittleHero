using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public LevelConfig currentLevelConfig;
    public Common.Location currentLocation;
    public Common.Difficulty currentDifficulty;
    public int gameIndex;

    public void OnGameCardClicked(int cardIndex, LevelConfig levelConfig)
    {
        currentLevelConfig = levelConfig;
        gameIndex = cardIndex;
        LoadScene(cardIndex+1);
    }

    public async UniTaskVoid OnFinishGameAsync(Common.LevelFinishData finishData = null)
    {
        GameProgressManager.MarkGamePlayed(gameIndex, currentLocation, currentDifficulty);
        await UniTask.Delay(1000);
        UIManager.Instance.ShowYouWon(finishData);
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