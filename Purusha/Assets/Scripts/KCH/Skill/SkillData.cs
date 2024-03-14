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

    public override void SetData(SkillData skillData)
    {
        this.ID = skillData.ID;
        this.Name = skillData.Name;
        this.Description = skillData.Description;
        this.Type = skillData.Type;
        this.AtkCoefficient = skillData.AtkCoefficient;
        this.DefCoefficient = skillData.DefCoefficient;
        this.HealthCoefficient = skillData.HealthCoefficient;
        this.CoolTime = skillData.CoolTime;
        this.DebuffProbability = skillData.DebuffProbability;
        this.Duration = skillData.Duration;
        this.SkillGage = skillData.SkillGage;
        this.EffectPath = skillData.EffectPath;

    }
}


