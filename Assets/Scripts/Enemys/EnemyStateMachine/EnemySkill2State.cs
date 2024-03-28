using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkill2State : EnemyBaseState
{
    public EnemySkill2State(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {

    }
    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Enemy.AnimationData.Skill2ParameterHash);
        AnimationTime();
    }
    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Enemy.AnimationData.Skill2ParameterHash);
    }
    public override void Update()
    {
        base.Update();
    }
}