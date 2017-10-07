using System;
using CrazySorting.Utility;
using UnityEngine;

namespace CrazySorting.EffectBehaviours
{
    public class ObstacleMover : MonoBehaviour
    {
        public float Speed = 0.1f;
        public bool ContinuousTargetAcquisition = false;

        public event Action OnTargetReached;
        [HideInInspector] [Dependency] public GroundBehaviour Ground;

        private Vector2 mTargetPosition;

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
            var step = Speed / 100f * (1 + Time.timeSinceLevelLoad / 100f);

            if (Vector3.Distance(transform.position, mTargetPosition) < step)
                if (ContinuousTargetAcquisition)
                    AcquireNewTarget();
                else
                    Finish();
            transform.position = Vector3.MoveTowards(transform.position, mTargetPosition, step);
        }
    }
}