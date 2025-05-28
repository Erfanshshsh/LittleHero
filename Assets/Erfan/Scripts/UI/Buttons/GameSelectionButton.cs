using Coffee.UIEffects;
using UnityEngine;

public class GameSelectionButton : MonoBehaviour
{
    public UIEffect uiEffect;
    public GameObject isPlayedText;
    public LevelConfig levelConfig;

    public void SetActive()
    {
        uiEffect.toneFilter = ToneFilter.None;
        isPlayedText.SetActive(false);
    }

    public void SetInactive()
    {
        uiEffect.toneFilter = ToneFilter.Grayscale;
        isPlayedText.SetActive(true);

    }
}
