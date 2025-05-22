using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TypoGameHandler : Singleton<TypoGameHandler>
{
    [SerializeField] private Transform stringParent;
    private TypoConfig _currentConfig;
    private List<TypoItem> _typoItems = new List<TypoItem>();
    public int rightScore;
    public int wrongScore;
    public int overallCounter;

    private void Start()
    {
        _currentConfig = GameManager.Instance.typoConfig;

        UIManager.Instance.HowToPlayAndInGameProcedure(_currentConfig.howToPlayText,
            () =>
            {
                var typoString = Instantiate(_currentConfig.typoString, stringParent);
                var typoItems = typoString.typoItems;
                foreach (var mTypoItem in typoItems)
                {
                    mTypoItem.onClickButton.AddListener(OnTypoClicked);
                    _typoItems.Add(mTypoItem);
                }
            });
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
    }


    private void OnRight()
    {
        rightScore++;
        if (HandleAnswer()) return;
        UIManager.Instance.inGameViewInstance.AddToRights(rightScore);
    }


    private void OnWrong()
    {
        wrongScore++;
        if (HandleAnswer()) return;
        UIManager.Instance.inGameViewInstance.AddToWrongs(wrongScore);
    }

    private bool HandleAnswer()
    {
        overallCounter++;

        if (overallCounter >= _currentConfig.typoString.typoItems.Count)
        {
            FinishGameBehaviour();
            return true;
        }

        return false;
    }

    private void FinishGameBehaviour()
    {
        var finishData = new Common.LevelFinishData(rightScore, wrongScore,
            (int)Timer.Instance.timeRemaining, rightScore >= wrongScore);
        UIManager.Instance.ShowYouWon(finishData);
    }
}