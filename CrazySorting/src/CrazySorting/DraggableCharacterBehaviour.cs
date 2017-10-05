using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character), typeof(Collider2D))]
class DraggableCharacterBehaviour : MonoBehaviour, IDraggableCharacter
{
    public float PickUpScaleFactor = 1.5f;

    float mDistToCamera;
    Vector3 mOffset;
    Vector3 mPreviousScale;
    protected Action mOnMouseUpAction;
    protected Character mCharacter;
    IEnumerable<Goal> mGoals;

    public void DisableDragging()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        enabled = false;
    }

    public void EnableDragging()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<CircleCollider2D>().enabled = true;
        enabled = true;
    }

    public virtual void Awake()
    {
        mCharacter = GetComponent<Character>();
        mCharacter.Register(this);

        mGoals = FindObjectsOfType<Goal>();
    }

    public virtual void OnMouseDown()
    {
        if (!enabled)
            return;
        
        GetComponent<BoxCollider2D>().enabled = false;

        mPreviousScale = transform.localScale;
        transform.localScale *= PickUpScaleFactor;
        mDistToCamera = Vector3.Distance(Camera.main.transform.position, transform.position);
        mOffset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mDistToCamera));
    }

    void OnMouseUp()
    {
        if (!enabled)
            return;
        
        GetComponent<BoxCollider2D>().enabled = true;

        OnMouseUpInternal();

    }

    protected virtual void OnMouseUpInternal()
    {
        transform.localScale = mPreviousScale;

        foreach(var goal in mGoals)
        {
            if (goal.IsCharacterNearGoal(mCharacter))
            {
                goal.MoveCharacterIntoGoal(mCharacter);
                break;
            }
        }

        mOnMouseUpAction?.Invoke();

    }

    public virtual void OnMouseDrag()
    {
        if (!enabled)
            return;
        
        Vector3 newPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, mDistToCamera);
        transform.position = Camera.main.ScreenToWorldPoint(newPosition) + mOffset;
        transform.position = KeepPointOnScreen(transform.position);
    }

    public void SetOnMouseUpAction(Action onMouseUpAction)
    {
        mOnMouseUpAction = onMouseUpAction;
    }

    protected Vector2 KeepPointOnScreen(Vector2 worldSpacePoint)
    {

        var bounds = mCharacter.GetComponent<SpriteRenderer>().sprite.bounds;

        var p = Camera.main.WorldToViewportPoint(worldSpacePoint);

        p.x = Mathf.Clamp(p.x, 0.1f, 0.9f);
        p.y = Mathf.Clamp(p.y, 0.05f, 0.95f);

        worldSpacePoint = Camera.main.ViewportToWorldPoint(p);

        return worldSpacePoint;
    }

    public virtual void Update()
    {

    }
}