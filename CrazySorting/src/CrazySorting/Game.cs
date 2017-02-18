using CrazySorting;
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
    public Text ScoreText;

    int mScore;
    List<Character> mCharacters = new List<Character>();

    CharacterSpawner mSpawner;

    private void Awake()
    {
        Initialize();

        GameOverSign.SetActive(false);

        ScoreText.text = "0";
    }

    private void HandleCharaterEnteredGoalEvent(Character character, Goal goal)
    {
        character.Stop();
        character.OnEnteredGoal -= HandleCharaterEnteredGoalEvent;
        character.transform.parent = goal.transform;

        if (character.Faction != goal.Faction)
        {
            GameOver();
        }
        else
        {
            mScore++;
            ScoreText.text = mScore.ToString();
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
        GameOverSign.transform.FindChild("Score").GetComponent<Text>().text = mScore.ToString();

        mCharacters.ForEach(c => c.Stop());

        mSpawner.Stop();
    }

    public void Reset()
    {
        SceneManager.LoadScene(0);
    }

    public void OnCharacterSpawned(Character character)
    {
        character.OnEnteredGoal += HandleCharaterEnteredGoalEvent;
        character.OnSelfDestruct += HandleSelfDestructEvent;

        mCharacters.Add(character);
    }

    public void Register(CharacterSpawner spawner)
    {
        mSpawner = spawner;
    }
}
