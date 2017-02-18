using CrazySorting.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
class Character : MonoBehaviour
{
    public Faction Faction;
    public AudioClip SelfDestructSound;

    public event Action<Character, Goal> OnEnteredGoal;
    public event Action<Character> OnSelfDestruct;

    IEnumerable<Goal> mGoals;
    Collider2D mCollider;
    IDraggableCharacter mDraggableBehaviour;
    CharacterMover mCharacterMover;
    bool mActive = true;
    SelfDestructBehaviour mSelfDestructor;

    public void Stop()
    {
        mActive = false;
        mDraggableBehaviour.DisableDragging();
        mSelfDestructor.Stop();
        mCharacterMover.Stop();
    }

    public void Register(IDraggableCharacter draggable)
    {
        mDraggableBehaviour = draggable;
        mDraggableBehaviour.SetOnMouseUpAction(CheckIfInGoal);
    }

    public void Register(SelfDestructBehaviour selfDestructor)
    {
        mSelfDestructor = selfDestructor;
        mSelfDestructor.SetOnSelfDestructAction(SelfDestruct);
    }

    public void Register(CharacterMover characterMover)
    {
        mCharacterMover = characterMover;
    }


    void Awake()
    {
        mGoals = FindObjectsOfType<Goal>();
        mCollider = GetComponent<BoxCollider2D>();
    }

    void CheckIfInGoal()
    {
        foreach (var g in mGoals)
        {
            if (g.IsCharacterInGoal(this))
                EnterGoal(g);
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

        OnSelfDestruct?.Invoke(this);
        GetComponent<AudioSource>().PlayOneShot(SelfDestructSound);
    }
}
