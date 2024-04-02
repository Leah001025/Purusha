using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTreePopUp : UIBase
{
    [SerializeField] private Button _upgradeButton1;
    [SerializeField] private Button _upgradeButton2;
    [SerializeField] private Button _upgradeButton3;
    [SerializeField] private Button _upgradeButton4;

    private void Awake()
    {
        base.CloseUI();
        SettingButtons();
    }

    //버튼마다 같은 창을 띄우지만, 설정을 다르게 해야할 수 도 있기에 일단 따로따로 만들었습니다. 
    private void SettingButtons()
    {
        _upgradeButton1.onClick.AddListener(() => UIManager.Instance.ShowPopup<UpgradePopUp>());
        _upgradeButton2.onClick.AddListener(() => UIManager.Instance.ShowPopup<UpgradePopUp>());
        _upgradeButton3.onClick.AddListener(() => UIManager.Instance.ShowPopup<UpgradePopUp>());
        _upgradeButton4.onClick.AddListener(() => UIManager.Instance.ShowPopup<UpgradePopUp>());

    }
}
