using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [field: Header("Animations")]
    [field: SerializeField] public EnemyAnimationData AnimationData { get; private set; }

    private EnemyStateMachine stateMachine;
    public TestHealthSystem TestHealthSystem { get; private set; }
    public Animator Animator { get; private set; }

    private int EnemyName;

    private void Awake()
    {
        EnemyName = Utility.GetHashWithTag(gameObject);
        AnimationData.Initialize();
        TestHealthSystem = GetComponent<TestHealthSystem>();
        Animator = GetComponent<Animator>();
        stateMachine = new EnemyStateMachine(this);
    }

    void Start()
    {
        stateMachine.ChangeState(stateMachine.IdleState);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void MonsterFindData(string name)
    {
        
    }
}
