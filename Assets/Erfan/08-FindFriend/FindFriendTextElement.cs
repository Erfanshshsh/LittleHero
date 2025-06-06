using System;
using RTLTMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FindFriendTextElement : MonoBehaviour
{
    public RTLTextMeshPro textElement;
    public FindFriendConfig.FriendType friendType;
    public UnityAction<FindFriendConfig.FriendType> onClick;
    public Button button;

    private void OnEnable()
    {
        button.onClick.AddListener(OnClicked);
    }

    

    private void OnDisable()
    {
        button.onClick.RemoveAllListeners();
    }

    private void OnClicked()
    {
        button.enabled = false;
        onClick?.Invoke(friendType);
    }
    public void SetText(String text)
    {
        textElement.text = text;
    }
}
