using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ScaleGameHandler : MonoBehaviour
{
    public Transform spawnPoint;
    public Transform referenceItemSpawnPoint;
    public Camera mainCamera;

    private int rights;
    private int wrongs;

    private ScaleConfig.ZoneDifficultyConfig _zoneDConfig;
    private ScalePrefab _scalePrefab;
    private ScaleItem _sampleItem;
    private void Start()
    {
        var currentConfig = GameManager.Instance.currentLevelConfig as ScaleConfig;
        _zoneDConfig = currentConfig.GetConfig(GameManager.Instance.currentLocation,
            GameManager.Instance.currentDifficulty);


        _scalePrefab = Instantiate(_zoneDConfig.prefab, spawnPoint);
        _sampleItem = Instantiate(_zoneDConfig.sampleScaleItem, referenceItemSpawnPoint);
        _sampleItem.EnableRightOutlinable();
        UIManager.Instance.HowToPlayAndInGameProcedure(currentConfig.howToPlayText);
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                var rayCastItem = hit.collider.GetComponent<ScaleItem>();
                if (rayCastItem == null) return;
                if (rayCastItem.isWrongScale)
                {
                    rights++;
                    UIManager.Instance.inGameViewInstance.AddToRights(rights);
                    rayCastItem.EnableRightOutlinable();
                    rayCastItem.enabled = false;
                    hit.collider.enabled = false;
                    if (rights >= _scalePrefab.wrongScaleItems.Count)
                    {
                        DelayFinishGameBehaviour();
                    }
                }
                else
                {
                    wrongs++;
                    hit.collider.enabled = false;
                    // var scale = rayCastItem.transform.localScale.x;
                    // StaticTweeners.DoYoyoScale(rayCastItem.transform, rayCastItem.transform.localScale );
                    UIManager.Instance.inGameViewInstance.AddToWrongs(wrongs);
                    rayCastItem.EnableWrongOutlinable();
                }
            }
        }
    }


    private async UniTaskVoid DelayFinishGameBehaviour()
    {
        await UniTask.DelayFrame(30);
        var finishData = new Common.LevelFinishData(rights, wrongs,
            (int)Timer.Instance.timeRemaining, rights >= wrongs);
        GameManager.Instance.OnFinishGameAsync(finishData);
    }
}