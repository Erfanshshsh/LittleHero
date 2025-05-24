using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TypoGameHandler : Singleton<TypoGameHandler>
{
    [SerializeField] private RectTransform stringParent;
    private TypoConfig _currentConfig;
    private List<TypoItem> _typoItems = new List<TypoItem>();
    public int rightScore;
    public int wrongScore;
    public int delayFrameCount = 30;
    private RectTransform sentenceRect;
    private int _totalWrongCount = 0;

    private void Start()
    {
        _currentConfig = GameManager.Instance.typoConfig;
        GetTotalWrongs();

        UIManager.Instance.HowToPlayAndInGameProcedure(_currentConfig.howToPlayText,
            () =>
            {
                var typoString = Instantiate(_currentConfig.typoString, stringParent);
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
        var typoItems = _currentConfig.typoString.typoItems;
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


    private void FinishGameBehaviour()
    {
        var finishData = new Common.LevelFinishData(rightScore, wrongScore,
            (int)Timer.Instance.timeRemaining, rightScore >= wrongScore);
        UIManager.Instance.ShowYouWon(finishData);
    }

    private async UniTaskVoid DelayFinishGameBehaviour()
    {
        await UniTask.DelayFrame(30);
        var finishData = new Common.LevelFinishData(rightScore, wrongScore,
            (int)Timer.Instance.timeRemaining, rightScore >= wrongScore);
        UIManager.Instance.ShowYouWon(finishData);
    }
}