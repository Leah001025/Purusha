using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class DataManager : SingleTon<DataManager>
{
    private PlayerDataBase _PlayerDB;
    private ItemDataBase _ItemDB;
    private SkillDataBase _skillDB;
    private EnemyDataBase _enemyDB;
    private WaveDataBase _WaveDB;
    private StageDataBase _stageDB;

    public DataList dataList;
    protected override void Awake()
    {
        base.Awake();

    }
    public PlayerDataBase PlayerDB { get { return _PlayerDB ??= new(); } }
    public ItemDataBase ItemDB { get { return _ItemDB ??= new(); } }
    public SkillDataBase SkillDB { get { return _skillDB ??= new(); } }
    public EnemyDataBase EnemyDB { get { return _enemyDB ??= new(); } }
    public StageDataBase StageDB { get { return _stageDB ??= new(); } }
    public WaveDataBase WaveDB { get { return _WaveDB ??= new(); } }
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
public class ItemDataBase : DataBase<ItemData>
{
    public ItemDataBase()
    {
        var resources = DataManager.Instance.dataList.ItemData;
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
public class StageDataBase : DataBase<StageData>
{
    public StageDataBase()
    {
        var resources = DataManager.Instance.dataList.StageData;
        foreach (var data in resources)
        {
            _data.Add(data.ID, data);
        }
    }
}
public class WaveDataBase : DataBase<WaveData>
{
    public WaveDataBase()
    {
        var resources = DataManager.Instance.dataList.WaveData;
        foreach (var data in resources)
        {
            _data.Add(data.ID, data);
        }
    }
}