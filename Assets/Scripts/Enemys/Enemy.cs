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
    public CharacterActionController actionController { get; private set; }
    public Animator Animator { get; private set; }

    private EnemyStateMachine stateMachine;

    private TargetUI targetUI;

    [Header("GameObject Point")]
    [SerializeField] private GameObject monsterInfoPoint;
    [SerializeField] private GameObject targetPoint;

    public event Action OnUpdate;

    private void Awake()
    {
        AnimationData.Initialize();
        actionController = new CharacterActionController();
        Animator = GetComponentInChildren<Animator>();
        stateMachine = new EnemyStateMachine(this);
    }
    private void Start()
    {
        stateMachine.ChangeState(stateMachine.IdleState);
        if (BattleManager.Instance != null)
        {
            targetUI = GetComponentInChildren<TargetUI>();
            actionController.OnTarget += TargetOn;
            actionController.OffTarget += TargetOff;
            actionController.OnDie += ThisDie;
        }
    }
    private void Update()
    {
        OnUpdate?.Invoke();
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
        actionController.OnTarget -= TargetOn;
        actionController.OffTarget -= TargetOff;
        actionController.OnDie -= ThisDie;
        Destroy(gameObject);
    }
}
