using System;
using UnityEngine;
using UnityEngine.UI;
using RTLTMPro;

public class ArrangingGamePointManager : Singleton<ArrangingGamePointManager>
{
    public RTLTextMeshPro inBoxText; // UI Text to display the count of objects in the box
    public Button checkButton;

    public int inBoxCount = 0;

    void Start()
    {
        checkButton.onClick.AddListener(UpdateText);
        UpdateText();
    }

    private void OnDisable()
    {
        checkButton.onClick.RemoveListener(UpdateText);
    }


    public void UpdateText()
    {
        inBoxText.text = "تعداد صحیح: " + inBoxCount;
    }
}