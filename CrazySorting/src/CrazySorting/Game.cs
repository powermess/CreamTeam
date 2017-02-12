using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

class Game : MonoBehaviour
{
    private void Awake()
    {
        Initialize();

        var characters = FindObjectsOfType<Character>().ToList();

        characters.ForEach(c => c.OnEnteredGoal += HandleCharaterEnteredGoalEvent);
    }

    private void HandleCharaterEnteredGoalEvent(Character character, Goal goal)
    {
        character.Stop();

        if(character.Faction != goal.Faction)
        {
            GameOver();
        }
    }

    private void Initialize()
    {
        Application.targetFrameRate = 60;
    }

    void GameOver()
    {
        Debug.Log("Game Over");
    }
}
