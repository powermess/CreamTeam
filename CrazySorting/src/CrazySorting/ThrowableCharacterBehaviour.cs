using CrazySorting.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CrazySorting
{
    [RequireComponent(typeof(Character))]
    class ThrowableCharacterBehaviour : DraggableCharacterBehaviour
    {
        public float ThrowDistance = 2f;
        public float ThrowSpeed = 20f;
        public AnimationCurve SpeedDevelopment;
        public string GoalLayerName = "Goals";

        Vector3? mThrowTargetPosition;
        Vector3 mStartThrowPosition;


        public override void Awake()
        {
            base.Awake();
            mCharacter = GetComponent<Character>();
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

            var goals = FindObjectsOfType<Goal>();

            "distance pos, mlastpos: {0}".Log(Vector2.Distance(transform.position, mLastPosition));

            if (Vector2.Distance(transform.position, mLastPosition) < 0.1f || goals.Any(g => g.IsCharacterInGoal(mCharacter)))
                return;

            if (hit.collider != null && Vector2.Distance(hit.point, transform.position) < ThrowDistance)
            {
                mThrowTargetPosition = hit.point + new Vector2(dirVector.x, dirVector.y).normalized * 1.5f;
            }
            else
            {
                mThrowTargetPosition = transform.position + dirVector.normalized * ThrowDistance;
            }

            mThrowTargetPosition = KeepPointOnScreen(mThrowTargetPosition.Value);

            "new throwTargetPosition: {0}".Log(mThrowTargetPosition);
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
