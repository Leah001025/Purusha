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

    //��ư���� ���� â�� �������, ������ �ٸ��� �ؾ��� �� �� �ֱ⿡ �ϴ� ���ε��� ��������ϴ�. 
    private void SettingButtons()
    {
        _upgradeButton1.onClick.AddListener(() => UIManager.Instance.ShowPopup<UpgradePopUp>());
        _upgradeButton2.onClick.AddListener(() => UIManager.Instance.ShowPopup<UpgradePopUp>());
        _upgradeButton3.onClick.AddListener(() => UIManager.Instance.ShowPopup<UpgradePopUp>());
        _upgradeButton4.onClick.AddListener(() => UIManager.Instance.ShowPopup<UpgradePopUp>());

    }
}
