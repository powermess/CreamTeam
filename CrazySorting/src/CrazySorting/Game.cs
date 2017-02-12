using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

class Game : MonoBehaviour
{

    public GameObject GameOverSign;
    List<Character> mCharacters;

    private void Awake()
    {
        Initialize();

        mCharacters = FindObjectsOfType<Character>().ToList();

        mCharacters.ForEach(c => c.OnEnteredGoal += HandleCharaterEnteredGoalEvent);

        GameOverSign.SetActive(false);
    }

    private void OnDestroy()
    {
        mCharacters?.ForEach(c => c.OnEnteredGoal -= HandleCharaterEnteredGoalEvent);
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
        GameOverSign.SetActive(true);
    }

    public void Reset()
    {
        SceneManager.LoadScene(0);
    }
}
