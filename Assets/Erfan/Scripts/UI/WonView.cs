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
    public RTLTextMeshPro rightCount;
    public RTLTextMeshPro wrongCount;
    public RTLTextMeshPro time;
    public RTLTextMeshPro youWinText;
    public RTLTextMeshPro youLooseText;
    

    private void OnEnable()
    {
        homeButton.onClick.AddListener(() => GameManager.Instance.LoadScene(0));
        restartButton.onClick.AddListener(() => GameManager.Instance.RestartCurrentLevel());
    }

    private void OnDisable()
    {
        homeButton.onClick.RemoveAllListeners();
        restartButton.onClick.RemoveAllListeners();
    }

    public void ShowLevelFinishData(Common.LevelFinishData finishData)
    {
        rightCount.text = finishData.RightCount.ToString();
        wrongCount.text = finishData.WrongCount.ToString();

        time.text = StaticUtils.GetRawMinAndSeconds(finishData.TimeCount);
        youWinText.gameObject.SetActive(finishData.IsWon);
        youLooseText.gameObject.SetActive(!finishData.IsWon);
    }

    protected override void OnBackBtn()
    {
    }
}
