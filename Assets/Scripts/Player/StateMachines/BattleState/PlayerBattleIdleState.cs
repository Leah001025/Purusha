using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleIdleState : PlayerBattleState
{
    public PlayerBattleIdleState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.BattleIdleParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.BattleIdleParameterHash);
    }

    public override void Update()
    {
        base.Update();
    }
}
