using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CrazySorting
{
    class GoalMover : MonoBehaviour
    {
        public float Speed = 1;
        float mTargetValue = 1;
        float mTime;

        private void Update()
        {
            mTime += Time.deltaTime;

            var x = Mathf.Lerp(mTargetValue * -1, mTargetValue, mTime * Speed);

            if (Mathf.Abs(mTargetValue - x) < 0.02f)
            {
                x = mTargetValue;
                mTargetValue *= -1;
                mTime = 0;
            }

            transform.position = new Vector2(x, transform.position.y);
        }
    }
}
