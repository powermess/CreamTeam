using System;
using UnityEngine;

namespace CrazySorting.CharacterEffects
{
    class InputForwarder : MonoBehaviour
    {
        public event Action OnMouseUpEvent;


        void Update()
        {
            Vector2 pos;
            if (Input.GetMouseButtonUp(0))
                pos = Input.mousePosition;
            else if ((Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended))
                pos = Input.touches[0].position;
            else
                return;

            var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pos), Vector2.zero, 10, 1 << LayerMask.NameToLayer("Effects"));

            if (hit.collider != null && hit.collider.gameObject == GetComponent<Collider2D>().gameObject)
            {
                OnMouseUpEvent?.Invoke();
            }
        }
    }
}