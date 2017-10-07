using UnityEngine;

namespace CrazySorting.CharacterEffects
{
    class ObjectSpawner : ACharacterEffect
    {
        public GameObject ObjectToSpawn;
        public float SpawnInterval;

        float mTime;
        
        void Update()
        {
            if (!Character.Active)
                return;
            
            mTime += Time.deltaTime;

            if (mTime > SpawnInterval)
            {
                mTime = 0;
                Spawn();
            }
        }

        void Spawn()
        {
            if (ObjectToSpawn != null)
                Instantiate(ObjectToSpawn, transform.position + new Vector3(0.1f, 0.1f, 0f), Quaternion.identity);
        }
    }
}