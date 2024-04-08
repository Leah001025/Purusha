using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWorldState : PlayerBaseState
{
    public PlayerWorldState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        PlayerInput input = stateMachine.Player.Input;
        input.PlayerActions.Movement.canceled += OnMovementCanceled;
        input.PlayerActions.Run.started += OnRunStarted;
        StartAnimation(stateMachine.Player.AnimationData.WorldParameterHash);
        stateMachine.Player.ActionController.OnWorldAttack += WorldAttack;
    }
    public override void Exit()
    {
        base.Exit();
        PlayerInput input = stateMachine.Player.Input;
        input.PlayerActions.Movement.canceled -= OnMovementCanceled;
        input.PlayerActions.Run.started -= OnRunStarted;
        StopAnimation(stateMachine.Player.AnimationData.WorldParameterHash);
        stateMachine.Player.ActionController.OnWorldAttack -= WorldAttack;
    }
    public override void Update()
    {
        base.Update();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
        if (stateMachine.MovementInput == Vector2.zero)
        {
            return;
        }

        stateMachine.ChangeState(stateMachine.WorldIdleState);

        base.OnMovementCanceled(context);
    }

    protected virtual void OnMove()
    {
        stateMachine.ChangeState(stateMachine.WorldWalkState);
    }
    private void WorldAttack()
    {
        stateMachine.ChangeState(stateMachine.WorldAttackState);
    }

}