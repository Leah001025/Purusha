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

    private SEnemyData? enemyData;
    public SEnemyData? EnemyData { 
        get { if (enemyData == null)
                enemyData = CreateEnemyData(enemyHash); 
                    return enemyData; } 
        set { } }

    private string enemyHash;

    private void Awake()
    {
        enemyHash = gameObject.tag;
        AnimationData.Initialize();
        actionController = GetComponent<CharacterActionController>();
        Animator = GetComponent<Animator>();
        stateMachine = new EnemyStateMachine(this);
    }
    void Start()
    {
        stateMachine.ChangeState(stateMachine.IdleState);
    }
    void Update()
    {

    }
    private SEnemyData CreateEnemyData(string name)
    {
        int _id = (int)Enum.Parse(typeof(EnemyID), name);
        SEnemyData sEnemyData = new SEnemyData(DataManager.Instance.EnemyDB.GetData(_id));
        return sEnemyData;
    }
}
