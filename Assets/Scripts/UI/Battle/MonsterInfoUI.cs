using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class MonsterInfoUI : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image hpBar;
    [SerializeField] private TMP_Text InfoText;
    [SerializeField] private TMP_Text damageText;
    [SerializeField] private GameObject damageObj;
    [SerializeField] private Animator damageAnimator;
    private string monsterName;
    private float monsterMaxHealth;
    private Color normalDam = new Color(1, 0.8f, 0);
    private Color criDam = new Color(1, 0.1f, 0);
    private UnitInfo monsterInfo;

    private void Start()
    {

        monsterName = gameObject.transform.parent.gameObject.transform.parent.gameObject.name;
        monsterMaxHealth = BattleManager.Instance.lUnitInfo[int.Parse(monsterName)].unitData.Health;
        BattleManager.Instance.OnAddDamage += OnDamageUI;
        DOTween.Init();
        damageAnimator = GetComponent<Animator>();
        monsterInfo = BattleManager.Instance.lUnitInfo[int.Parse(monsterName)];
        InfoText.text = $"Lv.{monsterInfo.unitData.Level} {monsterInfo.unitData.Name}";
    }
    private void Update()
    {
        float fillDamage = (BattleManager.Instance.lUnitInfo[int.Parse(monsterName)].unitData.Health) / monsterMaxHealth;
        hpBar.fillAmount = Mathf.Lerp(hpBar.fillAmount, fillDamage, Time.deltaTime * 20);
    }

    private void OnDamageUI(float damage, string name, bool isCritical)
    {
        damageText.gameObject.SetActive(true);
        damageText.text = damage.ToString("0");
        var seq = DOTween.Sequence();
        var seq2 = DOTween.Sequence();
        if (isCritical)
        {
            damageText.color = criDam;
            damageText.fontSize = 0.35f;
            seq.Append(damageText.transform.DOScale(1.6f, 0.2f));
            seq2.Append(damageText.GetComponent<RectTransform>().DOAnchorPosY(0, 0.5f));
            seq.Append(damageText.transform.DOScale(1f, 0.3f));
        }
        else
        {
            damageText.color = normalDam;
            damageText.fontSize = 0.3f;
            seq.Append(damageText.transform.DOScale(1.3f, 0.2f));
            seq2.Append(damageText.GetComponent<RectTransform>().DOAnchorPosY(0, 0.5f));
            seq.Append(damageText.transform.DOScale(1f, 0.3f));
        }
        seq.Play();
        seq2.Play();
        seq.Play().OnComplete(() => { damageText.gameObject.SetActive(false); });
    }
}
