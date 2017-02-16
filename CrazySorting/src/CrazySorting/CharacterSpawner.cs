using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CrazySorting
{
    [Serializable]
    class CharacterTemplate
    {
        public Character GameObjectToSpawn;
        public int Difficultylevel;
    }

    [RequireComponent(typeof(Game))]
    class CharacterSpawner : MonoBehaviour
    {
        public float SpawnPadding;
        public Renderer Bounds;
        public CharacterTemplate[] CharacterTemplates;
        public float SpeedIncreaseFactor = 1.5f;
        public float MinSpawnInterval = 0.5f;
        public float MaxSpawnInterval = 1.5f;

        Game mGame;
        float mTimeSinceLastSpawn;
        float mTimeToSpawnNext;
        UnityEngine.Random mRandom;


        private void Awake()
        {
            mGame = GetComponent<Game>();
            mGame.Register(this);

            mRandom = new UnityEngine.Random();
        }

        private void Update()
        {
            mTimeSinceLastSpawn += Time.deltaTime;

            if(mTimeSinceLastSpawn > mTimeToSpawnNext)
            {
                SpawnCharacter();

                SetNewSpawnTime();
            }
        }

        void SetNewSpawnTime()
        {
            mTimeSinceLastSpawn = 0f;

            var step = Time.timeSinceLevelLoad / 60 * SpeedIncreaseFactor;
            var maxTime = Mathf.Max(0.8f, MaxSpawnInterval - step);
            var minTime = Mathf.Max(0.2f, MinSpawnInterval - step);

            mTimeToSpawnNext = UnityEngine.Random.Range(minTime, maxTime);
        }

        internal void Stop()
        {
            enabled = false;
        }

        private void SpawnCharacter()
        {
            var randIndex = UnityEngine.Random.Range(0, CharacterTemplates.Length);

            var character = Instantiate(CharacterTemplates[randIndex].GameObjectToSpawn, Bounds.bounds.GetRandomPositionInBounds(SpawnPadding), Quaternion.identity);
            character.gameObject.SetActive(true);

            mGame.OnCharacterSpawned(character);
        }
    }
}
