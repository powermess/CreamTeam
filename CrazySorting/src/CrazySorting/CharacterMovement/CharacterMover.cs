using System.Linq;
using CrazySorting.Utility;
using UnityEngine;

[RequireComponent(typeof(Character))]
class CharacterMover : MonoBehaviour
{
    public float Speed = 0.1f;
    public float TargetPadding = 0.5f;

    [HideInInspector] [Dependency] public GroundBehaviour Ground;

    Vector3 mTargetPosition;
    const float NEW_TARGET_COOLDOWN = 0.25f;
    float mTimeSinceLastTargetAcquired;

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
        mTimeSinceLastTargetAcquired += Time.deltaTime;
        
        var step = Speed / 100f * (1 + Time.timeSinceLevelLoad / 100f);

        if (Vector3.Distance(transform.position, mTargetPosition) < step)
            AcquireNewTarget();

        GetComponent<Rigidbody2D>().MovePosition(Vector3.MoveTowards(transform.position, mTargetPosition, step));
        Debug.DrawLine(transform.position, mTargetPosition, Color.blue);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.DrawLine(transform.position, collision.relativeVelocity.normalized * 100f, Color.red, 3f);

        var v = collision.relativeVelocity.normalized * -1f;
        v = new Vector2(Mathf.Sign(v.x), Mathf.Sign(v.y));
        AcquireNewTarget();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        AcquireNewTarget();
    }

    void AcquireNewTarget()
    {
        if (mTimeSinceLastTargetAcquired < NEW_TARGET_COOLDOWN)
            return;


        //try to find a new target which currently has no immediate characters to collide with
        for (var i = 0; i < 3; i++)
        {
            mTargetPosition = Ground.GetRandomPositionOnGround(TargetPadding);

            var direction = mTargetPosition - transform.position;
            var distance = Mathf.Min(Vector2.Distance(transform.position, mTargetPosition), 1f); //max 1, only check for collisions in immediate neighborhood
            var hit = Physics2D.RaycastAll(transform.position, direction, distance);

            if (hit.Length == 0 || !hit.Any(h => h.collider.gameObject != this.gameObject)) //if no other collisions, target is ok
                break;
        }
        
        mTimeSinceLastTargetAcquired = 0;
    }
    
    internal void EnableMovement(bool enableMovement)
    {
        enabled = enableMovement;
    }
}

