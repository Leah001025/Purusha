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
    private Color rareItem;
    private Color epicItem;
    private Color regendItem;
    public int enabledIndex;
    public int enabledID;
    private int targetID;
    private string itemType;

    private void Start()
    {
        UIManager.Instance.itemInventoryUI = this;
        rareItem = new Color(0, 0.8f, 1);
        epicItem = new Color(0.9f,0,1);
        regendItem = new Color(1,0.8f,0);
        items = GameManager.Instance.User.itemInventory;
        characters = GameManager.Instance.User.characters;
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
                icon.color = Color.white;
                if (slot.itemID == 10101) icon.color = rareItem;
                if (slot.itemID == 10102) icon.color = epicItem;
                if (slot.itemID == 10103) icon.color = regendItem;
                if (slot.itemID == 10401) icon.color = rareItem;
                if (slot.itemID == 10402) icon.color = regendItem;
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
                if (UIManager.Instance.inventoryMainUI.isstatusOn)
                {
                    GameManager.Instance.User.itemInventory[enabledID].UseHealingItem(enabledID, characters[targetID]);
                    UIManager.Instance.charInventoryUI[targetID].ShowCharacterData();
                    UpdateInventory();
                }
                else UIManager.Instance.inventoryMainUI.OnStatusPanel();
                break;
            case "expPotion":
                if (UIManager.Instance.inventoryMainUI.isLevelUp)
                {
                    GameManager.Instance.User.itemInventory[enabledID].UseExpItem(enabledID, characters[targetID]);
                    UIManager.Instance.charInventoryUI[targetID].ShowCharacterData();
                    UpdateInventory();
                }
                else UIManager.Instance.inventoryMainUI.OnClicklevelUp();
                break;
            case "weaponPotion":
            case "ArmorPotion":
                if (UIManager.Instance.inventoryMainUI.isUpgradeOn)
                {
                    UpdateInventory();
                }
                else UIManager.Instance.inventoryMainUI.OnClickUpgrade();
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
