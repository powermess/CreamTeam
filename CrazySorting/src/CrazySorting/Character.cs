using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class Character : MonoBehaviour
{
    public Faction Faction;
    public AudioClip SelfDestructSound;

    public event Action<Character, Goal> OnEnteredGoal;
    public event Action<Character> OnSelfDestruct;

    Dictionary<Collider2D, Goal> mGoals;
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
        mGoals = FindObjectsOfType<Goal>().ToDictionary(k => k.GetComponent<Collider2D>(), v => v);
        mCollider = GetComponent<Collider2D>();
    }

    void CheckIfInGoal()
    {
        foreach (var goal in mGoals)
        {
            if (IsContainedInCollider(mCollider, goal.Key))
            {
                EnterGoal(goal.Value);
            }
        }
    }

    bool IsContainedInCollider(Collider2D containee, Collider2D container)
    {
        return container.bounds.Contains(containee.bounds.min) && container.bounds.Contains(containee.bounds.max);
    }

    void EnterGoal(Goal goal)
    {
        OnEnteredGoal?.Invoke(this, goal);
    }

    void SelfDestruct()
    {
        if (mActive)
        {
            OnSelfDestruct?.Invoke(this);
            GetComponent<AudioSource>().PlayOneShot(SelfDestructSound);
        }
    }    
}
