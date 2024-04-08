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
    }
}
