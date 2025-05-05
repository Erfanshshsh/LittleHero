using System;
using UnityEngine;

public class Container : MonoBehaviour
{
    public Common.ArrangingGameItemType itemType;
    public int point;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<DragObject>(out var dragObject))
        {
            if (dragObject.arrangingGameItemType == itemType)
            {
                point++;
                ArrangingGamePointManager.Instance.inBoxCount++;
                ArrangingGamePointManager.Instance.UpdateText();
                
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<DragObject>(out var dragObject))
        {
            if (dragObject.arrangingGameItemType == itemType)
            {
                point--;
                ArrangingGamePointManager.Instance.inBoxCount--;
                ArrangingGamePointManager.Instance.UpdateText();
            }
        }
    }
}