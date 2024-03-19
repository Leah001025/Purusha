using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyAnimationData
{
    [SerializeField] private string idleParameterName = "Idle";
    [SerializeField] private string hitParameterName = "Hit";
    [SerializeField] private string skill1ParameterName = "Skill1";

    public int IdleParameterHash { get; private set; }
    public int HitParameterHash { get; private set; }
    public int Skill1ParameterHash { get; private set; }

    public void Initialize()
    {
        IdleParameterHash = Animator.StringToHash(idleParameterName);
        HitParameterHash = Animator.StringToHash(hitParameterName);
        Skill1ParameterHash = Animator.StringToHash(skill1ParameterName);
    }
}
