﻿using UnityEngine;

public class Sortee : MonoBehaviour {

    public float Speed;
    private Vector3 mStartPosition;
    private Vector3 mStartScale;
    private float timeToChangeDirection;
    private float lastY;
    private bool mIsDragging;

    // Use this for initialization
    void Start () {
        mStartPosition = transform.position;
        mStartScale = transform.localScale;

        mCurrentPos = mLastPos = mStartPosition;

        //ChangeDirection();
	}
	
    Vector3 offset;

    void OnMouseDown()
    {
        transform.localPosition = new Vector3(transform.position.x, 3, transform.position.z);
        var rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
        transform.up = Vector3.up;
        Debug.Log("another debug message");
    }

    void OnMouseUp()
    {
        var rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;
        mIsDragging = false;

        rb.AddForce((transform.position - mLastPos) * 500);
    }

    void OnMouseDrag()
    {
        mIsDragging = true;
        float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        var worldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));
        transform.position = new Vector3(worldPos.x, transform.position.y, worldPos.z);
    }

    Vector3 mLastPos = Vector3.zero;
    Vector3 mCurrentPos = Vector3.zero;

    public void Update()
    {
        mLastPos = mCurrentPos;
        mCurrentPos = transform.position;

        //UpdateDirection();
    }

    void UpdateDirection()
    {
        timeToChangeDirection -= Time.deltaTime;

        if (timeToChangeDirection <= 0)
        {
            ChangeDirection();
        }

        var rb = GetComponent<Rigidbody>();

        var v = transform.forward * 2;
        rb.velocity = new Vector3(v.x, 0, v.y);
    }

    private void ChangeDirection()
    {
        float angle = Random.Range(0f, 360f);
        Quaternion quat = Quaternion.AngleAxis(angle, Vector3.up);
        Vector3 newForward = quat * Vector3.forward;
        newForward.y = 0;
        newForward.Normalize();
        transform.forward = newForward;



        timeToChangeDirection = Random.Range(1.5f, 3f);
    }

    public void Stop()
    {
    }
}