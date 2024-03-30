using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset (AssetPath = "Resources/DataBase")]
public class DataList : ScriptableObject
{
    public List<PlayerData> PlayerData;
    public List<ItemData> ItemData;
    public List<SkillData> SkillData; 
	public List<TestData> TestData;
	public List<EnemyData> EnemyData;
    public List<StageData> StageData;
}
