using Enums;
using Structs;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class EnemyCharacterData
{
    public SEnemyData enemyData;
    public Dictionary<int, CharacterSkill> enemySkillData = new Dictionary<int, CharacterSkill>(2);

    public EnemyCharacterData(int id)
    {
        enemyData = new SEnemyData(DataManager.Instance.EnemyDB.GetData(id));
        for (int i = 1; i <= 2; i++)
        {
            string skillIndex = id + $"{i:D2}".ToString() + "1";
            enemySkillData.Add(i, new CharacterSkill(int.Parse(skillIndex)));
        }
    }
}


public class EnemySkillController : MonoBehaviour
{
    public BuffData attackUp;
    public BuffData attackDown;
    public BuffData defUp;
    public BuffData defDown;
    public BuffData buffID;
    public BuffData shield;
    public BuffData stun;
    public BuffData provoke;
    public Vector3 targetPos;
    public Vector3 startPos;
    public Vector3 diffPos;
    private int skill2CoolTime;
    public float unitGauge;
    public float shieldQuantity;
    private bool isTargetPos = false;
    private bool isStartPos = true;
    private bool isAttack = false;
    public BuffAndDebuff buffAndDebuff;
    private GameObject skillObj;
    public EnemyCharacterData enemyCharacterData;
    public EnemyCharacterData enemyCharacterBuffData;
    private Dictionary<string, GameObject> OnBuff;
    private SEnemyData enemyData;
    private SkillData enemySkillData;
    private int teamIndex;
    BattleManager battleManager;
    private WaitForSeconds wait05 = new WaitForSeconds(0.5f);
    private void Start()
    {
        battleManager = BattleManager.Instance;
        teamIndex = int.Parse(gameObject.name);
        enemyData = battleManager.lUnitInfo[teamIndex].unitData;
        enemyCharacterData = new EnemyCharacterData(enemyData.ID);
        enemyCharacterBuffData = new EnemyCharacterData(enemyData.ID);
        buffAndDebuff = new BuffAndDebuff(0, enemyData.ID, "Enemy");
        OnBuff = new Dictionary<string, GameObject>();
        skill2CoolTime = enemyCharacterData.enemySkillData[2].coolTime;
        InitBuffData();
    }
    public void Init()
    {
        battleManager = BattleManager.Instance;
        teamIndex = int.Parse(gameObject.name);
        enemyData = battleManager.lUnitInfo[teamIndex].unitData;
        enemyCharacterData = new EnemyCharacterData(enemyData.ID);
        enemyCharacterBuffData = new EnemyCharacterData(enemyData.ID);
        buffAndDebuff = new BuffAndDebuff(0, enemyData.ID, "Enemy");
        OnBuff = new Dictionary<string, GameObject>();
        InitBuffData();
    }
    private void FixedUpdate()
    {

        //unitGauge = BattleManager.Instance.lUnitInfo[teamIndex].unitGauge;
        //battleManager.SetTurnIndicator(teamIndex, unitGauge);
        if (!isTargetPos && isAttack)
        {
            MoveToTarget();
        }
        if (!isStartPos && isAttack)
        {
            MoveToStart();
        }

    }
    public void TurnOn()
    {
        Debug.Log("적턴시작");
        Camera.main.transform.SetLocalPositionAndRotation(battleManager.defalutCameraPos, Quaternion.Euler(15, 0, 0));
        skill2CoolTime--;
        battleManager.TargetChange(CharacterType.Player);
        if (provoke != null && battleManager.lUnitInfo.ContainsKey(battleManager.provokeIndex)) 
        {
            battleManager.target = battleManager.lUnitInfo[battleManager.provokeIndex].unitObject;
            BuffDuration();
            StartCoroutine(Skill1());
            return;
        }             
        if(stun!=null)
        {
            BuffDuration();
            StunEffect();
            return;
        }
        BuffDuration();
        if (skill2CoolTime <= 0)
        {
            StartCoroutine(Skill2());
        }
        else
        {
            StartCoroutine(Skill1());
        }
    }
    private IEnumerator Skill1()
    {
        switch (enemyCharacterData.enemySkillData[1].range)
        {
            case 0:
                yield return StartCoroutine(MeleeSkillEffect("1"));
                //StartCoroutine(MeleeSkillEffect(battleManager.animForSeconds,"1"));
                //StartCoroutine(WaitForSkillEffect(3f));
                WaitForSkillEffect();
                break;
            case 1:
                yield return StartCoroutine(RangedSkillEffect("1"));
                //StartCoroutine(RangedSkillEffect(battleManager.animForSeconds, "1"));
                //StartCoroutine(WaitForSkillEffect(3.5f));
                WaitForSkillEffect();
                break;
        }
    }
    private IEnumerator Skill2()
    {
        switch (enemyCharacterData.enemySkillData[2].range)
        {
            case 0:
                yield return StartCoroutine(MeleeSkillEffect("2"));
                skill2CoolTime = enemyCharacterData.enemySkillData[2].coolTime;
                WaitForSkillEffect();
                break;
            case 1:
                yield return StartCoroutine(RangedSkillEffect("2"));
                skill2CoolTime = enemyCharacterData.enemySkillData[2].coolTime;
                WaitForSkillEffect();
                break;
                //case 0:
                //    StartCoroutine(MeleeSkillEffect(battleManager.animForSeconds,"2"));
                //    skill2CoolTime = enemyCharacterData.enemySkillData[2].coolTime;
                //    StartCoroutine(WaitForSkillEffect(3f));
                //    break;
                //case 1:
                //    StartCoroutine(RangedSkillEffect(battleManager.animForSeconds, "2"));
                //    skill2CoolTime = enemyCharacterData.enemySkillData[2].coolTime;
                //    StartCoroutine(WaitForSkillEffect(3.5f));
                //    break;
        }
    }
  private void WaitForSkillEffect()
    {
        battleManager.TargetChange(CharacterType.Enemy);
        battleManager.speedModifier = 1;
        battleManager.lUnitInfo[battleManager.onTurnIndex].unitGauge = 0;
        battleManager.isAttacking = false;
    }
    IEnumerator MeleeSkillEffect(string num)
    {
        int skillNum = int.Parse(num);
        startPos = transform.localPosition;
        targetPos = battleManager.target.transform.localPosition + new Vector3(0, 0, 2);
        battleManager.lUnitInfo[battleManager.onTurnIndex].actionController.WorldRun();
        isAttack = true;
        isStartPos = true;
        isTargetPos = false;
        yield return wait05;
        battleManager.lUnitInfo[battleManager.onTurnIndex].actionController.WorldIdle();
        yield return wait05;
        SoundManager.Instance.AttackAudio(enemyCharacterData.enemyData.ID, num);
        if (num == "1")
        {
            battleManager.lUnitInfo[battleManager.onTurnIndex].actionController.BattleSkill1();
            battleManager.AnimForSeconds(battleManager.newAnimTime, battleManager.beforeAnimTime);
        }
        else
        {
            battleManager.lUnitInfo[battleManager.onTurnIndex].actionController.BattleSkill2();
            battleManager.AnimForSeconds(battleManager.newAnimTime, battleManager.beforeAnimTime);
        }
        OnSkillEffect(enemyCharacterData.enemySkillData[skillNum]);
        battleManager.OnSkillEnemy(enemyCharacterBuffData, skillNum);
        yield return battleManager.animForSeconds;
        yield return wait05;
        Destroy(skillObj);
        transform.localPosition = startPos;
        isStartPos = false;
        isAttack = false;
        yield break;
    }
    IEnumerator RangedSkillEffect(string num)
    {
        int skillNum = int.Parse(num);
        targetPos = battleManager.target.transform.localPosition + new Vector3(0, 0, -1);
        isAttack = true;
        isTargetPos = true;
        isStartPos = true;
        SoundManager.Instance.AttackAudio(enemyCharacterData.enemyData.ID, num);
        if (num == "1")
        {
            battleManager.lUnitInfo[battleManager.onTurnIndex].actionController.BattleSkill1();
            battleManager.AnimForSeconds(battleManager.newAnimTime, battleManager.beforeAnimTime);
        }
        else
        {
            battleManager.lUnitInfo[battleManager.onTurnIndex].actionController.BattleSkill2();
            battleManager.AnimForSeconds(battleManager.newAnimTime, battleManager.beforeAnimTime);
        }
        OnSkillEffect(enemyCharacterData.enemySkillData[skillNum]);
        yield return battleManager.animForSeconds;
        battleManager.OnSkillEnemy(enemyCharacterBuffData, skillNum);
        Destroy(skillObj);
        yield return new WaitForSeconds(1f);
        isAttack = false;
        yield break;
    }
    private void StunEffect()
    {
        battleManager.TargetChange(CharacterType.Enemy);
        battleManager.speedModifier = 1;
        battleManager.lUnitInfo[battleManager.onTurnIndex].unitGauge = 0;
        battleManager.isAttacking = false;
    }
    private void OnSkillEffect(CharacterSkill skill)
    {
        var res = Resources.Load<GameObject>(skill.effectPath);
        if (res != null)
        {
            skillObj = Instantiate<GameObject>(res, transform);
        }
    }
    private void MoveToTarget()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, 7f * Time.deltaTime);
        diffPos = targetPos - transform.localPosition;
        if (diffPos.magnitude < 0.05f) { isTargetPos = true; }
    }
    private void MoveToStart()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, startPos, 7f * Time.deltaTime);
        diffPos = startPos - transform.localPosition;
        if (diffPos.magnitude < 0.05f) { isStartPos = true; }
    }

    private BuffData SetBuffData(BuffData buffData)
    {
        BuffData buff = new BuffData();
        buff.BuffID = buffData.BuffID;
        buff.Coefficient = buffData.Coefficient;
        buff.DebuffProbability = buffData.DebuffProbability;
        buff.Duration = buffData.Duration;
        buff.Name = buffData.Name;
        buff.IconPath = buffData.IconPath;
        buff.CharacterData = buffData.CharacterData;
        return buff;
    }
    private void InitBuffData()
    {
        attackUp = null;
        attackDown = null;
        defUp = null;
        defDown = null;
        buffID = null;
        shield = null;
        stun = null;
        provoke = null;
    }
    public void SetBuffandDebuff(int buffID)
    {
        switch (buffID)
        {
            case 101://쉴드
                buffAndDebuff.SetShield(buffID);
                if (shield != null && buffAndDebuff.shield != null)
                {
                    shieldQuantity = shield.CharacterData;
                    shield.Duration = buffAndDebuff.shield.Duration;
                }
                if (shield == null && buffAndDebuff.shield != null)
                {
                    shield = SetBuffData(buffAndDebuff.shield);
                    shieldQuantity = shield.CharacterData;
                    var obj = UIManager.Instance.EnemyBuffIcon(teamIndex, shield.IconPath);
                    OnBuff.Add("Shield", obj);
                }
                break;
            case 102://공증
                buffAndDebuff.SetAtkUp(buffID);
                if (attackUp != null && buffAndDebuff.attackUp != null) attackUp.Duration = buffAndDebuff.attackUp.Duration;
                if (attackUp == null && buffAndDebuff.attackUp != null)
                {
                    attackUp = SetBuffData(buffAndDebuff.attackUp);
                    enemyCharacterBuffData.enemyData.Atk += attackUp.CharacterData;
                    var obj = UIManager.Instance.EnemyBuffIcon(teamIndex, attackUp.IconPath);
                    OnBuff.Add("AttackUp", obj);
                }
                break;
            case 103://방증
                buffAndDebuff.SetDefUp(buffID);
                if (defUp != null && buffAndDebuff.defUp != null) defUp.Duration = buffAndDebuff.defUp.Duration;
                if (defUp == null && buffAndDebuff.defUp != null)
                {
                    defUp = SetBuffData(buffAndDebuff.defUp);
                    enemyCharacterBuffData.enemyData.Def += defUp.CharacterData;
                    var obj = UIManager.Instance.EnemyBuffIcon(teamIndex, defUp.IconPath);
                    OnBuff.Add("DefUp", obj);
                }
                break;
            case 201:
            case 202://공감
                buffAndDebuff.SetAtkDown(buffID);
                if (attackDown != null && buffAndDebuff.attackDown != null) attackDown.Duration = buffAndDebuff.attackDown.Duration;
                if (attackDown == null && buffAndDebuff.attackDown != null)
                {
                    attackDown = SetBuffData(buffAndDebuff.attackDown);
                    enemyCharacterBuffData.enemyData.Atk += attackDown.CharacterData;
                    var obj = UIManager.Instance.EnemyBuffIcon(teamIndex, attackDown.IconPath);
                    OnBuff.Add("AttackDown", obj);
                }
                break;
            case 203:
            case 204://방감
                buffAndDebuff.SetDefDown(buffID);
                if (defDown != null && buffAndDebuff.defDown != null) defDown.Duration = buffAndDebuff.defDown.Duration;
                if (defDown == null && buffAndDebuff.defDown != null)
                {
                    defDown = SetBuffData(buffAndDebuff.defDown);
                    enemyCharacterBuffData.enemyData.Def += defDown.CharacterData;
                    var obj = UIManager.Instance.EnemyBuffIcon(teamIndex, defDown.IconPath);
                    OnBuff.Add("DefDown", obj);
                }
                break;
            case 205:
            case 206://스턴
                buffAndDebuff.SetStun(buffID);
                if (stun != null && buffAndDebuff.stun != null) stun.Duration = buffAndDebuff.stun.Duration;
                if (stun == null && buffAndDebuff.stun != null)
                {
                    stun = SetBuffData(buffAndDebuff.stun);
                    var obj = UIManager.Instance.EnemyBuffIcon(teamIndex, stun.IconPath);
                    OnBuff.Add("Stun", obj);
                }
                break;
            case 207:
            case 208:
            case 209://도발
                buffAndDebuff.SetProvoke(buffID);
                if (provoke != null && buffAndDebuff.provoke != null) provoke.Duration = buffAndDebuff.provoke.Duration;
                if (provoke == null && buffAndDebuff.provoke != null)
                {
                    provoke = SetBuffData(buffAndDebuff.provoke);
                    var obj = UIManager.Instance.EnemyBuffIcon(teamIndex, provoke.IconPath);
                    OnBuff.Add("Provoke", obj);
                }
                break;
        }
        //buffAndDebuff.SetBuffandDebuff(buffID);
    }
    private void BuffDuration()
    {
        BuffCheck(attackUp, enemyCharacterBuffData.enemyData.Atk, enemyCharacterData.enemyData.Atk, "AttackUp");
        BuffCheck(attackDown, enemyCharacterBuffData.enemyData.Atk, enemyCharacterData.enemyData.Atk, "AttackDown");
        BuffCheck(defUp, enemyCharacterBuffData.enemyData.Def, enemyCharacterData.enemyData.Def, "DefUp");
        BuffCheck(defDown, enemyCharacterBuffData.enemyData.Def, enemyCharacterData.enemyData.Def, "DefDown");
        BuffCheck(shield, shieldQuantity, 0, "Shield");
        BuffCheck(stun, 0, 0, "Stun");
        BuffCheck(provoke, 0, 0, "Provoke");
    }
    public void BuffCheck(BuffData buff, float buffStat, float oriStat, string buffName)
    {
        if (buff != null)
        {
            buff.Duration--;
            if (buff.Duration <= 0)
            {
                //characterBuffData.status.atk = characterData.status.atk;
                if (OnBuff.ContainsKey(buffName))
                {
                    Destroy(OnBuff[buffName].gameObject);
                    OnBuff.Remove(buffName);
                }
                if (buff.BuffID == 101)
                {
                    shieldQuantity = oriStat;
                    shield = null;
                }
                if (buff.BuffID == 102)
                {
                    enemyCharacterBuffData.enemyData.Atk = oriStat;
                    attackUp = null;
                }
                if (buff.BuffID == 103)
                {
                    enemyCharacterBuffData.enemyData.Def = oriStat;
                    defUp = null;
                }
                if (buff.BuffID == 201 || buff.BuffID == 202)
                {
                    enemyCharacterBuffData.enemyData.Atk = oriStat;
                    attackDown = null;
                }
                if (buff.BuffID == 203 || buff.BuffID == 204)
                {
                    enemyCharacterBuffData.enemyData.Def = oriStat;
                    defDown = null;
                }
                if (buff.BuffID == 205 || buff.BuffID == 206) stun = null;
                if (buff.BuffID == 207 || buff.BuffID == 208 || buff.BuffID == 209) provoke = null;
            }
        }
    }
}