using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkill1State : EnemyBaseState
{
    public EnemySkill1State(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {

    }
    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Enemy.AnimationData.Skill1ParameterHash);
        NewAnimationTime();
    }
    public override void Exit() 
    { 
        base.Exit();
        StopAnimation(stateMachine.Enemy.AnimationData.Skill1ParameterHash);
    }
    public override void Update()
    {
        base.Update();
    }
}
