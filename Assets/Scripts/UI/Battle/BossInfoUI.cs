using DG.Tweening;
using Structs;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossInfoUI : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image hpBar;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private GameObject eventObj;
    private string monsterName;
    private float monsterMaxHealth;
    private float monsterHealth;
    private float monsterMinHealth;
    private Animator animator;
    private UnitInfo bossData;
    private void Start()
    {
        foreach (KeyValuePair<int,UnitInfo> info in BattleManager.Instance.lUnitInfo)
        {
            if (info.Value.unitType == Enums.CharacterType.Enemy)
            {
                monsterName = info.Key.ToString();
                monsterMaxHealth = info.Value.unitData.Health;
                monsterHealth = info.Value.unitData.Health;
                monsterMinHealth = monsterMaxHealth;
            }
        }
        animator = GetComponent<Animator>();
        hpText.text = $"{monsterMaxHealth} / {monsterMaxHealth}";
        bossData = BattleManager.Instance.lUnitInfo[int.Parse(monsterName)];
        nameText.text = $"Lv.{bossData.unitData.Level} {bossData.unitData.Name}";
        eventObj.SetActive(true);
        StartCoroutine(Warning());
    }
    private void Update()
    {
        float fillDamage = (BattleManager.Instance.lUnitInfo[int.Parse(monsterName)].unitData.Health) / monsterMaxHealth;
        hpBar.fillAmount = Mathf.Lerp(hpBar.fillAmount, fillDamage, Time.deltaTime * 20);
        if (BattleManager.Instance.lUnitInfo[int.Parse(monsterName)].unitData.Health < monsterMinHealth)
        {
            monsterHealth -= Time.deltaTime * 1000f;
        }
        monsterMinHealth = Mathf.Clamp(monsterHealth, BattleManager.Instance.lUnitInfo[int.Parse(monsterName)].unitData.Health, monsterMaxHealth);
        int MinHealth = Mathf.FloorToInt(monsterMinHealth);
        hpText.text = string.Format("{0} / {1}", monsterMaxHealth,MinHealth);
        monsterHealth = monsterMinHealth;
    }
    IEnumerator Warning()
    {
        SoundManager.Instance.EffentAudio("warning");
        yield return new WaitForSeconds(2f);
        eventObj.SetActive(false);
    }
}
