using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class EnemyData : DataBase<EnemyData>
{
    [SerializeField] private int _id;
    [SerializeField] private string _name;
    [SerializeField] private int _level;
    [SerializeField] private float _health;
    [SerializeField] private float _atk;
    [SerializeField] private float _def;
    [SerializeField] private float _criticalChance;
    [SerializeField] private float _criticalDamage;
    [SerializeField] private float _avoid;
    [SerializeField] private float _speed;
    [SerializeField] private float _breakGauge;
    [SerializeField] private string _prefabPath;
    [SerializeField] private string _spritePath;

    public int ID { get { return _id; } set { _id = value; } }
    public string Name { get { return _name; } set { _name = value; } }
    public int Level { get { return _level; } set { _level = value; } }
    public float Health { get { return _health; } set { _health = value; } }
    public float Atk { get { return _atk; } set { _atk = value; } }
    public float Def { get { return _def; } set { _def = value; } }
    public float CriticalChance { get { return _criticalChance; } set { _criticalChance = value; } }
    public float CriticalDamage { get { return _criticalDamage; } set { _criticalDamage = value; } }
    public float Avoid { get { return _avoid; } set { _avoid = value; } }
    public float Speed { get { return _speed; } set { _speed = value; } }
    public float BreakGauge { get { return _breakGauge; } set { _breakGauge = value; } }
    public string PrefabPath { get { return _prefabPath; } set { _prefabPath = value; } }
    public string SpritePath { get { return _spritePath; } set { _spritePath = value; } }
}
