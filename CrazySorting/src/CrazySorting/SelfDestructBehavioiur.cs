using System;
using UnityEngine;

[RequireComponent(typeof(Character))]
class SelfDestructBehaviour : MonoBehaviour
{
    public float SelfDestructTimer = 5;
    public float EnrageTimer = 3;

    bool enraged;
    float mTimer;
    Action mSelfDestructAction;
    Color mStartColor;

    void Awake()
    {
        GetComponent<Character>().Register(this);
        mStartColor = GetComponent<Renderer>().material.color;
    }

    void Update()
    {
        mTimer += Time.deltaTime;

        if(!enraged && mTimer > EnrageTimer)
        {
            SetColor(Color.red);
            enraged = true;
        }

        if (mTimer > SelfDestructTimer)
        {
            mSelfDestructAction?.Invoke();
            Stop();
        }
    }

    internal void Stop()
    {
        enabled = false;
        SetColor(mStartColor);
    }

    void SetColor(Color color)
    {
        gameObject.GetComponent<Renderer>().material.color = color;
    }

    internal void SetOnSelfDestructAction(Action selfDestruct)
    {
        mSelfDestructAction = selfDestruct;
    }
}
