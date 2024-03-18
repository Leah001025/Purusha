using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class DataManager : SingleTon<DataManager>
{
    private PlayerDataBase _PlayerDB;
    private SkillDataBase _skillDB;
    private EnemyDataBase _enemyDB;
    public DataList dataList;
    protected override void Awake()
    {
        base.Awake();

    }
    public PlayerDataBase PlayerDB { get { return _PlayerDB ??= new(); } }
    public SkillDataBase SkillDB { get { return _skillDB ??= new(); } }
    public EnemyDataBase EnemyDB { get { return _enemyDB ??= new(); } }
}
public class PlayerDataBase : DataBase<PlayerData>
{
    public PlayerDataBase()
    {
        var resources = DataManager.Instance.dataList.PlayerData;
        foreach (var data in resources)
        {
            _data.Add(data.ID, data);
        }
    }
}

public class SkillDataBase : DataBase<SkillData>
{
    public SkillDataBase()
    {
        var resources = DataManager.Instance.dataList.SkillData;
        foreach (var data in resources)
        {
            _data.Add(data.ID, data);
        }        
    }
}
public class EnemyDataBase : DataBase<EnemyData>
{
    public EnemyDataBase()
    {
        var resources = DataManager.Instance.dataList.EnemyData;
        foreach (var data in resources)
        {
            _data.Add(data.ID, data);
        }
    }
}