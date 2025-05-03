using UnityEngine;
using UnityEngine.UI;
using RTLTMPro; 

public class PointManager : MonoBehaviour
{
    public Transform box; // The box you want to check against
    public GameObject[] gameObjects; // The game objects to check
    public RTLTextMeshPro inBoxText; // UI Text to display the count of objects in the box
    public RTLTextMeshPro notInBoxText; // UI Text to display the count of objects not in the box
    public RTLTextMeshPro Time;
    public Button checkButton;
    public RTLTextMeshPro TimePoint; // Button to trigger the check
    private bool JustOnce = true;
    private string TP = "";

    void Start()
    {
        checkButton.onClick.AddListener(CheckGameObjects); 
    }

    void Update()
    {
        TP = Time.text;
        Debug.Log("z:"+Time.text);    
    }

    void CheckGameObjects()
    {
        int inBoxCount = 0;
        int notInBoxCount = 0;

        Collider boxCollider = box.GetComponent<Collider>();

        foreach (GameObject obj in gameObjects)
        {
            if (boxCollider.bounds.Contains(obj.transform.position))
            {
                inBoxCount++;
            }
            else
            {
                notInBoxCount++;
            }
        }
        if (JustOnce == true)
        {
        TimePoint.text = "مدت زمان:"+TP;
        JustOnce = false;
        }
        inBoxText.text = "تعداد صحیح: " + inBoxCount;
        notInBoxText.text = "تعداد غلط: " + notInBoxCount;
        
    }
}
