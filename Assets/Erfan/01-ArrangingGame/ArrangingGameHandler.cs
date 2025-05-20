using System;
using System.Collections.Generic;
using RTLTMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArrangingGameHandler : Singleton<ArrangingGameHandler>
{
    public List<Transform> spawnPoints = new List<Transform>();

    public RTLTextMeshPro inBoxText; // UI Text to display the count of objects in the box
    public Button checkButton;

    public int inBoxCount = 0;
    private List<DragObject> items = new List<DragObject>();

    private void Start()
    {
        items = GameManager.Instance.arrangingGameItems.items;

        for (var i = 0; i < items.Count; i++)
        {
            Instantiate(items[i], spawnPoints[i]);
        }

        checkButton.onClick.AddListener(UpdateScore);
        UpdateScore();
    }

    private void OnDisable()
    {
        checkButton.onClick.RemoveListener(UpdateScore);
    }


    public void UpdateScore()
    {
        inBoxText.text = "تعداد صحیح: " + inBoxCount;
        if (inBoxCount >= items.Count)
        {
            GameManager.Instance.OnFinishGameAsync();
        }
    }
}