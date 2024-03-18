using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    public Enemy Enemy { get; }
    public EnemyIdleState IdleState { get; set; }
    public EnemyHitState HitState { get; set; }
    public EnemyAttackState AttackState { get; set; }
    public EnemyStateMachine(Enemy enemy)
    {
        this.Enemy = enemy;

        IdleState = new EnemyIdleState(this);
        HitState = new EnemyHitState(this);
        AttackState = new EnemyAttackState(this);
    }
}
