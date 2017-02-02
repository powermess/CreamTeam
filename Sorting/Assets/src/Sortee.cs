using UnityEngine;
using System.Collections;

public class Sortee : MonoBehaviour {

    public Transform Target;
    public float Speed;
    public bool IsPatriot;
    private Vector3 mStartPosition;
    private Vector3 mStartScale;
    public bool IsMoving;

    // Use this for initialization
    void Start () {
        IsMoving = true;
        mStartPosition = transform.position;
        mStartScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
        if (!IsMoving)
            return;

        transform.right = Target.position - transform.position;
        transform.position = Vector2.MoveTowards(transform.position, Target.position, Time.deltaTime * Speed);
	}

    Vector3 offset;

    void OnMouseDown()
    {
        if (!IsMoving)
            return;

        transform.localScale *= 1.5f;
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
    }

    void OnMouseUp()
    {
        transform.localScale = mStartScale;
    }

    void OnMouseDrag()
    {
        if (!IsMoving)
            return;

        Vector3 newPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f);
        transform.position = Camera.main.ScreenToWorldPoint(newPosition) + offset;
    }

    public void Stop()
    {
        IsMoving = false;
    }
}
