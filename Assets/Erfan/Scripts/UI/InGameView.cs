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

    
    private void Start()
    {
        userRights.text = "0";
        userWrongs.text = "0";
    }
    protected override void OnBackBtn()
    {
    }

    public void AddToRights(int score)
    {
        userRights.text = score.ToString();
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