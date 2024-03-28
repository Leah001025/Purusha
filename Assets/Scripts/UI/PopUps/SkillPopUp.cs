using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPopUp : UIBase
{
    [SerializeField] private Button skill1;
    [SerializeField] private Button skill2;
    [SerializeField] private Button skill3;
    [SerializeField] private Button skill4;
    private int skill3CoolTime;
    private int skill4Gauge;

    private void Awake()
    {
        CloseUI();
    }

    public override void CloseUI()
    {
        skill1.onClick.AddListener(() => gameObject.SetActive(false));
        skill2.onClick.AddListener(() => gameObject.SetActive(false));
        skill3.onClick.AddListener(CheckCoolTime);
        skill4.onClick.AddListener(CheckGauge);
    }
    public void CheckCoolTime()
    {
        skill3CoolTime = BattleManager.Instance.skill3CoolTime;
        if (skill3CoolTime <= 0) 
        {
            gameObject.SetActive(false);
            return;
        }
        Debug.Log("스킬3을 사용할 수 없습니다.");
    }
    public void CheckGauge()
    {
        skill4Gauge = BattleManager.Instance.skill4Gauge;
        if (skill4Gauge >= 5)
        {
            gameObject.SetActive(false);
            return;
        }
        Debug.Log("스킬4를 사용할 수 없습니다.");
    }
}
