using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;

public class EnemyBaseState : IState
{
    protected EnemyStateMachine stateMachine;
    protected CharacterActionController actionController;
    public EnemyBaseState(EnemyStateMachine enemyStateMachine)
    {
        stateMachine = enemyStateMachine;
        actionController = stateMachine.Enemy.ActionController;
    }
    public virtual void HandleInput()
    {

    }

    public virtual void Enter()
    {
        actionController.OnWorldIdle += OnEnemyIdle;
        actionController.OnWorldRun += OnEnemyRun;

        actionController.OnBattleHit += OnEnemyHit;
        actionController.OnBattleSkill1 += OnEnemySkill1;
        actionController.OnBattleSkill2 += OnEnemySkill2;
    }
    public virtual void Exit()
    {
        actionController.OnWorldIdle -= OnEnemyIdle;
        actionController.OnWorldRun -= OnEnemyRun;

        actionController.OnBattleHit -= OnEnemyHit;
        actionController.OnBattleSkill1 -= OnEnemySkill1;
        actionController.OnBattleSkill1 -= OnEnemySkill2;
    }
    public void PhysicsUpdate()
    {
        
    }

    public virtual void Update()
    {
        if (stateMachine.Enemy.Animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") == false &&
            stateMachine.Enemy.Animator.GetCurrentAnimatorStateInfo(0).IsName("Sikll1") == false &&
            stateMachine.Enemy.Animator.GetCurrentAnimatorStateInfo(0).IsName("Sikll2") == false &&
            stateMachine.Enemy.Animator.GetCurrentAnimatorStateInfo(0).IsName("Hit") == false)
        {
            OnEnemyIdle();
        }
    }
    protected virtual void OnEnemyIdle()
    {
        stateMachine.ChangeState(stateMachine.IdleState);
    }
    protected virtual void OnEnemyRun()
    {
        stateMachine.ChangeState(stateMachine.RunState);
    }
    protected virtual void OnEnemyHit()
    {
        stateMachine.ChangeState(stateMachine.HitState);
    }
    protected virtual void OnEnemySkill1()
    {
        stateMachine.ChangeState(stateMachine.Skill1State);
    }
    protected virtual void OnEnemySkill2()
    {
        stateMachine.ChangeState(stateMachine.Skill2State); 
    }

    protected void StartAnimation(int animationHash)
    {
        stateMachine.Enemy.Animator.SetBool(animationHash, true);
    }
    protected void StopAnimation(int animationHash)
    {
        stateMachine.Enemy.Animator.SetBool(animationHash, false);
    }
    protected void OnTriggerAnimation(int animationHash)
    {
        stateMachine.Enemy.Animator.SetTrigger(animationHash);
    }
    protected void NewAnimationTime()
    {
        float curAnimationTime = stateMachine.Enemy.Animator.GetCurrentAnimatorStateInfo(0).length;
        BattleManager.Instance.newAnimTime = curAnimationTime;
    }
}
