using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Joyixir.GameManager.UI;
using RTLTMPro;
using UnityEngine;

public class HowToPlayView : View
{
    public RTLTextMeshPro howToPlayText;

    private CancellationTokenSource _cts;

    public void Initialize(string text, Action onHideComplete = null)
    {
        // Cancel any ongoing task before starting a new one
        _cts?.Cancel();
        _cts?.Dispose();
        _cts = new CancellationTokenSource();

        howToPlayText.text = text;
        HideAfterDelay(onHideComplete, _cts.Token).Forget();
    }

    private async UniTaskVoid HideAfterDelay(Action onHideComplete, CancellationToken token)
    {
        try
        {
            await UniTask.Delay(GS.INS.ChooseSimilarDelayAfterFinish * 2, cancellationToken: token);
            await AnimateDown();
            onHideComplete?.Invoke();
        }
        catch (OperationCanceledException)
        {
            // Task was cancelled; do nothing
        }
    }

    protected override void OnBackBtn()
    {
        _cts?.Cancel();
    }

    private void OnDestroy()
    {
        _cts?.Cancel();
        _cts?.Dispose();
    }
}