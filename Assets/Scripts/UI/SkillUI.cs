using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    int curID = 101;
    Dictionary<int, CharacterData> characters;

    private void Start()
    {
        InitSkillUI();
        UIManager.Instance.skillUI = this;
    }
    public void InitSkillUI()
    {
        curID = UIManager.Instance.curTargetID;
        characters = GameManager.Instance.User.characters;
        for (int i = 0; i < 4; i++)
        {
            Image icon = transform.GetChild(i).gameObject.GetComponent<Image>();
            var res = Resources.Load<Sprite>(characters[curID].skillData[i + 1].iconPath);
            icon.sprite = res;
            TextMeshProUGUI skillName = transform.GetChild(i + 4).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI description = transform.GetChild(i + 4).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
            skillName.text = characters[curID].skillData[i + 1].name;
            description.text = characters[curID].skillData[i + 1].description;
        }
    }
}
