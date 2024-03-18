using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class DataManager : SingleTon<DataManager>
{
    private SkillDataBase _skillDB;
    public DataList dataList;
    protected override void Awake()
    {
        base.Awake();

    }
    public SkillDataBase SkillDB { get { return _skillDB ??= new(); } }
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