using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTurnController : MonoBehaviour
{
    public Animator animator;
    public int teamIndex;
    CharacterData characterData;
    BattleManager battleManager;
    UnitInfo unitInfo;
    public GameObject uiPopup;
    public Vector3 targetPos;
    public Vector3 startPos;
    public Vector3 diffPos;
    public float unitGauge;
    int runHash;
    private bool isCharacterTurn;


    private void Start()
    {
        //animator = GetComponentInChildren<Animator>();
        battleManager = BattleManager.Instance;
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


    private void Update()
    {

        unitGauge = unitInfo.unitGauge;
        battleManager.SetTurnIndicator(teamIndex, unitGauge);
        //if (unitGauge >= 3f&& !TestBattle.Instance.isTurnPlay)
        //{
        //    uiPopup.gameObject.SetActive(true);            
        //    isCharacterTurn = true;
        //    TestBattle.Instance.isTurnPlay = true;
        //}

    }
    public void TurnOn()
    {
        isCharacterTurn = true;
    }
    private void Skill1()
    {
        if (isCharacterTurn)
        {
            animator.SetBool(runHash, true);
            StartCoroutine(SkillEffect(1.5f, "1"));
            animator.SetBool(runHash, false);
        }
        StartCoroutine(WaitForSkillEffect(2.5f));
    }
    private void Skill2()
    {
        if (isCharacterTurn)
        {
            StartCoroutine(SkillEffect(1.5f, "2"));
            animator.SetBool(runHash, false);
        }
        StartCoroutine(WaitForSkillEffect(2.5f));
    }
    private void Skill3()
    {
        if (isCharacterTurn)
        {
            StartCoroutine(SkillEffect(1.5f, "3"));
            animator.SetBool(runHash, false);
        }
        StartCoroutine(WaitForSkillEffect(2.5f));

    }
    private void Skill4()
    {
        if (isCharacterTurn)
        {
            StartCoroutine(SkillEffect(1.5f, "4"));
            animator.SetBool(runHash, false);
        }
        StartCoroutine(WaitForSkillEffect(2.5f));
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
        targetPos = battleManager.targerTrans.localPosition + new Vector3(0, 0, -1);
        bool isArrive = false;
        int count = 0;
        animator.SetBool(runHash,true);
        while (!isArrive && count < 1500)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, 5f * Time.deltaTime);
            yield return new WaitForEndOfFrame();
            diffPos = targetPos - transform.localPosition;
            count++;
            if (diffPos.magnitude < 0.3f) { isArrive = true; }
        }
        animator.SetTrigger(Animator.StringToHash("Skill" + num));
        yield return new WaitForSeconds(time);
        isArrive = false;
        count = 0;
        while (!isArrive && count < 1500)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, startPos, 5f * Time.deltaTime);
            yield return new WaitForEndOfFrame();
            diffPos = targetPos - transform.localPosition;
            count++;
            if (diffPos.magnitude < 0.3f) { isArrive = true; }
        }
        animator.SetBool(runHash, false);
    }
}
