using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] private Image skill3Button;
    [SerializeField] private Image skill4Button;
    [SerializeField] private TextMeshProUGUI skill3Text;
    [SerializeField] private TextMeshProUGUI skillDesText;
    [SerializeField] private GameObject skillDescription;
    private CharacterData characterData;
    private CharacterTurnController skillController;
    private int skill3CoolTime;
    private int useSkillNum;
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
        skillController = BattleManager.Instance.turnControllers[onTurnIndex];
        skillDescription.SetActive(false);
        useSkillNum = 0;
        Init();
    }

    public override void CloseUI()
    {
        skill1.onClick.AddListener(UseSkill1);
        skill2.onClick.AddListener(UseSkill2);
        skill3.onClick.AddListener(UseSkill3);
        skill4.onClick.AddListener(UseSkill4);
    }
    private void Init()
    {
        skill3Icon.color = Color.white;
        skill4Icon.color = Color.white;
        skill3Button.color = Color.white;
        skill4Button.color = Color.white;
        skill3Text.text = "";
        portraitIcon.sprite = Resources.Load<Sprite>("UI/Icon/Portrait_" + characterData.status.iD);
        skill1Icon.sprite = Resources.Load<Sprite>(characterData.skillData[1].iconPath);
        skill2Icon.sprite = Resources.Load<Sprite>(characterData.skillData[2].iconPath);
        skill3Icon.sprite = Resources.Load<Sprite>(characterData.skillData[3].iconPath);
        skill4Icon.sprite = Resources.Load<Sprite>(characterData.skillData[4].iconPath);
        if (skillController.skill3CoolTime > 0)
        {
            skill3Icon.color = Color.gray;
            skill3Button.color = Color.gray;
            skill3Text.text = skillController.skill3CoolTime.ToString() + "턴";
        }
        if (skillController.skill4Gauge < 5)
        {
            skill4Icon.color = Color.gray;
            skill4Button.color = Color.gray;
        }
    }
    public void UseSkill1()
    {
        if (useSkillNum != 1)
        {
            useSkillNum = 1;
            skillDescription.SetActive(true);
            skillDesText.text = characterData.skillData[1].description;
        }
        else
        {
            BattleManager.Instance.CallSkill1Event();
            gameObject.SetActive(false);
        }

    }
    public void UseSkill2()
    {
        if (useSkillNum != 2)
        {
            useSkillNum = 2;
            skillDescription.SetActive(true);
            skillDesText.text = characterData.skillData[2].description;
        }
        else
        {
            BattleManager.Instance.CallSkill2Event();
            gameObject.SetActive(false);
        }

    }
    public void UseSkill3()
    {
        if (useSkillNum != 3)
        {
            useSkillNum = 3;
            skillDescription.SetActive(true);
            skillDesText.text = characterData.skillData[3].description;
        }
        else
        {
            BattleManager.Instance.CallSkill3Event();
            CheckCoolTime();
        }

    }
    public void UseSkill4()
    {
        if (useSkillNum != 4)
        {
            useSkillNum = 4;
            skillDescription.SetActive(true);
            skillDesText.text = characterData.skillData[4].description;
        }
        else
        {
            BattleManager.Instance.CallSkill4Event();
            CheckGauge();
        }

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
