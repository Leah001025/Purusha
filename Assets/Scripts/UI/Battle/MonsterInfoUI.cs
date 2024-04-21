using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterInfoUI : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image hpBar;
    private string monsterName;
    private float monsterMaxHealth;

    private void Start()
    {
        monsterName = gameObject.transform.parent.gameObject.transform.parent.gameObject.name;
        monsterMaxHealth = BattleManager.Instance.lUnitInfo[int.Parse(monsterName)].unitData.Health;
    }
    private void Update()
    {
        float fillDamage = (BattleManager.Instance.lUnitInfo[int.Parse(monsterName)].unitData.Health) / monsterMaxHealth;
        hpBar.fillAmount = Mathf.Lerp(hpBar.fillAmount, fillDamage, Time.deltaTime * 20);
    }
}
