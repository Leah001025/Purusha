using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breesha : MonoBehaviour
{
    Animator animator;
    public string teamIndex;
    CharacterData characterData;
    public GameObject uiPopup;
    public Vector3 targetPos;
    public Vector3 startPos;
    public Vector3 diffPos;
    public float curTime;
    private float tempTime = 0f;
    bool isCharacterTurn;


    private void Start()
    {
        animator = GetComponent<Animator>();
        teamIndex = gameObject.name;
        characterData = GameManager.Instance.User.teamData[int.Parse(teamIndex)];
        startPos = transform.localPosition;
        isCharacterTurn = false;
        TestBattle.Instance.skill1 += Skill1;
        TestBattle.Instance.skill2 += Skill2;
        TestBattle.Instance.skill3 += Skill3;
        TestBattle.Instance.skill4 += Skill4;
    }


    private void Update()
    {
        curTime = (TestBattle.Instance.time - tempTime) * characterData.status.speed;
        TestBattle.Instance.SetTurnIndicator(teamIndex, curTime);
        if (curTime >= 3f&& !TestBattle.Instance.isTurnPlay)
        {
            TestBattle.Instance.timeModifier = 0;
            uiPopup.gameObject.SetActive(true);            
            isCharacterTurn = true;
            TestBattle.Instance.isTurnPlay = true;
        }

    }
    private void Skill1()
    {
        if (isCharacterTurn)
        {
            StartCoroutine(SkillEffect(1.5f, "1"));
            uiPopup.gameObject.SetActive(false);
            tempTime = TestBattle.Instance.time;
        }
        StartCoroutine(WaitForSkillEffect(2.5f));
    }
    private void Skill2()
    {
        if (isCharacterTurn)
        {
            StartCoroutine(SkillEffect(1.5f, "2"));
            uiPopup.gameObject.SetActive(false);
            tempTime = TestBattle.Instance.time;
        }
        StartCoroutine(WaitForSkillEffect(2.5f));
    }
    private void Skill3()
    {
        if (isCharacterTurn)
        {
            StartCoroutine(SkillEffect(1.5f,"3"));
            uiPopup.gameObject.SetActive(false);
            tempTime = TestBattle.Instance.time;
        }
        StartCoroutine(WaitForSkillEffect(2.5f));

    }
    private void Skill4()
    {
        if (isCharacterTurn)
        {
            StartCoroutine(SkillEffect(1.5f, "4"));
            uiPopup.gameObject.SetActive(false);
            tempTime = TestBattle.Instance.time;
        }
        StartCoroutine(WaitForSkillEffect(2.5f));
    }
    IEnumerator WaitForSkillEffect(float time)
    {
        yield return new WaitForSeconds(time);
        TestBattle.Instance.timeModifier = 1;
        TestBattle.Instance.isTurnPlay = false;
        isCharacterTurn = false;

    }
    IEnumerator SkillEffect(float time, string num)
    {
        targetPos = TestBattle.Instance.targetObject.transform.localPosition + new Vector3(0, 0, -1);
        bool isArrive = false;
        int count = 0;
        while (!isArrive&&count<1500)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, 5f * Time.deltaTime);
            yield return new WaitForEndOfFrame();          
            diffPos = targetPos - transform.localPosition;
            count++;
            if (diffPos.magnitude < 0.3f) { isArrive = true; }            
        }
        animator.SetTrigger(Animator.StringToHash("Skill"+num));
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
    }
}
