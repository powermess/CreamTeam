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
        public int DifficultyLevel;
    }

    class CharacterSpawner : MonoBehaviour
    {
        public float SpawnPadding;
        public CharacterTemplate[] CharacterTemplates;
        public float SpeedIncreaseFactor = 1.5f;
        public float MinSpawnInterval = 0.5f;
        public float MaxSpawnInterval = 1.5f;
        public int SimulataneousSpawns = 1;

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
            mTimeSinceLastSpawn += Time.deltaTime;
            mElapsedTime += Time.deltaTime;

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
            var randIndex = UnityEngine.Random.Range(0, CharacterTemplates.Length);

            var character = Instantiate(CharacterTemplates[randIndex].GameObjectToSpawn, Vector2.zero, Quaternion.identity);
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
