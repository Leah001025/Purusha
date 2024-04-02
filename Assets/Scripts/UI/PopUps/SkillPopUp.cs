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
    [SerializeField] private Image portraitIcon;
    [SerializeField] private Image skill1Icon;
    [SerializeField] private Image skill2Icon;
    [SerializeField] private Image skill3Icon;
    [SerializeField] private Image skill4Icon;
    private CharacterData characterData;
    private int skill3CoolTime;
    private int skill4Gauge;
    private int onTurnIndex;

    private void Awake()
    {
        CloseUI();
    }
    private void OnEnable()
    {
        onTurnIndex = BattleManager.Instance.onTurnIndex;
        characterData = BattleManager.Instance.lUnitInfo[onTurnIndex].characterData;
        Init();
    }

    public override void CloseUI()
    {
        skill1.onClick.AddListener(() => gameObject.SetActive(false));
        skill2.onClick.AddListener(() => gameObject.SetActive(false));
        skill3.onClick.AddListener(CheckCoolTime);
        skill4.onClick.AddListener(CheckGauge);
    }
    private void Init()
    {
        portraitIcon.sprite = Resources.Load<Sprite>("UI/Icon/Portrait_" + characterData.status.iD);
        skill1Icon.sprite = Resources.Load<Sprite>(characterData.skillData[1].iconPath);
        skill2Icon.sprite = Resources.Load<Sprite>(characterData.skillData[2].iconPath);
        skill3Icon.sprite = Resources.Load<Sprite>(characterData.skillData[3].iconPath);
        skill4Icon.sprite = Resources.Load<Sprite>(characterData.skillData[4].iconPath);
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
