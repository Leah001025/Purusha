using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBattleState : PlayerBaseState
{
    public PlayerBattleState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        actionController.OnBattleIdle += OnPlayerBattleIdle;
        actionController.OnBattleJump += OnPlayerBattleJump;
        actionController.OnBattleMove += OnPlayerBattleMove;
        actionController.OnBattleSkill1 += OnPlayerBattleSkill1;
        actionController.OnBattleSkill2 += OnPlayerBattleSkill2;
        actionController.OnBattleSkill3 += OnPlayerBattleSkill3;
        actionController.OnBattleSkill4 += OnPlayerBattleSkill4;
        StartAnimation(stateMachine.Player.AnimationData.BattleParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        actionController.OnBattleIdle += OnPlayerBattleIdle;
        actionController.OnBattleJump += OnPlayerBattleJump;
        actionController.OnBattleMove += OnPlayerBattleMove;
        actionController.OnBattleSkill1 += OnPlayerBattleSkill1;
        actionController.OnBattleSkill2 += OnPlayerBattleSkill2;
        actionController.OnBattleSkill3 += OnPlayerBattleSkill3;
        actionController.OnBattleSkill4 += OnPlayerBattleSkill4;
        StopAnimation(stateMachine.Player.AnimationData.BattleParameterHash);
    }

    public override void Update()
    {
        base.Update();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    private void OnPlayerBattleSkill4()
    {

    }

    private void OnPlayerBattleSkill3()
    {

    }

    private void OnPlayerBattleSkill2()
    {

    }

    private void OnPlayerBattleSkill1()
    {

    }

    private void OnPlayerBattleMove()
    {

    }

    private void OnPlayerBattleJump()
    {

    }

    private void OnPlayerBattleIdle()
    {

    }
}
