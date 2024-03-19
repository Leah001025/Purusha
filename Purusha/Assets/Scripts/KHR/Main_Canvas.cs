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

    private void Awake()
    {
        SettingButtons();
    }

    private void SettingButtons()
    {
        _storyBtn.onClick.AddListener(() => SceneLoadManager.Instance.LoadingChangeScene("Dev_Main_Scene"));
        _gachaBtn.onClick.AddListener(() => UIManager.Instance.ShowPopup<GachaPopUp>()); //ui 오브젝트 아래 오브젝트에 생기도록 
        _departmentBtn.onClick.AddListener(() => UIManager.Instance.ShowPopup<DepartmentPopUp>());
        _intelligenceBtn.onClick.AddListener(() => UIManager.Instance.ShowPopup<IntelligencePopUp>());
    }
}
