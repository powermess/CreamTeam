using UnityEngine;

[RequireComponent(typeof(Character))]
class CharacterMover : MonoBehaviour
{
    public float Speed = 0.1f;
    public float TargetPadding = 0.5f;

    Vector3 mStartPosition;
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

        transform.position = Vector3.MoveTowards(transform.position, mTargetPosition, step);
    }

    Vector2 GetRandomTarget(Bounds bounds)
    {
        return bounds.GetRandomPositionInBounds(TargetPadding);
    }

    void AcquireNewTarget()
    {
        mStartPosition = transform.position;
        mTargetPosition = GetRandomTarget(mBounds);
    }
    
    internal void Stop()
    {
        enabled = false;
    }
}

