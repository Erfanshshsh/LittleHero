using UnityEngine;

public class GameHandler : MonoBehaviour
{
    protected int checkBtnCount = 0;
    public virtual void CheckForFinish()
    {
        checkBtnCount = UIManager.Instance.inGameViewInstance.checkButtonCount;
    }
}
