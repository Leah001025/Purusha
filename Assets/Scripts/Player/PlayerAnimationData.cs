using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerAnimationData
{
    [SerializeField] private string worldParameterName = "@World";
    [SerializeField] private string worldIdleParameterName = "WorldIdle";
    [SerializeField] private string worldWalkParameterName = "WorldWalk";
    [SerializeField] private string worldRunParameterName = "WorldRun";
    [SerializeField] private string worldAttackParameterName = "WorldAttack";

    [SerializeField] private string battleParameterName = "@Battle";
    [SerializeField] private string battleIdleParameterName = "BattleIdle";
    [SerializeField] private string battleJumpParameterName = "BattleJump";
    [SerializeField] private string battleMoveParameterName = "BattleMove";
    [SerializeField] private string battleHitParameterName = "BattleHit";
    [SerializeField] private string battleSkill1ParameterName = "BattleSkill1";
    [SerializeField] private string battleSkill2ParameterName = "BattleSkill2";
    [SerializeField] private string battleSkill3ParameterName = "BattleSkill3";
    [SerializeField] private string battleSkill4ParameterName = "BattleSkill4";

    public int WorldParameterHash { get; private set; }
    public int WorldIdleParameterHash { get; private set; }
    public int WorldWalkParameterHash { get; private set; }
    public int WorldRunParameterHash { get; private set; }
    public int WorldAttackParameterHash { get; private set; }

    public int BattleParameterHash { get; private set; }
    public int BattleIdleParameterHash { get; private set; }
    public int BattleJumpParameterHash { get; private set; }
    public int BattleMoveParameterHash { get; private set; }
    public int BattleHitParameterHash { get; private set; }
    public int BattleSkill1ParameterHash { get; private set; }
    public int BattleSkill2ParameterHash { get; private set; }
    public int BattleSkill3ParameterHash { get; private set; }
    public int BattleSkill4ParameterHash { get; private set; }

    public void Initialize()
    {
        WorldParameterHash = Animator.StringToHash(worldParameterName);
        WorldIdleParameterHash = Animator.StringToHash(worldIdleParameterName);
        WorldWalkParameterHash = Animator.StringToHash(worldWalkParameterName);
        WorldRunParameterHash = Animator.StringToHash(worldRunParameterName);
        WorldAttackParameterHash = Animator.StringToHash(worldAttackParameterName);

        BattleParameterHash = Animator.StringToHash(battleParameterName);
        BattleIdleParameterHash = Animator.StringToHash(battleIdleParameterName);
        BattleJumpParameterHash = Animator.StringToHash(battleJumpParameterName);
        BattleMoveParameterHash = Animator.StringToHash(battleMoveParameterName);
        BattleHitParameterHash = Animator.StringToHash(battleHitParameterName);
        BattleSkill1ParameterHash = Animator.StringToHash(battleSkill1ParameterName);
        BattleSkill2ParameterHash = Animator.StringToHash(battleSkill2ParameterName);
        BattleSkill3ParameterHash = Animator.StringToHash(battleSkill3ParameterName);
        BattleSkill4ParameterHash = Animator.StringToHash(battleSkill4ParameterName);
    }
}