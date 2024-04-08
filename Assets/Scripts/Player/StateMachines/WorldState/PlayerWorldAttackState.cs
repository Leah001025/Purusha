using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWorldAttackState : PlayerWorldState
{
    public PlayerWorldAttackState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.WorldAttackParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
    }
}
