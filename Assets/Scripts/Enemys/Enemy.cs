using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;
using Enums;
using Structs;
using System;

public class Enemy : MonoBehaviour
{
    [field: Header("Animations")]
    [field: SerializeField] public EnemyAnimationData AnimationData { get; private set; }
    public CharacterActionController ActionController { get; private set; }
    public Animator Animator { get; private set; }

    private EnemyStateMachine stateMachine;

    private TargetUI targetUI;

    [Header("GameObject Point")]
    [SerializeField] private GameObject monsterInfoPoint;
    [SerializeField] private GameObject targetPoint;

    private void Awake()
    {
        AnimationData.Initialize();
        ActionController = new CharacterActionController();
        Animator = GetComponentInChildren<Animator>();
        stateMachine = new EnemyStateMachine(this);
    }
    private void Start()
    {
        stateMachine.ChangeState(stateMachine.IdleState);
        if (BattleManager.Instance != null)
        {
            targetUI = GetComponentInChildren<TargetUI>();
            ActionController.OnTarget += TargetOn;
            ActionController.OffTarget += TargetOff;
            ActionController.OnDie += ThisDie;
        }
    }
    private void Update()
    {
        stateMachine.Update();
    }
    private void TargetOn()
    {
        targetUI.OnTarget();
    }
    private void TargetOff()
    {
        targetUI.OffTarget();
    }
    private void ThisDie()
    {
        ActionController.OnTarget -= TargetOn;
        ActionController.OffTarget -= TargetOff;
        ActionController.OnDie -= ThisDie;
        Destroy(gameObject);
    }
}
