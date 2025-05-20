using System;
using System.Collections.Generic;
using Joyixir.GameManager.UI;
using RTLTMPro;
using UnityEngine;
using UnityEngine.UI;


public class WonView : View
{
    public Button homeButton;

    private void OnEnable()
    {
        homeButton.onClick.AddListener(() => GameManager.Instance.LoadScene(0));
    }

    private void OnDisable()
    {
        homeButton.onClick.RemoveAllListeners();
    }

    protected override void OnBackBtn()
    {
    }
}
