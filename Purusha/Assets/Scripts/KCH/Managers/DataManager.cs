using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class DataManager : SingleTon<DataManager>
{
    private SkillDataBase _skillDB;
    private PlayerDataBase _playerDB;
    public DataList dataList;
    protected override void Awake()
    {
        base.Awake();

    }
    public SkillDataBase SkillDB { get { return _skillDB ??= new(); } }
    public PlayerDataBase PlayerDB { get { return _playerDB ??= new(); } }
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