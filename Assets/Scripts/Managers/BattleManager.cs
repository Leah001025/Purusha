using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using Structs;
using System;
using UnityEditor.PackageManager;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
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

    private Queue<int> attackOrder;

    [HideInInspector]
    public Transform targerTrans;
    [HideInInspector]
    public GameObject target;
    [SerializeField]
    private Transform stageTrans;

    private EnemyDataBase enemyDB;
    private StageDataBase stageDB;

    private StageData stageData;

    private WaitForSeconds gameForSeconds;
    private WaitForSeconds animForSeconds;

    private CharacterActionController characterActionController;

    [HideInInspector]
    public int tempIndex;
    [HideInInspector]
    public bool isAttacking = false;
    private bool playerAttack = true;

    private Vector3[] playerSpawnPos = new Vector3[5];
    private Vector3[] enemySpawnPos = new Vector3[5];
    public float speedModifier = 1;

    [HideInInspector]
    public float turnIndicator1;
    [HideInInspector]
    public float turnIndicator2;
    [HideInInspector]
    public float turnIndicator3;
    [HideInInspector]
    public float turnIndicator4;
    [HideInInspector]
    public float turnIndicator5;

    [HideInInspector]
    public float animTime;

    public Action skill1;
    public Action skill2;
    public Action skill3;
    public Action skill4;

    private float DefaultGauge = 100.0f;
    private float DefaultUpGauge = 1.0f;
    protected override void Awake()
    {
        base.Awake();
        stageDB = DataManager.Instance.StageDB;
        enemyDB = DataManager.Instance.EnemyDB;
        lUnitInfo = new List<UnitInfo>();
        teamData = GameManager.Instance.User.teamData;
        SetSpawnPos();
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
        GameForSeconds(0.03f);
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
                var _Resources = Resources.Load(enemyDB.GetData(unitInfo.unitID).PrefabPath) as GameObject;
                unitInfo.unitType = CharacterType.Enemy;
                unitInfo.unitGauge = 0;
                unitInfo.unitObject = Instantiate(_Resources, stageTrans);
                unitInfo.unitObject.transform.localPosition = enemySpawnPos[j];
                unitInfo.unitObject.transform.GetChild(0).transform.AddComponent<EnemyClickController>();
                unitInfo.unitData = CreateEnemyData(unitInfo.unitObject.tag);

                lUnitInfo.Add(unitInfo);

                targerTrans = unitInfo.unitObject.transform;
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
                unitInfo.unitObject = Instantiate(Resource, stageTrans);
                if (!turnControllers.ContainsKey(unitInfo.unitID))
                {
                    turnControllers.Add(unitInfo.unitID, unitInfo.unitObject.GetComponent<CharacterTurnController>());
                }
                turnControllers[unitInfo.unitID].teamIndex = i;
                unitInfo.unitObject.transform.localPosition = playerSpawnPos[i - 1];
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
        attackOrder = new Queue<int>();
        while (testtun > 0)  // 실험용@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        {
            yield return gameForSeconds;
            if (lUnitInfo != null && !isAttacking)
            {
                foreach (UnitInfo _unitData in lUnitInfo)
                {
                    switch (_unitData.unitType)
                    {
                        case CharacterType.Player:
                            _unitData.unitGauge += _unitData.characterData.status.speed * speedModifier * DefaultUpGauge;
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
                    while (j >= 0 && lUnitInfo[j].unitGauge < Gauge)
                    {
                        UnitInfo temp = lUnitInfo[j];
                        lUnitInfo[j] = lUnitInfo[j + 1];
                        lUnitInfo[j + 1] = temp;
                        j--;
                    }
                }
                for (int i = 0; lUnitInfo.Count > i; i++)
                {
                    if (lUnitInfo[i].unitGauge >= DefaultGauge)
                    {
                        attackOrder.Enqueue(i);
                        tempIndex = i;
                    }
                }
                while (attackOrder.Count > 0)
                {
                    isAttacking = true;
                    yield return StartCoroutine(UnitAttack(attackOrder.Peek()));
                    attackOrder.Dequeue();
                }
            }
            testtun--;  // 실험용@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        }
    }
    private IEnumerator UnitAttack(int index)
    {
        switch (lUnitInfo[index].unitType)
        {
            case CharacterType.Player:
                //int waitTime = 10;
                turnControllers[lUnitInfo[index].unitID].TurnOn();
                var popup = UIManager.Instance.ShowPopup<SkillPopUp>();
                //while (!playerAttack && waitTime != 0)
                //{
                //    yield return new WaitForSeconds(1);
                //    waitTime--;
                //}
                playerAttack = false;
                break;
            case CharacterType.Enemy:
                var isAnimTime = animTime;
                characterActionController = lUnitInfo[index].unitObject.GetComponent<Enemy>().actionController;
                EnemyAttack(characterActionController);
                AnimForSeconds(isAnimTime, animTime);

                speedModifier = 1;
                isAttacking = false;
                yield return animForSeconds;
                //딜계산
                lUnitInfo[index].unitGauge = 0;
                characterActionController.Idle();
                break;
        }
        yield break;
    }
    public WaitForSeconds GameForSeconds(float time)
    {
        gameForSeconds = new WaitForSeconds(time);
        return gameForSeconds;
    }
    public WaitForSeconds AnimForSeconds(float IsAnimTime, float animTime)
    {
        if (IsAnimTime != animTime)
        {
            animForSeconds = new WaitForSeconds(animTime);
        }
        return animForSeconds;
    }
    private void EnemyAttack(CharacterActionController Action)
    {
        if (false) //몬스터 스킬 쿨타임
        {
            characterActionController.Skill2();
        }
        else
        {
            characterActionController.Skill1();
        }
    }
    private void SetSpawnPos()
    {
        playerSpawnPos[0] = new Vector3(0.5f, 0, -5f);
        playerSpawnPos[1] = new Vector3(3.5f, 0, -5f);
        playerSpawnPos[2] = new Vector3(-1, 0, -6.5f);
        playerSpawnPos[3] = new Vector3(2, 0, -6.5f);
        playerSpawnPos[4] = new Vector3(5, 0, -6.5f);

        enemySpawnPos[0] = new Vector3(0.5f, 0, -1.5f);
        enemySpawnPos[1] = new Vector3(3.5f, 0, -1.5f);
        enemySpawnPos[2] = new Vector3(-1, 0, -0.5f);
        enemySpawnPos[3] = new Vector3(2, 0, -0.5f);
        enemySpawnPos[4] = new Vector3(5, 0, -0.5f);
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
        //playerAttack = true;
    }
    public void CallSkill2Event()
    {
        skill2?.Invoke();
        //playerAttack = true;
    }
    public void CallSkill3Event()
    {
        skill3?.Invoke();
        //playerAttack = true;
    }
    public void CallSkill4Event()
    {
        skill4?.Invoke();
        //playerAttack = true;
    }
}
