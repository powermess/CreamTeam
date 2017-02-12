﻿using System;
using UnityEngine;

[RequireComponent(typeof(Character), typeof(BoxCollider2D))]
class DraggableCharacterBehaviour : MonoBehaviour, IDraggableCharacter
{
    public float PickUpScaleFactor = 1.5f;

    float mDistToCamera;
    Vector3 mOffset;
    Vector3 mPreviousScale;
    private Action mOnMouseUpAction;
    Character mCharacter;


    public void DisableDragging()
    {
        GetComponent<Collider2D>().enabled = false;
    }

    void Awake()
    {
        mCharacter = GetComponent<Character>();
        mCharacter.Register(this);
    }

    void OnMouseDown()
    {
        mPreviousScale = transform.localScale;
        transform.localScale *= PickUpScaleFactor;
        mDistToCamera = Vector3.Distance(Camera.main.transform.position, transform.position);
        mOffset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mDistToCamera));
    }

    void OnMouseUp()
    {
        transform.localScale = mPreviousScale;
        mOnMouseUpAction?.Invoke();
    }

    void OnMouseDrag()
    {
        Vector3 newPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, mDistToCamera);
        transform.position = Camera.main.ScreenToWorldPoint(newPosition) + mOffset;
    }

    public void SetOnMouseUpAction(Action onMouseUpAction)
    {
        mOnMouseUpAction = onMouseUpAction;
    }
}