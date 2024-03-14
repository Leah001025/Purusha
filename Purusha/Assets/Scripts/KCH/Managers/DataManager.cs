using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : ScriptableObject
{
    //public Dictionary<int, SkillData> skillDB;
    //public Dictionary<int, TestData> testDB;
    public SkillDataList skillDataList = new SkillDataList();
    private void Awake()
    {
        var resources = Resources.Load<DataList>("KCH/DataBase/DataList");
        var skillData = Instantiate(resources).SkillData;
        var testData = Instantiate(resources).TestData;
        //skillDB = SetData<SkillData>(skillData);
        //testDB = SetData<TestData>(testData);

        skillDataList.SetData(skillData);
        Debug.Log(skillDataList.datas[101011]);
    }

    //private Dictionary<int,T> SetData<T>(List<T> data)
    //{
    //    Dictionary<int, T> datas = new Dictionary<int, T>(data.Count);
    //    for (int i = 0; i < data.Count; i++)
    //    {
    //        datas.Add(i+1, data[i]);
    //    }
    //    return datas;
    //}
}
