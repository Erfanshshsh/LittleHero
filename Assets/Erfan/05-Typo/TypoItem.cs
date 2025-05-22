using System;
using RTLTMPro;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TypoItem : MonoBehaviour
{
    public RTLTextMeshPro text;
    public bool isWrong;
    public Button button;
    public UnityEvent<bool> onClickButton;
    [ShowIf("isWrong")]
    public string rightText;
    private void OnEnable()
    {
        button.onClick.AddListener(OnClickButton);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(OnClickButton);
    }

    private void OnClickButton()
    {
        onClickButton?.Invoke(isWrong);
        if (isWrong)
        {
            text.text = rightText;
        }
    }
}
