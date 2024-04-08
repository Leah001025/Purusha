using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleSkill4State : PlayerBattleState
{
    public PlayerBattleSkill4State(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.BattleSkill4ParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.BattleSkill4ParameterHash);
    }

    public override void Update()
    {
        base.Update();
    }
}
