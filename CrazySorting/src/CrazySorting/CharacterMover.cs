using UnityEngine;

[RequireComponent(typeof(Character))]
class CharacterMover : MonoBehaviour
{
    public float Speed = 0.1f;
    public float TargetPadding = 0.5f;

    Vector3 mTargetPosition;
    Bounds mBounds;

    void Awake()
    {
        mBounds = GameObject.Find("ground").GetComponent<Renderer>().bounds;
        GetComponent<Character>().Register(this);
    }

    void Start()
    {
        AcquireNewTarget();
    }

    void OnMouseUp()
    {
        AcquireNewTarget();
    }

    void Update()
    {
        var step = Speed / 100f * (1 + Time.timeSinceLevelLoad / 100f);

        if (Vector3.Distance(transform.position, mTargetPosition) < step)
            AcquireNewTarget();

        GetComponent<Rigidbody2D>().MovePosition(Vector3.MoveTowards(transform.position, mTargetPosition, step));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.DrawLine(transform.position, collision.relativeVelocity.normalized * 100f, Color.red, 3f);

        var v = collision.relativeVelocity.normalized * -1f;
        v = new Vector2(Mathf.Sign(v.x), Mathf.Sign(v.y));
        AcquireNewTarget();
    }

    Vector2 GetRandomTarget(Bounds bounds)
    {
        return bounds.GetRandomPositionInBounds(TargetPadding);
    }

    void AcquireNewTarget()
    {
        AcquireNewTarget(new Vector2(1, 1));
    }

    void AcquireNewTarget(Vector2 bias)
    {
        mTargetPosition = Vector2.Scale(GetRandomTarget(mBounds), bias);
    }
    
    internal void EnableMovement(bool enabled)
    {
        this.enabled = enabled;
        GetComponent<Rigidbody2D>().simulated = enabled;
    }
    
}

