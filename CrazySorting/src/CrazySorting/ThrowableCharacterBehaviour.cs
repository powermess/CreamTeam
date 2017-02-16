using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CrazySorting
{
    class ThrowableCharacterBehaviour : DraggableCharacterBehaviour
    {
        public float ThrowDistance = 3f;
        public float ThrowSpeed = 20f;
        public string GoalLayerName = "Goals";

        Vector3? mThrowTargetPosition;

        public override void Awake()
        {
            base.Awake();
        }

        Vector3 mLastPosition;
        Vector3 mCurrentPosition;

        public override void OnMouseDown()
        {
            mLastPosition = transform.position;
            mCurrentPosition = transform.position;
        }

        public override void OnMouseDrag()
        {
            mLastPosition = mCurrentPosition;
            mCurrentPosition = transform.position;
        }

        public override void OnMouseUp()
        {
            var hit = Physics2D.Raycast(transform.position, transform.position - mLastPosition, 10000, 1 << LayerMask.NameToLayer(GoalLayerName));

            var dirVector = transform.position - mLastPosition;

            if (hit.collider != null)
            {
                mThrowTargetPosition = hit.point + new Vector2(dirVector.x, dirVector.y).normalized * 1.5f;

                //GetComponent<CharacterMover>().Stop();
            }
        }

        public override void Update()
        {
            base.Update();

            if (mThrowTargetPosition.HasValue)
            {
                var step = Time.deltaTime * Mathf.Max(Vector3.Distance(mCurrentPosition, mLastPosition) * ThrowSpeed, 30);
                transform.position = Vector3.MoveTowards(transform.position, mThrowTargetPosition.Value, step);

                mOnMouseUpAction();

                if (Vector3.Distance(transform.position, mThrowTargetPosition.Value) <= step)
                    mThrowTargetPosition = null;
            }
        }
    }
}
