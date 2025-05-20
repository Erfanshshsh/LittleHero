using System;
using UnityEngine;


public class FindPathGameHandler : Singleton<FindPathGameHandler>
{
    public Transform levelPrefabTf;

    public int rightScore;
    public int wrongScore;
    private void Start()
    {
        var config = GameManager.Instance.findPathConfig;
        UIManager.Instance.ShowHowToPlay(config.howToPlayText);
        UIManager.Instance.ShowInGameView();
        var level = Instantiate(config.FindPathLevel, levelPrefabTf);

        for (var i = 0; i < level.splines.Count; i++)
        {
            var spline = level.splines[i];
            var firstKnot = spline[^1];
            Vector3 localPos = firstKnot.Position;

            // Convert local spline point to world space
            Vector3 worldPos = level.m_Spline.transform.TransformPoint(localPos);
            var butterfly = Instantiate(config.butterflyPrefab, worldPos, Quaternion.identity);
            butterfly.Initialize(i);
            butterfly.onSelectRightFlower.AddListener(OnRight);
            butterfly.onSelectWrongFlower.AddListener(OnWrong);
        }
    }



    private void OnRight()
    {
        rightScore++;
        UIManager.Instance.inGameViewInstance.AddToRights(rightScore);
    }
    
    private void OnWrong()
    {
        wrongScore++;
        UIManager.Instance.inGameViewInstance.AddToWrongs(wrongScore);

    }
}