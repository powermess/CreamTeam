using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Faction Faction;

    public event Action<Character, Goal> OnEnteredGoal;
    Dictionary<Collider2D, Goal> mGoals;
    Collider2D mCollider;
    DraggableCharacterBehaviour mDraggableBehaviour;

    void Awake()
    {
        mGoals = FindObjectsOfType<Goal>().ToDictionary(k => k.GetComponent<Collider2D>(), v => v);
        mCollider = GetComponent<Collider2D>();

        mDraggableBehaviour = gameObject.AddComponent<DraggableCharacterBehaviour>();
        mDraggableBehaviour.OnCharacterReleased += CheckIfInGoal;
    }

    void OnDestroy()
    {
        mDraggableBehaviour.OnCharacterReleased -= CheckIfInGoal;
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

    private void EnterGoal(Goal goal)
    {
        OnEnteredGoal?.Invoke(this, goal);
    }

    public void Stop()
    {
        mDraggableBehaviour.DisableDragging();
    }
}
