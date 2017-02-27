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
        public int SimulataneousSpawns = 1;

        Game mGame;
        float mTimeSinceLastSpawn;
        float mTimeToSpawnNext;
        UnityEngine.Random mRandom;
        IEnumerable<Goal> mGoals;


        private void Awake()
        {
            mGame = GetComponent<Game>();
            mGoals = FindObjectsOfType<Goal>();

            mGame.Register(this);

            mRandom = new UnityEngine.Random();
            
        }

        private void Update()
        {
            mTimeSinceLastSpawn += Time.deltaTime;

            if(mTimeSinceLastSpawn > mTimeToSpawnNext)
            {
                for(var i = 0; i < SimulataneousSpawns; i++)
                {
                    SpawnCharacter();
                }

                SetNewSpawnTime();
            }
        }

        void SetNewSpawnTime()
        {
            mTimeSinceLastSpawn = 0f;

            var step = Time.timeSinceLevelLoad / 60 * SpeedIncreaseFactor;
            var maxTime = Mathf.Max(0.2f, MaxSpawnInterval - step);
            var minTime = Mathf.Max(0.1f, MinSpawnInterval - step);

            mTimeToSpawnNext = UnityEngine.Random.Range(minTime, maxTime);
        }

        internal void Stop()
        {
            enabled = false;
        }

        private void SpawnCharacter()
        {
            var randIndex = UnityEngine.Random.Range(0, CharacterTemplates.Length);

            var character = Instantiate(CharacterTemplates[randIndex].GameObjectToSpawn, Vector2.zero, Quaternion.identity);
            SetRandomPosition(character);

            character.gameObject.SetActive(true);

            mGame.OnCharacterSpawned(character);
        }

        private void SetRandomPosition(Character character)
        {

            character.transform.position = Bounds.bounds.GetRandomPositionInBounds(character.GetComponent<Renderer>().bounds.size.x / 2f);

            if (mGoals.Any(g => g.IsCharacterInGoal(character)))
                SetRandomPosition(character);
        }
    }
}
