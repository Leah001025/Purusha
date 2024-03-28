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
    }
    private void Update()
    {
        OnUpdate?.Invoke();
    }
}
