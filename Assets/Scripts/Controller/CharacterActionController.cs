using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class CharacterActionController
{
    public event Action OnWorldIdle;
    public event Action OnWorldWalk;
    public event Action OnWorldRun;
    public event Action OnWorldAttack;
    
    public event Action OnBattleIdle;
    public event Action OnBattleJump;
    public event Action OnBattleMove;
    public event Action OnBattleHit;
    public event Action OnBattleSkill1;
    public event Action OnBattleSkill2;
    public event Action OnBattleSkill3;
    public event Action OnBattleSkill4;

    public event Action OnDie;

    public event Action OnTarget;
    public event Action OffTarget;

    //World Action
    public void WorldIdle()
    {
        OnWorldIdle?.Invoke();
    }
    public void WorldWalk()
    {
        OnWorldWalk?.Invoke();
    }
    public void WorldRun()
    {
        OnWorldRun?.Invoke();
    }
    public void WorldAttack()
    {
        OnWorldAttack?.Invoke();
    }
    //Battle Action
    public void BattleIdle()
    {
        OnBattleIdle?.Invoke();
    }
    public void BattleJump()
    {
        OnBattleJump?.Invoke();
    }
    public void BattleMove()
    {
        OnBattleMove?.Invoke();
    }
    public void BattleHit()
    {
        OnBattleHit?.Invoke();
    }
    public void BattleSkill1()
    {
        OnBattleSkill1?.Invoke();
    }
    public void BattleSkill2()
    {
        OnBattleSkill2?.Invoke();
    }
    public void BattleSkill3()
    {
        OnBattleSkill3?.Invoke();
    }
    public void BattleSkill4()
    {
        OnBattleSkill4?.Invoke();
    }
    public void Die()
    {
        OnDie?.Invoke();
    }
    //Target Action
    public void TargetOn()
    {
        OnTarget?.Invoke();
    }
    public void TargetOff()
    {
        OffTarget?.Invoke();
    }
}
