using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ScalePrefab : MonoBehaviour
{
    public List<ScaleItem> rightScaleItems = new List<ScaleItem>();
    public List<ScaleItem> wrongScaleItems = new List<ScaleItem>();


    [Button]
    public void GetItemsInChildren()
    {
        var items = GetComponentsInChildren<ScaleItem>();
        foreach (var item in items)
        {
            if (item.isWrongScale)
            {
                wrongScaleItems.Add(item);
            }
            else
            {
                rightScaleItems.Add(item);
            }
        }
    }
}
