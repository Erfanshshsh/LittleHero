using System;
using UnityEngine;

public class NumbersContainer : MonoBehaviour
{
    public Common.NumbersGameItemType itemType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<NumbersGameDragObject>(out var dragObject))
        {
            if (dragObject.numbersGameItemType == itemType)
            {

                NumbersGameHandler.Instance.rightInBoxCount++;
                NumbersGameHandler.Instance.UpdateScore();
            }
            else
            {
                NumbersGameHandler.Instance.wrongInBoxCount++;
                NumbersGameHandler.Instance.UpdateScore();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<NumbersGameDragObject>(out var dragObject))
        {
            if (dragObject.numbersGameItemType == itemType)
            {
                NumbersGameHandler.Instance.rightInBoxCount--;
                NumbersGameHandler.Instance.UpdateScore();
            }
            else
            {
                NumbersGameHandler.Instance.wrongInBoxCount--;
                NumbersGameHandler.Instance.UpdateScore();
            }
        }
    }
}