using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class OpenWorldSceneUI : MonoBehaviour
{
    [SerializeField] private Transform contentTrans;
    [SerializeField] private GameObject offPanel;
    [SerializeField] private Button onPanelBtn;
    [SerializeField] private Button offPanelBtn;
    [SerializeField] private Button continueBtn;
    [SerializeField] private Button mainBtn;
    private Dictionary<int, CharacterData> teamData = new Dictionary<int, CharacterData>();
    private Dictionary<int, GameObject> uiObject = new Dictionary<int, GameObject>();

    private void Start()
    {
        UIManager.Instance.openWorldSceneUI = this;
        onPanelBtn.onClick.AddListener(() => UIManager.Instance.ShowPopup<DepartmentPopUp>());
        offPanelBtn.onClick.AddListener(() => offPanel.gameObject.SetActive(true));
        continueBtn.onClick.AddListener(() => offPanel.gameObject.SetActive(false));
        mainBtn.onClick.AddListener(() => SceneLoadManager.Instance.LoadingChangeScene("MainScene"));
        teamData = GameManager.Instance.User.teamData;
        Init();
    }
    private void Init()
    {
        foreach (KeyValuePair<int, CharacterData> item in teamData)
        {
            var res = ResourceManager.Instance.Load<GameObject>("UI/WorldMapUI/WorldTeam");
            var sprite = ResourceManager.Instance.Load<Sprite>($"UI/Image/World_{item.Value.status.iD}");

            var obj = Instantiate(res, contentTrans);
            TextMeshProUGUI lvText = obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            Image image = obj.transform.GetChild(2).GetComponent<Image>();
            Slider hpSlider = obj.transform.GetChild(4).GetComponent<Slider>();
            TextMeshProUGUI hpText = obj.transform.GetChild(4).GetChild(3).GetComponent<TextMeshProUGUI>();
            lvText.text = "Lv." + item.Value.status.level.ToString("0");
            image.sprite = sprite;
            hpText.text = item.Value.status.health.ToString("0");
            hpSlider.value = item.Value.status.health/item.Value.status.maxhealth;
            uiObject.Add(item.Key, obj);
        }
    }
    public void UpdateData()
    {
        teamData = GameManager.Instance.User.teamData;
        foreach (KeyValuePair<int, GameObject> obj in uiObject)
        {
            TextMeshProUGUI lvText = obj.Value.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            Image image = obj.Value.transform.GetChild(2).GetComponent<Image>();
            Slider hpSlider = obj.Value.transform.GetChild(4).GetComponent<Slider>();
            TextMeshProUGUI hpText = obj.Value.transform.GetChild(4).GetChild(3).GetComponent<TextMeshProUGUI>();
            lvText.text = "Lv." + teamData[obj.Key].status.level.ToString("0");
            hpText.text = teamData[obj.Key].status.health.ToString("0");
            hpSlider.value = teamData[obj.Key].status.health / teamData[obj.Key].status.maxhealth;
        }
    }
}
