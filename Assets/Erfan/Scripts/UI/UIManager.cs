using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Joyixir.GameManager.UI;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;


public class UIManager : Singleton<UIManager>
{
    [SerializeField] private List<View> windowInstances;
    [SerializeField] private ChooseGameView chooseGameView;


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
        var gamesView = (ChooseGameView)ShowWindow(chooseGameView, ViewPriority.High);
        gamesView.Initialize();
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