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

        Game mGame;
        float mTimeSinceLastSpawn;
        float mTimeToSpawnNext;
        UnityEngine.Random mRandom;


        private void Awake()
        {
            mGame = GetComponent<Game>();
            mRandom = new UnityEngine.Random();
        }

        private void Update()
        {
            mTimeSinceLastSpawn += Time.deltaTime;

            if(mTimeSinceLastSpawn > mTimeToSpawnNext)
            {
                SpawnCharacter();
                mTimeSinceLastSpawn = 0f;

                var maxTime = 3 - Time.time / 60 * SpeedIncreaseFactor;
                Debug.Log(string.Format("maxTime: {0}", maxTime));
                mTimeToSpawnNext = UnityEngine.Random.Range(0f, maxTime);
            }
        }

        private void SpawnCharacter()
        {
            var minX = Bounds.bounds.min.x + SpawnPadding;
            var minY = Bounds.bounds.min.y + SpawnPadding;
            var maxX = Bounds.bounds.max.x - SpawnPadding;
            var maxY = Bounds.bounds.max.y - SpawnPadding;

            var randX = UnityEngine.Random.Range(minX, maxX);
            var randY = UnityEngine.Random.Range(minY, maxY);

            var randIndex = UnityEngine.Random.Range(0, CharacterTemplates.Length);

            var character = Instantiate(CharacterTemplates[randIndex].GameObjectToSpawn, new Vector3(randX, randY, 0), Quaternion.identity);
            character.gameObject.SetActive(true);

            mGame.OnCharacterSpawned(character);
        }


    }
}
