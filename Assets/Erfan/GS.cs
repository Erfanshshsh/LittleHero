using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "GeneralSettings", menuName = "Razhia/GeneralSettings", order = 0)]
public class GS : SingletonScriptableObject<GS>
{
    [TitleGroup("UI")]
    public float CBButtonsAnimateTime;
    public Ease CBButtonsOffEase;
    public Ease CBButtonsOnEase;
    
    public Ease FadeOutEase;
    public Ease FadeIntEase;
    public int ChooseSimilarDelayAfterFinish = 1000;
}