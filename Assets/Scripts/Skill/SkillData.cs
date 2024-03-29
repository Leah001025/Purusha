using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillData : DataBase<SkillData>
{
    public int ID;
    public string Name;
    public string Description;
    public int Type;
    public int Range;
    public float AtkCoefficient;
    public float DefCoefficient;
    public float HealthCoefficient;
    public int CoolTime;
    public float DebuffProbability;
    public int Duration;
    public int SkillGage;
    public string EffectPath;
    public string IconPath;
}


