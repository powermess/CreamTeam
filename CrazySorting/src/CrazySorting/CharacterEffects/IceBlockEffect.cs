using System;
using UnityEngine;

namespace CrazySorting.CharacterEffects
{
    [RequireComponent(typeof(Character))]
    abstract class ACharacterEffect : MonoBehaviour
    {
        protected Character Character { get { return mCharacter ?? (mCharacter = GetComponent<Character>()); }}

        private Character mCharacter;
    }
    
    class IceBlockEffect : ACharacterEffect
    {
        public int TouchesToBreak = 3;

        GameObject mIceBlockPrefab;
        int touchCnt;
        
        void Awake()
        {
            var prefab = Resources.Load<GameObject>("iceblock");
            mIceBlockPrefab = Instantiate(prefab);
            mIceBlockPrefab.transform.position = transform.position;
            transform.parent = mIceBlockPrefab.transform.parent;
            
            var inputForwarder = mIceBlockPrefab.AddComponent<InputForwarder>();
            inputForwarder.OnMouseUpEvent += HandleMouseUpEvent;
            
            Character.Disable();
        }

        private void HandleMouseUpEvent()
        {
            touchCnt++;
            if (touchCnt >= TouchesToBreak)
            {
                OnEffectComplete();
            }
        }

        private void OnEffectComplete()
        {
            transform.parent = null;
            mIceBlockPrefab.GetComponent<InputForwarder>().OnMouseUpEvent -= HandleMouseUpEvent;
            
            Character.Enable();
            
            Destroy(mIceBlockPrefab);
            Destroy(this);
        }
    }

    class InputForwarder : MonoBehaviour
    {
        public event Action OnMouseUpEvent;
        
        void OnMouseUp()
        {
            OnMouseUpEvent?.Invoke();
        }
    }
}