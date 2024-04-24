using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleMoveState : PlayerBattleState
{
    public PlayerBattleMoveState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.BattleMoveParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.BattleMoveParameterHash);
    }

    public override void Update()
    {
        base.Update();
        if (stateMachine.Player.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f && stateMachine.Player.Animator.GetCurrentAnimatorStateInfo(0).IsName("Move"))
        {
            stateMachine.ChangeState(stateMachine.BattleIdleState);
        }
    }
}
