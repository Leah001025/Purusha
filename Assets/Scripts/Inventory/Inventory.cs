using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _level;
    [SerializeField] private TMP_Text _atk;
    [SerializeField] private TMP_Text _criC;
    [SerializeField] private TMP_Text _criD;
    [SerializeField] private TMP_Text _def;
    [SerializeField] private TMP_Text _health;
    [SerializeField] private TMP_Text _speed;

    private Dictionary<string, int> character = new Dictionary<string, int>()
    {
        { "Breesha", 101 },
        { "Eve", 102 },
        { "Adam", 103 },
        { "Abel", 104 },
        { "Lain", 105 },
        { "Kana", 106 }
    };

    private void Start()
    {
        if(gameObject.name == "Eve")
        {
            ShowCharacterData();
        }
        if (!GameManager.Instance.User.characterDatas.ContainsKey(character[gameObject.name]))
        {
            gameObject.SetActive(false);
        }
        UIManager.Instance.charInventoryUI.Add(character[gameObject.name],this);
        //ShowCharacterData();
    }

    public void ShowCharacterData()
    {
        string gameObjectName = gameObject.name;
        UIManager.Instance.curTargetID = character[gameObjectName];
        if(UIManager.Instance.upgradeStatUI!=null)UIManager.Instance.upgradeStatUI.Init();
        if (UIManager.Instance.levelUpUI != null) UIManager.Instance.levelUpUI.Init();
        if (UIManager.Instance.skillUI != null) UIManager.Instance.skillUI.InitSkillUI();
        if (character.ContainsKey(gameObjectName))
        {
            int _id = character[gameObjectName];
            CharacterData playerData = GameManager.Instance.User.characterDatas[_id];

            if (playerData != null)
            {
                _name.text = playerData.status.name;
                _level.text = playerData.status.level.ToString("0");
                _atk.text = playerData.status.atk.ToString("0");
                _criC.text = (playerData.status.criticalChance * 100).ToString("0") + "%";
                _criD.text = (playerData.status.criticalDamage * 100).ToString("0") + "%";
                _def.text = playerData.status.def.ToString("0");
                _health.text = playerData.status.health.ToString("0");
                _speed.text = (playerData.status.speed * 100).ToString("0");
            }
        }
    }
    public void InitCharacterUI()
    {
        if (GameManager.Instance.User.characterDatas.ContainsKey(character[gameObject.name]))
        {
            gameObject.SetActive(true);
        }
    }
}
