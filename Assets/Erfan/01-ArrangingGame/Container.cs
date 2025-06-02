using System;
using UnityEngine;

public class Container : MonoBehaviour
{
    public Common.ArrangingGameItemType itemType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<DragObject>(out var dragObject))
        {
            if (dragObject.arrangingGameItemType == itemType)
            {
                ArrangingGameHandler.Instance.inBoxCount++;
                ArrangingGameHandler.Instance.UpdateScore();
            }
            else
            {
                ArrangingGameHandler.Instance.wrongInBoxCount++;
                ArrangingGameHandler.Instance.UpdateScore();
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<DragObject>(out var dragObject))
        {
            if (dragObject.arrangingGameItemType == itemType)
            {
                ArrangingGameHandler.Instance.inBoxCount--;
                ArrangingGameHandler.Instance.UpdateScore();
            }
            else
            {
                ArrangingGameHandler.Instance.wrongInBoxCount--;
                ArrangingGameHandler.Instance.UpdateScore();
            }
        }
    }
}