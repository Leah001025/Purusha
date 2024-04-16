using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using Structs;
using System;
using UnityEditor.PackageManager;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using UnityEngine.Timeline;
using System.Linq;
public class BattleInfo
{
    public Dictionary<int, CharacterBattleInfo> characterInfo;
    public BattleInfo()
    {
        characterInfo = new Dictionary<int, CharacterBattleInfo>();
    }
    public float battleTime;
    public int battleTurn;
}
public class CharacterBattleInfo
{
    public int characterTurn;
    public float attackDamages;
    public float receivedDamages;
    public float support;
}
public class UnitInfo
{
    public int unitID;
    public CharacterType unitType;
    public float unitGauge;
    public GameObject unitObject;
    public CharacterActionController actionController;
    public SEnemyData unitData;
    public CharacterData characterData;
}
public class BattleManager : MonoBehaviour
{
    private static BattleManager instance = null;
    public BattleManager()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
    }
    public static BattleManager Instance { get { if (instance == null) return null; return instance; } }

    public Dictionary<int, UnitInfo> lUnitInfo;
    public Dictionary<int, CharacterTurnController> turnControllers = new Dictionary<int, CharacterTurnController>(5);
    public Dictionary<int, EnemySkillController> enemySkillControllers = new Dictionary<int, EnemySkillController>();
    private Dictionary<int, CharacterData> teamData;
    private Queue<int> attackOrder;
    private UnitInfo unitInfo;

    private EnemyDataBase enemyDB;
    private WaveDataBase waveDB;
    private WaveData WaveData;

    private WaitForSeconds gameForSeconds;
    public WaitForSeconds animForSeconds;

    [HideInInspector]
    public BattleInfo battleInfo;
    [HideInInspector]
    public GameEnd gameState;

    [Header("Target Point")]
    public GameObject target;
    public Transform stageTrans;
    public Transform playerInfoTrans;
    private Vector3[] playerSpawnPos = new Vector3[5];
    private Vector3[] enemySpawnPos = new Vector3[5];

    [Header("Skill")]
    public int onTurnIndex;
    public bool isAttacking = false;
    public int skill3CoolTime;
    public int skill4Gauge;

    private float defaultGauge = 100.0f;
    private float defaultUpGauge = 1.0f;
    private int playerCreateCount;
    private int playerUnitCount;
    private int enemyCreateCount;
    private int enemyUnitCount;
    private int healIndex;


    public float speedModifier = 1;

    [Header("TurnIndicator")]
    public float turnIndicator1;
    public float turnIndicator2;
    public float turnIndicator3;
    public float turnIndicator4;
    public float turnIndicator5;

    [Header("Animation Time")]
    public float beforeAnimTime = 0;
    public float newAnimTime;

    [Header("Camera")]
    public Vector3 defalutCameraPos;
    public Vector3 cameraPos;

    public Action skill1;
    public Action skill2;
    public Action skill3;
    public Action skill4;

    public event Action<float, string> OnAddDamage;

    private void Awake()
    {
        waveDB = DataManager.Instance.WaveDB;
        enemyDB = DataManager.Instance.EnemyDB;
        lUnitInfo = new Dictionary<int, UnitInfo>();
        battleInfo = new BattleInfo();
        teamData = GameManager.Instance.User.teamData;
        SetSpawnPos();
        BattleStart(GameManager.Instance.waveID);
    }
    private void Start()
    {
        UIManager.Instance.ShowPopup<TurnIndicatorUI>();
        cameraPos = new Vector3(5f, 2f, -6.2f);
    }
    private void Update()
    {
        if (gameState == GameEnd.Paly)
        {
            battleInfo.battleTime += Time.deltaTime;
        }
    }
    public void BattleStart(int StageID)
    {
        gameState = GameEnd.Paly;
        AddUnitInfo(StageID);
        foreach (EnemySkillController skillController in enemySkillControllers.Values)
        {
            skillController.Init();
        }
        GameForSeconds(0.03f);
        StartCoroutine(AttackOrder());
        defalutCameraPos = Camera.main.transform.localPosition;
    }
    private void AddUnitInfo(int StageID)
    {
        for (int i = 1; i <= teamData.Count; i++)
        {
            if (teamData.ContainsKey(i))
            {
                unitInfo = new UnitInfo();
                unitInfo.unitID = teamData[i].status.iD;
                var Resource = Resources.Load<GameObject>(teamData[i].status.prefabPath);
                unitInfo.unitType = CharacterType.Player;
                unitInfo.unitObject = Instantiate(Resource, stageTrans);
                unitInfo.unitObject.AddComponent<Player>();
                unitInfo.actionController = unitInfo.unitObject.GetComponent<Player>().ActionController;
                if (!turnControllers.ContainsKey(i))
                {
                    turnControllers.Add(i, unitInfo.unitObject.GetComponent<CharacterTurnController>());
                }
                turnControllers[i].teamIndex = i;
                unitInfo.unitObject.transform.localPosition = playerSpawnPos[i - 1];
                unitInfo.characterData = CreateCharacterData(i);

                lUnitInfo.Add(i, unitInfo);
                UIManager.Instance.BattlePlayerPopup(i, playerInfoTrans);
                unitInfo.unitObject.name = lUnitInfo.Count.ToString();

                battleInfo.characterInfo.Add(i, CreateCharacterBattleInfo());
                playerCreateCount++;
            }
            else Debug.Log($"TeamData{i} : null");
        }
        playerUnitCount = playerCreateCount;
        WaveData = waveDB.GetData(StageID);
        for (int i = 0; WaveData.Enemys.Count > i; i++)
        {
            for (int j = 0; WaveData.Enemys[i]._enemyCount > j; j++)
            {
                unitInfo = new UnitInfo();
                unitInfo.unitID = WaveData.Enemys[i]._enemyID;
                var _Resources = Resources.Load<GameObject>(enemyDB.GetData(unitInfo.unitID).PrefabPath);
                unitInfo.unitType = CharacterType.Enemy;
                unitInfo.unitObject = Instantiate(_Resources, stageTrans);
                unitInfo.unitObject.AddComponent<EnemySkillController>();

                unitInfo.actionController = unitInfo.unitObject.GetComponent<Enemy>().ActionController;
                unitInfo.unitObject.transform.localPosition = enemySpawnPos[j];
                unitInfo.unitData = CreateEnemyData(unitInfo.unitObject.tag);

                lUnitInfo.Add(lUnitInfo.Count + 1, unitInfo);
                unitInfo.unitObject.name = lUnitInfo.Count.ToString();
                UIManager.Instance.BattleShowPopup(unitInfo.unitObject);
                enemySkillControllers.Add(lUnitInfo.Count, unitInfo.unitObject.GetComponent<EnemySkillController>());
                enemyCreateCount++;
            }
        }
        enemyUnitCount = enemyCreateCount;
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
    private CharacterBattleInfo CreateCharacterBattleInfo()
    {
        CharacterBattleInfo characterBattleInfo = new CharacterBattleInfo();
        return characterBattleInfo;
    }
    private IEnumerator AttackOrder()
    {
        attackOrder = new Queue<int>();
        yield return new WaitForSeconds(1f);
        while (gameState == GameEnd.Paly)
        {
            yield return gameForSeconds;
            if (lUnitInfo != null && !isAttacking)
            {
                GaugeUp();
                GaugeChack();
                while (attackOrder.Count > 0 && !isAttacking)//�� ���ð���
                {
                    isAttacking = true;
                    onTurnIndex = attackOrder.Peek();
                    yield return StartCoroutine(UnitAttack(attackOrder.Peek()));
                    attackOrder.Dequeue();
                    battleInfo.battleTurn++;
                }
            }
            WaveEndChack();
        }
        GameResult(gameState);
    }
    private void GaugeUp()
    {
        foreach (UnitInfo _unitData in lUnitInfo.Values)
        {
            switch (_unitData.unitType)
            {
                case CharacterType.Player:
                    _unitData.unitGauge += _unitData.characterData.status.speed * speedModifier * defaultUpGauge;
                    break;
                case CharacterType.Enemy:
                    _unitData.unitGauge += _unitData.unitData.Speed * speedModifier * defaultUpGauge;
                    break;
            }
        }
    }
    private void GaugeChack()
    {
        foreach (KeyValuePair<int, UnitInfo> _unitData in lUnitInfo) //Gauge Chack
        {
            if (_unitData.Value.unitGauge >= defaultGauge && !attackOrder.Contains(_unitData.Key)) //���� Ű �ߺ���� ����
            {
                attackOrder.Enqueue(_unitData.Key);
            }
        }
    }
    private IEnumerator UnitAttack(int index)
    {
        switch (lUnitInfo[index].unitType)
        {
            case CharacterType.Player:
                turnControllers[index].TurnOn();
                var popup = UIManager.Instance.ShowPopup<SkillPopUp>();
                break;
            case CharacterType.Enemy:
                enemySkillControllers[index].TurnOn();
                //EnemyAttack(lUnitInfo[index]);

                //AnimForSeconds(newAnimTime, beforeAnimTime);
                //speedModifier = 1;
                //isAttacking = false;
                //yield return animForSeconds;
                //lUnitInfo[index].unitGauge = 0;
                break;
        }
        yield break;
    }
    //private void EnemyAttack(UnitInfo unitInfo)
    //{
    //    TargetChange(CharacterType.Player);
    //    if (false) //���� ��ų ��Ÿ��
    //    {
    //        unitInfo.actionController.Skill2();
    //    }
    //    else
    //    {
    //        unitInfo.actionController.Skill1();
    //    }
    //    TargetChange(CharacterType.Enemy);
    //}
    public void OnSkillPlayer(CharacterData characterData, int skillNum)
    {
        float _damage = AddDamage(characterData, skillNum);
        int buffID = characterData.skillData[skillNum].buffID;
        int buffID2 = characterData.skillData[skillNum].buffID2;
        int targetIndex = int.Parse(target.name);
        int unitCount = lUnitInfo.Count;
        switch (characterData.skillData[skillNum].type)
        {
            case 0:
                lUnitInfo[targetIndex].unitData.Health -= _damage;
                battleInfo.characterInfo[onTurnIndex].attackDamages += _damage;
                lUnitInfo[targetIndex].actionController.BattleHit();
                enemySkillControllers[targetIndex].SetBuffandDebuff(buffID);
                AddDamageUI(_damage, target.name);
                DieCheck(lUnitInfo[targetIndex]);
                break;
            case 1:
                int count = 1;
                foreach (EnemySkillController skillController in enemySkillControllers.Values)
                {
                    skillController.SetBuffandDebuff(buffID);
                }
                for (int i = 1; i <= enemyCreateCount+playerCreateCount; i++)
                {
                    targetIndex = int.Parse(target.name);
                    if (lUnitInfo.ContainsKey(count)&&lUnitInfo[count].unitType == CharacterType.Enemy)
                    {
                        lUnitInfo[count].unitData.Health -= _damage;
                        battleInfo.characterInfo[onTurnIndex].attackDamages += _damage;
                        AddDamageUI(_damage, lUnitInfo[count].unitObject.name);
                        //if (DieCheck(lUnitInfo[count])) i =unitCount-1-(enemyCreateCount - enemyUnitCount);
                    }
                    if ((enemyUnitCount) == 0) return;
                    count++;
                }
                //foreach (UnitInfo _unitData in lUnitInfo.Values)
                //{
                //    if (_unitData.unitType == CharacterType.Enemy)
                //    {
                //        _unitData.unitData.Health -= _damage;
                //        battleInfo.characterInfo[onTurnIndex].attackDamages += _damage;
                //        AddDamageUI(_damage, _unitData.unitObject.name);
                //        DieCheck(lUnitInfo[targetIndex]);
                //    }

                //}
                break;
            case 2:
                turnControllers[onTurnIndex].SetBuffandDebuff(buffID);
                break;
            case 3:
                float temp = 2;
                for (int i = 1; i <= lUnitInfo.Count; i++)
                {
                    if (lUnitInfo.ContainsKey(i) && lUnitInfo[i].unitType == CharacterType.Player)
                    {
                        if (lUnitInfo[i].characterData.status.health / lUnitInfo[i].characterData.status.maxhealth < temp)
                        {
                            temp = lUnitInfo[i].characterData.status.health / lUnitInfo[i].characterData.status.maxhealth;
                            healIndex = i;
                        }
                    }
                }
                PlayerHeal(lUnitInfo[healIndex], skillNum);
                AddDamageUI(_damage, healIndex.ToString());
                break;
            case 4:
                if (lUnitInfo[onTurnIndex].unitType == CharacterType.Player)
                {
                    foreach (UnitInfo _unitData in lUnitInfo.Values)
                    {
                        if (_unitData.unitType == CharacterType.Player)
                        {
                            PlayerHeal(_unitData, skillNum);
                            AddDamageUI(_damage, _unitData.unitObject.name);
                        }

                    }
                    foreach (CharacterTurnController turnController in turnControllers.Values)
                    {
                        turnController.SetBuffandDebuff(buffID);
                        if (buffID2 != 0) turnController.SetBuffandDebuff(buffID2);
                    }
                }

                break;
        }
    }
    private void PlayerHeal(UnitInfo unitInfo, int skillNum)
    {
        var status = unitInfo.characterData.status;
        float healthCoefficient = lUnitInfo[onTurnIndex].characterData.skillData[skillNum].healthCoefficient;
        status.health = status.health + (status.maxhealth * healthCoefficient) >= status.maxhealth ?
            status.maxhealth : status.health + (status.maxhealth * healthCoefficient);
    }
    public void OnSkillEnemy(EnemyCharacterData characterData, int skillNum)
    {
        float _damage = AddDamagePlayer(characterData, skillNum);
        int buffID = characterData.enemySkillData[skillNum].buffID;
        int buffID2 = characterData.enemySkillData[skillNum].buffID2;
        int targetIndex = int.Parse(target.name);

        switch (characterData.enemySkillData[skillNum].type)
        {
            case 0:
                lUnitInfo[targetIndex].characterData.status.health -= _damage;
                turnControllers[targetIndex].SetBuffandDebuff(buffID);
                //lUnitInfo[targetIndex].actionController.Hit();
                //turnControllers[targetIndex].SetBuffandDebuff(buffID);
                AddDamageUI(_damage, target.name);
                DieCheck(lUnitInfo[targetIndex]);
                break;
            case 1:
                foreach (CharacterTurnController turnController in turnControllers.Values)
                {
                    turnController.SetBuffandDebuff(buffID);
                }
                foreach (UnitInfo _unitData in lUnitInfo.Values)
                {
                    if (_unitData.unitType == CharacterType.Enemy)
                    {
                        _unitData.unitData.Health -= _damage;
                        AddDamageUI(_damage, _unitData.unitObject.name);
                        DieCheck(lUnitInfo[targetIndex]);
                    }

                }
                break;
            case 2:
                enemySkillControllers[onTurnIndex].SetBuffandDebuff(buffID);
                break;
                //case 3:
                //    break;
                //case 4:
                //    if (lUnitInfo[onTurnIndex].unitType == CharacterType.Player)
                //    {
                //        foreach (UnitInfo _unitData in lUnitInfo.Values)
                //        {
                //            if (_unitData.unitType == CharacterType.Player)
                //            {
                //                var status = _unitData.characterData.status;
                //                float healthCoefficient = lUnitInfo[onTurnIndex].characterData.skillData[skillNum].healthCoefficient;
                //                status.health = status.health + (status.maxhealth * healthCoefficient) >= status.maxhealth ?
                //                    status.maxhealth : status.health + (status.maxhealth * healthCoefficient);
                //                AddDamageUI(_damage, _unitData.unitObject.name);
                //            }

                //        }
                //        foreach (CharacterTurnController turnController in turnControllers.Values)
                //        {
                //            turnController.SetBuffandDebuff(buffID);
                //            if (buffID2 != 0) turnController.SetBuffandDebuff(buffID2);
                //        }
                //    }

                //    break;
        }
    }
    private float AddDamage(CharacterData characterData, int skillNum)
    {
        float damage = 0;
        float crirocalChance = UnityEngine.Random.Range(0, 1.0f);
        if (crirocalChance <= characterData.status.criticalChance)
        {
            damage = ((characterData.status.atk * characterData.skillData[skillNum].atkCoefficient) +
                (characterData.status.def * characterData.skillData[skillNum].defCoefficient) +
                (characterData.status.health * characterData.skillData[skillNum].healthCoefficient))
                * (100 / (100.0f + lUnitInfo[int.Parse(target.name)].unitData.Def)) * characterData.status.criticalDamage;
        }
        else
        {
            damage = ((characterData.status.atk * characterData.skillData[skillNum].atkCoefficient) +
                (characterData.status.def * characterData.skillData[skillNum].defCoefficient) +
                (characterData.status.health * characterData.skillData[skillNum].healthCoefficient))
                * (100 / (100.0f + lUnitInfo[int.Parse(target.name)].unitData.Def));
        }
        return damage;
    }
    private float AddDamagePlayer(EnemyCharacterData characterData, int skillNum)
    {
        float damage;
        CharacterTurnController skillController = turnControllers[int.Parse(target.name)];
        float shield = skillController.shieldQuantity;
        float totalDamage = 0;
        float crirocalChance = UnityEngine.Random.Range(0, 1.0f);
        if (crirocalChance <= characterData.enemyData.CriticalChance)
        {
            damage = (characterData.enemyData.Atk * characterData.enemySkillData[skillNum].atkCoefficient)
                * (100 / (100.0f + lUnitInfo[int.Parse(target.name)].unitData.Def)) * characterData.enemyData.CriticalDamage * 10;
        }
        else
        {
            damage = (characterData.enemyData.Atk * characterData.enemySkillData[skillNum].atkCoefficient)
                * (100 / (100.0f + lUnitInfo[int.Parse(target.name)].unitData.Def)) * 10;
        }
        if (shield > damage)
        {
            totalDamage = 0;
            skillController.shieldQuantity -= damage;
            skillController.CallChangeShieldGauge();
        }
        else
        {
            totalDamage = damage - shield;
            skillController.RemoveShield();
            skillController.CallChangeShieldGauge();
        }
        return totalDamage;
    }
    private void GameResult(GameEnd gameState)
    {
        string waveID = GameManager.Instance.waveID.ToString();
        Debug.Log(waveID.Substring(waveID.Length - 1, 1));
        if (gameState == GameEnd.success)
        {
            switch (waveID.Substring(waveID.Length - 1, 1))
            {
                case "1":
                    GameManager.Instance.wave1Clear = true;
                    break;
                case "2":
                    GameManager.Instance.wave2Clear = true;
                    break;
                case "3":
                    GameManager.Instance.wave3Clear = true;
                    //GameManager.Instance.User.stageClear.Push(GameManager.Instance.stageID);
                    break;
            }
            foreach (Compensations _data in waveDB.GetData(GameManager.Instance.waveID).Compensations)
            {
                //GameManager.Instance.User.AddItem(_data._compensation, _data._compensationCount);
            }
        }
        Debug.Log("gameEnd");
        UIManager.Instance.BattleEnd();
    }
    private void SetSpawnPos()
    {
        playerSpawnPos[0] = new Vector3(0.5f, 0, -7f);
        playerSpawnPos[1] = new Vector3(3.5f, 0, -7f);
        playerSpawnPos[2] = new Vector3(-1, 0, -7f);
        playerSpawnPos[3] = new Vector3(2, 0, -7f);
        playerSpawnPos[4] = new Vector3(5, 0, -7f);

        enemySpawnPos[0] = new Vector3(0.5f, 0, 1.5f);
        enemySpawnPos[1] = new Vector3(3.5f, 0, 1.5f);
        enemySpawnPos[2] = new Vector3(-1, 0, 2.5f);
        enemySpawnPos[3] = new Vector3(2, 0, 2.5f);
        enemySpawnPos[4] = new Vector3(5, 0, 2.5f);
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
    private bool DieCheck(UnitInfo unitInfo)
    {
        switch (unitInfo.unitType)
        {
            case CharacterType.Player:
                if (unitInfo.characterData.status.health <= 0)
                {
                    playerUnitCount--;
                    return true;
                }
                break;
            case CharacterType.Enemy:
                if (unitInfo.unitData.Health <= 0)
                {
                    unitInfo.actionController.Die();
                    lUnitInfo[int.Parse(unitInfo.unitObject.name)].actionController.Die();
                    lUnitInfo.Remove(int.Parse(unitInfo.unitObject.name));
                    enemyUnitCount--;
                    TargetChange(CharacterType.Enemy);
                    return true;
                }
                break;
        }
        return false;
    }
    private GameEnd WaveEndChack()
    {
        gameState = GameEnd.Paly;
        if (playerUnitCount == 0)
        {
            gameState = GameEnd.fail;
        }
        else if (enemyUnitCount == 0)
        {
            gameState = GameEnd.success;
        }
        return gameState;
    }
    private void AddDamageUI(float damage, string name)
    {
        OnAddDamage?.Invoke(damage, name);
    }
    public void TargetChange(CharacterType type)
    {
        switch (type)
        {
            case CharacterType.Player:
                if (playerUnitCount == 0) break;
                var usercount = UnityEngine.Random.Range(1, playerCreateCount + 1);
                bool isPlayer = true;
                while (isPlayer)
                {
                    foreach (int key in lUnitInfo.Keys)
                    {
                        if (usercount == key)
                        {
                            target = lUnitInfo[usercount].unitObject;
                            isPlayer = false;
                        }
                        else
                        {
                            usercount = UnityEngine.Random.Range(1, playerCreateCount + 1);
                        }
                    }
                }
                break;
            case CharacterType.Enemy:
                if (enemyUnitCount == 0) break;
                var enemycount = UnityEngine.Random.Range(playerCreateCount + 1, enemyCreateCount + playerCreateCount + 1);
                bool isEnemy = true;
                while (isEnemy)
                {
                    foreach (int key in lUnitInfo.Keys)
                    {
                        if (target.name == key.ToString() && enemycount == key)
                        {
                            OffTarget();
                        }
                        if (enemycount == key)
                        {
                            target = lUnitInfo[enemycount].unitObject;
                            isEnemy = false;
                            OnTarget();
                        }
                        else
                        {
                            enemycount = UnityEngine.Random.Range(playerCreateCount + 1, enemyCreateCount + playerCreateCount + 1);
                        }
                    }
                }
                break;
        }
    }
    public void OffTarget()
    {
        lUnitInfo[int.Parse(target.name)].actionController.TargetOff();
    }
    public void OnTarget()
    {
        lUnitInfo[int.Parse(target.name)].actionController.TargetOn();
    }
    public WaitForSeconds GameForSeconds(float time)
    {
        gameForSeconds = new WaitForSeconds(time);
        return gameForSeconds;
    }
    public WaitForSeconds AnimForSeconds(float newAnim, float beforeAnim)
    {
        if (newAnim != beforeAnim)
        {
            animForSeconds = new WaitForSeconds(newAnim);
        }
        beforeAnimTime = newAnimTime;
        return animForSeconds;
    }

}
