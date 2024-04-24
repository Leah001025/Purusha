using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleHitState : PlayerBattleState
{
    public PlayerBattleHitState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.BattleHitParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.BattleHitParameterHash);
    }

    public override void Update()
    {
        base.Update();
        if (stateMachine.Player.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f && stateMachine.Player.Animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
        {
            stateMachine.ChangeState(stateMachine.BattleIdleState);
        }
    }
}