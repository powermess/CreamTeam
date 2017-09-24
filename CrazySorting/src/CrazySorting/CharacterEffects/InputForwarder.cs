using System;
using UnityEngine;

namespace CrazySorting.CharacterEffects
{
    class InputForwarder : MonoBehaviour
    {
        public event Action OnMouseUpEvent;
        
        void OnMouseUp()
        {
            OnMouseUpEvent?.Invoke();
        }
    }
}