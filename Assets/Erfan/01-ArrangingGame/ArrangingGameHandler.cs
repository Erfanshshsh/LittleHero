using System;
using System.Collections.Generic;
using UnityEngine;

public class ArrangingGameHandler : Singleton<ArrangingGameHandler>
{
    public List<Transform> spawnPoints = new List<Transform>();
    

    private void Start()
    {
        var items = GameManager.Instance.arrangingGameItems.items;

        for (var i = 0; i < items.Count; i++)
        {
            Instantiate(items[i], spawnPoints[i]);
        }
    }
}
