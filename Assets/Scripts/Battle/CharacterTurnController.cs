using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterTurnController : MonoBehaviour
{
    public int teamIndex;
    CharacterData characterData;
    CharacterData characterBuffData;
    BattleManager battleManager;
    UnitInfo unitInfo;
    public Vector3 targetPos;
    public Vector3 startPos;
    public Vector3 diffPos;
    private Vector3 onTurnPos;
    private Vector3 offTurnPos;
    public BuffData attackUp;
    public BuffData attackDown;
    public BuffData defUp;
    public BuffData defDown;
    public BuffData buffID;
    public BuffData shield;
    public BuffData stun;
    public BuffData provoke;
    public float unitGauge;
    public float shieldQuantity;
    public int skill3CoolTime;
    public int skill4Gauge;
    private bool isCharacterTurn;
    private bool isTargetPos = false;
    private bool isStartPos = true;
    private bool isAttack = false;
    public Action changeSkill4Gauge;
    public Action changeShieldGauge;
    public GameObject character;
    private GameObject skillObj;
    private BuffAndDebuff buffAndDebuff;
    private WaitForSeconds wait05 = new WaitForSeconds(0.5f);
    private Dictionary<string, GameObject> OnBuff;
    private CharacterActionController actionController;
    private Player player;

    private void Start()
    {
        player = GetComponent<Player>();
        actionController = player.ActionController;
        skill4Gauge = 5;
        battleManager = BattleManager.Instance;
        onTurnPos = new Vector3(2, 0, -4.5f);
        offTurnPos = transform.localPosition;
        if (BattleManager.Instance.lUnitInfo.ContainsKey(teamIndex))
        {
            characterData = BattleManager.Instance.lUnitInfo[teamIndex].characterData;
            foreach (UnitInfo _unitInfo in battleManager.lUnitInfo.Values)
            {
                if (_unitInfo.unitID == characterData.status.iD)
                {
                    unitInfo = _unitInfo;
                }
            }
        }
        isCharacterTurn = false;
        characterBuffData = (CharacterData)characterData.CloneCharacter(characterData.status.iD);
        buffAndDebuff = new BuffAndDebuff(characterData.status.iD, 0, "Player");
        OnBuff = new Dictionary<string, GameObject>();
        InitBuffData();
        battleManager.skill1 += Skill1;
        battleManager.skill2 += Skill2;
        battleManager.skill3 += Skill3;
        battleManager.skill4 += Skill4;
    }


    private void FixedUpdate()
    {

        //unitGauge = unitInfo.unitGauge;
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
    public void CallChangeSkill4Gauge()
    {
        changeSkill4Gauge?.Invoke();
    }
    public void CallChangeShieldGauge()
    {
        changeShieldGauge?.Invoke();
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
                    var obj = UIManager.Instance.PlayerBuffIcon(teamIndex, shield.IconPath);
                    OnBuff.Add("Shield", obj);
                }
                CallChangeShieldGauge();

                break;
            case 102://공증
                buffAndDebuff.SetAtkUp(buffID);
                if (attackUp != null && buffAndDebuff.attackUp != null) attackUp.Duration = buffAndDebuff.attackUp.Duration;
                if (attackUp == null && buffAndDebuff.attackUp != null)
                {
                    attackUp = SetBuffData(buffAndDebuff.attackUp);
                    characterBuffData.status.atk += attackUp.CharacterData;
                    var obj = UIManager.Instance.PlayerBuffIcon(teamIndex, attackUp.IconPath);
                    OnBuff.Add("AttackUp", obj);
                }
                break;
            case 103://방증
                buffAndDebuff.SetDefUp(buffID);
                if (defUp != null && buffAndDebuff.defUp != null) defUp.Duration = buffAndDebuff.defUp.Duration;
                if (defUp == null && buffAndDebuff.defUp != null)
                {
                    defUp = SetBuffData(buffAndDebuff.defUp);
                    characterBuffData.status.def += defUp.CharacterData;
                    var obj = UIManager.Instance.PlayerBuffIcon(teamIndex, defUp.IconPath);
                    OnBuff.Add("DefUp", obj);
                }
                break;
            case 201:
            case 202://공감
                buffAndDebuff.SetAtkUp(buffID);
                if (attackDown != null && buffAndDebuff.attackDown != null) attackDown.Duration = buffAndDebuff.attackDown.Duration;
                if (attackDown == null && buffAndDebuff.attackDown != null)
                {
                    attackDown = SetBuffData(buffAndDebuff.attackDown);
                    characterBuffData.status.atk += attackDown.CharacterData;
                    var obj = UIManager.Instance.PlayerBuffIcon(teamIndex, attackDown.IconPath);
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
                    characterBuffData.status.def += defDown.CharacterData;
                    var obj = UIManager.Instance.PlayerBuffIcon(teamIndex, defDown.IconPath);
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
                    var obj = UIManager.Instance.PlayerBuffIcon(teamIndex, stun.IconPath);
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
                    var obj = UIManager.Instance.PlayerBuffIcon(teamIndex, provoke.IconPath);
                    OnBuff.Add("Provoke", obj);
                }
                break;
        }
        //buffAndDebuff.SetBuffandDebuff(buffID);
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
    }
    private void BuffDuration()
    {
        BuffCheck(attackUp, characterBuffData.status.atk, characterData.status.atk, "AttackUp");
        BuffCheck(attackDown, characterBuffData.status.atk, characterData.status.atk, "AttackDown");
        BuffCheck(defUp, characterBuffData.status.def, characterData.status.def, "DefUp");
        BuffCheck(defDown, characterBuffData.status.def, characterData.status.def, "DefDown");
        BuffCheck(shield, shieldQuantity, 0, "Shield");
        BuffCheck(stun, 0, 0, "Stun");
        BuffCheck(provoke, 0, 0, "Provoke");
        CallChangeShieldGauge();
    }
    public void BuffCheck(BuffData buff, float buffStat, float oriStat, string buffName)
    {
        if (buff != null)
        {
            buff.Duration--;
            if (buff.Duration <= 0)
            {
                buffStat = oriStat;
                //characterBuffData.status.atk = characterData.status.atk;
                if (OnBuff.ContainsKey(buffName))
                {
                    Destroy(OnBuff[buffName].gameObject);
                    OnBuff.Remove(buffName);
                }
                buff = null;
            }
        }
    }
    public void RemoveShield()
    {
        shieldQuantity = 0;
        if (OnBuff.ContainsKey("Shield"))
        {
            Destroy(OnBuff["Shield"].gameObject);
            OnBuff.Remove("Shield");
        }
        shield = null;
    }
    public void TurnOn()
    {
        isCharacterTurn = true;
        BuffDuration();
        skill3CoolTime--;
        transform.localPosition = onTurnPos;
        battleManager.skill3CoolTime = skill3CoolTime;
        battleManager.skill4Gauge = skill4Gauge;
        CallChangeSkill4Gauge();
        Camera.main.transform.SetLocalPositionAndRotation(battleManager.cameraPos, Quaternion.Euler(10, -30, 0));
    }
    private void Skill1()
    {
        if (isCharacterTurn)
        {
            switch (characterData.skillData[1].range)
            {
                case 0:
                    StartCoroutine(MeleeSkillEffect("1"));
                    StartCoroutine(WaitForSkillEffect(4f));
                    break;
                case 1:
                    StartCoroutine(RangedSkillEffect(1f, "1"));
                    StartCoroutine(WaitForSkillEffect(3f));
                    break;
            }
            skill4Gauge += characterData.skillData[1].skillGage;
            CallChangeSkill4Gauge();

        }
    }
    private void Skill2()
    {
        if (isCharacterTurn)
        {
            switch (characterData.skillData[2].range)
            {
                case 0:
                    StartCoroutine(MeleeSkillEffect("2"));
                    StartCoroutine(WaitForSkillEffect(3.5f));
                    break;
                case 1:
                    StartCoroutine(RangedSkillEffect(1f, "2"));
                    StartCoroutine(WaitForSkillEffect(3f));
                    break;
            }
        }
    }
    private void Skill3()
    {
        if (isCharacterTurn && skill3CoolTime <= 0)
        {
            switch (characterData.skillData[3].range)
            {
                case 0:
                    StartCoroutine(MeleeSkillEffect("3"));
                    skill3CoolTime = characterData.skillData[3].coolTime;
                    StartCoroutine(WaitForSkillEffect(4f));
                    break;
                case 1:
                    StartCoroutine(RangedSkillEffect(1f, "3"));
                    skill3CoolTime = characterData.skillData[3].coolTime;
                    StartCoroutine(WaitForSkillEffect(2.5f));
                    break;
            }
            skill4Gauge += characterData.skillData[3].skillGage;
            CallChangeSkill4Gauge();
        }

    }
    private void Skill4()
    {
        if (isCharacterTurn && skill4Gauge >= 5)
        {
            switch (characterData.skillData[4].range)
            {
                case 0:
                    StartCoroutine(MeleeSkillEffect("4"));
                    StartCoroutine(WaitForSkillEffect(3.7f));
                    break;
                case 1:
                    StartCoroutine(RangedSkillEffect(2f, "4"));
                    StartCoroutine(WaitForSkillEffect(3f));
                    break;
            }
            skill4Gauge = 0;
            CallChangeSkill4Gauge();
        }
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
    IEnumerator WaitForSkillEffect(float time)
    {
        yield return new WaitForSeconds(time);
        //Camera.main.transform.SetLocalPositionAndRotation(battleManager.defalutCameraPos, Quaternion.Euler(20, 0, 0));
        battleManager.speedModifier = 1;
        battleManager.lUnitInfo[battleManager.onTurnIndex].unitGauge = 0;
        battleManager.isAttacking = false;
        isCharacterTurn = false;
    }
    IEnumerator MeleeSkillEffect(string num)
    {
        int skillNum = int.Parse(num);
        startPos = transform.localPosition;
        targetPos = battleManager.target.transform.localPosition + new Vector3(0, 0, -2);
        actionController.BattleMove();
        isAttack = true;
        isStartPos = true;
        isTargetPos = false;

        yield return wait05;
        SkillAnim(skillNum);
        yield return new WaitForSeconds(1f);
        OnSkillEffect(characterData.skillData[skillNum]);
        SoundManager.Instance.AttackAudio(characterData.status.iD, num);
        battleManager.OnSkillPlayer(characterBuffData, skillNum);
        yield return wait05;
        yield return wait05;
        Destroy(skillObj);
        yield return wait05;
        actionController.BattleJump();
        yield return new WaitForSeconds(0.2f);
        isStartPos = false;
        yield return wait05;
        isTargetPos = true;
        transform.localPosition = offTurnPos;
        isAttack = false;
    }
    IEnumerator RangedSkillEffect(float time, string num)
    {
        int skillNum = int.Parse(num);
        targetPos = battleManager.target.transform.localPosition + new Vector3(0, 0, -1);
        isAttack = true;
        isTargetPos = true;
        isStartPos = true;
        SkillAnim(skillNum);
        OnSkillEffect(characterData.skillData[skillNum]);
        SoundManager.Instance.AttackAudio(characterData.status.iD, num);
        yield return new WaitForSeconds(time);
        battleManager.OnSkillPlayer(characterBuffData, skillNum);
        Destroy(skillObj);
        yield return new WaitForSeconds(0.2f);
        //character.transform.localPosition = Vector3.zero;
        transform.localPosition = offTurnPos;
        isAttack = false;
    }
    private void SkillAnim(int number)
    {
        switch (number)
        {
            case 1:
                actionController.BattleSkill1();
                break;
            case 2:
                actionController.BattleSkill2();
                break;
            case 3:
                actionController.BattleSkill3();
                break;
            case 4:
                actionController.BattleSkill4();
                break;
        }
    }
}
