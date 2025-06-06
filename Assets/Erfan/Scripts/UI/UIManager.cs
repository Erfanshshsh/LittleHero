﻿using System;
using System.Collections.Generic;
using Joyixir.GameManager.UI;
using RTLTMPro;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIManager : Singleton<UIManager>
{
    [SerializeField] private List<View> windowInstances;
    [SerializeField] private ChooseGameView chooseGameView;
    [SerializeField] private InGameView inGameView;
    [SerializeField] private WonView wonView;
    [SerializeField] private HowToPlayView howToPlayView;
    [SerializeField] private SelectNumberView selectNumberView;
    [SerializeField] private FindFriendView findFriendView;
    [SerializeField] private StatisticsView statisticsView;
   
    
    [SerializeField] private TextElement popUpTextPrefab;

    [NonSerialized] public InGameView inGameViewInstance;
    [NonSerialized] public HowToPlayView howToPlayViewInstance;
    [NonSerialized] public WonView wonViewInstance;

    [PropertyTooltip("3 Different Layers for UI Views")] [FoldoutGroup("UI Containers")]
    public List<GameObject> containers;

    private void OnEnable()
    {
        CloseAllWindows();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
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
        var gamesView = (ChooseGameView)ShowWindow(chooseGameView, ViewPriority.High);
    }


    public void ShowInGameView(int totalRights = 0)
    {
        inGameViewInstance = (InGameView)ShowWindow(inGameView, ViewPriority.High);
        inGameViewInstance.Initialize(totalRights);
    }

    public void ShowYouWon(Common.LevelFinishData finishData = null)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        wonViewInstance = (WonView)ShowWindow(wonView, ViewPriority.High);
        if (finishData != null)
            wonViewInstance.ShowLevelFinishData(finishData);
    }


    public void ShowHowToPlay()
    {
        var config = GameManager.Instance.currentLevelConfig;
        if (howToPlayViewInstance != null)
        {
            howToPlayViewInstance.gameObject.SetActive(true);
            howToPlayViewInstance.AnimateUp();
            howToPlayViewInstance.Initialize(config.howToPlayText);
            return;
        }

        howToPlayViewInstance = (HowToPlayView)ShowWindow(howToPlayView, ViewPriority.High);
        howToPlayViewInstance.Initialize(config.howToPlayText);
    }

    public void HowToPlayAndInGameProcedure(string text, Action onHideComplete = null, int totalRights = 0)
    {
        howToPlayViewInstance = (HowToPlayView)ShowWindow(howToPlayView, ViewPriority.High);
        howToPlayViewInstance.Initialize(text, () =>
        {
            Debug.Log("Hide finished!");
            ShowInGameView(totalRights);
            onHideComplete?.Invoke();
            // Add other logic here
        });
    }

    public SelectNumberView ShowSelectNumber()
    {
        var viewIns = (SelectNumberView)ShowWindow(selectNumberView, ViewPriority.High);
        return viewIns;
    }
    
    public FindFriendView ShowFindFriendView()
    {
        var viewIns = (FindFriendView)ShowWindow(findFriendView, ViewPriority.High);
        return viewIns;
    }
    
    [Button]
    public void ShowStatisticsView(Common.Difficulty currentDifficulty, Common.Location currentLocation)
    {
        GameManager.Instance.DisableController();
        var viewIns = (StatisticsView)ShowWindow(statisticsView, ViewPriority.High);
        viewIns.Initialize(currentDifficulty, currentLocation);
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

    public async void ShowText(string mName)
    {
        var textElement = Instantiate(popUpTextPrefab, containers[2].transform);
        textElement.transform.localScale = Vector3.zero;
        textElement.gameObject.SetActive(true);
        textElement.SetText(mName);
        await StaticTweeners.AnimateUp(textElement.transform, 1, 1.2f);
        await StaticTweeners.AnimateDown(textElement.transform);
        Destroy(textElement.gameObject);
    }


}