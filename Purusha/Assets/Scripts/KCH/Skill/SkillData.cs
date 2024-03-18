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
    public double AtkCoefficient;
    public double DefCoefficient;
    public double HealthCoefficient;
    public int CoolTime;
    public double DebuffProbability;
    public int Duration;
    public int SkillGage;
    public string EffectPath;

}


