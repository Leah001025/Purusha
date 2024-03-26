using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class CharacterData
{
    public CharacterStatus status;
    public Dictionary<int, CharacterSkill> skillData = new Dictionary<int, CharacterSkill>(4);

    public CharacterData(int id)
    {
        status = new CharacterStatus(id);
        for (int i = 1; i <= 4; i++)
        {
            string skillIndex = id + $"{i:D2}".ToString() + "1";
            skillData.Add(i, new CharacterSkill(int.Parse(skillIndex)));
        }
    }

}
public class CharacterStatus
{
    public int iD;
    public string name;
    public float health;
    public float maxhealth;
    public int level;
    public int exp;
    public int equipLevel;
    public float atk;
    public float def;
    public float criticalChance;
    public float criticalDamage;
    public float avoid;
    public float speed;
    public string prefabPath;
    public string spritePath;

    public CharacterStatus(int id)
    {
        PlayerData data = DataManager.Instance.PlayerDB.GetData(id);
        iD = data.ID;
        name = data.Name;
        health = data.Health;
        maxhealth = data.MaxHealth;
        level = data.Level;
        exp = data.Exp;
        equipLevel = data.EquipLevel;
        atk = data.Atk;
        def = data.Def;
        criticalChance = data.CriticalChance;
        criticalDamage = data.CriticalDamage;
        avoid = data.Avoid;
        speed = data.Speed;
        prefabPath = data.PrefabPath;
        spritePath = data.SpritePath;
    }
}
public class CharacterSkill
{
    public int iD;
    public string name;
    public string description;
    public int type;
    public float atkCoefficient;
    public float defCoefficient;
    public float healthCoefficient;
    public int coolTime;
    public float debuffProbability;
    public int duration;
    public int skillGage;
    public string effectPath;
    public CharacterSkill(int id)
    {
        SkillData data = DataManager.Instance.SkillDB.GetData(id);
        iD = data.ID;
        name = data.Name;
        description = data.Description;
        type = data.Type;
        atkCoefficient = data.AtkCoefficient;
        defCoefficient = data.DefCoefficient;
        healthCoefficient = data.HealthCoefficient;
        coolTime = data.CoolTime;
        debuffProbability = data.DebuffProbability;
        duration = data.Duration;
        skillGage = data.SkillGage;
        effectPath = data.EffectPath;
    }
}
