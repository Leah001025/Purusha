using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWorldRunState : PlayerWorldState
{
    public PlayerWorldRunState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = groundData.RunSpeedModifier;
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.WorldRunParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.WorldRunParameterHash);
    }
}