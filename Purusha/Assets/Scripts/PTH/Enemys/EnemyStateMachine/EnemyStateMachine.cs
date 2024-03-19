using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    public Enemy Enemy { get; }
    public EnemyIdleState IdleState { get; set; }
    public EnemyHitState HitState { get; set; }
    public EnemySkill1State Skill1State { get; set; }
    public EnemyStateMachine(Enemy enemy)
    {
        this.Enemy = enemy;

        IdleState = new EnemyIdleState(this);
        HitState = new EnemyHitState(this);
        Skill1State = new EnemySkill1State(this);
    }
}
