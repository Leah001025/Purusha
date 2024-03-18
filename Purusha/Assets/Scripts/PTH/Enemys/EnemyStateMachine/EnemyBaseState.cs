using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;

public class EnemyBaseState : IState
{
    protected EnemyStateMachine stateMachine;
    protected TestHealthSystem testHealthSystem;
    public EnemyBaseState(EnemyStateMachine enemyStateMachine)
    {
        stateMachine = enemyStateMachine;
        testHealthSystem = stateMachine.Enemy.TestHealthSystem;
    }
    public virtual void Enter()
    {
        testHealthSystem.OnHit += OnEnemyHit;
        // += OnAttack;
    }
    public virtual void Exit()
    {
        testHealthSystem.OnHit -= OnEnemyHit;
        // -= OnAttack;
    }
    public void PhysicsUpdate()
    {
        
    }

    public virtual void Update()
    {
        if (stateMachine.Enemy.Animator.GetBool("Attack") == false 
            && stateMachine.Enemy.Animator.GetBool("Hit") == false)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }
    protected virtual void OnEnemyHit()
    {
        stateMachine.ChangeState(stateMachine.HitState);
    }
    protected virtual void OnEnemyAttack()
    {
        stateMachine.ChangeState(stateMachine.AttackState);
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
