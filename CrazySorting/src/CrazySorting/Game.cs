using CrazySorting;
using CrazySorting.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

class Game : MonoBehaviour, IGame
{
    public GameObject GameOverSign;
    public Text ScoreText;
    public Stage[] Stages;

    int mScore;
    List<Character> mActiveCharacters = new List<Character>();
    int mCurrentStageIndex;
    float mCurrentStageDuration;
    bool mTransitioning;

    CharacterSpawner mSpawner;

    private void Awake()
    {
        Initialize();

        GameOverSign.SetActive(false);

        ScoreText.text = "0";

        Stages.Apply(s => s.gameObject.SetActive(false));
    }

    private void Start()
    {
        StartStage(mCurrentStageIndex);
    }

    void Update()
    {
        mCurrentStageDuration += Time.deltaTime;

        CheckEndCondition(mCurrentStageDuration);
    }

    private void CheckEndCondition(float duration)
    {
        if(!mTransitioning && duration > Stages[mCurrentStageIndex].Duration)
        {
            StartTransition();
        }else if (mTransitioning && !mActiveCharacters.Any())
        {
            StartNextStage();
        }
    }

    private void StartTransition()
    {
        "Start transition".Log();
        mTransitioning = true;
        Stages[mCurrentStageIndex].Spawner.Stop();
        mSpawner.gameObject.SetActive(false);
    }

    private void StartNextStage()
    {
        "Start next stage".Log();
        mCurrentStageIndex++;

        if (mCurrentStageIndex >= Stages.Length) //should never happen in an endless game
        {
            GameOver();
            return;
        }

        Reset();

        StartStage(mCurrentStageIndex);
    }

    private void StartStage(int index)
    {
        "Stages count: {0}".Log(Stages.Length);
        var currentStage = Stages[index];
        "currentStage null: {0}".Log(currentStage == null);
        mSpawner = currentStage.Spawner;
        "spawner null: {0}".Log(mSpawner == null);
        mSpawner.gameObject.SetActive(true);
        mSpawner.RegisterGame(this);
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

        mActiveCharacters.Remove(character);
    }

    void Initialize()
    {
        Application.targetFrameRate = 60;
    }

    public void GameOver()
    {
        enabled = false;
        GameOverSign.SetActive(true);
        GameOverSign.transform.FindChild("Score").GetComponent<Text>().text = mScore.ToString();

        mActiveCharacters.ForEach(c => c.Stop());

        mSpawner.Stop();
        mTransitioning = false;
    }

    void Reset()
    {
        mActiveCharacters.Clear();
        mCurrentStageDuration = 0;
        mTransitioning = false;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OnCharacterSpawned(Character character)
    {
        character.OnEnteredGoal += HandleCharaterEnteredGoalEvent;

        mActiveCharacters.Add(character);
    }

    public void OnCharacterDespawned(Character character)
    {
        character.OnEnteredGoal -= HandleCharaterEnteredGoalEvent;
        
        mActiveCharacters.Remove(character);
    }
}
