using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpUI : MonoBehaviour
{
    private int curID;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI level;
    public TextMeshProUGUI curExp;
    public TextMeshProUGUI requireExp;
    public TextMeshProUGUI sQuantity;
    public TextMeshProUGUI mQuantity;
    public TextMeshProUGUI lQuantity;
    private Dictionary<int, CharacterData> characterDatas;
    private Dictionary<int, Item> inventory;
    public Button levelUpBtn;
    private float rExp;
    private float cExp;
    int cLevel;

    private void OnEnable()
    {
        UIManager.Instance.levelUpUI = this;
        curID = UIManager.Instance.curTargetID;
        characterDatas = GameManager.Instance.User.characters;
        inventory = GameManager.Instance.User.itemInventory;
        levelUpBtn.onClick.AddListener(NextLevelUp);
        Init();
    }
    public void Init()
    {
        GetData();
        characterName.text = $"{characterDatas[curID].status.name} ·¹º§¾÷";
        level.text = cLevel.ToString("0");
        curExp.text = cExp.ToString("0");
        requireExp.text = cLevel==10?"Max": (rExp).ToString("");
        if (inventory.ContainsKey(10101)) sQuantity.text = inventory[10101].quantity.ToString("0");
        else sQuantity.text = "0";
        if (inventory.ContainsKey(10102)) mQuantity.text = inventory[10102].quantity.ToString("0");
        else mQuantity.text = "0";
        if (inventory.ContainsKey(10103)) lQuantity.text = inventory[10103].quantity.ToString("0");
        else lQuantity.text = "0";
    }
    public void NextLevelUp()
    {
        if(cLevel==10) { return; }
        GetData();
        while(rExp >0&& inventory.ContainsKey(10103))
        {
            rExp -= inventory[10103].value;
            inventory[10103].UseExpItem(10103, characterDatas[curID]);
        }
        while (rExp > 0 && inventory.ContainsKey(10102))
        {
            rExp -= inventory[10102].value;
            inventory[10102].UseExpItem(10102, characterDatas[curID]);
        }
        while (rExp > 0 && inventory.ContainsKey(10101))
        {
            rExp -= inventory[10101].value;
            inventory[10101].UseExpItem(10101, characterDatas[curID]);
        }
        UIManager.Instance.itemInventoryUI.UpdateInventory();
        Init();
    }
    private void GetData()
    {
        curID = UIManager.Instance.curTargetID;
        cLevel = characterDatas[curID].status.level;
        cExp = characterDatas[curID].status.exp;
        if(cLevel == 10) { return; }
        rExp = DataManager.Instance.LevelDB.GetData(cLevel + 1).Exp - cExp;
    }
}
