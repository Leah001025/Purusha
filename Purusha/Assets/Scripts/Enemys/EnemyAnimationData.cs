using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyAnimationData
{
    [SerializeField] private string idleParameterName = "Idle";
    [SerializeField] private string runParameterName = "Run";
    [SerializeField] private string hitParameterName = "Hit";
    [SerializeField] private string skill1ParameterName = "Skill1";
    [SerializeField] private string skill2ParameterName = "Skill2";

    public int IdleParameterHash { get; private set; }
    public int RunParameterHash { get; private set; }
    public int HitParameterHash { get; private set; }
    public int Skill1ParameterHash { get; private set; }
    public int Skill2ParameterHash { get; private set; }

    public void Initialize()
    {
        IdleParameterHash = Animator.StringToHash(idleParameterName);
        RunParameterHash = Animator.StringToHash(runParameterName);
        HitParameterHash = Animator.StringToHash(hitParameterName);
        Skill1ParameterHash = Animator.StringToHash(skill1ParameterName);
        Skill2ParameterHash = Animator.StringToHash(skill2ParameterName);
    }
}
