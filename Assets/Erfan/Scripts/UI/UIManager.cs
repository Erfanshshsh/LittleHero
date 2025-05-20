using System;
using System.Collections.Generic;
using Joyixir.GameManager.UI;
using Sirenix.OdinInspector;
using UnityEngine;


public class UIManager : Singleton<UIManager>
{
    [SerializeField] private List<View> windowInstances;
    [SerializeField] private ChooseGameView chooseGameView;
    [SerializeField] private InGameView inGameView;
    [SerializeField] private WonView wonView;
    [SerializeField] private HowToPlayView howToPlayView;
    [SerializeField] private SelectNumberView selectNumberView;

    [NonSerialized] public InGameView inGameViewInstance;

    [PropertyTooltip("3 Different Layers for UI Views")] [FoldoutGroup("UI Containers")]
    public List<GameObject> containers;

    private void OnEnable()
    {
        CloseAllWindows();
    }


    private View ShowWindow(View viewPrefab, ViewPriority priorityOrder, Transform customParent = null)
    {
        var windowsContainer = containers[(int)priorityOrder];
        if (customParent != null)
        {
            windowsContainer = customParent.gameObject;
        }

        View window = MonoUtils.CreateUIInstance(viewPrefab, windowsContainer, true);
        window.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        windowInstances.Add(window);
        return window;
    }


    [Button]
    public void ShowChooseGameView()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        var gamesView = (ChooseGameView)ShowWindow(chooseGameView, ViewPriority.High);
    }

    
    public void ShowInGameView()
    {
        inGameViewInstance = (InGameView)ShowWindow(inGameView, ViewPriority.High);
    }
    public void ShowYouWon()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        ShowWindow(wonView, ViewPriority.High);
    }
    
    
    
    public void ShowHowToPlay(string text)
    {
        var howToPlay = (HowToPlayView)ShowWindow(howToPlayView, ViewPriority.High);
        howToPlay.Initialize(text);
    }
    
    public SelectNumberView ShowSelectNumber()
    {
        var viewIns = (SelectNumberView)ShowWindow(selectNumberView, ViewPriority.High);
        return viewIns;
    }
    public void CloseAllWindows()
    {
        for (var i = 0; i < windowInstances.Count; i++)
        {
            var window = windowInstances[i];
            if (window != null)
            {
                window.Close();
            }
        }

        windowInstances = new List<View>();
    }


    private enum ViewPriority
    {
        Low = 0,
        Medium = 1,
        High = 2
    }



}