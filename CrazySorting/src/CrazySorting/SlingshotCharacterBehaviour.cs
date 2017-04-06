using CrazySorting.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CrazySorting
{
    [RequireComponent(typeof(Character))]
    class SlingshotCharacterBehaviour : DraggableCharacterBehaviour
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
            base.OnMouseDown();

            mLastPosition = transform.position;
            mCurrentPosition = transform.position;
            GetComponent<BoxCollider2D>().enabled = false;
            mStartThrowPosition = transform.position;

            Time.timeScale = 0.3f;
        }

        public override void OnMouseDrag()
        {
            base.OnMouseDrag();

            mLastPosition = mCurrentPosition;
            mCurrentPosition = transform.position;
        }

        protected override void OnMouseUpInternal()
        {
            base.OnMouseUpInternal();

            Time.timeScale = 1f;

            var hit = Physics2D.Raycast(transform.position, mStartThrowPosition - transform.position, 10000, 1 << LayerMask.NameToLayer(GoalLayerName));

            var dirVector = mStartThrowPosition - transform.position;

            var goals = FindObjectsOfType<Goal>();


            if (Vector2.Distance(transform.position, mStartThrowPosition) < 0.1f || goals.Any(g => g.IsCharacterInGoal(mCharacter)))
            {
                GetComponent<BoxCollider2D>().enabled = true;
                return;
            }
            
            if (hit.collider != null)
            {
                mThrowTargetPosition = hit.point + new Vector2(dirVector.x, dirVector.y).normalized * 1.5f;
            }
            else
            {
                mThrowTargetPosition = transform.position + dirVector.normalized * ThrowDistance;
            }

            mThrowTargetPosition = KeepPointOnScreen(mThrowTargetPosition.Value);
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
