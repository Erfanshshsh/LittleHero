using System;
using System.Collections.Generic;
using Joyixir.GameManager.UI;
using RTLTMPro;
using UnityEngine;
using UnityEngine.UI;


public class WonView : View
{
    public Button homeButton;
    public Button restartButton;
    public Button closeButton;
    public RTLTextMeshPro rightCount;
    public RTLTextMeshPro wrongCount;
    public RTLTextMeshPro time;
    public GameObject youWinText;
    public GameObject youLooseText;
    public GameObject stillNotWonText;
    

    private void OnEnable()
    {
        homeButton.onClick.AddListener(() => GameManager.Instance.LoadScene(0));
        restartButton.onClick.AddListener(() => GameManager.Instance.RestartCurrentLevel());
        closeButton.onClick.AddListener(() => AnimateDown());
    }

    private void OnDisable()
    {
        homeButton.onClick.RemoveAllListeners();
        restartButton.onClick.RemoveAllListeners();
        closeButton.onClick.RemoveAllListeners();

    }

    public void ShowLevelFinishData(Common.LevelFinishData finishData)
    {
        rightCount.text = finishData.RightCount.ToString();
        wrongCount.text = finishData.WrongCount.ToString();

        time.text = StaticUtils.GetRawMinAndSeconds(finishData.TimeCount);

        HandleWinState(finishData);
    }

    private void HandleWinState(Common.LevelFinishData finishData)
    {
        youWinText.SetActive(false);
        youLooseText.SetActive(false);
        stillNotWonText.SetActive(false);
        closeButton.gameObject.SetActive(false);
        switch (finishData.gameWinState)
        {
            case Common.GameWinState.Neutral:
                stillNotWonText.SetActive(true);
                homeButton.gameObject.SetActive(false);
                restartButton.gameObject.SetActive(false);
                closeButton.gameObject.SetActive(true);
                break;

            case Common.GameWinState.Win:
                youWinText.SetActive(true);
                homeButton.gameObject.SetActive(true);
                restartButton.gameObject.SetActive(true);
                break;

            case Common.GameWinState.Loose:
                youLooseText.SetActive(true);
                homeButton.gameObject.SetActive(true);
                restartButton.gameObject.SetActive(true);


                break;

            default:
                Debug.LogWarning("Unknown game state.");
                break;
        }
    }

    protected override void OnBackBtn()
    {
    }

}
