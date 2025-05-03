using System.Collections;
using RTLTMPro;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MaleCharacter : MonoBehaviour
{

    [InlineEditor] public DialogueSO firstDialogue;
    [InlineEditor] public DialogueSO doNotPassRoadDialog;
    [InlineEditor] public DialogueSO prizeDialog;
    [InlineEditor] public DialogueSO trashDialogue;
    [InlineEditor] public DialogueSO trashPrize;
    [InlineEditor] public DialogueSO iceCreamDialogue;
    
    
    public VoidChannelEventSO onGetStar;
    public VoidChannelEventSO onIceCream;

    void Start()
    {
        DialogueManager.Instance.StartConversation(firstDialogue);
    }
    async void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("wall"))
        {
            DialogueManager.Instance.StartConversation(doNotPassRoadDialog);
        }
        if (other.gameObject.CompareTag("point"))
        {
            Debug.Log("Mamad");
            await StaticTweeners.AnimateDown(other.gameObject.transform);
            other.gameObject.SetActive(false);
            await DialogueManager.Instance.StartConversation(prizeDialog);
            onGetStar.RaiseEvent();
        }
        if (other.gameObject.CompareTag("Trash"))
        {
           await DialogueManager.Instance.StartConversation(trashDialogue);
           await StaticTweeners.AnimateDown(other.gameObject.transform);
           other.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("garbage"))
        {
            await DialogueManager.Instance.StartConversation(trashPrize);
            onGetStar.RaiseEvent();
        }
        
        if (other.gameObject.CompareTag("Icecream"))
        {
            await DialogueManager.Instance.StartConversation(iceCreamDialogue);
            onIceCream.RaiseEvent();
        }
        
        if (other.gameObject.CompareTag("SchoolZone"))
        {
            SceneManager.LoadScene("FreeMode");

        }
    }




}
