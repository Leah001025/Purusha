using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class PlayerData : DataBase<PlayerData>
{
    public int ID;
    public string Name;
    public float Health;
    public int Level;
    public int Exp;
    public int EquipLevel;
    public float Atk;
    public float Def;
    public float CriticalChance;
    public float CriticalDamage;
    public float Avoid;
    public float Speed;
}
