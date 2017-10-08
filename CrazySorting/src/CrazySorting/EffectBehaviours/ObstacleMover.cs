using System;
using CrazySorting.Utility;
using UnityEngine;

namespace CrazySorting.EffectBehaviours
{
    public class ObstacleMover : MonoBehaviour
    {
        public float TimeToLive = 3;
        public float Speed = 0.1f;
        public bool ContinuousTargetAcquisition = false;

        public event Action OnTargetReached;
        [HideInInspector] [Dependency] public GroundBehaviour Ground;

        Vector2 mTargetPosition;
        float mTimeAlive;

        void Start()
        {
            this.Inject();
            AcquireNewTarget();
        }

        private void AcquireNewTarget()
        {
            mTargetPosition = Ground.GetRandomPositionOnGround(0.5f);
        }

        void OnMouseUp()
        {
            Finish();
        }

        private void Finish()
        {
            Destroy(gameObject); //kill self on mouse up
            OnTargetReached?.Invoke();
        }

        void Update()
        {
            mTimeAlive += Time.deltaTime;
            var isDead = (TimeToLive > 0 && mTimeAlive >= TimeToLive);

            var step = Speed / 100f * (1 + Time.timeSinceLevelLoad / 100f);

            if (isDead || TargetReached(step))
            {
                Finish();
                return;
            }

            transform.position = Vector3.MoveTowards(transform.position, mTargetPosition, step);
        }

        bool TargetReached(float tolerance)
        {
            if (Vector3.Distance(transform.position, mTargetPosition) >= tolerance)
                return false;
            
            if (ContinuousTargetAcquisition)
                AcquireNewTarget();

            return !ContinuousTargetAcquisition;
        }
    }
}