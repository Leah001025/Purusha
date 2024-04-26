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
        base.Update();
        if (stateMachine.Enemy.Animator.GetCurrentAnimatorStateInfo(0).IsName("Sikll1") == false)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }
}
