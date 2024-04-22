using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MonsterInfoUI : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image hpBar;
    [SerializeField] private TMP_Text InfoText;
    [SerializeField] private TMP_Text damageText;
    [SerializeField] private GameObject damageObj;
    [SerializeField] private Animator damageAnimator;
    private string monsterName;
    private float monsterMaxHealth;
    private UnitInfo monsterInfo;

    private void Start()
    {

        monsterName = gameObject.transform.parent.gameObject.transform.parent.gameObject.name;
        damageAnimator = GetComponent<Animator>();
        monsterInfo = BattleManager.Instance.lUnitInfo[int.Parse(monsterName)];
        InfoText.text = $"Lv.{monsterInfo.unitData.Level} {monsterInfo.unitData.Name}";
        monsterMaxHealth = BattleManager.Instance.lUnitInfo[int.Parse(monsterName)].unitData.Health;
        BattleManager.Instance.OnAddDamage += DamageUI;
    }
    private void Update()
    {
        float fillDamage = (BattleManager.Instance.lUnitInfo[int.Parse(monsterName)].unitData.Health) / monsterMaxHealth;
        hpBar.fillAmount = Mathf.Lerp(hpBar.fillAmount, fillDamage, Time.deltaTime * 20);
    }
    private void DamageUI(float damage, string name)
    {
        if (monsterName == name)
        {
            damageObj.SetActive(true);
            damageText.text = $"- {damage.ToString("N2")}";
            damageAnimator.SetTrigger("OnDamage");
        }
    }
}
