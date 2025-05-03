using UnityEngine;

public class DragObject : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private Camera mainCamera;
    public Transform box; // The box you want to check against
    public bool isInBox = false; // Variable to indicate if the object is in the box

    void Start()
    {
        mainCamera = Camera.main;
    }

    void OnMouseDown()
    {
        isDragging = true;
        offset = gameObject.transform.position - GetMouseWorldPosition();
    }

    void OnMouseUp()
    {
        isDragging = false;
        CheckIfInBox();
    }

    void Update()
    {
        if (isDragging)
        {
            transform.position = GetMouseWorldPosition() + offset;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mainCamera.WorldToScreenPoint(gameObject.transform.position).z;
        return mainCamera.ScreenToWorldPoint(mousePoint);
    }

    private void CheckIfInBox()
    {
        if (box != null)
        {
            Collider boxCollider = box.GetComponent<Collider>();
            if (boxCollider.bounds.Contains(transform.position))
            {
                isInBox = true;
                Debug.Log("Object is in the box: " + isInBox);
            }
            else
            {
                isInBox = false;
                Debug.Log("Object is not in the box: " + isInBox);
            }
        }
    }
}
