using CrazySorting.Utility;
using UnityEngine;

[RequireComponent(typeof(Character))]
class CharacterMover : MonoBehaviour
{
    public float Speed = 0.1f;
    public float TargetPadding = 0.5f;

    [HideInInspector] [Dependency] public GroundBehaviour Ground;

    Vector3 mTargetPosition;

    void Awake()
    {
        this.Inject();
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

    void AcquireNewTarget()
    {
        mTargetPosition = Ground.GetRandomPositionOnGround(TargetPadding);
    }

    internal void EnableMovement(bool enableMovement)
    {
        enabled = enableMovement;
    }
}

