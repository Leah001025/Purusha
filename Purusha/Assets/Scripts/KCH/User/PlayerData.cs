using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData : DataBase<PlayerData>
{
    public int ID;
    public string Name;
    public int Level;
    public float Atk;
    public float Health;
    public float Def;
    public float CriticalChance;
    public float CriticalDamage;
    public float Avoid;
    public float Speed;
    public float BrakeGauge;
    public string PrefabPath;
    public string SpritePath;
}
