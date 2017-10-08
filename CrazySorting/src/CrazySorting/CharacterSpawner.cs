using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrazySorting.Utility;
using UnityEngine;

namespace CrazySorting
{
    [Serializable]
    class CharacterTemplateBucket
    {
        [Tooltip("Character type to spawn")]
        public Character[] CharactersToSpawn;
        [Tooltip("Weight of how likely it is this character will spawn. The smaller the less likely")]
        public float Weight = 1;
    }

    [Serializable]
    class SimultaneousSpawn
    {
        [Tooltip("Amount of Characters to spawn at the same times")]
        public int Amount = 1;
        [Tooltip("How likely this amount will be spawned.")]
        public float Weight = 1;
    }

    class CharacterSpawner : MonoBehaviour
    {
        [Tooltip("Padding within the spawn area")]
        public float SpawnPadding;
        [Tooltip("Character types to spawn")]
        public CharacterTemplateBucket[] CharacterTemplatesBucket;
        [Tooltip("Rate with which the time between spawns decreases over time")]
        public float SpeedIncreaseFactor = 1.5f;
        [Tooltip("Minimum time between spawns in seconds")]
        public float MinSpawnInterval = 0.5f;
        [Tooltip("Maximum time between spawns in seconds")]
        public float MaxSpawnInterval = 1.5f;
        [Tooltip("Amount of spawns to happen at the same time")]
        public SimultaneousSpawn[] SimulataneousSpawns;

        public SpawnPoint[] SpawnPoints;

        Game mGame;
        float mTimeSinceLastSpawn;
        float mTimeToSpawnNext;
        IEnumerable<Goal> mGoals;
        float mElapsedTime;

        public void RegisterGame(Game game)
        {
            mGame = game;
        }

        private void Awake()
        {
            mGoals = FindObjectsOfType<Goal>();
        }

        private void Update()
        {
            if (mGame.ActiveCharacters.Count == 0)
                mTimeToSpawnNext = 0;
                
            mTimeSinceLastSpawn += Time.deltaTime;
            mElapsedTime += Time.deltaTime;

            if(mTimeSinceLastSpawn > mTimeToSpawnNext)
            {
                var maxWeight = SimulataneousSpawns.Max(s => s.Weight);
                var ran = UnityEngine.Random.Range(0f, 1f);
                var simultaneousSpawn = SimulataneousSpawns.Where(c => c.Weight >= ran * maxWeight).RandomElement();
                
                for(var i = 0; i < simultaneousSpawn.Amount; i++)
                {
                    SpawnCharacter();
                }

                SetNewSpawnTime();
            }
        }

        void SetNewSpawnTime()
        {
            mTimeSinceLastSpawn = 0f;

            var step = mElapsedTime / 60 * SpeedIncreaseFactor;
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
            var maxWeight = CharacterTemplatesBucket.Max(c => c.Weight);
            var ran = UnityEngine.Random.Range(0f, 1f);
            var bucket = CharacterTemplatesBucket.Where(c => c.Weight >= ran * maxWeight).OrderBy(c=>c.Weight).First();
            var templateToSpawn = bucket.CharactersToSpawn.RandomElement();

            var character = Instantiate(templateToSpawn, Vector2.zero, Quaternion.identity);
            SetRandomPosition(character);

            character.gameObject.SetActive(true);

            mGame.OnCharacterSpawned(character);
        }

        private void SetRandomPosition(Character character)
        {
            var randomSpawnPoint = SpawnPoints[UnityEngine.Random.Range(0, SpawnPoints.Length)];

            character.transform.position = randomSpawnPoint.Bounds.GetRandomPositionInBounds(character.GetComponent<Renderer>().bounds.size.x / 2f);
            
            if (mGoals.Any(g => g.IsCharacterInGoal(character)))
                SetRandomPosition(character);
        }
    }
}
