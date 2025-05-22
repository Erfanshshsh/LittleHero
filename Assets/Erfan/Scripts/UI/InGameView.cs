using System;
using Joyixir.GameManager.UI;
using RTLTMPro;
using Sirenix.OdinInspector;
using UnityEngine;


public class InGameView : View
{

    public RTLTextMeshPro userRights;
    public RTLTextMeshPro userWrongs;
    public Timer timer;

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