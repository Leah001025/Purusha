using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using Structs;
using System;
using UnityEditor.PackageManager;
public class UnitInfo
{
    public int unitID;
    public CharacterType unitType;
    public float unitGauge;
    public GameObject unitObject;
    public SEnemyData unitData;
}
public class BattleManager : SingleTon<BattleManager>
{
    private List<UnitInfo> lUnitInfo;
    private UnitInfo unitInfo;

    private Queue<UnitInfo> attackOrder;

    private EnemyDataBase enemyDB;
    private StageDataBase stageDB;

    private StageData stageData;

    public GameObject target;

    private float DefaultGauge = 100.0f;
    private float DefaultUpGauge = 5.0f;
    protected override void Awake()
    {
        base.Awake();
        stageDB = DataManager.Instance.StageDB;
        enemyDB = DataManager.Instance.EnemyDB;
    }
    public void BattleStart(int StageID)
    {
        CreateUnit(StageID);
        StartCoroutine(AttackOrder());
    }
    private void CreateUnit(int StageID)
    {
        lUnitInfo = new List<UnitInfo>();
        stageData = stageDB.GetData(StageID);
        for (int i = 0; stageData.Enemys.Count > i; i++)
        {
            for (int j = 0; stageData.Enemys[i]._enemyCount > j; j++)
            {
                unitInfo = new UnitInfo();
                unitInfo.unitID = stageData.Enemys[i]._enemyID;
                var _Resources = Resources.Load($"PTH/EnemyPrefabs/{enemyDB.GetData(unitInfo.unitID).Name}") as GameObject;
                unitInfo.unitType = CharacterType.Enemy;
                unitInfo.unitGauge = 0;
                unitInfo.unitObject = Instantiate(_Resources);
                unitInfo.unitData = CreateEnemyData(unitInfo.unitObject.tag);

                lUnitInfo.Add(unitInfo);
            }
        }
    }
    private SEnemyData CreateEnemyData(string name)
    {
        int _id = (int)Enum.Parse(typeof(EnemyID), name);
        SEnemyData sEnemyData = new SEnemyData(DataManager.Instance.EnemyDB.GetData(_id));
        return sEnemyData;
    }
    private IEnumerator AttackOrder()
    {
        int testtun = 200;  // 실험용@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        attackOrder = new Queue<UnitInfo>();
        while (testtun > 0)  // 실험용@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        {
            yield return new WaitForSeconds(1f);
            if (lUnitInfo != null)
            {
                foreach (UnitInfo _unitData in lUnitInfo)
                {
                    switch (_unitData.unitType)
                    {
                        case CharacterType.Player:
                            break;
                        case CharacterType.Enemy:
                            _unitData.unitGauge += _unitData.unitData.Speed * DefaultUpGauge;
                            Debug.Log($"{_unitData.unitObject.GetHashCode()},{_unitData.unitGauge}");// 실험용@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                            break;
                    }
                }
                for (int i = 1; lUnitInfo.Count > i; i++)
                {
                    float Gauge = lUnitInfo[i].unitGauge;
                    unitInfo = lUnitInfo[i];
                    for (int j = i - 1; j > 0 && lUnitInfo[j].unitGauge < Gauge; j--)
                    {
                        lUnitInfo[j + 1] = lUnitInfo[j];
                        lUnitInfo[j + 1] = unitInfo;
                    }
                }
                for (int i = 0; lUnitInfo.Count > i; i++)
                {
                    if (lUnitInfo[i].unitGauge >= DefaultGauge)
                    {
                        attackOrder.Enqueue(lUnitInfo[i]);
                        lUnitInfo[i].unitGauge = 0;
                    }
                }
                if (attackOrder.Count >= 1)
                {
                    yield return StartCoroutine(UnitAttack(attackOrder.Dequeue()));
                }
                testtun--;  // 실험용@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            }
        }
    }
    private IEnumerator UnitAttack(UnitInfo attackOrder)
    {
        yield return null;
        switch (attackOrder.unitType)
        {
            case CharacterType.Player:
                break;
            case CharacterType.Enemy:
                attackOrder.unitObject.GetComponent<CharacterActionController>().Skill1();
                Debug.Log(attackOrder.unitID); // 실험용@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                break;
        }
    }
}
