using System;
using UnityEngine;

namespace CrazySorting.CharacterEffects
{
    class IceBlockEffect : ACharacterEffect
    {
        public int TapsToBreak = 1;
        public GameObject EffectPrefab;
        public bool Frozen = false;

        GameObject mIceBlockPrefab;
        int touchCnt;
        
        void Start()
        {
            SetupPrefab();

            var inputForwarder = mIceBlockPrefab.AddComponent<InputForwarder>();
            inputForwarder.OnMouseUpEvent += HandleMouseUpEvent;
            
            if(Frozen)
                Character.DisableMovement();
            Character.DisableDragging();
        }

        private void SetupPrefab()
        {
            if (EffectPrefab == null)
                Debug.LogError("Effect Prefab not set");

            mIceBlockPrefab = Instantiate(EffectPrefab);
            mIceBlockPrefab.transform.position = transform.position;
            mIceBlockPrefab.transform.parent = transform;
        }

        private void HandleMouseUpEvent()
        {
            touchCnt++;
            if (touchCnt >= TapsToBreak)
            {
                OnEffectComplete();
            }
        }

        private void OnEffectComplete()
        {
            transform.parent = null;
            mIceBlockPrefab.GetComponent<InputForwarder>().OnMouseUpEvent -= HandleMouseUpEvent;
            
            Character.EnableMovement();
            Character.EnableDragging();
            
            Destroy(mIceBlockPrefab);
            Destroy(this);
        }
    }
}