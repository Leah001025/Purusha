using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class ItemInventory : MonoBehaviour
{
    public Dictionary<int, Item> items;
    private Dictionary<int,CharacterData> characters;
    List<int> containsKey = new List<int>();
    List<Outline> outlines = new List<Outline>();
    public Button useButton;
    public TextMeshProUGUI description;
    public TextMeshProUGUI itemName;
    public int enabledIndex;
    public int enabledID;
    private int targetID;
    private string itemType;

    private void Start()
    {
        UIManager.Instance.itemInventoryUI = this;
        items = GameManager.Instance.User.itemInventory;
        characters = GameManager.Instance.User.characterDatas;
        useButton.onClick.AddListener(UseItem);
        foreach (KeyValuePair<int, Item> item in items)
        {
            containsKey.Add(item.Key);
        }
        UpdateInventory();
    }
    public void UpdateInventory()
    {
        containsKey.Clear();
        foreach (KeyValuePair<int, Item> item in items)
        {
            containsKey.Add(item.Key);
        }
        for (int i = 0; i <16; i++)
        {
            var obj = transform.GetChild(2).GetChild(0).GetChild(i).gameObject;
            obj.gameObject.SetActive(false);
            obj.gameObject.SetActive(i<items.Count);
            Outline outline = obj.GetComponent<Outline>();
            ItemSlot slot = obj.GetComponent<ItemSlot>();
            Image icon = obj.transform.GetChild(0).gameObject.GetComponent<Image>();
            TextMeshProUGUI quantity = obj.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
            outlines.Add(outline);
            slot.index = i;
            if (i < items.Count)
            {
                slot.itemID = containsKey[i];
                icon.sprite = Resources.Load<Sprite>(items[containsKey[i]].spritePath);
                quantity.text = items[containsKey[i]].quantity.ToString();
            }
        }
    }
    public void OnOutLine(int index)
    {
        foreach (Outline outline in outlines)
        {
            outline.enabled = false;
        }
        outlines[index].enabled = true;
    }

    private void UseItem()
    {
        if (!items.ContainsKey(enabledID))
        {
            description.text = "";
            itemName.text = "";
            return;
        }
        targetID = UIManager.Instance.curTargetID;
        itemType = items[enabledID].type;
        switch (itemType)
        {
            case "hpPotion":
                if (PopupManager.Instance.isstatusOn)
                {
                    GameManager.Instance.User.itemInventory[enabledID].UseHealingItem(enabledID, characters[targetID]);
                    UIManager.Instance.charInventoryUI[targetID].ShowCharacterData();
                    UpdateInventory();
                }
                else PopupManager.Instance.OnStatusPanel();
                break;
            case "expPotion":
                if (PopupManager.Instance.isLevelUp)
                {
                    GameManager.Instance.User.itemInventory[enabledID].UseExpItem(enabledID, characters[targetID]);
                    UIManager.Instance.charInventoryUI[targetID].ShowCharacterData();
                    UpdateInventory();
                }
                else PopupManager.Instance.OnClicklevelUp();
                break;
            case "weaponPotion":
            case "ArmorPotion":
                if (PopupManager.Instance.isUpgradeOn)
                {
                    UpdateInventory();
                }
                else PopupManager.Instance.OnClickUpgrade();
                break;
            case "etc":
                UIManager.Instance.ShowPopup<GachaPopUp>();
                break;
            case "skill":
                GameManager.Instance.User.itemInventory[enabledID].UseSkillItem(enabledID, characters[(int)items[enabledID].value]);
                UIManager.Instance.skillUI.InitSkillUI();
                UpdateInventory();
                break;

        }

    }
}
