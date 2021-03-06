﻿using System;
using System.Collections.Generic;
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
    EnrageConditionBehaviour mEnrageCondition;
    IEnrageEffect mEnrageEffect;

    public bool Active => mActive;

    public void Stop()
    {
        mActive = false;
        mEnrageCondition.Stop();
        DisableMovement();
        DisableDragging();
        GetComponent<Rigidbody2D>().simulated = false;
    }

    public void DisableMovement()
    {
        mCharacterMover.EnableMovement(false);
    }

    public void DisableDragging()
    {
        mDraggableBehaviour.DisableDragging();
    }

    public void EnableMovement()
    {
        mCharacterMover.EnableMovement(true);
    }

    public void EnableDragging()
    {
        if (!mActive)
            return;
        mDraggableBehaviour.EnableDragging();
    }

    public void Register(IDraggableCharacter draggable)
    {
        mDraggableBehaviour = draggable;
        mDraggableBehaviour.SetOnMouseUpAction(CheckIfInGoal);
    }

    public void Register(EnrageConditionBehaviour selfDestructor)
    {
        mEnrageCondition = selfDestructor;
        mEnrageCondition.SetEnrageAction(Enrage);
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

    void Enrage()
    {
        if (!mActive)
            return;
        
        mEnrageEffect?.Enrage();
        
        GetComponent<AudioSource>().PlayOneShot(SelfDestructSound);
    }
}
