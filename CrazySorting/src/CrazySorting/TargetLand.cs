using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.src
{
    class TargetLand : MonoBehaviour
    {
        public bool IsMurica;

        public Action<bool, Sortee> OnCollisionAction;
        void OnTriggerEnter2D(Collider2D coll)
        {
            var sortee = coll.gameObject.GetComponent<Sortee>();
            if (sortee == null)
                return;

            OnCollisionAction(IsMurica, sortee);
        }
    }
}
