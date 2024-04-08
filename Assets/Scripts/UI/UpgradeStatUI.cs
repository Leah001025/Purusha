using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeStatUI : MonoBehaviour
{
    private int curID;
    public TextMeshProUGUI message;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI weaponQuantity;
    public TextMeshProUGUI armorQuantity;
    public TextMeshProUGUI atk;
    public TextMeshProUGUI criChan;
    public TextMeshProUGUI criDam;
    public TextMeshProUGUI health;
    public TextMeshProUGUI def;
    public TextMeshProUGUI speed;
    private Dictionary<int,CharacterData> characterDatas;
    private Dictionary<int,UpgradeStatData> upgrades;
    public Dictionary<int,Item> inventory;

    private void OnEnable()
    {
        UIManager.Instance.upgradeStatUI =this;
        curID = UIManager.Instance.curTargetID;
        characterDatas = GameManager.Instance.User.characterDatas;
        upgrades = GameManager.Instance.User.upgrades;
        inventory = GameManager.Instance.User.itemInventory;
        Init();
    }
    public void Init()
    {
        curID = UIManager.Instance.curTargetID;
        atk.text = upgrades[curID].atk.ToString("0");
        criChan.text = (upgrades[curID].criticalChance*100).ToString("0")+"%";
        criDam.text = (upgrades[curID].criticalDamage*100).ToString("0")+"%";
        health.text = upgrades[curID].health.ToString("0");
        def.text = upgrades[curID].def.ToString("0");
        speed.text = (upgrades[curID].speed * 100).ToString("0");
        characterName.text = $"{characterDatas[curID].status.name} 전용장비 강화";
        weaponQuantity.text = $"강화재료 {inventory[10201].quantity.ToString()}개";
        armorQuantity.text = $"강화재료 {inventory[10301].quantity.ToString()}개";
        message.text = "";
    }
    public void UpgradeStatus(int index)
    {
        if (!(inventory.ContainsKey(10201) || inventory.ContainsKey(10301)))
        {
            Debug.Log("강화재료가 부족합니다");
            return;
        }
        if (index <3) { inventory[10201].UseWeaponUpgradeItem(10201); }
        if (index >= 3) { inventory[10301].UseArmorUpgradeItem(10301); }
        UIManager.Instance.itemInventoryUI.UpdateInventory();
        weaponQuantity.text = $"강화재료 {inventory[10201].quantity.ToString()}개";
        armorQuantity.text = $"강화재료 {inventory[10301].quantity.ToString()}개";
        float random = Random.Range(0f, 1f);
        if (random > 0.5f)
        {
            switch (index)
            {
                case 0:
                    characterDatas[curID].status.atk++;
                    upgrades[curID].atk++;
                    upgrades[curID].totalPoint++;
                    atk.text = upgrades[curID].atk.ToString("0");
                    break;
                case 1:
                    characterDatas[curID].status.criticalChance+=0.04f;
                    upgrades[curID].criticalChance+=0.04f;
                    upgrades[curID].totalPoint++;
                    criChan.text = (upgrades[curID].criticalChance*100).ToString("0")+"%";
                    break;
                case 2:
                    characterDatas[curID].status.criticalDamage+=0.06f;
                    upgrades[curID].criticalDamage+=0.06f;
                    upgrades[curID].totalPoint++;
                    criDam.text = (upgrades[curID].criticalDamage*100).ToString("0")+"%";
                    break;
                case 3:
                    characterDatas[curID].status.maxhealth+=50f;
                    characterDatas[curID].status.health += 50f;
                    upgrades[curID].health+=50f;
                    upgrades[curID].totalPoint++;
                    health.text = upgrades[curID].health.ToString("0");
                    break;
                case 4:
                    characterDatas[curID].status.def++;
                    upgrades[curID].def++;
                    upgrades[curID].totalPoint++;
                    def.text = upgrades[curID].def.ToString("0");
                    break;
                case 5:
                    characterDatas[curID].status.speed+=0.02f;
                    upgrades[curID].speed += 0.02f;
                    upgrades[curID].totalPoint++;
                    speed.text = (upgrades[curID].speed * 100).ToString("0");
                    break;
            }
            message.color = Color.blue;
            message.text = "강화 성공";
        }
        else
        {
            message.color = Color.red;
            message.text = "강화 실패";
        }
    }
}
