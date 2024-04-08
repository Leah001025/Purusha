using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleJumpState : PlayerBattleState
{
    public PlayerBattleJumpState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.BattleJumpParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.BattleJumpParameterHash);
    }

    public override void Update()
    {
        base.Update();
    }
}
