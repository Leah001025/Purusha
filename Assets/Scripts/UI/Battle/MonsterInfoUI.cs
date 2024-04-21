using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class MonsterInfoUI : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image hpBar;
    [SerializeField] private TextMeshProUGUI damageNum;
    private string monsterName;
    private float monsterMaxHealth;
    private Color normalDam = new Color(1, 0.8f, 0);
    private Color criDam = new Color(1, 0.1f, 0);

    private void Start()
    {
        monsterName = gameObject.transform.parent.gameObject.transform.parent.gameObject.name;
        monsterMaxHealth = BattleManager.Instance.lUnitInfo[int.Parse(monsterName)].unitData.Health;
        BattleManager.Instance.OnAddDamage += ChangeHealthBarAmount;
        DOTween.Init();
    }

    private void ChangeHealthBarAmount(float damage, string name, bool isCritical)
    {
        if (monsterName == name)
        {
            float fillDamage = (BattleManager.Instance.lUnitInfo[int.Parse(monsterName)].unitData.Health) / monsterMaxHealth;
            hpBar.fillAmount = fillDamage;
            OnDamageUI(damage, isCritical);
        }
    }

    private void OnDamageUI(float damage, bool isCritical)
    {
        damageNum.gameObject.SetActive(true);
        damageNum.text = damage.ToString("0");
        var seq = DOTween.Sequence();
        var seq2 = DOTween.Sequence();
        if (isCritical)
        {
            damageNum.color = criDam;
            damageNum.fontSize = 0.35f;
            seq.Append(damageNum.transform.DOScale(1.6f, 0.2f));
            seq2.Append(damageNum.GetComponent<RectTransform>().DOAnchorPosY(0, 0.5f));
            seq.Append(damageNum.transform.DOScale(1f, 0.3f));
        }
        else
        {
            damageNum.color = normalDam;
            damageNum.fontSize = 0.3f;
            seq.Append(damageNum.transform.DOScale(1.3f, 0.2f));
            seq2.Append(damageNum.GetComponent<RectTransform>().DOAnchorPosY(0, 0.5f));
            seq.Append(damageNum.transform.DOScale(1f, 0.3f));
        }
        seq.Play();
        seq2.Play();
        seq.Play().OnComplete(() => { damageNum.gameObject.SetActive(false); });
    }
}
