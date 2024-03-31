using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterTurnController : MonoBehaviour
{
    public Animator animator;
    public int teamIndex;
    CharacterData characterData;
    BattleManager battleManager;
    UnitInfo unitInfo;
    public Vector3 targetPos;
    public Vector3 startPos;
    public Vector3 diffPos;
    private Vector3 onTurnPos;
    private Vector3 offTurnPos;
    public float unitGauge;
    public float buffAtk;
    public float buffDef;
    int runHash;
    private int skill3CoolTime;
    public int skill4Gauge;
    private bool isCharacterTurn;
    private bool isTargetPos = false;
    private bool isStartPos = true;
    private bool isAttack = false;
    public Action changeSkill4Gauge;
    public GameObject character;
    private GameObject skillObj;
    private WaitForSeconds wait05 = new WaitForSeconds(0.5f);

    private void Start()
    {
        //skill4Gauge = 5;
        battleManager = BattleManager.Instance;
        onTurnPos = new Vector3(2, 0, -4.5f);
        offTurnPos = transform.localPosition;
        if (GameManager.Instance.User.teamData.ContainsKey(teamIndex))
        {
            characterData = GameManager.Instance.User.teamData[teamIndex];
            unitInfo = battleManager.lUnitInfo.Find(x => x.unitID == characterData.status.iD);
        }
        isCharacterTurn = false;
        battleManager.skill1 += Skill1;
        battleManager.skill2 += Skill2;
        battleManager.skill3 += Skill3;
        battleManager.skill4 += Skill4;
    }


    private void FixedUpdate()
    {

        unitGauge = unitInfo.unitGauge;
        battleManager.SetTurnIndicator(teamIndex, unitGauge);
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
    public void SetBuffandDebuff(int buffID)
    {

    }

    public void TurnOn()
    {
        isCharacterTurn = true;
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
                    StartCoroutine(WaitForSkillEffect(3.5f));
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
                    StartCoroutine(WaitForSkillEffect(3.5f));
                    break;
                case 1:
                    StartCoroutine(RangedSkillEffect(1f, "3"));
                    skill3CoolTime = characterData.skillData[3].coolTime;
                    StartCoroutine(WaitForSkillEffect(2f));
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
        transform.localPosition = offTurnPos;
        //Camera.main.transform.SetLocalPositionAndRotation(battleManager.defalutCameraPos, Quaternion.Euler(20, 0, 0));
        battleManager.speedModifier = 1;
        battleManager.lUnitInfo[battleManager.onTurnIndex].unitGauge = 0;
        battleManager.isAttacking = false;
        isCharacterTurn = false;
        skill3CoolTime--;
    }
    IEnumerator MeleeSkillEffect(string num)
    {
        int skillNum = int.Parse(num);
        startPos = transform.localPosition;
        targetPos = battleManager.target.transform.localPosition + new Vector3(0, 0, -2);
        animator.SetTrigger(Animator.StringToHash("Move"));
        isAttack = true;
        isStartPos = true;
        isTargetPos = false;

        yield return wait05;
        animator.SetTrigger(Animator.StringToHash("Skill" + num));
        yield return new WaitForSeconds(1f);
        OnSkillEffect(characterData.skillData[skillNum]);
        yield return wait05;

        battleManager.OnSkillPlayer(characterData, skillNum);

        yield return wait05;
        Destroy(skillObj);
        character.transform.localPosition = Vector3.zero;
        animator.SetTrigger(Animator.StringToHash("Jump"));
        yield return new WaitForSeconds(0.2f);
        isStartPos = false;
        yield return wait05;

        isAttack = false;
    }
    IEnumerator RangedSkillEffect(float time, string num)
    {
        int skillNum = int.Parse(num);
        targetPos = battleManager.target.transform.localPosition + new Vector3(0, 0, -1);
        isAttack = true;
        isTargetPos = true;
        isStartPos = true;
        animator.SetTrigger(Animator.StringToHash("Skill" + num));
        OnSkillEffect(characterData.skillData[skillNum]);
        yield return new WaitForSeconds(0.2f);
        battleManager.OnSkillPlayer(characterData, skillNum);
        yield return new WaitForSeconds(time);
        Destroy(skillObj);
        character.transform.localPosition = Vector3.zero;
        isAttack = false;
    }
}
