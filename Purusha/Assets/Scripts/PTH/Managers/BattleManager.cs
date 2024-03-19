using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using Structs;
using System;
public class UnitInfo
{
    public int unitID;
    public int unitType;
    public float unitGauge;
    public GameObject unitObject;
}
public class StageInfo
{
    public int Enemy;
    public int Player;
}
public class BattleManager : SingleTon<BattleManager>
{
    private List<UnitInfo> battleInfo;
    private Queue<UnitInfo> attackOrder;

    private SEnemyData enemyData;

    private PlayerDataBase playerDB;
    private PlayerData playerData;

    public event Action SkillSlot1;
    public event Action SkillSlot2;
    public event Action SkillSlot3;
    public event Action SkillSlot4;

    public GameObject target;

    private float DefaultGauge = 100.0f;
    private float DefaultUpGauge = 5.0f;
    protected override void Awake()
    {
        base.Awake();
        playerDB = DataManager.Instance.PlayerDB;
    }
    private void Start()
    {
        CreateUnit();
        StartCoroutine(AttackOrder());
    }
    private void CreateUnit()
    {
        battleInfo = new List<UnitInfo>();
        battleInfo.Add(new UnitInfo());
        var _Resources = Resources.Load($"PTH/EnemyPrefabs/{battleInfo}");

        attackOrder = new Queue<UnitInfo>();

        var unitsObj = new GameObject("Units");
    }
    private IEnumerator AttackOrder()
    {
        yield return null;
        while (false)
        {
            if (battleInfo != null)
            {
                foreach (UnitInfo _unitData in battleInfo)
                {
                    switch ((CharacterType)_unitData.unitType)
                    {
                        case CharacterType.Player:
                            playerData = playerDB.GetData(_unitData.unitID);
                            _unitData.unitGauge += (playerData.Speed * DefaultUpGauge) * Time.deltaTime;
                            break;
                        case CharacterType.Enemy:
                            enemyData = (SEnemyData)_unitData.unitObject.GetComponent<Enemy>().EnemyData;
                            _unitData.unitGauge += (enemyData.Speed * DefaultUpGauge) * Time.deltaTime;
                            Debug.Log(_unitData.unitID);// 실험용@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                            Debug.Log(_unitData.unitGauge);// 실험용@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                            break;
                    }
                }
                for (int i = 0; battleInfo.Count - 1 > i; i++)
                {
                    for (int j = i + 1; battleInfo.Count > j; j++)
                    {
                        if (battleInfo[i].unitGauge < battleInfo[j].unitGauge)
                        {
                            float temp = battleInfo[i].unitGauge;
                            battleInfo[i].unitGauge = battleInfo[j].unitGauge;
                            battleInfo[j].unitGauge = temp;
                        }
                    }
                }
                for (int i = 0; battleInfo.Count > i; i++)
                {
                    if (battleInfo[i].unitGauge >= DefaultGauge)
                    {
                        attackOrder.Enqueue(battleInfo[i]);
                        battleInfo[i].unitGauge = 0;
                    }
                }
                if (attackOrder.Count >= 1)
                {
                    yield return StartCoroutine(UnitAttack(attackOrder.Dequeue()));
                }
            }
        }
    }
    private IEnumerator UnitAttack(UnitInfo attackOrder)
    {
        yield return null;
        switch ((CharacterType)attackOrder.unitType)
        {
            case CharacterType.Player:
                break;
            case CharacterType.Enemy:
                attackOrder.unitObject.GetComponent<CharacterActionController>();
                Debug.Log(attackOrder.unitID); // 실험용@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                break;
        }
    }
    public void OnDamage(float damage,int skillRange, GameObject target)
    {

    }
    public void StageEnemy()
    {

    }
}
