using Cysharp.Threading.Tasks;
using Joyixir.GameManager.Utils;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public ArrangingGameItems arrangingGameItems;
    public FindDifferenceGame findDifferenceGame;
    public FindSimilarConfig findSimilarConfig;
    public FindPathConfig findPathConfig;
    
    public Common.Location currentLocation;
    public Common.Difficulty currentDifficulty;
    public int gameIndex;

    public void OnGameCardClicked(int cardIndex)
    {
        gameIndex = cardIndex;
        LoadScene(cardIndex+1);
    }

    [Button]
    public async UniTaskVoid OnFinishGameAsync()
    {
        GameProgressManager.MarkGamePlayed(gameIndex, currentLocation, currentDifficulty);
        await UniTask.Delay(1000);
        UIManager.Instance.ShowYouWon();
    }



    public void LoadScene(int sceneIndex)
    {
        UIManager.Instance.CloseAllWindows();
        SceneManager.LoadScene(sceneIndex);
    }

}