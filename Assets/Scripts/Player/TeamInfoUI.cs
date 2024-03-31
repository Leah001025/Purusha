using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TeamInfoUI : MonoBehaviour
{
    [SerializeField] private Slider hpBar;
    [SerializeField] private Slider skill4Gauge;
    [SerializeField] private Slider shieldBar;
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private TextMeshProUGUI characterHealth;
    [SerializeField] private Outline outline;
    private UnitInfo characterInfo;
    private CharacterTurnController turnController;
    private float shield;
    private int index;
    private float characterMaxHealth;
    private float characterCurHealth;

    private void Start()
    {
        index = int.Parse(gameObject.name);
        characterInfo = BattleManager.Instance.lUnitInfo[index];
        characterName.text = characterInfo.characterData.status.name;
        characterCurHealth = characterInfo.characterData.status.health;
        characterMaxHealth = characterInfo.characterData.status.maxhealth;
        turnController = BattleManager.Instance.turnControllers[index];
        turnController.changeSkill4Gauge += ChangeSkill4BarAmount;
        turnController.changeShieldGauge += ChangeShieldBarAmount;
        BattleManager.Instance.OnAddDamage += ChangeHealthBarAmount;
        characterHealth.text = characterCurHealth.ToString() + " / " + characterMaxHealth.ToString();
        hpBar.value = characterCurHealth/ characterMaxHealth;
        skill4Gauge.value = 0;
    }
    private void ChangeShieldBarAmount()
    {
        shield = turnController.buffShield;
        shieldBar.value = shield / characterMaxHealth; 
    }
    private void ChangeSkill4BarAmount()
    {
        float value = (turnController.skill4Gauge / 5f);
        skill4Gauge.value = value;
        if(value < 1) 
        { 
            outline.enabled = false;
        }
        else
        {
            outline.enabled = true;
        }
    }

    private void ChangeHealthBarAmount(float damage, string name)
    {
        if (gameObject.name == name)
        {
            characterCurHealth = characterInfo.characterData.status.health;
            characterHealth.text = characterCurHealth.ToString() + " / " + characterMaxHealth.ToString();
            float fillDamage = characterCurHealth / characterMaxHealth;
            hpBar.value = fillDamage;
        }
    }
}
