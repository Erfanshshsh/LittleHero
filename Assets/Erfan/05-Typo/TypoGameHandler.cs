using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class TypoGameHandler : Singleton<TypoGameHandler>
{
    [SerializeField] private RectTransform stringParent;
    private List<TypoItem> _typoItems = new List<TypoItem>();
    public int rightScore;
    public int wrongScore;
    public int delayFrameCount = 30;
    private RectTransform sentenceRect;
    private int _totalWrongCount = 0;
    private TypoConfig.ZoneDifficultyConfig _zoneDConfig;
    private void Start()
    {
        var currentConfig = GameManager.Instance.currentLevelConfig as TypoConfig;
        _zoneDConfig = currentConfig.GetConfig(GameManager.Instance.currentLocation, 
            GameManager.Instance.currentDifficulty);
        GetTotalWrongs();
        UIManager.Instance.HowToPlayAndInGameProcedure(currentConfig.howToPlayText,
            () =>
            {
                var typoString = Instantiate(_zoneDConfig.typoString, stringParent);
                sentenceRect = typoString.transform as RectTransform;
                var typoItems = typoString.typoItems;
                foreach (var mTypoItem in typoItems)
                {
                    mTypoItem.onClickButton.AddListener(OnTypoClicked);
                    _typoItems.Add(mTypoItem);
                }
            }, _totalWrongCount);
    }

    private void GetTotalWrongs()
    {
        var typoItems = _zoneDConfig.typoString.typoItems;
        foreach (var typoItem in typoItems)
        {
            if (typoItem.isWrong)
            {
                _totalWrongCount++;
            }
        }
    }

    private void OnDisable()
    {
        foreach (var mTypoItem in _typoItems)
        {
            mTypoItem.onClickButton.RemoveAllListeners();
        }
    }

    private void OnTypoClicked(bool isCorrect)
    {
        if (isCorrect)
            OnRight();
        else
            OnWrong();

        DelayRefreshLayout().Forget();
    }

    public async UniTaskVoid DelayRefreshLayout()
    {
        await UniTask.DelayFrame(delayFrameCount);
        LayoutRebuilder.ForceRebuildLayoutImmediate(sentenceRect);
    }


    private void OnRight()
    {
        rightScore++;
        UIManager.Instance.inGameViewInstance.AddToRights(rightScore);
        if (rightScore >= _totalWrongCount)
            DelayFinishGameBehaviour().Forget();

    }


    private void OnWrong()
    {
        wrongScore++;
        UIManager.Instance.inGameViewInstance.AddToWrongs(wrongScore);
    }




    private async UniTaskVoid DelayFinishGameBehaviour()
    {
        await UniTask.DelayFrame(30);
        var finishData = new Common.LevelFinishData(rightScore, wrongScore,
            (int)Timer.Instance.timeRemaining, rightScore >= wrongScore);
        GameManager.Instance.OnFinishGameAsync(finishData);
    }
}