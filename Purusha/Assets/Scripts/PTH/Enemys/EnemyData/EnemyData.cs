using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class EnemyData
{
    public byte ID;
    public string Name;
    public short Level;
    public int Health;
    public int Ark;
    public int Def;
    public byte CriticalChance;
    public short criticalChance;
    public byte Avoid;
    public byte Speed;
    public short BrakeGauge;
    public string PrefabPath;
    public string SpritePath;
}
