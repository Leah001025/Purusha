using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset (AssetPath = "DataBase")]
public class DataList : ScriptableObject
{
    public List<PlayerData> PlayerData;
    public List<ItemData> ItemData;
    public List<SkillData> SkillData; 
    public List<BuffData> BuffData;
	public List<EnemyData> EnemyData;
    public List<WaveData> WaveData;
    public List<StageData> StageData;
    public List<LevelData> LevelData;
    public List<ChapterData> ChapterData;

}
