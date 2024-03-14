using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    public EnemyAttackState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {

    }
    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Enemy.AnimationData.AttackParameterHash);
    }
    public override void Exit() 
    { 
        base.Exit();
        StopAnimation(stateMachine.Enemy.AnimationData.AttackParameterHash);
    }
    public override void Update()
    {
        base.Update();
    }
}
