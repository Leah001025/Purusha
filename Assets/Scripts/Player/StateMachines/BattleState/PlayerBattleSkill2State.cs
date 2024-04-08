using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleSkill2State : PlayerBattleState
{
    public PlayerBattleSkill2State(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.BattleSkill2ParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.BattleSkill2ParameterHash);
    }

    public override void Update()
    {
        base.Update();
    }
}
