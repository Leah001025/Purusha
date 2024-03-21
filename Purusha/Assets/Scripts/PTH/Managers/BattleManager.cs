using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using Structs;
using System;
using UnityEditor.PackageManager;
using Unity.Burst.Intrinsics;
public class UnitInfo
{
    public int unitID;
    public CharacterType unitType;
    public float unitGauge;
    public GameObject unitObject;
    public SEnemyData unitData;
    public CharacterData characterData;
}
public class BattleManager : SingleTon<BattleManager>
{
    public List<UnitInfo> lUnitInfo;
    private Dictionary<int, CharacterData> teamData;
    private Dictionary<int, CharacterTurnController> turnControllers = new Dictionary<int, CharacterTurnController>(5);
    private UnitInfo unitInfo;

    private Queue<UnitInfo> attackOrder;
    public Transform targerTrans;

    private EnemyDataBase enemyDB;
    private StageDataBase stageDB;

    private StageData stageData;

    public GameObject target;
    private WaitForSeconds forSeconds;

    public int tempIndex;
    public bool isAttacking=false;
    private Vector3[] playerPos = new Vector3[5];
    private Vector3[] enemyPos = new Vector3[5];
    public float speedModifier = 1;
    public float turnIndicator1;
    public float turnIndicator2;
    public float turnIndicator3;
    public float turnIndicator4;
    public float turnIndicator5;
    public event Action skill1;
    public event Action skill2;
    public event Action skill3;
    public event Action skill4;
    //public bool isAttacking = false;
    private float DefaultGauge = 100.0f;
    private float DefaultUpGauge = 1.0f;
    protected override void Awake()
    {
        base.Awake();
        stageDB = DataManager.Instance.StageDB;
        enemyDB = DataManager.Instance.EnemyDB;
        lUnitInfo = new List<UnitInfo>();
        teamData = GameManager.Instance.User.teamData;
        SetPos();
        BattleStart(110101);
    }
    private void Start()
    {
        UIManager.Instance.ShowPopup<TurnIndicatorUI>();
    }
    public void BattleStart(int StageID)
    {
        AddUnitInfo();
        CreateUnit(StageID);
        ForSeconds(0.03f);
        //target = lUnitInfo.Find(x => x.unitType == CharacterType.Enemy).unitObject;        
        StartCoroutine(AttackOrder());
    }
    private void CreateUnit(int StageID)
    {        
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
                unitInfo.unitObject.transform.position = enemyPos[j];
                unitInfo.unitData = CreateEnemyData(unitInfo.unitObject.tag);

                lUnitInfo.Add(unitInfo);
            }
        }
    }
    private void AddUnitInfo()
    {
        for (int i = 1; i <= teamData.Count; i++)
        {
            if (teamData.ContainsKey(i))
            {
                unitInfo = new UnitInfo();
                unitInfo.unitID = teamData[i].status.iD;
                var Resource = Resources.Load<GameObject>($"KCH/Prefabs/{teamData[i].status.name}");
                unitInfo.unitType = CharacterType.Player;
                unitInfo.unitGauge = 0;
                unitInfo.unitObject = Instantiate(Resource);
                if (!turnControllers.ContainsKey(unitInfo.unitID))
                {
                    turnControllers.Add(unitInfo.unitID, unitInfo.unitObject.GetComponent<CharacterTurnController>());
                }
                turnControllers[unitInfo.unitID].teamIndex = i;
                unitInfo.unitObject.transform.position = playerPos[i - 1];
                unitInfo.characterData = CreateCharacterData(i);
                lUnitInfo.Add(unitInfo);
            }
            else Debug.Log($"TeamData{i} : null");
        }
    }
    private SEnemyData CreateEnemyData(string name)
    {
        int _id = (int)Enum.Parse(typeof(EnemyID), name);
        SEnemyData sEnemyData = new SEnemyData(DataManager.Instance.EnemyDB.GetData(_id));
        return sEnemyData;
    }
    private CharacterData CreateCharacterData(int index)
    {
        CharacterData CharacterData = GameManager.Instance.User.teamData[index];
        return CharacterData;
    }
    private IEnumerator AttackOrder()
    {
        int testtun = 992200;  // 실험용@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        attackOrder = new Queue<UnitInfo>();
        while (testtun > 0)  // 실험용@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        {
            yield return forSeconds;
            if (lUnitInfo != null&&!isAttacking)
            {
                foreach (UnitInfo _unitData in lUnitInfo)
                {
                    switch (_unitData.unitType)
                    {
                        case CharacterType.Player:
                            _unitData.unitGauge += _unitData.characterData.status.speed*speedModifier * DefaultUpGauge;
                            break;
                        case CharacterType.Enemy:
                            _unitData.unitGauge += _unitData.unitData.Speed * speedModifier * DefaultUpGauge;
                            Debug.Log($"{_unitData.unitObject.GetHashCode()},{_unitData.unitGauge}");// 실험용@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                            break;
                    }
                }
                for (int i = 1; lUnitInfo.Count > i; i++)
                {
                    int j = i - 1;
                    float Gauge = lUnitInfo[i].unitGauge;
                    while(j>=0&& lUnitInfo[j].unitGauge < Gauge)
                    {
                        UnitInfo temp = lUnitInfo[j];
                        lUnitInfo[j] = lUnitInfo[j+1];
                        lUnitInfo[j+1] = temp;
                        j--;
                    }
                }
                for (int i = 0; lUnitInfo.Count > i; i++)
                {
                    if (lUnitInfo[i].unitGauge >= DefaultGauge)
                    {
                        attackOrder.Enqueue(lUnitInfo[i]);
                        tempIndex = i;
                        //lUnitInfo[i].unitGauge = 0; 행동 한 후에 게이지를 0으로 바꿔주려고 변경
                    }
                }
                if (attackOrder.Count >= 1)
                {
                    speedModifier = 0;
                    isAttacking = true;
                    StartCoroutine(UnitAttack(attackOrder.Dequeue()));                    
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
                turnControllers[attackOrder.unitID].TurnOn();                
                var popup = UIManager.Instance.ShowPopup<SkillPopUp>();
                break;
            case CharacterType.Enemy:
                attackOrder.unitObject.GetComponent<CharacterActionController>().Skill1();
                lUnitInfo[tempIndex].unitGauge = 0;
                speedModifier = 1;
                isAttacking = false;
                Debug.Log(attackOrder.unitID); // 실험용@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                break;
        }
        yield break;
    }
    public WaitForSeconds ForSeconds(float time)
    {
        forSeconds = new WaitForSeconds(time);
        return forSeconds;
    }
    private void SetPos()
    {
        playerPos[0] = new Vector3(1, 1, -5);
        playerPos[1] = new Vector3(5, 1, -5);
        playerPos[2] = new Vector3(-1, 1, -8);
        playerPos[3] = new Vector3(3, 1, -8);
        playerPos[4] = new Vector3(7, 1, -8);
        enemyPos[0] = new Vector3(1, 2, 5);
        enemyPos[1] = new Vector3(5, 2, 5);
        enemyPos[2] = new Vector3(-1, 2, 8);
        enemyPos[3] = new Vector3(3, 2, 8);
        enemyPos[4] = new Vector3(7, 2, 8);
    }

    public void SetTurnIndicator(int teamIndex, float curtime)
    {
        switch (teamIndex)
        {
            case 1:
                turnIndicator1 = curtime * 6;
                break;
            case 2:
                turnIndicator2 = curtime * 6;
                break;
            case 3:
                turnIndicator3 = curtime * 6;
                break;
            case 4:
                turnIndicator4 = curtime * 6;
                break;
            case 5:
                turnIndicator5 = curtime * 6;
                break;
        }
    }
    public float GetTurnIndicator(string teamIndex)
    {
        switch (teamIndex)
        {
            case "1":
                return turnIndicator1;
            case "2":
                return turnIndicator2;
            case "3":
                return turnIndicator3;
            case "4":
                return turnIndicator4;
            case "5":
                return turnIndicator5;
            default: return 0;
        }
    }
    public void CallSkill1Event()
    {
        skill1?.Invoke();
    }
    public void CallSkill2Event()
    {
        skill2?.Invoke();
    }
    public void CallSkill3Event()
    {
        skill3?.Invoke();
    }
    public void CallSkill4Event()
    {
        skill4?.Invoke();
    }
}
