using System;
using System.Collections.Generic;
using Joyixir.GameManager.UI;
using UnityEngine.UI;


public class ChooseGameView : View
{
    public List<Button> Buttons = new List<Button>();

    private void OnEnable()
    {
        for (int i = 0; i < Buttons.Count; i++)
        {
            int sceneIndex = i; // Capture the index correctly in the closure
            Buttons[i].onClick.AddListener(() => OnButtonClick(sceneIndex));
        }
    }
    
    private void OnDisable()
    {
        // Remove all listeners to prevent duplicates or memory leaks
        foreach (var button in Buttons)
        {
            button.onClick.RemoveAllListeners();
        }
    }



    public void OnButtonClick(int sceneIndex)
    {
        GameManager.Instance.OnGameCardClicked(sceneIndex);
    }
    public void Initialize()
    {
    }


        
        
    protected override void OnBackBtn()
    {
    }
}
