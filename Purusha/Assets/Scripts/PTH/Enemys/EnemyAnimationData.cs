using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyAnimationData
{
    [SerializeField] private string idleParameterName = "Idle";
    [SerializeField] private string attackParameterName = "Attack";
    [SerializeField] private string hitParameterName = "Hit";

    public int IdleParameterHash { get; private set; }
    public int AttackParameterHash { get; private set; }
    public int HitParameterHash { get; private set; }

    public void Initialize()
    {
        IdleParameterHash = Animator.StringToHash(idleParameterName);
        AttackParameterHash = Animator.StringToHash(attackParameterName);
        HitParameterHash = Animator.StringToHash(hitParameterName);
    }
}