using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player Player { get; }

    public PlayerWorldIdleState WorldIdleState { get; }
    public PlayerWorldWalkState WorldWalkState { get; }
    public PlayerWorldRunState WorldRunState { get; }
    public PlayerWorldAttackState WorldAttackState { get; }

    public PlayerBattleIdleState BattleIdleState { get; }
    public PlayerBattleJumpState BattleJumpState { get; }
    public PlayerBattleMoveState BattleMoveState { get; }
    public PlayerBattleSkill1State BattleSkill1State { get; }
    public PlayerBattleSkill2State BattleSkill2State { get; }
    public PlayerBattleSkill3State BattleSkill3State { get; }
    public PlayerBattleSkill4State BattleSkill4State { get; }

    public Vector2 MovementInput { get; set; }
    public float MovementSpeed { get; private set; }
    public float RotationDamping { get; private set; }
    public float MovementSpeedModifier { get; set; } = 1f;

    public Transform MainCameraTransform { get; set; }

    public PlayerStateMachine(Player player)
    {
        this.Player = player;

        WorldWalkState = new PlayerWorldWalkState(this);
        WorldIdleState = new PlayerWorldIdleState(this);
        WorldRunState = new PlayerWorldRunState(this);
        WorldAttackState = new PlayerWorldAttackState(this);

        BattleIdleState = new PlayerBattleIdleState(this);
        BattleJumpState = new PlayerBattleJumpState(this);
        BattleMoveState = new PlayerBattleMoveState(this);
        BattleSkill1State = new PlayerBattleSkill1State(this);
        BattleSkill2State = new PlayerBattleSkill2State(this);
        BattleSkill3State = new PlayerBattleSkill3State(this);
        BattleSkill4State = new PlayerBattleSkill4State(this);

        MainCameraTransform = Camera.main.transform;

        MovementSpeed = player.Data.GroundedData.BaseSpeed;
        RotationDamping = player.Data.GroundedData.BaseRotationDamping;
    }
}