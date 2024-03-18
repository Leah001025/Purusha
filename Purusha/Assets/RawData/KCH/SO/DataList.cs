using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset (AssetPath = "Resources/KCH/DataBase")]
public class DataList : ScriptableObject
{
	public List<SkillData> SkillData;
	public List<PlayerData> PlayerData;
	public List<TestData> TestData;


}
