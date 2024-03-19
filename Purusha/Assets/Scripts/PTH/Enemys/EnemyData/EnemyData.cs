using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class EnemyData : DataBase<EnemyData>
{
    public int ID;
    public string Name;
    public int Level;
    public float Health;
    public float Atk;
    public float Def;
    public float CriticalChance;
    public float CriticalDamage;
    public float Avoid;
    public float Speed;
    public float BreakGauge;
    public string PrefabPath;
    public string SpritePath;
}
