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
    public float unitGauge;
    int runHash;
    private bool isCharacterTurn;
    private bool isTargetPos = false;
    private bool isStartPos = true;
    private bool isAttack = false;
    public GameObject character;


    private void Start()
    {
        //animator = GetComponentInChildren<Animator>();
        battleManager = BattleManager.Instance;
        //character = GetComponentInChildren<GameObject>();
        if (GameManager.Instance.User.teamData.ContainsKey(teamIndex))
        {
            characterData = GameManager.Instance.User.teamData[teamIndex];
            unitInfo = battleManager.lUnitInfo.Find(x => x.unitID == characterData.status.iD);
        }
        isCharacterTurn = false;
        runHash = Animator.StringToHash("isRun");
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
    public void TurnOn()
    {
        isCharacterTurn = true;
    }
    private void Skill1()
    {
        if (isCharacterTurn)
        {
            StartCoroutine(SkillEffect(2f, "1"));
        }
        StartCoroutine(WaitForSkillEffect(4f));
    }
    private void Skill2()
    {
        if (isCharacterTurn)
        {
            StartCoroutine(SkillEffect(1f, "2"));
        }
        StartCoroutine(WaitForSkillEffect(4f));
    }
    private void Skill3()
    {
        if (isCharacterTurn)
        {
            StartCoroutine(SkillEffect(1f, "3"));
        }
        StartCoroutine(WaitForSkillEffect(2f));

    }
    private void Skill4()
    {
        if (isCharacterTurn)
        {
            StartCoroutine(SkillEffect(1f, "4"));
        }
        StartCoroutine(WaitForSkillEffect(2f));
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
        battleManager.speedModifier = 1;
        battleManager.lUnitInfo[battleManager.tempIndex].unitGauge = 0;
        battleManager.isAttacking = false;
        isCharacterTurn = false;

    }
    IEnumerator SkillEffect(float time, string num)
    {
        startPos = transform.localPosition;
        targetPos = battleManager.target.transform.localPosition + new Vector3(0, 0, -2);
        animator.SetTrigger(Animator.StringToHash("Move"));
        isAttack = true;
        isStartPos = true;
        isTargetPos = false;
        //int count = 0;
        yield return new WaitForSeconds(0.5f);
        //while (!isArrive && count < 1500)
        //{
        //    transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, 5f * Time.deltaTime);
        //    yield return new WaitForEndOfFrame();
        //    diffPos = targetPos - transform.localPosition;
        //    count++;
        //    if (diffPos.magnitude < 0.3f) { isArrive = true; }
        //}
        animator.SetTrigger(Animator.StringToHash("Skill" + num));

        battleManager.OnSkill(characterData, int.Parse(num));

        yield return new WaitForSeconds(time);
        //isArrive = false;
        //count = 0;
        character.transform.localPosition = Vector3.zero;
        animator.SetTrigger(Animator.StringToHash("Jump"));
        yield return new WaitForSeconds(0.2f);
        isStartPos = false;
        yield return new WaitForSeconds(0.5f);
        //while (!isArrive && count < 1500)
        //{
        //    transform.localPosition = Vector3.Lerp(transform.localPosition, startPos, 5f * Time.deltaTime);
        //    yield return new WaitForEndOfFrame();
        //    diffPos = targetPos - transform.localPosition;
        //    count++;
        //    if (diffPos.magnitude < 0.3f) { isArrive = true; }
        //}
        isAttack = false;
    }
}
