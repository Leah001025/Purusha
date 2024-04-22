using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class PopupManager : MonoBehaviour
{
    private static PopupManager _instance;
    public GameObject infoPanel;
    public GameObject skillPanel;
    public GameObject statusPanel;
    public GameObject upgradePanel;
    public GameObject levelUpPanel;
    public bool isInventoryOn = false;
    public bool isstatusOn = false;
    public bool isUpgradeOn = false;
    public bool isLevelUp = false;
    private int curID=102;
    public static PopupManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        DOTween.Init();
    }

    public void Show()
    {
        SoundManager.Instance.ButtonAudio("BasicMenuO_1");
        gameObject.SetActive(true);
        var seq = DOTween.Sequence();
        seq.Append(transform.DOScale(1.1f, 0.2f));
        seq.Append(transform.DOScale(1f, 0.1f));
        seq.Play();
    }

    public void Hide()
    {
        SoundManager.Instance.ButtonAudio("BasicMenuC_1");
        var seq = DOTween.Sequence();
        seq.Play().OnComplete(() => { gameObject.SetActive(false); });
    }
    public void ShowItem()
    {
        SoundManager.Instance.ButtonAudio("BasicMenuO_1");
        var seq = DOTween.Sequence();
        seq.Append(infoPanel.GetComponent<RectTransform>().DOAnchorPosX(-629, 0.2f));
        seq.Play();
    }

    public void HideItem()
    {
        SoundManager.Instance.ButtonAudio("BasicMenuC_1");
        var seq = DOTween.Sequence();
        seq.Append(infoPanel.GetComponent<RectTransform>().DOAnchorPosX(0, 0.2f));
        seq.Play();
    }
    public void ShowSkill()
    {
        SoundManager.Instance.ButtonAudio("BasicMenuO_1");
        var seq = DOTween.Sequence();
        seq.Append(skillPanel.GetComponent<RectTransform>().DOAnchorPosY(0, 0.2f));
        seq.Play();
    }

    public void HideSkill()
    {
        SoundManager.Instance.ButtonAudio("BasicMenuC_1");
        var seq = DOTween.Sequence();
        seq.Append(skillPanel.GetComponent<RectTransform>().DOAnchorPosY(-810, 0.2f));
        seq.Play();
    }
    public void CloseBtn()
    {
        SceneLoadManager.Instance.LoadingChangeScene("MainScene");
    }
    public void OnClickShowItem()
    {
        if (!isInventoryOn) 
        {
            ShowItem();
            isInventoryOn = true;
        }
        else
        {
            HideItem();
            isInventoryOn = false;
        }
            
    }
    public void ShowUI(GameObject gameObject)
    {
        SoundManager.Instance.ButtonAudio("BasicMenuO_1");
        gameObject.SetActive(true);
        var seq = DOTween.Sequence();
        seq.Append(gameObject.transform.DOScale(1.1f, 0.2f));
        seq.Append(gameObject.transform.DOScale(1f, 0.1f));
        seq.Play();
    }

    public void HideUI(GameObject gameObject)
    {
        SoundManager.Instance.ButtonAudio("BasicMenuC_1");
        var seq = DOTween.Sequence();
        seq.Play().OnComplete(() => { gameObject.SetActive(false); });
    }
    public void OnClickUpgrade()
    {
        if (!isUpgradeOn)
        {
            HideUI(statusPanel);
            HideUI(levelUpPanel);
            ShowUI(upgradePanel);
            isUpgradeOn = true;
            isstatusOn = false;
            isLevelUp = false;
        }
        else
        {
            ShowUI(statusPanel);
            curID = UIManager.Instance.curTargetID;
            UIManager.Instance.charInventoryUI[curID].ShowCharacterData();
            HideUI(upgradePanel);
            isUpgradeOn = false;
            isstatusOn = true;
        }
    }
    public void OnClicklevelUp()
    {
        if (!isLevelUp)
        {
            HideUI(statusPanel);
            HideUI(upgradePanel);
            ShowUI(levelUpPanel);
            isLevelUp = true;
            isstatusOn=false;
            isUpgradeOn = false;
        }
        else
        {
            ShowUI(statusPanel);
            HideUI(levelUpPanel);
            curID = UIManager.Instance.curTargetID;
            UIManager.Instance.charInventoryUI[curID].ShowCharacterData();
            isLevelUp = false;
            isstatusOn = true;
        }
    }
    public void OnStatusPanel()
    {
        if (!isstatusOn)
        {
            HideUI(levelUpPanel);
            HideUI(upgradePanel);
            ShowUI(statusPanel);
            curID = UIManager.Instance.curTargetID;
            UIManager.Instance.charInventoryUI[curID].ShowCharacterData();
            isLevelUp = false;
            isstatusOn = true;
            isUpgradeOn = false;
        }
    }
}
