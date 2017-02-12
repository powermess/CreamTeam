using System;
using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
class DraggableCharacterBehaviour : MonoBehaviour, IDraggableCharacter
{
    public float PickUpScaleFactor = 1.5f;

    public event Action OnCharacterReleased;

    float mDistToCamera;
    Vector3 mOffset;
    Vector3 mPreviousScale;

    void OnMouseDown()
    {
        mPreviousScale = transform.localScale;
        transform.localScale *= PickUpScaleFactor;
        mDistToCamera = Vector3.Distance(Camera.main.transform.position, transform.position);
        mOffset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mDistToCamera));
    }

    private void OnMouseUp()
    {
        transform.localScale = mPreviousScale;
        OnCharacterReleased?.Invoke();
    }

    void OnMouseDrag()
    {
        Vector3 newPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, mDistToCamera);
        transform.position = Camera.main.ScreenToWorldPoint(newPosition) + mOffset;
    }

    internal void DisableDragging()
    {
       // GetComponent<Collider2D>().enabled = false;
    }
}