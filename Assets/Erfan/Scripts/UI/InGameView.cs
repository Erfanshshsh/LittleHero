using System;
using Joyixir.GameManager.UI;
using RTLTMPro;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;


public class InGameView : View
{

    public RTLTextMeshPro userRights;
    public RTLTextMeshPro userWrongs;
    public Timer timer;
    public Button restartButton;
    public Button homeButton;

    public int totalRights;
    
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

    


    public void Initialize(int mTotalRights = 0)
    {
        userRights.text = "0";
        userWrongs.text = "0";
        totalRights = mTotalRights;
        if (totalRights > 0)
        {
            userRights.text = $"{0} / {totalRights}";
        }
    }
    protected override void OnBackBtn()
    {
    }

    public void AddToRights(int score)
    {
        userRights.text = score.ToString();
        if (totalRights > 0)
        {
            userRights.text = $"{score} / {totalRights}";
        }
    }
    
    public void AddToWrongs(int wrongs)
    {
        userWrongs.text = wrongs.ToString();
    }

    public void StartTimer()
    {
        timer.StartTimer();
    }
}