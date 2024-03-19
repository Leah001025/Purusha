using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBattle : SingleTon<TestBattle>
{
    public float time = 0;
    public float timeModifier = 1;
    public float turnIndicator1;
    public float turnIndicator2;
    public float turnIndicator3;
    public float turnIndicator4;
    public float turnIndicator5;
    public bool isBattle = false;
    public bool isTurnPlay = false;
    public event Action skill1;
    public event Action skill2;
    public event Action skill3;
    public event Action skill4;
    public GameObject targetObject;

    protected override void Awake()
    {
        base.Awake();
    }
    public void Update()
    {
        if (isBattle)
        {
            time += Time.deltaTime * timeModifier;
            if (time > 3600) { }
        }
    }

    public void CallSkill1Event()
    {
        skill1?.Invoke();
    }
    public void CallSkill2Event()
    {
        skill2?.Invoke();
    }
    public void CallSkill3Event()
    {
        skill3?.Invoke();
    }
    public void CallSkill4Event()
    {
        skill4?.Invoke();
    }
    public void SetTurnIndicator(string teamIndex, float curtime)
    {
        switch (teamIndex)
        {
            case "1":
                turnIndicator1 = curtime * 200;
                break;
            case "2":
                turnIndicator2 = curtime * 200;
                break;
            case "3":
                turnIndicator3 = curtime * 200;
                break;
            case "4":
                turnIndicator4 = curtime * 200;
                break;
            case "5":
                turnIndicator5 = curtime * 200;
                break;
        }
    }
    public float GetTurnIndicator(string teamIndex)
    {
        switch (teamIndex)
        {
            case "1":
                return turnIndicator1;
            case "2":
                return turnIndicator2;
            case "3":
                return turnIndicator3;
            case "4":
                return turnIndicator4;
            case "5":
                return turnIndicator5;
            default: return 0;
        }
    }

}
