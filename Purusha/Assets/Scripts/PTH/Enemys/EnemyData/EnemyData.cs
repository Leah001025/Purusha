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

    public int ID => _id;
    public string Name => _name;
    public int Level => _level;
    public float Health => _health;
    public float Atk => _atk;
    public float Def => _def;
    public float CriticalChance => _criticalChance;  
    public float CriticalDamage => _criticalDamage;
    public float Avoid => _avoid; 
    public float Speed => _speed;
    public float BreakGauge => _breakGauge;
    public string PrefabPath => _prefabPath;
    public string SpritePath => _spritePath;
}
