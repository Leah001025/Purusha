using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main_Canvas : MonoBehaviour
{
    [SerializeField] private Button _storyBtn;
    [SerializeField] private Button _gachaBtn;
    [SerializeField] private Button _departmentBtn;
    [SerializeField] private Button _intelligenceBtn;
    [SerializeField] private Button _upgradeBtn;
    private void Awake()
    {
        SettingButtons();
    }

    private void SettingButtons()
    {
        _storyBtn.onClick.AddListener(() => SceneLoadManager.Instance.LoadingChangeScene("Dev_Main_Scene"));
        _gachaBtn.onClick.AddListener(() => SceneLoadManager.Instance.LoadingChangeScene("InventoryScene")); //ui ������Ʈ �Ʒ� ������Ʈ�� ���⵵�� 
        _departmentBtn.onClick.AddListener(() => UIManager.Instance.ShowPopup<DepartmentPopUp>());
        _intelligenceBtn.onClick.AddListener(() => UIManager.Instance.ShowPopup<IntelligencePopUp>());
        _upgradeBtn.onClick.AddListener(() => UIManager.Instance.ShowPopup<UpgradeTreePopUp>());
    }
}
