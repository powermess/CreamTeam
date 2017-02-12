using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

class Game : MonoBehaviour
{

    public GameObject GameOverSign;

    private void Awake()
    {
        Initialize();

        GameOverSign.SetActive(false);
    }

    private void HandleCharaterEnteredGoalEvent(Character character, Goal goal)
    {
        character.Stop();
        character.OnEnteredGoal -= HandleCharaterEnteredGoalEvent;

        if(character.Faction != goal.Faction)
        {
            GameOver();
        }
    }

    void HandleSelfDestructEvent(Character character)
    {
        GameOver();
    }

    private void Initialize()
    {
        Application.targetFrameRate = 60;
    }

    void GameOver()
    {
        GameOverSign.SetActive(true);
    }

    public void Reset()
    {
        SceneManager.LoadScene(0);
    }

    public void OnCharacterSpawned(Character character)
    {
        character.OnEnteredGoal += HandleCharaterEnteredGoalEvent;
        character.OnSelfDestruct += HandleSelfDestructEvent;
    }
}
