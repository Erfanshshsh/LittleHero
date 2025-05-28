using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using EPOOutline;
using UnityEngine;
using UnityEngine.Events;

public class ChooseSimilarGameHandler : MonoBehaviour
{
    public Transform sampleSpawnTf;
    public List<Transform> spawnPoints = new List<Transform>();
    public Camera mainCamera;
    private ChooseSimilarItem sampleItem;

    private int rights;
    private int wrongs;

    private FindSimilarConfig.ZoneDifficultyConfig _zoneDConfig;

    private void Start()
    {
        var currentConfig = GameManager.Instance.currentLevelConfig as FindSimilarConfig;
        _zoneDConfig = currentConfig.GetConfig(GameManager.Instance.currentLocation,
            GameManager.Instance.currentDifficulty);

        sampleItem = Instantiate(_zoneDConfig.sampleItem, sampleSpawnTf);
        var items = _zoneDConfig.GetShuffledCombinedList();

        for (var i = 0; i < items.Count; i++)
        {
            Instantiate(items[i], spawnPoints[i]);
        }

        UIManager.Instance.HowToPlayAndInGameProcedure(currentConfig.howToPlayText);
    }


    private async void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                var rayCastItem = hit.collider.GetComponent<ChooseSimilarItem>();
                if (rayCastItem == null) return;
                if (rayCastItem.itemType == sampleItem.itemType)
                {
                    rights++;
                    StaticTweeners.AnimateDown(rayCastItem.transform);
                    UIManager.Instance.inGameViewInstance.AddToRights(rights);
                    rayCastItem.EnableRightOutlinable();

                    rayCastItem.enabled = false;
                    if (rights >= _zoneDConfig.bottomCorrectItems.Count)
                    {
                        DelayFinishGameBehaviour();
                    }
                }
                else
                {
                    wrongs++;
                    rayCastItem.gameObject.GetComponent<BoxCollider>().enabled = false;
                    StaticTweeners.DoYoyoScale(rayCastItem.transform);
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