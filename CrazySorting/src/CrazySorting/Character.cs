using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using CrazySorting.Enraging;
using UnityEngine;

class Character : MonoBehaviour
{
    public Faction Faction;
    public AudioClip SelfDestructSound;

    public event Action<Character, Goal> OnEnteredGoal;

    IEnumerable<Goal> mGoals;
    IDraggableCharacter mDraggableBehaviour;
    CharacterMover mCharacterMover;
    bool mActive = true;
    EnrageConditionBehaviour mSelfDestructor;
    IEnrageEffect mEnrageEffect;

    public void Stop()
    {
        mActive = false;
        mSelfDestructor.Stop();
        Disable();
    }

    public void Disable()
    {
        mDraggableBehaviour.DisableDragging();
        mCharacterMover.EnableMovement(false);
    }

    public void Enable()
    {
        mDraggableBehaviour.EnableDragging();
        mCharacterMover.EnableMovement(true);
    }

    public void Register(IDraggableCharacter draggable)
    {
        mDraggableBehaviour = draggable;
        mDraggableBehaviour.SetOnMouseUpAction(CheckIfInGoal);
    }

    public void Register(EnrageConditionBehaviour selfDestructor)
    {
        mSelfDestructor = selfDestructor;
        mSelfDestructor.SetOnSelfDestructAction(SelfDestruct);
    }

    public void Register(CharacterMover characterMover)
    {
        mCharacterMover = characterMover;
    }

    public void Register(IEnrageEffect enrageEffect)
    {
        mEnrageEffect = enrageEffect;
    }


    void Awake()
    {
        mGoals = FindObjectsOfType<Goal>();
    }

    void CheckIfInGoal()
    {
        foreach (var goal in mGoals)
        {
            if (goal.IsCharacterInGoal(this))
                EnterGoal(goal);
        };
    }

    void EnterGoal(Goal goal)
    {
        OnEnteredGoal?.Invoke(this, goal);
    }

    void SelfDestruct()
    {
        if (!mActive)
            return;
        
        mEnrageEffect?.Enrage();
        
        GetComponent<AudioSource>().PlayOneShot(SelfDestructSound);
    }
}
