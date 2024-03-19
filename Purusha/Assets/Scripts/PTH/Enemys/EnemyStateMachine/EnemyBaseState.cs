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
        actionController = stateMachine.Enemy.actionController;
    }
    public virtual void Enter()
    {
        actionController.OnHit += OnEnemyHit;
        actionController.OnDie += OnEnemyDie;
        actionController.OnSkill1 += OnEnemySkill1;
        actionController.OnSkill2 += OnEnemySkill2;
    }
    public virtual void Exit()
    {
        actionController.OnHit -= OnEnemyHit;
        actionController.OnDie -= OnEnemyDie;
        actionController.OnSkill1 -= OnEnemySkill1;
        actionController.OnSkill2 -= OnEnemySkill2;
    }
    public void PhysicsUpdate()
    {
        
    }

    public virtual void Update()
    {
        if (stateMachine.Enemy.Animator.GetBool("Skill1") == false 
            && stateMachine.Enemy.Animator.GetBool("Hit") == false)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }
    protected virtual void OnEnemyHit()
    {
        stateMachine.ChangeState(stateMachine.HitState);
    }
    protected virtual void OnEnemyDie()
    {
        
    }
    protected virtual void OnEnemySkill1()
    {
        stateMachine.ChangeState(stateMachine.Skill1State);
    }
    protected virtual void OnEnemySkill2()
    {
        //stateMachine.ChangeState(stateMachine.Skill2State); 
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
}
