using System;
using RTLTMPro;
using UnityEngine;


public class FindDifferenceGameHandler : Singleton<FindDifferenceGameHandler>
{
    public Transform prefabParent;
    public Camera mainCamera;
    public RTLTextMeshPro levelText;

    private int _diffCount;
    private int _foundCount = 0;
    private void Start()
    {
        var findDifferenceImage = GameManager.Instance.findDifferenceGame.prefab;
        var instance = Instantiate(findDifferenceImage, prefabParent);
        instance.transform.localPosition = Vector3.zero;
        _diffCount = instance.diffItems.Count;
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
                        GameManager.Instance.OnFinishGameAsync();
                    }
                }
            }
        }
    }
    
    private void UpdateText()
    {
        levelText.text = $"{_foundCount}/{_diffCount}";
    }
}