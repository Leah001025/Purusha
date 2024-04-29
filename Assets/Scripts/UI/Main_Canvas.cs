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
    [SerializeField] private Button _previousBtn;
    private void Awake()
    {
        SettingButtons();
    }

    private void SettingButtons()
    {
        _storyBtn.onClick.AddListener(() => UIManager.Instance.ShowPopup<ChapterSelectPopUp>());
        _gachaBtn.onClick.AddListener(() => UIManager.Instance.ShowPopup<GachaPopUp>());
        _departmentBtn.onClick.AddListener(() => UIManager.Instance.ShowPopup<DepartmentPopUp>());
        _previousBtn.onClick.AddListener(() => SceneLoadManager.Instance.LoadingChangeScene("StartScene"));
        _upgradeBtn.onClick.AddListener(() => UIManager.Instance.ShowPopup<UpgradeTreePopUp>());
    }
}
