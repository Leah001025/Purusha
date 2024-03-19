using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class CharacterActionController : MonoBehaviour
{
    public event Action OnHit;
    public event Action OnDie;

    public event Action OnSkill1;
    public event Action OnSkill2;
    public event Action OnSkill3;
    public event Action OnSkill4;

    public void Hit()
    {
        OnHit?.Invoke();
    }
    public void Die()
    {
        OnDie?.Invoke();
    }
    public void Skill1()
    {
        OnSkill1?.Invoke();
    }
    public void Skill2()
    {
        OnSkill2?.Invoke();
    }
    public void Skill3()
    {
        OnSkill1?.Invoke();
    }
    public void Skill4()
    {
        OnSkill1?.Invoke();
    }
}
