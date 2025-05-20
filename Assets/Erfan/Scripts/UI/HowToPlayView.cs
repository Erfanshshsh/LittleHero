using Cysharp.Threading.Tasks;
using Joyixir.GameManager.UI;
using RTLTMPro;
using UnityEngine;

public class HowToPlayView : View
{
    public RTLTextMeshPro howToPlayText;



    public void Initialize(string text)
    {
        howToPlayText.text = text;
        HideAfterDelay();
    }
    
    
    private async UniTask HideAfterDelay()
    {
        await UniTask.Delay(GS.INS.ChooseSimilarDelayAfterFinish*3); // delay in milliseconds (1000 = 1 second)
        AnimateDown();
    }
    
    
    protected override void OnBackBtn()
    {
        
    }
}
