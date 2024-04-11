using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserData
{
    public int userLv;
    public int userExp;
    public string userName;
    public int gold;
    public int cash;
    public bool introScene;
    public bool tutorial1;
    public bool tutorial2;
    public SaveData saveData;
    public Stack<StageInfo> stageClear = new Stack<StageInfo>();
    public Dictionary<int, UpgradeStatData> upgrades = new Dictionary<int, UpgradeStatData>();
    public Dictionary<int, CharacterData> characters = new();
    public Dictionary<int, CharacterData> teamData = new Dictionary<int, CharacterData>(5);
    public Dictionary<int, Item> itemInventory = new Dictionary<int, Item>();
    //public List<ItemData> Inventory = new();
    //public ItemDataBase ItemData = DataManager.Instance.ItemDB; �����۵����� �ٷ� ������

    //UseData �ʱ� ����
    public UserData(string name)
    {
        userLv = 1;
        userExp = 0;
        userName = name;
        gold = 1000;
        cash = 100;
        saveData = new SaveData();
        CharacterData eve = new CharacterData(102);
        characters.Add(eve.status.iD, eve);
        upgrades.Add(eve.status.iD, new UpgradeStatData());
        AddTeam(eve.status.iD);
        StageInfo stageInfo = new StageInfo();
        stageInfo.stageID = 1101;
        stageClear.Push(stageInfo);
    }
    //ĳ���� �߰�
    public void AddCharacter(int id)
    {
        CharacterData newCharacter = new CharacterData(id);
        UpgradeStatData newUpgradeStat = new UpgradeStatData();
        characters.Add(newCharacter.status.iD, newCharacter);
        upgrades.Add(newCharacter.status.iD, newUpgradeStat);
    }

    //���� �߰�
    public void AddTeam(int id)
    {
        for (int i = 1;  i <=5; i++)
        {
            if (!teamData.ContainsKey(i)) 
            {
                teamData.Add(i, characters[id]);
                return;
            }
        }        
    }

    //���� ����
    public void RemoveTeam(int index)
    {
        teamData.Remove(index);
    }

    public void AddItem(int id, int quantity=1)
    {
        Item newItem = new Item(id);
        if(!itemInventory.ContainsKey(id)) 
        {            
            itemInventory.Add(id, newItem);
        }
        else
        {
            itemInventory[id].quantity+=quantity;
        }
    }
    public void RemoveItem(int id)
    {
        Item newItem = new Item(id);
        if (itemInventory.ContainsKey(id))
        {
            itemInventory[id].quantity--;
            if (itemInventory[id].quantity <= 0) itemInventory.Remove(id);
        }
        else
        {
            Debug.Log("�κ��丮�� ������ �������� �����ϴ�.");
        }
    }
}