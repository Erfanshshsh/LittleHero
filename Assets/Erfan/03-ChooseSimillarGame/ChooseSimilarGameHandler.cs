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


    private void Start()
    {
        var config = GameManager.Instance.findSimilarConfig;

        sampleItem = Instantiate(config.sampleItem, sampleSpawnTf);
        var items = config.GetShuffledCombinedList();

        for (var i = 0; i < items.Count; i++)
        {
            Instantiate(items[i], spawnPoints[i]);
        }


        UIManager.Instance.HowToPlayAndInGameProcedure(config.howToPlayText);
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
                    if (rights >= GameManager.Instance.findSimilarConfig.bottomCorrectItems.Count)
                    {
                        await ShowWinAfterDelay();
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
    
    private async UniTask ShowWinAfterDelay()
    {
        await UniTask.Delay(GS.INS.ChooseSimilarDelayAfterFinish); // delay in milliseconds (1000 = 1 second)
        UIManager.Instance.ShowYouWon();
    }
}