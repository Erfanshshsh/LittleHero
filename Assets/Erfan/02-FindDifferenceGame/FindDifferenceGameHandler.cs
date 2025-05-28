using System;
using Cysharp.Threading.Tasks;
using RTLTMPro;
using UnityEngine;


public class FindDifferenceGameHandler : Singleton<FindDifferenceGameHandler>
{
    public Transform prefabParent;
    public Camera mainCamera;
    public RTLTextMeshPro levelText;

    private int _diffCount;
    private int _foundCount = 0;
    private FindDifferenceGame.ZoneDifficultyConfig _zoneDConfig;
    public Color levelRightSpriteColor;
    private void Start()
    {
        var currentConfig = GameManager.Instance.currentLevelConfig as FindDifferenceGame;
        _zoneDConfig = currentConfig.GetConfig(GameManager.Instance.currentLocation,
            GameManager.Instance.currentDifficulty);
        var findDifferenceImage = _zoneDConfig.prefab;
        levelRightSpriteColor = _zoneDConfig.levelRightSpriteColor;
        var instance = Instantiate(findDifferenceImage, prefabParent);
        instance.transform.localPosition = Vector3.zero;
        _diffCount = instance.diffItems.Count;
        UIManager.Instance.HowToPlayAndInGameProcedure(currentConfig.howToPlayText,
            () => { });
        UpdateText();
    }




    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log("Clicked on: " + hit.collider.name);

                // Optional: check for a specific component
                DifferenceItem item = hit.collider.GetComponentInParent<DifferenceItem>();
                if (item != null)
                {
                    item.OnFound();
                    _foundCount++;
                    UpdateText();
                    if (_foundCount >= _diffCount)
                    {
                        Debug.Log("Level Won");
                        DelayFinishGameBehaviour();
                    }
                }
            }
        }
    }
    
    private void UpdateText()
    {
        levelText.text = $"{_foundCount}/{_diffCount}";
    }
    
    private async UniTaskVoid DelayFinishGameBehaviour()
    {
        await UniTask.DelayFrame(30);
        var finishData = new Common.LevelFinishData(_foundCount, 0,
            (int)Timer.Instance.timeRemaining, true);
        GameManager.Instance.OnFinishGameAsync(finishData);
    }
}