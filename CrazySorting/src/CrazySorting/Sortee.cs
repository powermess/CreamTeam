using UnityEngine;

public class Sortee : MonoBehaviour {

    public float Speed;
    private Vector3 mStartPosition;
    private Vector3 mStartScale;
    private float timeToChangeDirection;
    private float lastY;
    private bool mIsDragging;

    private Vector3 offset;

    private float mTimeStartDragging;

    // Use this for initialization
    void Start () {
        mStartPosition = transform.position;
        mStartScale = transform.localScale;

        mCurrentPos = mLastPos = mStartPosition;

        //ChangeDirection();
	}
	
    

    void OnMouseDown()
    {
        mTimeStartDragging = Time.time;

        var rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;

        transform.position += Vector3.Normalize(Camera.main.transform.position - transform.position) / 2f;
        var screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
    }

    void OnMouseUp()
    {
        var rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;

        rb.AddForce((transform.position - mLastPos) * 500);
    }



void OnMouseDrag()
    {
        var screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        transform.position = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;

    //if (transform.up.y != 0)
    //{
    //    transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, new Vector3(0, transform.localEulerAngles.y, 0), (Time.time - mTimeStartDragging));
    //}


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
